using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using pageObjects;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace Spec
{
    [Binding]
    public class SpecFlowFeatureSteps
    {
        static RemoteWebDriver Driver;
        static MainPage mainPage;
        static SearchPage searchPage;
        static public By searchArea = By.CssSelector("input[aria-label='Search']");
        static public By searchButton = By.XPath("//center/input[@name='btnK']");
        IWebElement targetElement;
        IWebElement targetLink;
        bool languageIsSwitched;
        int numberOfNews;
        string bottomLabel;
        string[] actualResultArray;
        string randomGeneratedString;

        [BeforeScenario("google", "calendar", "main")]
        public void BeforeScenario()
        {
            Driver = new ChromeDriver();
            mainPage = new MainPage(Driver);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Driver.Manage().Window.Maximize();
        }

        [Before("search")]
        public void BeforeScenarioSearch()
        {
            Driver = new ChromeDriver();
            mainPage = new MainPage(Driver);
            searchPage = new SearchPage(Driver);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Driver.Manage().Window.Maximize();
        }

        [When(@"I navigate to Google")]
        public void WhenINavigateToGoogle()
        {

            Driver.Navigate().GoToUrl(@"http://google.com");
        }

        [When(@"I search rw\.by")]
        public void WhenISearchRw_By()
        {
            targetElement = Driver.FindElement(searchArea);
            targetElement.SendKeys("rw.by");
        }

        [When(@"I press Search")]
        public void WhenIPressSearch()
        {
            try
            {
                Driver.FindElement(searchButton).Click();
                Console.WriteLine("Through Button");
            }
            catch (Exception)
            {
                targetElement.Submit();
                Console.WriteLine("Through Submit");
            }
        }

        [When(@"I find result in the list")]
        public void WhenIFindResultInTheList()
        {
            targetLink = Driver.FindElementByCssSelector("a[href = 'https://www.rw.by/']");
        }

        [When(@"I open link with search criteria")]
        public void WhenIOpenLinkWithSearchCriteria()
        {
            targetLink.Click();
        }

        [Then(@"The site is successfully loaded")]
        public void ThenTheSiteIsSuccessfullyLoaded()
        {
            Assert.IsTrue(mainPage.MainPageIsDownloaded());
            Assert.IsTrue(mainPage.MainPageLanguageIsDownloaded());
        }

        [Given(@"I am on rw\.by page for calendar")]
        public void GivenIAmOnRw_ByPageForCalendar()
        {
            Driver.Navigate().GoToUrl(@"http://rw.by");
        }


        [When(@"I enter Locations ""(.*)"" and ""(.*)""")]
        public void WhenIEnterLocationsAnd(string p0, string p1)
        {
            mainPage.EnterFromAndToLocations(p0, p1);

        }

        [When(@"I choose day in calendar in (.*)")]
        public void WhenIChooseDayInCalendarIn(int p0)
        {
            mainPage.ChooseDayInCalendarIn(p0);

        }

        [When(@"I press Search Day button")]
        public void WhenIPressSearchDayButton()
        {
            mainPage.ClickSearch();

        }

        [Then(@"Console displays Schedule")]
        public void ThenConsoleDisplaysSchedule()
        {
            Console.WriteLine(mainPage.WriteToConsoleSchedule());
        }

        [Given(@"I am on schedule page for ""(.*)"" and ""(.*)"" and date in (.*)")]
        public void GivenIAmOnSchedulePageForAndAndDateIn(string p0, string p1, int p2)
        {
            DateTime date1 = new DateTime();
            date1 = DateTime.Now;
            date1 = date1.AddDays(p2);
            string dateS = date1.ToString("yyyy-MM-dd");
            Driver.Navigate().GoToUrl(@"https://rasp.rw.by/ru/route/?from=" + p0 + "&to=" + p1 + "&date=" + dateS);
        }


        [When(@"I click on the first result")]
        public void WhenIClickOnTheFirstResult()
        {
            mainPage.ClickOnTheFirstResult();
        }

        [Given(@"I click on the first result")]
        public void GivenIClickOnTheFirstResult()
        {
            mainPage.ClickOnTheFirstResult();
        }

        [Then(@"Route is displayed")]
        public void ThenRouteIsDisplayed()
        {
            Assert.IsTrue(mainPage.CheckRouteIsDisplayed());
        }

        [Then(@"Calendar description isn't empty")]
        public void ThenCalendarDescriptionIsnTEmpty()
        {
            Assert.IsNotNull(mainPage.GetCalendarDescription());
        }

        [When(@"I click logo")]
        public void WhenIClickLogo()
        {
            mainPage.ClickLogo();
        }

        [Then(@"Main Page appears")]
        public void ThenMainPageAppears()
        {
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
            Assert.IsTrue(mainPage.MainPageIsDownloaded());
        }

        [Given(@"I am on rw\.by page")]
        public void GivenIAmOnRw_ByPage()
        {
            Driver.Navigate().GoToUrl(@"http://rw.by");
        }

        [When(@"I switch language")]
        public void WhenISwitchLanguage()
        {
            languageIsSwitched = mainPage.ChangeLanguageToEnglish();
        }

        [Given(@"I switch language")]
        public void GivenISwitchLanguage()
        {
            mainPage.ChangeLanguageToEnglish();
        }


        [Then(@"Language changed")]
        public void ThenLanguageChanged()
        {
            Assert.IsTrue(languageIsSwitched);
        }

        [When(@"I get number of news")]
        public void WhenIGetNumberOfNews()
        {
            numberOfNews = mainPage.NumberOfNews();
        }

        [Then(@"Number of news is (.*)")]
        public void ThenNumberOfNewsIs(int p0)
        {
            Assert.AreEqual(p0, numberOfNews);
        }

        [When(@"I check label in page bottom")]
        public void WhenICheckLabelInPageBottom()
        {
            bottomLabel = mainPage.GetCopyrightText();
        }

        [Then(@"Label is ""(.*)""")]
        public void ThenLabelIs(string p0)
        {
            Assert.AreEqual(p0, bottomLabel);
        }

        [When(@"I check find top buttons")]
        public void WhenICheckFindTopButtons()
        {
            actualResultArray = (string[])mainPage.GetTopMenuItems().Clone();
        }

        [Then(@"Top buttons are""(.*)"", ""(.*)"", ""(.*)"" and ""(.*)""")]
        public void ThenTopButtonsAreAnd(string p0, string p1, string p2, string p3)
        {
            string[] expectedResultsArray = new string[] { p0, p1, p2, p3 };
            Assert.IsTrue(expectedResultsArray.Intersect(actualResultArray).Count() == expectedResultsArray.Length);
        }

        [Given(@"I am on rw\.by page for search")]
        public void GivenIAmOnRw_ByPageForSearch()
        {
            Driver.Navigate().GoToUrl(@"http://rw.by");
        }


        [Given(@"I am on search page for random string")]
        public void GivenIAmOnSearchPageForRandomString()
        {
            Driver.Navigate().GoToUrl(@"http://www.rw.by/search/?search=" + searchPage.GenRandomString(20));
        }

        [When(@"I type random String with (.*) characters in Search Bar")]
        public void WhenITypeRandomStringWithCharactersInSearchBar(int p0)
        {
            randomGeneratedString = searchPage.GenRandomString(p0);
            mainPage.TypeInSearchBar(randomGeneratedString);
        }

        [When(@"I clean Search bar")]
        public void WhenICleanSearchBar()
        {
            searchPage.CleanBigSearchBar();
        }

        [When(@"I type ""(.*)"" in Search Bar")]
        public void WhenITypeInSearchBar(string p0)
        {
            mainPage.TypeInSearchBar(p0);
        }

        [When(@"I press Search button")]
        public void WhenIPressSearchButton()
        {
            searchPage.PressSearchButton();
        }

        [Then(@"Link contains this String")]
        public void ThenLinkContainsThisString()
        {
            Assert.AreEqual($"https://www.rw.by/search/?search={randomGeneratedString}", Driver.Url);
        }

        [Then(@"Page contains label ""(.*)""")]
        public void ThenPageContainsLabel(string p0)
        {
            Assert.AreEqual(p0, searchPage.FindSearchResultLebel());
        }

        [Then(@"Page displays (.*) search results")]
        public void ThenPageDisplaysSearchResults(int p0)
        {
            Assert.AreEqual(p0, searchPage.NumberOfSearchResults());
        }

        [Then(@"I bring out results in console")]
        public void ThenIBringOutResultsInConsole()
        {
            Console.OpenStandardInput();
            Console.WriteLine(searchPage.GetTextFromResults());
        }

        [AfterScenario("google", "calendar", "main", "search")]
        public void AfterScenario()
        {
            Driver.Quit();
        }

    }
}
