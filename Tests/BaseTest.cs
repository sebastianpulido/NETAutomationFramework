namespace Tests;

using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using Settings;
using System;
using System.IO;
using Utilities;
using Model.Pages;
using System.Collections.Generic;

[TestFixture]
[Parallelizable]
public class BaseTest
{
    protected IWebDriver webDriver;
    protected LoginParameters loginParameters;
    protected BrowserParameters browserParameters;

    static private int testSuccessCount = 0;
    static private int testFailureCount = 0;

    [SetUp]
    public void OnBaseStart()
    {
        browserParameters = new BrowserParameters();
        loginParameters = new LoginParameters();
        WebDriverProvider webDriverProvider = new(browserParameters);
        webDriver = webDriverProvider.getDriver();
        Console.WriteLine($"Test Step : Navigating to {loginParameters.url}");
        webDriver.Navigate().GoToUrl(loginParameters.url);
    }

    [TearDown]
    public void OnBaseFinish()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed)
            testSuccessCount++;

        if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Passed && webDriver != null)
        {
            JsUtil.Zoom(webDriver, 50);
            TakeScreenShot("Failure");
            testFailureCount++;
        }

        Console.WriteLine($"Progress : {testSuccessCount + testFailureCount} executed ; {testSuccessCount} passed ; {testFailureCount} failed !");
        webDriver.Close();
        webDriver.Quit();
        webDriver.Dispose();
    }

    public void TakeScreenShot(String name)
    {
        Screenshot screenshot = ((ITakesScreenshot)webDriver).GetScreenshot();
        string screenshotFilePath = Path.Combine(Directory.GetCurrentDirectory(), TestContext.CurrentContext.Test.FullName.Replace("\"", "").Replace("(", "").Replace(")", "").Replace(",", "").Replace("\\", "") + "_" + name + ".png");
        screenshot.SaveAsFile(screenshotFilePath, ScreenshotImageFormat.Png);
        TestContext.AddTestAttachment(screenshotFilePath);
    }

    protected void CloseInformationPopupWindow()
    {
        String CurrentWindow = webDriver.CurrentWindowHandle;
        String PopupWindow = new HomePage(webDriver).GetWindow("Sign in to your account", 7);
        if (!String.IsNullOrEmpty(PopupWindow))
            webDriver.SwitchTo().Window(PopupWindow).Close();
        webDriver.SwitchTo().Window(CurrentWindow);
    }

    public void CloseBrowser()
    {
        Console.WriteLine("Closing browser");
        webDriver.Close();
        webDriver.Quit();
        webDriver.Dispose();
    }

    public void OpenNewBrowser()
    {
        browserParameters = new BrowserParameters();
        loginParameters = new LoginParameters();
        WebDriverProvider webDriverProvider = new(browserParameters);
        webDriver = webDriverProvider.getDriver();
        Console.WriteLine($"Test Step : Navigating to {loginParameters.url}");
        webDriver.Navigate().GoToUrl(loginParameters.url);
    }
}