using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WordPressPCL.Models;

namespace Manage_HH
{
    class Selenium
    {
        public static ChromeOptions options = new ChromeOptions();
        public static IWebDriver driver = new ChromeDriver(Selenium.getOptions(options));
        public static WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

        public static ChromeOptions getOptions(ChromeOptions options)
        {
            options.AddArguments("--disable-popup-blocking");
            return options;
        }

        /// <summary>
        /// removes duplicate elements from products list
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="wait"></param>
        public static void removeDuplicates(IWebDriver driver, WebDriverWait wait)
        {
            foreach (Product product in Product.products)
            {
                product.change = false;
            }

            foreach (Product product1 in Product.products)
            {
                String name1;

                driver.Navigate().GoToUrl(product1.URL);

                try
                {
                    wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//span [@id = 'productTitle']")));
                    name1 = driver.FindElement(By.XPath("//span [@id = 'productTitle']")).Text;
                }
                catch (Exception e)
                {
                    if(e is NoSuchElementException)
                    {
                        name1 = "";
                        product1.change = true;
                    }
                    else if(e is WebDriverTimeoutException)
                    {
                        name1 = "";
                        product1.change = true;
                    }
                    else
                    {
                        throw;
                    }
                }

                foreach (Product product2 in Product.products)
                {
                    String name2;

                    driver.Navigate().GoToUrl(product2.URL);

                    try
                    {
                        name2 = driver.FindElement(By.XPath("//span [@id = 'productTitle']")).Text;
                    }
                    catch (Exception e)
                    {
                        if (e is NoSuchElementException)
                        {
                            name2 = "";
                            product2.change = true;
                        }
                        else if (e is WebDriverTimeoutException)
                        {
                            name2 = "";
                            product2.change = true;
                        }
                        else
                        {
                            throw;
                        }
                    }

                    if(name1 == name2)
                    {
                        product2.change = true;
                    }
                }
            }

            restart:
            foreach (Product product in Product.products)
            {
                if(product.change)
                {
                    Product.products.Remove(product);
                    Wordpress.RemoveProduct(product).Wait();
                    goto restart;
                }
            }
        }

        /// <summary>
        /// gets the amazon link of a post from the website
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="wait"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public static String getLink(IWebDriver driver, WebDriverWait wait, Post post)
        {
            int totalPages;
            String tag = Wordpress.SendGetTag(Wordpress.GetTag(post));
            driver.FindElement(By.XPath("//div [@class = 'wp-menu-image dashicons-before dashicons-admin-post']")).Click();
            if (driver.FindElements(By.XPath("//span [@class = 'total-pages']")).Count > 0)
            {
                totalPages = int.Parse(driver.FindElement(By.XPath("//span [@class = 'total-pages']")).Text);
            }
            else
            {
                totalPages = 1;
            }

            for (int x = 1; x <= totalPages; x++)
            {
                List<IWebElement> elements = driver.FindElements(By.XPath("//td [@class = 'tags column-tags']")).ToList();
                foreach (IWebElement element in elements)
                {
                    if(element.Text == tag)
                    {
                        return driver.FindElement(By.XPath("(//span [@class = 'url'])[" + (elements.IndexOf(element)+1).ToString() + "]")).GetAttribute("innerHTML");
                    }
                }
                if(x!=totalPages)
                {
                    driver.FindElement(By.XPath("//a [@class = 'next-page button']")).Click();
                    wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//td [@id = 'cb']")));
                }
            }
            MessageBox.Show("Link not found");
            return "Not Found";

        }

        /// <summary>
        /// gets the data of existing posts from their amazon pages. Used to check if the sale data is still correct
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="wait"></param>
        public static void GetOldPostData()
        {
            foreach (Product update in Product.updates)
            {
                driver.Navigate().GoToUrl(update.URL);
                try
                {
                    //waits till the title elemenet loads in. At this point everything we need is open on the site
                    wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//span [@id = 'productTitle']")));
                }
                //If the page doesnt exist, mark the post as (TO DELETE) by setting the price to -1. This price is impossible so itll be deleted
                catch (OpenQA.Selenium.WebDriverTimeoutException e)
                {
                    //usually if the dog page opens, it means the product isnt there anymore
                    if (driver.FindElements(By.XPath("//img[@alt = 'Dogs of Amazon']")).Count > 0)
                    {
                        update.Price = -1;
                        update.Xprice = -1;
                        update.change = true;
                        continue;
                    }
                    else
                    {
                        throw;
                    }
                }

                //Grab Price. Many times amazon uses different elements for the price so check for all possibilities
                if (driver.FindElements(By.XPath("//span [@id = 'priceblock_ourprice']")).Count > 0)
                {
                    update.Price = Formatting.ElementToInt(driver, "//span [@id = 'priceblock_ourprice']");
                }
                else if (driver.FindElements(By.XPath("//span [@id = 'priceblock_saleprice']")).Count > 0)
                {
                    update.Price = Formatting.ElementToInt(driver, "//span [@id = 'priceblock_saleprice']");
                }
                else if (driver.FindElements(By.XPath("//span [@id = 'priceblock_dealprice']")).Count > 0)
                {
                    update.Price = Formatting.ElementToInt(driver, "//span [@id = 'priceblock_dealprice']");
                }
                else
                {
                    update.Price = -1;
                    update.change = true;
                    continue;
                }

                //Grab Xprice
                if (update.Price != -1 && driver.FindElements(By.XPath("//span [@class = 'priceBlockStrikePriceString a-text-strike']")).Count > 0)
                {
                    update.Xprice = Formatting.ElementToInt(driver, "//span [@class = 'priceBlockStrikePriceString a-text-strike']");
                }
                else
                {
                    update.Xprice = -1;
                    update.change = true;
                }
            }
        }

