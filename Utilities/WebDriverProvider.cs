namespace Utilities;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using Settings;
using System;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using OpenQA.Selenium.Chrome;
using Microsoft.Edge.SeleniumTools;
using WebDriverManager.Helpers;

class WebDriverProvider
{
    BrowserParameters browserparameters;
    IWebDriver webDriver;

    public WebDriverProvider(BrowserParameters _browserparameters)
    {
        browserparameters = _browserparameters;
    }

    public IWebDriver getDriver()
    {
        if (browserparameters.browser.Equals("chrome") || browserparameters.browser.Equals("Chrome"))
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            var chromeOptions = new ChromeOptions();

            if (browserparameters.headless)
                chromeOptions.AddArguments("headless");

            chromeOptions.AddArguments("--disable-gpu");
            chromeOptions.AddArguments("--no-sandbox");
            chromeOptions.AddArguments("--allow-insecure-localhost");
            chromeOptions.AddArguments("--window-size=1280,800");
            //chromeOptions.AddExcludedArgument("disable-popup-blocking");
            chromeOptions.AddAdditionalCapability("acceptInsecureCerts", true, true);
            webDriver = new ChromeDriver(chromeOptions);
        }

        if (browserparameters.browser.Equals("edge") || browserparameters.browser.Equals("Edge"))
        {
            new DriverManager().SetUpDriver(new EdgeConfig(), VersionResolveStrategy.MatchingBrowser);
            var edgeOptions = new EdgeOptions();
            edgeOptions.UseChromium = true;

            if (browserparameters.headless)
                edgeOptions.AddArguments("--headless");

            webDriver = new EdgeDriver(edgeOptions);
        }

        webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(browserparameters.implicitWait);
        webDriver.Manage().Cookies.DeleteAllCookies();
        webDriver.Manage().Window.Maximize();

        //EventFiringWebDriver firingDriver = new EventFiringWebDriver(webDriver);
        //firingDriver.ElementClicking += FiringDriver_ElementClicking;
        //firingDriver.ElementValueChanging += FiringDriver_ElementValueChanging;
        //webDriver = firingDriver;

        //Remote
        //webDriver = new RemoteWebDriver(new Uri(browserParameters.gridUrl, UriKind.Absolute), options);
        return webDriver;
    }

    private void FiringDriver_ElementClicking(object sender, WebElementEventArgs e)
    {
        ScrollAndHighlight(e.Driver, e.Element);
    }
    private void FiringDriver_ElementValueChanging(object sender, WebElementEventArgs e)
    {
        ScrollAndHighlight(e.Driver, e.Element);
    }

    private static void ScrollAndHighlight(IWebDriver driver, IWebElement element)
    {
        try
        {
            var jsDriver = (IJavaScriptExecutor)driver;
            jsDriver.ExecuteScript("arguments[0].scrollIntoViewIfNeeded(true);", element);
            string highlightJavascript = @"arguments[0].style.cssText = ""border-width: 2px; border-style: solid; border-color: red"";";
            jsDriver.ExecuteScript(highlightJavascript, new object[] { element });
        }
        catch (Exception)
        {
            //continue;
        }
    }
}