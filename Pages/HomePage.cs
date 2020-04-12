using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace SHETest.Pages
{
    public class HomePage
    {
        private RemoteWebDriver _driver;

        public HomePage(RemoteWebDriver driver) => _driver = driver;

        IWebElement btnSignIn => _driver.FindElementByXPath("//a[@class='login']");

        public LoginPage ClickSignIn()
        {
            btnSignIn.Click();
            return new LoginPage(_driver);
        }
    }
}
