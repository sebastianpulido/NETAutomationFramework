namespace Tests.E2E;

using NUnit.Framework;
using System;
using Model.Pages;

[TestFixture]
[Parallelizable]
public class PBIxxxx_PageNavigation : BaseTest
{
    private readonly static string testID_1 = "TC01";
    private readonly static string testID_2 = "TC02";

    private readonly static TestCaseData[] TestCaseData = {
        new TestCaseData(testID_1),
        new TestCaseData(testID_2),
    };

    [Test]
    [Category("E2E")]
    [TestCaseSource("TestCaseData")]
    public void PBIxxxx_PageNavigation_01Test(string testDataIndex)
    {
        string testkey = "PBIxxxx_PageNavigation_Test_" + testDataIndex;
        Console.WriteLine($"testkey: {testkey}");

        LoginPage login = new(webDriver);
        login.LoginAsAdminUser();

        HomePage home = new(webDriver);
        home.ScrollToBottomOfThePage().ScrollToTopOfThePage();

        if (testDataIndex.Equals(testID_1))
        {
            home.GetWidgetHeaders();
            home.CliclOnOnlineApplicationManagementTab();
            home.ClickOnEmergencyManagementTab();
            home.ClickOnFinanceManagementTab();
        }

        else if (testDataIndex.Equals(testID_2))
        {
            home.GetWidgetHeaders();
            home.ClickOnAdministrationTab();
            home.ScrollToBottomOfThePage();
        }

        else
        {
            Console.WriteLine($"Test ID not supported.");
        }   
    }
}