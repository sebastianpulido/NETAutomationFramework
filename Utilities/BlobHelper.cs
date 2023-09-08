namespace Utilities;

using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;

public class BlobHelper
{
    private String connectionString;
    private BlobClient blobClient;
    private Azure.AzureSasCredential sasCredential;
    private string uriString;
    private Azure.Storage.Blobs.BlobClientOptions options = default;

    public BlobHelper(string connectionstring)
    {
        connectionString = connectionstring;
    }

    public BlobHelper(string uri, string sasToken)
    {
        uriString = uri;
        sasCredential = new Azure.AzureSasCredential(sasToken);
    }

    public void listAllBlobs(string container)
    {
        BlobContainerClient blobContainerClient = new BlobContainerClient(connectionString, container);
        Console.WriteLine("Listing blobs...");

        foreach (BlobItem blobItem in blobContainerClient.GetBlobs())
            Console.WriteLine("\t" + blobItem.Name);
    }

    public Azure.Response<BlobContentInfo> UploadBlob(string container, string localPath, string fileName)
    {
        BlobClient blobClient = new BlobClient(connectionString, container, fileName);
        string localFilePath = Path.Combine(localPath, fileName);

        // Open the file and upload its data
        using FileStream uploadFileStream = File.OpenRead(localFilePath);
        var response = blobClient.Upload(uploadFileStream, true);
        uploadFileStream.Close();
        return response;
    }

    public Azure.Response<BlobContentInfo> UploadToBlobWithSAS(string containerName, string localFilePath)
    {
        Console.WriteLine("Test Step : Uploading blob {0} to {1}", Path.GetFileName(localFilePath), containerName);
        var path = uriString + "/" + containerName;
        var uri = new Uri(path);
        BlobContainerClient blobContainerClient = new BlobContainerClient(uri, sasCredential, options);

        FileStream uploadFileStream = File.OpenRead(localFilePath);
        var response = blobContainerClient.UploadBlob(Path.GetFileName(localFilePath), uploadFileStream);
        uploadFileStream.Close();
        return response;
    }
}