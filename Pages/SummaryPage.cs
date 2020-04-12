using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace SHETest.Pages
{
    public class SummaryPage
    {

        private readonly RemoteWebDriver _driver;

        public SummaryPage(RemoteWebDriver driver) => _driver = driver;

        public IWebElement SummaryTable => _driver.FindElementByXPath("//table");

        IList<IWebElement> TrashIcon => _driver.FindElementsByClassName("icon-trash");
        IWebElement BtnSignOut => _driver.FindElementByClassName("logout");

        public void DeleteItemFromSummaryPage(int index)
        {
            TrashIcon[index].Click();
        }

        public void SignOut()
        {
            BtnSignOut.Click();
        }
      
    }
}
