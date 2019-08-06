using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pageObjects
{
    public class SearchPage
    {

        RemoteWebDriver Driver;

        public SearchPage(RemoteWebDriver Driver)
        {
            this.Driver = Driver;
        }

        static public By NotFoundLebel = By.ClassName("result");
        static public By BigSearchBar = By.Id("searchinpm");
        static public By SearchButton = By.ClassName("search-button");
        static public By SearchResultList = By.XPath("//ol[contains(@class, 'search-result')]/li");

        public string FindSearchResultLebel()
        {
            return Driver.FindElement(NotFoundLebel).Text;
        }

        public void CleanBigSearchBar()
        {
            Driver.FindElement(BigSearchBar).Clear();
        }

        public void TypeInBigSearchBar(string query) {
            Driver.FindElement(BigSearchBar).SendKeys(query);
        }

        public void PressSearchButton() {
            Driver.FindElement(SearchButton).Click();
        }

        public int NumberOfSearchResults() {
            return Driver.FindElements(SearchResultList).Count;
        }

        public string GetTextFromResults() {
            int i = 0;
            int count = Driver.FindElements(SearchResultList).Count;
            string array2 = "";
            while (i < count)
            {
                array2 += Driver.FindElementByXPath($"//ol[contains(@class, 'search-result')]/li[{i + 1}]/h3/a").Text;
                array2 += "\n";
                array2 += Driver.FindElementByXPath($"//ol[contains(@class, 'search-result')]/li[{i + 1}]/p").Text;
                array2 += "\n\n";
                i++;
            }
            return array2;
        }

        public string GenRandomString(int Length)
        {
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder(Length - 1);
            char english, englishL;
            char[] twoLetters;
            for (int i = 0; i < Length; i++)
            {
                english = (char)rnd.Next(0x0041, 0x005A);
                englishL = (char)rnd.Next(0x0061, 0x007A);
                twoLetters = new char[] { english, englishL };
                sb.Append(twoLetters[rnd.Next(twoLetters.Length)]);
            }
            return sb.ToString();
        }

    }

}

