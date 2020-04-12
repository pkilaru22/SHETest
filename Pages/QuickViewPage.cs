using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace SHETest.Pages
{
    public class QuickViewPage
    {

        private readonly RemoteWebDriver _driver;

        public QuickViewPage(RemoteWebDriver driver) => _driver = driver;


        IWebElement QuickViewFrame => _driver.FindElementByClassName("fancybox-iframe");
        IWebElement DressModel => _driver.FindElementByXPath("//p[@id='product_reference']/span");
        IWebElement DressPrice => _driver.FindElementById("our_price_display");
        IWebElement DressSize => _driver.FindElementById("group_1");
        IWebElement DressColor => _driver.FindElementByXPath("//a[@class='color_pick selected']");
        IWebElement BtnAddToCart => _driver.FindElementById("add_to_cart");

        

        public String GetDressModel()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("fancybox-iframe")));
            _driver.SwitchTo().Frame(QuickViewFrame);
             return DressModel.Text;
        }

        public String GetDressPrice()
        {
            return DressPrice.Text;
        }

        public String GetDressSize()
        {
            return new SelectElement(DressSize).SelectedOption.Text;
        }

        public String GetDressColor()
        {
            return DressColor.GetAttribute("title");
        }

        public ProductsPage ClickAddToCart()
        {
            BtnAddToCart.Click();
            return new ProductsPage(_driver);
        }

      

            
    }
}
