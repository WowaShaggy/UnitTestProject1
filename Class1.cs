using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rwbyTestProject
{
    public class rwbyMainPage
    {

        RemoteWebDriver Driver;

        public rwbyMainPage(RemoteWebDriver Driver) {
             this.Driver = Driver;
        }

       static public By SearchFromBar = By.Name("train_from");

        public void ClickSearchFromBar() {
            Driver.FindElement(SearchFromBar).Click();
        }
    }
}
