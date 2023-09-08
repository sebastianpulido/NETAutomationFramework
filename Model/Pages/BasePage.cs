namespace Model.Pages;

using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Utilities;

public abstract class BasePage
{
    public IWebDriver webDriver;
    protected string XLocatorHomePageIcon = ".//span[@class='glyphicon glyphicon-home']";
    protected string XLocatorOnlineApplicationManagementTab = ".//a[text()='Online application management']";
    protected string XLocatorEmergencyManagementTab = ".//a[text()='Emergency management']";
    protected string XLocatorFinanceManagementTab = ".//a[text()='Finance management']";
    protected string XLocatorAdministrationTab = ".//a[text()='Administration']";

    public BasePage(IWebDriver driver)
    {
        webDriver = driver;
    }

    public string GetWindow(string title, int attempts)
    {
        int attempted = 0;
        while (attempted < attempts)
        {
            foreach (var handle in webDriver.WindowHandles)
            {
                webDriver.SwitchTo().Window(handle);
                if (webDriver.Title.Contains(title))
                    return handle;
            }
            attempted++;
            WaitUtil.WaitForSeconds(2);
        }
        return null;
    }

    public IWebDriver SetWebDriverByWindow(string window)
    {
        Console.WriteLine("Test Step : Going to window");
        webDriver.SwitchTo().Window(window);
        return webDriver;
    }

    public void SetWebDriver(string framePath)
    {
        if (string.IsNullOrEmpty(framePath))
        {
            webDriver.SwitchTo().DefaultContent();
        }
        else
        {
            IWebElement frame = webDriver.FindElement(By.XPath(framePath));
            webDriver.SwitchTo().Frame(frame);
        }
    }

    public string GetPageTitle()
    {
        WaitUtil.WaitForSeconds(10);
        return webDriver.Title;
    }

    public void ElementHighlight(IWebElement element)
    {
        WaitUtil.WaitForSeconds(1);
        var jsDriver = (IJavaScriptExecutor)webDriver;
        string highlightJavascript = @"arguments[0].style.cssText = ""border-width: 2px; border-style: solid; border-color: red"";";
        jsDriver.ExecuteScript(highlightJavascript, new object[] { element });
    }

    public IWebElement ScrollToView(IWebElement element)
    {
        Console.WriteLine("Test Step : Scrolling to element");
        ((IJavaScriptExecutor)webDriver).ExecuteScript("arguments[0].scrollIntoViewIfNeeded(true);", element);
        WaitUtil.WaitForSeconds(1);
        ElementHighlight(element);
        return element;
    }

    public IWebElement GetInputByLabelXpath(string XLocatorLabel)
    {
        string id = WaitUtil.WaitForElementVisibility(webDriver, By.XPath(XLocatorLabel), WaitUtil.MEDIUM_WAIT).GetAttribute("for");
        return webDriver.FindElement(By.XPath("//*[@id='" + id + "']"));
    }

    protected bool GetCheckboxStatusByLabelXpath(string XLocatorLabel)
    {
        IWebElement checkbox = GetInputByLabelXpath(XLocatorLabel);
        ScrollToView(checkbox);
        Console.WriteLine("Checkbox status attribute in GET: " + checkbox.GetAttribute("checked"));
        return !string.IsNullOrEmpty(checkbox.GetAttribute("checked"));
    }

    public void ClearInputByLabelXpath(string XLocatorLabel)
    {
        GetInputByLabelXpath(XLocatorLabel).Clear();
    }

    public void SelectValueByLabelXpath(string XLocatorLabel)
    {
        string id = WaitUtil.WaitForElementVisibility(webDriver, By.XPath(XLocatorLabel), WaitUtil.MEDIUM_WAIT).GetAttribute("for");
        webDriver.FindElement(By.XPath("//*[@id='" + id + "']")).SendKeys(Keys.Enter);
    }

    public void SetInputByLabelXpath(string XLocatorLabel, string input)
    {
        Console.WriteLine($"Test Step : Setting input {input}");
        //JsUtil.InputText(webDriver,GetInputByLabelXpath(XLocatorLabel), input);
        GetInputByLabelXpath(XLocatorLabel).SendKeys(input);
    }

    public void ChooseFromLinkComboBox(string XLocatorComboBox, string text)
    {
        Console.WriteLine($"Test Step : Choosing from combo box {text}");
        string xpath_element = "//a[@title='" + text + "']";
        webDriver.FindElement(By.XPath(XLocatorComboBox)).Click();
        WaitUtil.WaitForElementVisibility(webDriver, By.XPath(xpath_element), WaitUtil.SMALL_WAIT).Click();
    }

    public void ChooseFromPopupComboBox(string XLocatorLabel, string text)
    {
        Console.WriteLine($"Test Step : Choosing from combo box {text}");
        string id = WaitUtil.WaitForElementVisibility(webDriver, By.XPath(XLocatorLabel), WaitUtil.MEDIUM_WAIT).GetAttribute("for");
        IWebElement input = webDriver.FindElement(By.XPath("//button[@id='" + id + "']"));
        JsUtil.Click(webDriver, input);
        IWebElement option = webDriver.FindElement(By.XPath("//*[contains(@data-item-id,'" + id + "')]//span[text()='" + text + "']"));
        JsUtil.Click(webDriver, option);
    }

    protected string GetTextValueByLabel(string label)
    {

        return "";
    }

    public IList<IWebElement> GetWidgetHeaders()
    {
        WaitUtil.WaitForSeconds(3);
        string xpath_widget_headers = ".//div[@class='widget-header']";
        IList<IWebElement> elements = webDriver.FindElements(By.XPath(xpath_widget_headers));
        foreach (IWebElement element in elements)
        {
            ScrollToView(element);
            ElementHighlight(element);
            Console.WriteLine($"> header: {element.Text}");
        }
        return elements;
    }

    public BasePage ScrollToBottomOfThePage()
    {
        JsUtil.ScrollToBottomOfThePage(webDriver);
        return this;
    }

    public BasePage ScrollToTopOfThePage()
    {
        JsUtil.ScrollToTopOfThePage(webDriver);
        return this;
    }
}