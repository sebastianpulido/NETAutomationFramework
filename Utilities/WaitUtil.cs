namespace Utilities;

using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Linq;

public static class WaitUtil
{
    public const int EXTRA_SMALL_WAIT = 5;
    public const int SMALL_WAIT = 10;
    public const int MEDIUM_WAIT = 20;
    public const int LARGE_WAIT = 50;
    public const int EXTRA_LARGE_WAIT = 90;

    public static IWebElement WaitForElementVisibilityFromRootElement(IWebDriver webDriver, IWebElement rootElement, By by, int timeout)
    {
        WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout));
        wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        try
        {
            return wait.Until<IWebElement>(driver =>
            {
                IWebElement elementToReturn = rootElement.FindElement(by);
                return (elementToReturn.Displayed && elementToReturn.Enabled) ? elementToReturn : null;
            }
            );
        }
        catch (WebDriverTimeoutException)
        {
            Assert.Fail($"Timout Exception - element located by {rootElement.ToString() + by.ToString()} not visible and enabled within {timeout} seconds.");
        }
        return null;
    }

    public static IWebElement WaitForElementExistence(IWebDriver webDriver, By by, int timeout)
    {
        WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout));
        return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
    }

    public static IWebElement WaitForElementVisibility(IWebDriver webDriver, By by, int timeout)
    {
        try
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout));
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
        }
        catch (WebDriverTimeoutException)
        {
            return null;
        }
    }

    public static IWebElement WaitForElementClickability(IWebDriver webDriver, By by, int timeout)
    {
        WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout));
        return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
    }
    public static IWebElement WaitForElementClickability(IWebDriver webDriver, IWebElement element, int timeout)
    {
        WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout));
        return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
    }

    public static void WaitForPageLoading(IWebDriver webDriver)
    {
        try
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(EXTRA_LARGE_WAIT));
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }
        catch (WebDriverException)
        {
            Console.WriteLine("Wait for page loading exception");
        }
    }

    public static void CheckForSpinnerToDisappear(IWebDriver webDriver, int timeoutInSeconds)
    {
        var Spinners = webDriver.FindElements(By.XPath("//*[contains(@class,'spinner')]"));

        if (Spinners.Any())
        {
            foreach (var spinner in Spinners)
            {
                try
                {
                    WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));
                    wait.Until(driver =>
                    {
                        try
                        {
                            return !spinner.Displayed;
                        }
                        catch (StaleElementReferenceException)
                        {
                            //Console.WriteLine("One spinner gone stale");
                            return true;
                        }
                    });
                }
                catch (NoSuchElementException)
                {
                    //Console.WriteLine("One less Stale");
                }
                catch (StaleElementReferenceException)
                {
                    //Console.WriteLine("One spinner gone stale");
                }
                catch (WebDriverTimeoutException)
                {
                    //Console.WriteLine("Spinner hasn't disappeared within timeout ");
                    //Console.WriteLine("It still provides more value to continue tests");
                }
            }
        }
    }
        
    public static void WaitForActionOnStaleElement(Action Action, int maximumAttempt)
    {
        int attempt = 0;
        while (attempt < maximumAttempt)
        {
            try
            {
                Action();
                break;
            }
            catch (Exception error)
            {
                if (error.Message.Contains("stale element reference"))
                    attempt++;
                else
                    throw error; // Re-throws the error so the test fails as normal if the assertion fails.
            }
        }
    }
    public static void WaitForActionOnInterceptedElement(Action Action, int maximumAttempt)
    {
        WaitForSeconds(2);
        int attempt = 0;
        while (attempt < maximumAttempt)
        {
            try
            {
                Action();
                break;
            }
            catch (Exception error)
            {
                if (error.Message.Contains("element click intercepted"))
                {
                    attempt++;
                    Thread.Sleep(500);
                }
                else
                    throw error; // Re-throws the error so the test fails as normal if the assertion fails.
            }
        }
    }

    public static void WaitForSeconds(int seconds)
    {
        Thread.Sleep(TimeSpan.FromSeconds(seconds));
    }

    public static void CheckForToastMessageToDisappear(IWebDriver webDriver, int timeoutInSeconds)
    {
        var toastMessages = webDriver.FindElements(By.ClassName("forceActionsText"));

        if (toastMessages.Any())
        {
            WaitForSeconds(timeoutInSeconds);
        }
    }
}