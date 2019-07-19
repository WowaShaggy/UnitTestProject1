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

        public rwbyMainPage(RemoteWebDriver Driver) {
             this.Driver = Driver;
        }

       static public By SearchFromBar = By.Name("trvain_from");
       static public By SearchToBar = By.Name("train_to");
       static public By MenuBar = By.Id("main_menu");
        
        public bool MainPageIsDownloaded() {
            try
            {
                Driver.FindElement(SearchFromBar);
                Driver.FindElement(SearchToBar);
                Driver.FindElement(MenuBar);
            }
            catch (Exception) {
                MessageBox.Show("Error!!! One of elements hasn't been found!!!");
                return false;
            } 
            return true;
        }
    }
}
