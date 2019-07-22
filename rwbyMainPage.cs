using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace rwbyTestProject
{
    public class rwbyMainPage
    {

        RemoteWebDriver Driver;

        public rwbyMainPage(RemoteWebDriver Driver)
        {
            this.Driver = Driver;
        }

        static public By SearchFromBar = By.Name("train_from");
        static public By SearchToBar = By.Name("train_to");
        static public By MenuBar = By.Id("main_menu");
        static public By Language = By.ClassName("langText");
        static public By LanguageEn = By.XPath("//ul[contains(@class, 'lang-select')]/li/a[contains(@href, '/en/')]");
        static public By NewsList = By.XPath("//div[@class='index-news-list']/dl/dt");

        public bool MainPageIsDownloaded()
        {
            try
            {
                Driver.FindElement(SearchFromBar);
                Driver.FindElement(SearchToBar);
                Driver.FindElement(MenuBar);
            }
            catch (Exception)
            {
                MessageBox.Show("Error!!! One of elements hasn't been found!!!");
                return false;
            }
            return true;
        }

        public bool MainPageLanguageIsDownloaded()
        {
            return (Driver.FindElement(Language).GetAttribute("display") != "none");
        }

        public bool ChangeLanguageToEnglish()
        {
            try
            {
                Driver.FindElement(Language).Click();
                Driver.FindElement(LanguageEn).Click();
                return (Driver.Url.Contains("en") == true);
            }
            catch (Exception)
            {
                MessageBox.Show("Something goes wrong");
                return false;
            }

        }

        public int NumberOfNews() {
            return Driver.FindElements(NewsList).Count();
        }
    }
}
