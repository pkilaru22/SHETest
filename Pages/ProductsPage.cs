using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace SHETest.Pages
{
    public class ProductsPage
    {

       private readonly RemoteWebDriver _driver;

       public ProductsPage(RemoteWebDriver driver) => _driver = driver;
             

        IWebElement SummerDresses => _driver.FindElementByXPath("//ul[contains(@class, 'submenu')]/li/a[@title='Summer Dresses']");
        IList<IWebElement> WebElements => _driver.FindElementsByXPath("//div[@class='product-image-container']//img");
        IWebElement BtnContinueShopping => _driver.FindElementByXPath("//span[@title='Continue shopping']");
        IWebElement BtnProceedToCheckOut => _driver.FindElementByXPath("//span[contains(text(), 'Proceed to checkout')]");

        public void SelectSummerDresses()
        {
            //Doing a MouseHover  
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//a[@title='Dresses']/following-sibling::ul[contains(@class, 'submenu')]/../a")));
            Actions action = new Actions(_driver);
            action.MoveToElement(element).Perform();
            //Clicking the SubMenu on MouseHover   
            SummerDresses.Click();
        }

        public int getNumberOfSummerDresses()
        {
            //Wait Until Summer Dresses are loaded
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//h1/span[contains(text(), 'Summer Dresses')]")));

            return WebElements.Count;
        }

        public QuickViewPage QuickViewRandomSummerDress(int index)
        {
            Actions action = new Actions(_driver);
            //Mouse Hover over a random Summer Dress
            action.MoveToElement(WebElements[index]).Perform();
            //Click on the Quick-View button
            var element = _driver.FindElementByXPath("//li[contains(@class, 'hovered')]/div/div/div/a[@class='quick-view']/span");
            element.Click();

            
            return new QuickViewPage(_driver);
        }

        public void ClickContinueShopping()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//span[@title='Continue shopping']")));
            BtnContinueShopping.Click();
        }

        public void AddRandomDressToCart(int index)
        {
            Actions action = new Actions(_driver);
            action.MoveToElement(WebElements[index]).Perform();
            var element = _driver.FindElementByXPath("//li[contains(@class, 'hovered')]/div/div/div[@class='button-container']/a/span");
            element.Click();
        }

        public SummaryPage ClickProceedToCheckout()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//span[contains(text(), 'Proceed to checkout')]")));
            BtnProceedToCheckOut.Click();

            return new SummaryPage(_driver);
        }


    }
}
