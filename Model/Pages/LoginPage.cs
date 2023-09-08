namespace Model.Pages;

using OpenQA.Selenium;
using System;
using Utilities;

public class LoginPage : BasePage
{ 
    public LoginPage(IWebDriver driver) : base(driver)
    {
        webDriver = driver;
    }

    public void LoginAsAdminUser()
    {
        string xpath_username = ".//div[@role='button' and @data-test-id='sebastian.pulido@health.vic.gov.au']";
        WaitUtil.WaitForPageLoading(webDriver);
        IWebElement element = webDriver.FindElement(By.XPath(xpath_username));
        ElementHighlight(element);
        JsUtil.Click(webDriver, element);
        WaitUtil.WaitForPageLoading(webDriver);
    }

    public LoginPage SetUsername(string username)
    {
        Console.WriteLine($"Test Step : Entering username : {username}");
        webDriver.FindElement(By.Id("username")).SendKeys(username);
        return this;
    }

    public LoginPage SetPassword(string password)
    {
        Console.WriteLine("Test Step : Entering password ****");
        webDriver.FindElement(By.Id("password")).SendKeys(password);
        return this;
    }

    public LoginPage ClickLoginButton()
    {
        Console.WriteLine("Test Step : Clicking Login button");
        webDriver.FindElement(By.Id("Login")).Click();
        WaitUtil.WaitForSeconds(10);
        return this;
    }
}
