namespace Utilities;

using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

public static class JsUtil
{
    public static void Click(IWebDriver webDriver, IWebElement webElement)
    {
        IJavaScriptExecutor executor = (IJavaScriptExecutor)webDriver;
        executor.ExecuteScript("arguments[0].click();", webElement);
    }

    public static void SetValueAttribute(IWebDriver webDriver, IWebElement webElement, string value)
    {
        IJavaScriptExecutor executor = (IJavaScriptExecutor)webDriver;
        executor.ExecuteScript("arguments[0].value='" + value + "';", webElement);
    }

    public static void Zoom(IWebDriver webDriver, int zoomValue)
    {
        IJavaScriptExecutor executor = (IJavaScriptExecutor)webDriver;
        executor.ExecuteScript("document.body.style.zoom='" + zoomValue + "%'");
        WaitUtil.WaitForSeconds(4);
    }

    public static void InputText(IWebDriver webDriver, IWebElement element, string text)
    {
        IJavaScriptExecutor executor = (IJavaScriptExecutor)webDriver;
        executor.ExecuteScript("arguments[0].innerText=arguments[1];", element, text);
        WaitUtil.WaitForSeconds(4);
    }

    public static void ScrollToTopOfThePage(IWebDriver webDriver)
    {
        IJavaScriptExecutor executor = (IJavaScriptExecutor)webDriver;
        executor.ExecuteScript("document.documentElement.scrollTop = 0");
        WaitUtil.WaitForSeconds(2);
    }
        
    public static void ScrollToBottomOfThePage(IWebDriver webDriver)
    {
        /*IJavaScriptExecutor executor = (IJavaScriptExecutor)webDriver;
        executor.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");*/
        Actions actions = new Actions(webDriver);
        actions.KeyDown(Keys.Control).SendKeys(Keys.End).Perform();
        WaitUtil.WaitForSeconds(2);
    }
}