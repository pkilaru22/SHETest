using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace SHETest.Pages
{
    public class LoginPage
    {

        private readonly RemoteWebDriver _driver;

        public LoginPage(RemoteWebDriver driver) => _driver = driver;

        IWebElement TextEmail => _driver.FindElementById("email");
        IWebElement TextPassword => _driver.FindElementById("passwd");
        IWebElement BtnSignIn => _driver.FindElementById("SubmitLogin");


        public ProductsPage SignIn(String email, String password)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("email")));
            TextEmail.Clear();
            TextEmail.SendKeys(email);

            TextPassword.Clear();
            TextPassword.SendKeys(password);
            BtnSignIn.Click();

            return new ProductsPage(_driver);
        }
    }
}
