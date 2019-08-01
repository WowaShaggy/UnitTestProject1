using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
        static public By CalendarButton = By.ClassName("calendar");
        static public By CalendarTrElements = By.XPath("//table[@class='ui-datepicker-calendar']/tbody/tr");
        static public By CalendarNextArrow = By.XPath("//div[@id='ui-datepicker-div']/div/a[2]/span");
        static public By TodayLevel = By.XPath("//div[@class='hint']/a[1]");
        static public By SearchButton = By.Id("find-rout");
        static public By ScheduleListelements = By.XPath("//tbody[@class='schedule_list']/tr");
        static public By ScheduleListFirstResult = By.XPath("//tbody[@class='schedule_list']/tr[1]//a[@class='train_text']");
        static public By RouteName = By.ClassName("page-title_heading");
        static public By CalendarDescription = By.ClassName("calendar_description");
        static public By Logo = By.ClassName("logo_link");

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

        public void EnterFromAndToLocations(string locationFrom, string locationTo)
        {
            Driver.FindElement(SearchFromBar).Clear();
            Driver.FindElement(SearchToBar).Clear();
            Driver.FindElement(SearchFromBar).SendKeys(locationFrom);
            Driver.FindElement(SearchToBar).SendKeys(locationTo);
        }

        public void ChooseDayInCalendarIn(int day)
        {
            Driver.FindElement(CalendarButton).Click();
            int NumberOfLines = Driver.FindElements(CalendarTrElements).Count;
            By [,] CalendarArray = new By [7, NumberOfLines];
            int dayOfInterest = DateTime.Today.Day + day;
            if (dayOfInterest <= DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month))
            {
                for (int i = 0; i < 7; i++)
                {
                    for (int ii = 0; ii < NumberOfLines; ii++)
                    {
                        CalendarArray[i, ii] = By.XPath($"//table[@class='ui-datepicker-calendar']/tbody/tr[{i + 1}]/td[{ii + 1}]");
                        string indexDay = Driver.FindElementByXPath($"//table[@class='ui-datepicker-calendar']/tbody/tr[{i + 1}]/td[{ii + 1}]").Text;
                        if (Int32.Parse(indexDay) == dayOfInterest)
                        {
                            Driver.FindElementByXPath($"//table[@class='ui-datepicker-calendar']/tbody/tr[{i + 1}]/td[{ii + 1}]").Click();
                        }
                    }
                }

            }
            else 
            {   // I decided do not consider the case, when the necessary date is in the other year
                for (int m = 1; DateTime.Today.Month + m <= 12; m++)
                {
                    dayOfInterest = dayOfInterest - DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month + (m-1));
                    Driver.FindElement(CalendarNextArrow).Click();
                    NumberOfLines = Driver.FindElements(CalendarTrElements).Count;
                    CalendarArray = new By[7, NumberOfLines];
                    if (DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month + m) >= dayOfInterest)
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            for (int ii = 0; ii < NumberOfLines; ii++)
                            {
                                CalendarArray[i, ii] = By.XPath($"//table[@class='ui-datepicker-calendar']/tbody/tr[{i + 1}]/td[{ii + 1}]");
                                string indexDay = Driver.FindElementByXPath($"//table[@class='ui-datepicker-calendar']/tbody/tr[{i + 1}]/td[{ii + 1}]").Text;
                                Int32.TryParse(indexDay, out int indexDayInt);
                                if (indexDayInt == dayOfInterest)
                                {
                                    Driver.FindElementByXPath($"//table[@class='ui-datepicker-calendar']/tbody/tr[{i + 1}]/td[{ii + 1}]").Click();
                                    return;
                                }
                            }
                        }
                    }
                }
            }

        }

        public void ClickSearch() {
            Driver.FindElement(SearchButton).Click();
        }

        public string WriteToConsoleSchedule() {
            int i = 0;
            int count = Driver.FindElements(ScheduleListelements).Count;
            string array = "";
            while (i < count)
            {
                array += Driver.FindElementByXPath($"//tbody[@class='schedule_list']/tr[{i + 1}]//a[@class='train_text']").Text;
                array += "\t";
                array += Driver.FindElementByXPath($"//tbody[@class='schedule_list']/tr[{i + 1}]/td[@class='train_item train_start']/b").Text;
                array += "\n\n";
                i++;
            }
            return array;
        }

        public void ClickOnTheFirstResult() {
            Driver.FindElement(ScheduleListFirstResult).Click();
        }

        public bool CheckRouteIsDisplayed() {
            return Driver.FindElement(RouteName).Displayed;
        }

        public string GetCalendarDescription() {
            return Driver.FindElement(CalendarDescription).Text;
        }

        public void ClickLogo() {
            Driver.FindElement(Logo).Click();
        }

    }
}

