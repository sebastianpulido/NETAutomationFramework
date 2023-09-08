namespace Model.Pages;

using OpenQA.Selenium;
using Utilities;

public class HomePage : BasePage
{
    public HomePage(IWebDriver driver) : base(driver) 
    {
    }

    public ApplicationManagementPage CliclOnOnlineApplicationManagementTab()
    {
        WaitUtil.WaitForSeconds(1);
        IWebElement element = webDriver.FindElement(By.XPath(XLocatorOnlineApplicationManagementTab));
        ScrollToView(element);
        ElementHighlight(element);
        WaitUtil.WaitForSeconds(1);
        JsUtil.Click(webDriver, element);
        WaitUtil.WaitForPageLoading(webDriver);
        webDriver.Navigate().Refresh();
        return new ApplicationManagementPage(webDriver);
    }

    public EmergencyManagementPage ClickOnEmergencyManagementTab()
    {
        WaitUtil.WaitForSeconds(1);
        IWebElement element = webDriver.FindElement(By.XPath(XLocatorEmergencyManagementTab));
        ScrollToView(element);
        ElementHighlight(element);
        WaitUtil.WaitForSeconds(1);
        JsUtil.Click(webDriver, element);
        WaitUtil.WaitForPageLoading(webDriver);
        webDriver.Navigate().Refresh();
        return new EmergencyManagementPage(webDriver);
    }

    public FinanceManagementPage ClickOnFinanceManagementTab()
    {
        WaitUtil.WaitForSeconds(1);
        IWebElement element = webDriver.FindElement(By.XPath(XLocatorFinanceManagementTab));
        ScrollToView(element);
        ElementHighlight(element);
        WaitUtil.WaitForSeconds(1);
        JsUtil.Click(webDriver, element);
        WaitUtil.WaitForPageLoading(webDriver);
        webDriver.Navigate().Refresh();
        return new FinanceManagementPage(webDriver);
    }

    public FinanceManagementPage ClickOnAdministrationTab()
    {
        WaitUtil.WaitForSeconds(1);
        IWebElement element = webDriver.FindElement(By.XPath(XLocatorAdministrationTab));
        ScrollToView(element);
        ElementHighlight(element);
        WaitUtil.WaitForSeconds(1);
        JsUtil.Click(webDriver, element);
        WaitUtil.WaitForPageLoading(webDriver);
        webDriver.Navigate().Refresh();
        return new FinanceManagementPage(webDriver);
    }
}
