﻿using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace pageObjects
{
    public class MainPage
    {

        RemoteWebDriver Driver;

        public MainPage(RemoteWebDriver Driver)
        {
           this.Driver = Driver;
        }

        static public By SearchFromBar = By.Name("train_from");
        static public By SearchToBar = By.Name("train_to");
        static public By MenuBar = By.Id("main_menu");
        static public By Language = By.ClassName("langText");
        static public By LanguageEn = By.XPath("//ul[contains(@class, 'lang-select')]/li/a[contains(@href, '/en/')]");
        static public By NewsList = By.XPath("//div[@class='index-news-list']/dl/dt");
        static public By Copyright = By.ClassName("copyright");
        static public By TopMenuItems = By.XPath("//table[@class='menu-items']/tbody/tr/td");
        static public By SearchBySiteBar = By.Name("search");
        static public By NotFoundLebel = By.ClassName("result");


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

        public string GetCopyrightText() {
            string fullText = Driver.FindElement(Copyright).Text;
            int ind = fullText.IndexOf('\r');
            return fullText.Remove(ind);
        }

        public string [] GetTopMenuItems() {
            TextInfo tI = new CultureInfo("en-US", false).TextInfo;
            int i = 0;
            int count = Driver.FindElements(TopMenuItems).Count;
            string[] array = new string[count];
             while (i < count) {
                array[i] = tI.ToTitleCase(Driver.FindElementByXPath($"//table[@class='menu-items']/tbody/tr/td[{i+1}]/a/em/u/b").Text.ToLower());
                i++;
             }
            return array;
        }

        public void TypeInSearchBar(string searchText) {
            Driver.FindElement(SearchBySiteBar).SendKeys(searchText);
            Driver.FindElement(SearchBySiteBar).Submit();
        }
    }
}
