namespace Settings;

using Microsoft.Extensions.Configuration;

public class BrowserParameters : Parameters
{
    public string browser { get; set; }
    public bool headless { get; set; }
    public int implicitWait { get; set; }
    public string gridUrl { get; set; }

    public BrowserParameters()
    {
        browser = configuration.GetSection("BrowserParameters:browser").Get<string>();
        headless = configuration.GetSection("BrowserParameters:headless").Get<bool>();
        implicitWait = configuration.GetSection("BrowserParameters:implicitWait").Get<int>();
        gridUrl = configuration.GetSection("BrowserParameters:gridUrl").Get<string>();

        if (string.IsNullOrEmpty(browser)) browser = "chrome";
        if (implicitWait<=0) implicitWait = 10;
    }
}