        /// <summary>
        /// Sends Selenium to the Posts page in the admin wordpress site
        /// </summary>
        /// <param name="driver"></param>
        public static void GoToPosts(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            String[] creds = Formatting.getCreds();
            driver.Navigate().GoToUrl("https://zed.exioite.com/wp-login.php");
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//input[@id='user_login']")));

            IWebElement username = driver.FindElement(By.XPath("//input[@id='user_login']"));
            IWebElement password = driver.FindElement(By.XPath("//input[@id='user_pass']"));

            Thread.Sleep(500);
            username.Clear();
            username.SendKeys(creds[0]);
            Thread.Sleep(500);
            password.Clear();
            password.SendKeys(creds[1]);
            Thread.Sleep(500);

            driver.FindElement(By.XPath("//input[@id='wp-submit']")).Click();
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//div [@class = 'wp-menu-name' and contains(text(),'Posts')]")));

            driver.FindElement(By.XPath("//div [@class = 'wp-menu-name' and contains(text(),'Posts')]")).Click();
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//a[contains(text(),'Add New')]")));

           
        }

        /// <summary>
        /// Adds the amazon link to a newly created post manually with selenium
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="URL"></param>
        /// <param name="id"></param>
        public static void AddLink(IWebDriver driver, String URL, String id)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Navigate().Refresh();
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//a [@class='row-title']")));

            bool clicked = false;
            int totalPages;
            if(driver.FindElement(By.XPath("//span [@class='total-pages']")).Text == "")
            {
                totalPages = 1;
            }
            else
            {
                totalPages = int.Parse(driver.FindElement(By.XPath("//span [@class='total-pages']")).Text);
            }
            for (int x = 1; x <= totalPages; x++)
            {
                foreach (IWebElement element in driver.FindElements(By.XPath("//td [@class = 'tags column-tags']//a")))
                {
                    if (element.Text == id)
                    {
                        element.Click();
                        clicked = true;
                        break;
                    }
                }
                if (!clicked && x!=totalPages)
                {
                    driver.FindElement(By.XPath("//a [@class = 'next-page button']")).Click();
                }
                else
                {
                    break;
                }
            }

            if(!clicked)
            {
                MessageBox.Show("Post not found (Method: Selenium.AddLink())");
            }

            driver.FindElement(By.XPath("//a [@class = 'row-title']")).Click();
            Thread.Sleep(500);

            if (driver.FindElements(By.XPath("//div [@class = 'components-guide__container']")).Count > 0)
            {
                driver.FindElement(By.XPath("//button [@aria-label = 'Close dialog']")).Click();
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//button [@data-label = 'Post']")));
            }

            //driver.FindElement(By.XPath("//button [@data-label = 'Document']")).Click();
            //wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//button [contains(text(),'Page Links To')]")));
            Thread.Sleep(1000);

            if (driver.FindElements(By.XPath("//label [contains(text(),'A custom URL')]")).Count == 0)
            {
                driver.FindElement(By.XPath("//button [contains(text(),'Page Links To')]")).Click();
            }

            driver.FindElement(By.XPath("//label [contains(text(),'A custom URL')]")).Click();
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("// input  [@class = 'components-text-control__input']")));

            driver.FindElement(By.XPath("// input  [@class = 'components-text-control__input']")).Clear();

            Thread.Sleep(500);
            driver.FindElement(By.XPath("// input  [@class = 'components-text-control__input']")).SendKeys(URL);
            Thread.Sleep(500);

            driver.FindElement(By.XPath("//input [@data-testid = 'plt-newtab']")).Click();

            driver.FindElement(By.XPath("//button [contains(text(),'Update')]")).Click();
            WaitForPageLoad(wait);

            driver.FindElement(By.XPath("//a [@aria-label = 'View Posts']")).Click();

            try
            {
                driver.SwitchTo().Alert().Accept();
            }
            catch (Exception)
            {

            }

        }

        /// <summary>
        /// Waits for the page to load
        /// </summary>
        /// <param name="wait"></param>
        public static void WaitForPageLoad(WebDriverWait wait)
        {
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        //public static bool checkProducts(IWebDriver driver, Product product, Product update)
        //{
        //    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        //    driver.Navigate().GoToUrl(update.URL);
        //    wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//span [@id = 'productTitle']")));

        //    //Grab Price
        //    if (driver.FindElements(By.XPath("//span [@id = 'priceblock_ourprice']")).Count > 0)
        //    {
        //        update.Price = Formatting.ElementToInt(driver, "//span [@id = 'priceblock_ourprice']");
        //    }
        //    else if (driver.FindElements(By.XPath("//span [@id = 'priceblock_saleprice']")).Count > 0)
        //    {
        //        update.Price = Formatting.ElementToInt(driver, "//span [@id = 'priceblock_saleprice']");
        //    }
        //    else if (driver.FindElements(By.XPath("//span [@id = 'priceblock_dealprice']")).Count > 0)
        //    {
        //        update.Price = Formatting.ElementToInt(driver, "//span [@id = 'priceblock_dealprice']");
        //    }

        //    //Grab Xprice
        //    if (driver.FindElements(By.XPath("//span [@class = 'priceBlockStrikePriceString a-text-strike']")).Count > 0)
        //    {
        //        update.Xprice = Formatting.ElementToInt(driver, "//span [@class = 'priceBlockStrikePriceString a-text-strike']");
        //    }
        //}
    }
}
