namespace Model.Pages;

using OpenQA.Selenium;
using Utilities;
using System;
using NUnit.Framework;

public class BaseNewObjectPage<T> : BasePage where T : BaseNewObjectPage<T>
{
    protected string XLocatorNextButton = "//button//span[contains(text(),'Next')]";
    protected string XLocatorSaveButton = ".//button[text()='Save']";

    public BaseNewObjectPage(IWebDriver driver) : base(driver)
    {

    }

    public BaseNewObjectPage<T> ChooseTypeOption(string option)
    {
        Console.WriteLine($"Test Step : Choosing option {option}");
        string xpath_option = "//label//span[contains(text(),'" + option + "')]";
        IWebElement radioBtn = WaitUtil.WaitForElementVisibility(webDriver, By.XPath(xpath_option), WaitUtil.SMALL_WAIT);
        JsUtil.Click(webDriver, radioBtn);
        return this;
    }

    public T ClickNext()
    {
        Console.WriteLine("Test Step : Clicking Next");
        WaitUtil.WaitForElementVisibility(webDriver, By.XPath(XLocatorNextButton), WaitUtil.SMALL_WAIT).Click();
        WaitUtil.WaitForPageLoading(webDriver);
        return (T)this;
    }

    public BaseNewObjectPage<T> ClickSave()
    {
        Console.WriteLine("Test Step : Clicking Save");
        IWebElement btn = WaitUtil.WaitForElementVisibility(webDriver, By.XPath(XLocatorSaveButton), WaitUtil.SMALL_WAIT);
        JsUtil.Click(webDriver, btn);
        WaitUtil.WaitForPageLoading(webDriver);
        return this;
    }
}