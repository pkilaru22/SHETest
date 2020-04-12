using System;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using SHETest.Pages;


namespace SHETest
{
    [TestFixture]
    public class Program
    {

        RemoteWebDriver driver;        
       
        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://automationpractice.com/index.php");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait.Seconds.Equals(50);

        }


        [Test]
        public void Test()
        {
            
            HomePage homePage = new HomePage(driver);

            //Login to the site
            LoginPage loginPage = homePage.ClickSignIn();            
            ProductsPage productsPage = loginPage.SignIn("d1474341@urhen.com", "Password");
            Console.WriteLine("Logged in to the site");

            //From the Main menu select Dresses > Summer Dresses
            productsPage.SelectSummerDresses();
            Console.WriteLine("Selected Dresses > Summer Dresses");

            //Quick View a Dress
            //Selecting 2 random summer dress's index - one to quick view and add to cart and second one to add to the cart directly
            int numberOfSummerDresses = productsPage.getNumberOfSummerDresses();
            Random rand = new Random();
            int randomIndex1 = rand.Next(0, numberOfSummerDresses);
            int randomIndex2 = rand.Next(0, numberOfSummerDresses);
            while (randomIndex2 == randomIndex1)
                randomIndex2 = rand.Next(0, numberOfSummerDresses);

            //Quick viewing a summer dress
            QuickViewPage quickViewPage = productsPage.QuickViewRandomSummerDress(randomIndex1);
            Console.WriteLine("Opened dress in Quick view and noted down its details to check them in the summary page");

            //Noting down the dress details to assert them in the summary page
            String expectedModel = quickViewPage.GetDressModel();
            String expectedColor = quickViewPage.GetDressColor();
            String expectedSize = quickViewPage.GetDressSize();
            String expectedPrice = quickViewPage.GetDressPrice();

            //Add the dress to cart from quick view
            productsPage = quickViewPage.ClickAddToCart();
            Console.WriteLine("Added first dress to the cart from quick view");

            // Continue shopping to add another dress to cart
            productsPage.ClickContinueShopping();

            //Add Another Dress to the Cart
            productsPage.AddRandomDressToCart(randomIndex2);
            Console.WriteLine("Added another dress to the cart without quick view");

            //Proceed to Checkout
            SummaryPage summaryPage = productsPage.ClickProceedToCheckout();

            //Verify on Summary page correct dress is selected
            AssertSelectedDressInSummaryPageTable(summaryPage.SummaryTable, expectedModel, expectedColor, expectedSize, expectedPrice);

            //Delete the second dress that was added to the cart
            summaryPage.DeleteItemFromSummaryPage(1);

            //Sign out
            summaryPage.SignOut();
            Console.WriteLine("Signed Out");

        }

        private void AssertSelectedDressInSummaryPageTable(IWebElement table, String expectedModel, String expectedColor, String expectedSize, String expectedPrice)
        {
            //Getting the first dress details from summary page
            Utilities.ReadTable(table);            
            String description = Utilities.ReadCell("Description", 0);           
            String actualDressModel = Utilities.GetSubStringBetweenStrings(description, "SKU : ", "Color").Trim();         
            String actualDressColor = Utilities.GetSubStringBetweenStrings(description, "Color : ", ",").Trim();         
            String actualDressSize = Utilities.ReadCell("Description", 1).Split(':').Last().Trim();    
            String actualDressPrice = Utilities.ReadCell("Total", 0);

            //Asserting the first selected dress details
            Console.WriteLine("Asserting first dress model is: " + expectedModel);
            Assert.AreEqual(expectedModel, actualDressModel);
            Console.WriteLine("Asserting first dress color is: " + expectedColor);
            Assert.AreEqual(expectedColor, actualDressColor);
            Console.WriteLine("Asserting first dress size is: " + expectedSize);
            Assert.AreEqual(expectedSize, actualDressSize);
            Console.WriteLine("Asserting first dress price is: " + expectedPrice);
            Assert.AreEqual(expectedPrice, actualDressPrice);
       }


        [TearDown]
        public void TearDown()
        {
          driver.Quit();
        }

    }


   
}
