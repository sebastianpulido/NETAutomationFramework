namespace Tests;

using NUnit.Framework;

[SetUpFixture]
public class SetUpClass
{
    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        //TestData.LoadTestData();
    }
}