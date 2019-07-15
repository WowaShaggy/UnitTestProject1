using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            RemoteWebDriver Driver = new ChromeDriver();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Driver.Manage().Window.Maximize();

            Driver.Navigate().GoToUrl(@"http://google.com");
            var targetElement = Driver.FindElement(By.CssSelector("input[aria-label='Search']"));
            // var targetElement = Driver.FindElementByCssSelector("input[aria-label='Search']"); // Похоже, что то же само

            targetElement.SendKeys("rw.by");
         
            //targetElement.Submit();   // Это тоже как Enter, но попробуем найти кнопку и нажать на нее
            var searchInGoogleButton = Driver.FindElementByCssSelector("center > input[value*='Google']");
            searchInGoogleButton.Click();
            
        }
    }
}
