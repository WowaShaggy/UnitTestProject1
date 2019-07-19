using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace rwbyTestProject
{
    [TestClass]
    public class rwbyTestMain
    {
        [TestMethod]
        public void Main()
        {
            RemoteWebDriver Driver = new ChromeDriver();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Driver.Manage().Window.Maximize();

            Driver.Navigate().GoToUrl(@"http://google.com");
            var targetElement = Driver.FindElement(By.CssSelector("input[aria-label='Search']"));
            // var targetElement = Driver.FindElementByCssSelector("input[aria-label='Search']"); // It appears that it's the same as line above

            targetElement.SendKeys("rw.by");
         
            //targetElement.Submit();   // it's like PRESS ENTER, but let's try another option:
            var searchInGoogleButton = Driver.FindElementByCssSelector("center > input[value*='Google']");
            searchInGoogleButton.Click();

            var rwByLink = Driver.FindElementByCssSelector("a[href = 'https://www.rw.by/'] ");
            rwByLink.Click();

            // Need to make sure that the Page has been loaded fine
            // But before, I'd try to create another class, thereby realize PageObject or something
            // Let's check presence of several page objects

            rwbyMainPage mainPage = new rwbyMainPage(Driver);

            NUnit.Framework.Assert.IsTrue(mainPage.MainPageIsDownloaded()); // Strange Strange Assert. Not sure that it should be so..

            Driver.Quit();
        }
    }
}
