namespace Model.Pages;

using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Utilities;

public class BaseDetailPage<T> : BasePage where T : BaseDetailPage<T>
{
    public BaseDetailPage(IWebDriver driver) : base(driver)
    {
    }

    protected IWebElement GetValueElByLabel(string label)
    {
        Console.WriteLine($"Test Step : Getting value of {label}");

        return webDriver.FindElement(By.XPath(".//span[text()='" + label + "']//ancestor::*[contains(@class,'slds-form-element')]//*[contains(@class,'test-id__field-value')]"));
    }

    protected bool GetCheckboxValueByLabel(string label)
    {
        Console.WriteLine("Test Step : Getting value of {0}", label);
        return Convert.ToBoolean(GetValueElByLabel(label).FindElement(By.XPath(".//input[@type='checkbox']")).GetAttribute("checked"));
    }

    public T ClickFirstEditIcon()
    {
        Console.WriteLine("Test Step : Clicking first edit icon");
        string xpath_element = ".//button[contains(@class,'inline-edit-trigger')]";
        List<IWebElement> inlineEditBtns = webDriver.FindElements(By.XPath(xpath_element)).ToList();
        if (inlineEditBtns.Any())
        {
            JsUtil.Click(webDriver, inlineEditBtns[0]);
        }
        return (T)this;
    }

    public T ClickSave()
    {
        Console.WriteLine("Test Step : Clicking Save");
        IWebElement btn = webDriver.FindElement(By.XPath("//button[@name='SaveEdit']"));
        btn.Click();
        WaitUtil.WaitForPageLoading(webDriver);
        return (T)this;
    }

    protected string GetLabelXLoactor(string labelText)
    {
        return "//label[text()='" + labelText + "']";
    }
}