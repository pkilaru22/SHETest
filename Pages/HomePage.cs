using System;
using OpenQA.Selenium;

namespace SHETest.Pages
{
    public class HomePage
    {
        private OpenQA.Selenium.Remote.RemoteWebDriver _driver;

        public HomePage(OpenQA.Selenium.Remote.RemoteWebDriver driver) => _driver = driver;

        IWebElement btnSignIn => _driver.FindElementByXPath("//a[@class='login']");

        public LoginPage ClickSignIn()
        {
            btnSignIn.Click();
            return new LoginPage(_driver);
        }
    }
}
