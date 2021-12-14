using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        //XPath strings to find elements for each amazon deal
        //Page = xpath for finding something on amazon page browsing
        //Product = xpath for finding something on actual amazon product selected
        public static String PagePriceXpath = "//span[@class='a-price a-text-price']/parent::a/span[not(@data-a-strike= 'true')]/span[@class]";
        public static String PageXpriceXpath = "//span[@data-a-strike='true']/span[@class]";
        public static String PageNameXpath = "//span[@class='a-price a-text-price']/parent::a/parent::div/parent::div/parent::div/div/h2/a/span";
        public static String PageURLXpath = "//span[@class='a-price a-text-price']/parent::a";
        public static String ProductPriceXpath = "//span [@class = 'a-price a-text-price a-size-medium apexPriceToPay']/span";
        public static String ProductXpriceXpath = "//span[@data-a-strike='true']/span";
        public static String ProductNameXpath = "//span [@id = 'productTitle']";
        public static String ProductPicURLXpath = "//div [@id = 'imgTagWrapperId']/img";
        public static String[] ProductASINXpath = { "//div [@id= 'productDetails_db_sections']//td",
                                                    "//th [contains(text(),'ASIN')]//following-sibling::td"};

        public static ChromeOptions options = new ChromeOptions();
        //public static List<IWebDriver> drivers = new ChromeDriver(Selenium.getOptions(options));
        //public static IWebDriver[] drivers =
        //{
        //    new ChromeDriver(Selenium.getOptions(options)),
        //    new ChromeDriver(Selenium.getOptions(options)),
        //    new ChromeDriver(Selenium.getOptions(options))
        //};
        public static IWebDriver[] drivers =
        {
            new ChromeDriver(),
            new ChromeDriver(),
            new ChromeDriver()
        };
        public static IWebDriver driver = new ChromeDriver(Selenium.getOptions(options));

        public static WebDriverWait[] waits =
        {
            new WebDriverWait(drivers[0], TimeSpan.FromSeconds(5)),
            new WebDriverWait(drivers[1], TimeSpan.FromSeconds(5)),
            new WebDriverWait(drivers[2], TimeSpan.FromSeconds(5))
        };
        public static WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

        public static ChromeOptions getOptions(ChromeOptions options)
        {
            options.AddArguments("--disable-popup-blocking");
            return options;
        }

        /// <summary>
        /// Searches amazon based on category
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="wait"></param>
        /// <param name="category"></param>
        public static void GetInfoByCategories(IWebDriver driver, WebDriverWait wait, String[] categories)
        {
            foreach (String category in categories)
            {
                bool searched = true;
                if (category == "Laptops")
                {
                    Search.searchLaptop(driver, wait);
                }
                else if (category == "Desktops")
                {
                    searched = Search.searchDesktop(driver, wait);
                }
                else if (category == "Tower")
                {
                    Search.searchTower(driver, wait);
                }
                else if (category == "All-in-One")
                {
                    Search.searchAllinOne(driver, wait);
                }
                else if (category == "PC Gaming")
                {
                    Search.searchPCGaming(driver, wait);
                }
                else if (category == "Monitors")
                {
                    searched = Search.searchMonitors(driver, wait);
                }
                else if (category == "Tablets")
                {
                    Search.searchTablets(driver, wait);
                }
                else if (category == "Computer Accessories")
                {
                    Search.searchComputerAccessories(driver, wait);
                }
                else if (category == "Networking")
                {
                    Search.searchNetworking(driver, wait);
                }
                else if (category == "Computer Components")
                {
                    Search.searchComputerComponents(driver, wait);
                }
                else if (category == "Storage")
                {
                    Search.searchStorage(driver, wait);
                }
                else if (category == "TV & Video")
                {
                    Search.searchTV(driver, wait);
                }
                else if (category == "Cell Phones & Accessories")
                {
                    Search.searchCellAccessories(driver, wait);
                }
                else if (category == "Speakers")
                {
                    Search.searchBluetoothSpeakers(driver, wait);
                }
                else if (category == "Headphones")
                {
                    Search.searchHeadphones(driver, wait);
                }
                else if (category == "Bluetooth Earbuds")
                {
                    Search.searchBluetoothBuds(driver, wait);
                }
                else if (category == "Phones")
                {
                    Search.searchPhones(driver, wait);
                }
                else
                {
                    Console.WriteLine("Category not found");
                }

                if (!searched && category == "Monitors")
                {
                    return;
                }

                if (!searched && category == "Desktops")
                {
                    GetInfoByCategories(driver, wait, new String[] { "Tower" });
                    GetInfoByCategories(driver, wait, new String[] { "All-in-One" });
                    return;
                }

                //String listingsURL; //1
                //TODO AMAZON IS DUMB AND CANT MKE GOOD WEBSITE. There are times where you may click on page 2, but theres no page navigation bar at the bottom like in page 1 for some reason. im too lazy to account for this error so just rerun the program again. amazon hire better web devs pls
                //Grabs data for each deal and updates Products List directly
                //listingsURL = driver.Url; //1
                List<String> pageURLs = new List<String> {
                driver.FindElement(By.XPath("//a [contains(@href,'pg_2')]")).GetAttribute("href"),
                driver.FindElement(By.XPath("//a [contains(@href,'pg_3')]")).GetAttribute("href")
                };

                //page 1
                GetDataAndWriteProducts(driver, wait, GetURLs(driver, category), category);

                //page 2
                driver.Navigate().GoToUrl(pageURLs[0]);
                GetDataAndWriteProducts(driver, wait, GetURLs(driver, category), category);

                //page 3
                driver.Navigate().GoToUrl(pageURLs[0]);
                GetDataAndWriteProducts(driver, wait, GetURLs(driver, category), category);
            }
        }

        /// <summary>
        /// Gets URL's from Amazon page. If URL is approved, its name, price, and xprice are saved in an object[]
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public static List<String> GetURLs(IWebDriver driver, String category)
        {
            List<String> names = driver.FindElements(By.XPath(PageNameXpath)).Select(k => k.Text).ToList();
            List<double> prices = driver.FindElements(By.XPath(PagePriceXpath)).Select(k => Math.Round(Double.Parse(k.GetAttribute("innerText").Replace("$", "").Replace(",", "")))).ToList();
            List<double> xprices = driver.FindElements(By.XPath(PageXpriceXpath)).Select(k => Math.Round(Double.Parse(k.GetAttribute("innerText").Replace("$", "").Replace(",", "")))).ToList();
            List<String> urls = driver.FindElements(By.XPath(PageURLXpath)).Select(k => k.GetAttribute("href")).ToList();

            if(names.Count != prices.Count || names.Count != xprices.Count || names.Count != urls.Count)
            {
                //if somethign goes wrong and the lists dont match then just return the whole urls list.
                //List<String> tags = new List<String>();
                //foreach (String url in urls)
                //{
                //    if (url.Contains("/dp/"))
                //    {
                //        url.Substring(url.IndexOf("/dp/") + 4, 10);
                //    }
                //    else
                //    {
                //        url.Substring(url.IndexOf("dp%") + 5, 10);
                //    }
                //}
                //Wordpress.tags.AddRange(tags.Except(Wordpress.tags));

                //removes urls that have a asin already in tags list
                urls.RemoveAll(t => Wordpress.tags.Contains(t.Substring(t.IndexOf("/dp/") + 4, 10)) || Wordpress.tags.Contains(t.Substring(t.IndexOf("dp%") + 5, 10)));


                return urls;
            }

            restart:
            //Loop starts at one because xpath indexes starting at 1
            for (int i = 0; i < names.Count; i++)
            {
                String name = names[i];

                //remove any bad products I don't like
                //TODO have it check for key words from a file
                if (name.ToLower().Contains("mount") ||
                    name.ToLower().Contains("screen protector") ||
                    name.ToLower().Contains("gaems vanguard") ||
                    name.ToLower().Contains("toner") ||
                    name.ToLower().Contains("ethernet splitter") ||
                    name.ToLower().Contains("flash drive") ||
                    name.ToLower().Contains("antenna") ||
                    name.ToLower().Contains("projection screen") ||
                    ((category == "Bluetooth Earbuds") && name.ToLower().Contains("wired")) ||
                    name.ToLower().Contains("adapter") ||
                    name.ToLower().Contains("live stream switcher"))
                {
                    names.RemoveAt(i);
                    prices.RemoveAt(i);
                    xprices.RemoveAt(i);
                    urls.RemoveAt(i);
                    i--;
                    continue;
                }

                if (prices[i] / xprices[i] > 0.9 && xprices[i] - prices[i] <= 15)
                {
                    names.RemoveAt(i);
                    prices.RemoveAt(i);
                    xprices.RemoveAt(i);
                    urls.RemoveAt(i);
                    i--;
                    continue;
                }

            }

            //removes urls that have a asin already in tags list
            urls.RemoveAll(t => Wordpress.tags.Contains(t.Substring(t.IndexOf("/dp/") + 4, 10)) || Wordpress.tags.Contains(t.Substring(t.IndexOf("dp%") + 5, 10)));
            return urls;
        }

        /// <summary>
        /// Grabs data of products given a list of URLs
        /// products object[] = [String, String, int, int] = [URL, Name, price, xprice]
        /// Returns a tuple
        ///     Item 1 is a list of all the urls of the page that were successful
        ///     Item 2 is a list of all the skipped url's
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="wait"></param>
        /// <param name="URLs"></param>
        /// <param name="category"></param>
        public static void GetDataAndWriteProducts(IWebDriver driver, WebDriverWait wait, List<String> urls, String category)
        {
            foreach (String url in urls)
            {
                driver.Navigate().GoToUrl(url);
                try
                {
                    wait.Until(element => element.FindElement(By.XPath(ProductNameXpath)));
                }
                catch (WebDriverTimeoutException)
                {
                    //skippedURLs.Add(url);
                    Console.WriteLine("Something missing in listing. Skipped");
                    continue;
                }

                String ID = url.Contains("/dp/") ? url.Substring(url.IndexOf("/dp/") + 4, 10) : url.Substring(url.IndexOf("dp%") + 5, 10);

                ////Product ASIN is already on website
                //foreach (String xpath in ProductASINXpath)
                //{
                //    try
                //    {
                //        ID = driver.FindElement(By.XPath(xpath)).Text.Replace("&lrm;","");
                //        if (Wordpress.tags.Contains(ID))
                //        {
                //            continue;
                //        }

                //    }
                //    catch (NoSuchElementException)
                //    {
                //        //Skip product if it doesnt have ASIN
                //        //TODO find a way to implement products without an AIN
                //        continue;
                //    }
                //}

                ////ID would be blank if non of the ASIN xpaths worked
                //if (ID == "")
                //{
                //    continue;
                //}
                

                String name = driver.FindElement(By.XPath(ProductNameXpath)).Text;
                int price = 0;
                int xprice = 0;
                try
                {
                    price = Convert.ToInt32(Math.Round(Double.Parse(driver.FindElement(By.XPath(ProductPriceXpath)).GetAttribute("innerText").Replace("$", "").Replace(",", ""))));
                    xprice = Convert.ToInt32(Math.Round(Double.Parse(driver.FindElement(By.XPath(ProductXpriceXpath)).GetAttribute("innerText").Replace("$", "").Replace(",", ""))));
                }
                catch (NoSuchElementException)
                {
                    continue;
                }
                

                Product.products.Add(new Product(   name,
                                                    price,
                                                    xprice,
                                                    category,
                                                    "https://amazon.com/dp/" + ID,
                                                    ID));
                
                Wordpress.tags.Add(ID);

                //Saves Images
                WebClient web = new WebClient();
                String picURL = driver.FindElement(By.XPath(ProductPicURLXpath)).GetAttribute("data-old-hires");
                if (picURL.Length < 5 || picURL.Substring(0, 4) != "http")
                {
                    picURL = driver.FindElement(By.XPath(ProductPicURLXpath)).GetAttribute("src");
                }
                web.DownloadFile(picURL, @"C:\Users\email\Desktop\Hardware Hub\images\" + ID + ".png");
            }
        }

        /// <summary>
        /// removes duplicate elements from products list
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="wait"></param>
        //public static void removeDuplicates(IWebDriver driver, WebDriverWait wait)
        //{
        //    foreach (Product product in Product.products)
        //    {
        //        product.Change = false;
        //    }

        //    foreach (Product product1 in Product.products)
        //    {
        //        String name1;

        //        driver.Navigate().GoToUrl(product1.URL);

        //        try
        //        {
        //            wait.Until(element => element.FindElement(By.XPath("//span [@id = 'productTitle']")));
        //            name1 = driver.FindElement(By.XPath("//span [@id = 'productTitle']")).Text;
        //        }
        //        catch (Exception e)
        //        {
        //            if (e is NoSuchElementException)
        //            {
        //                name1 = "";
        //                product1.Change = true;
        //            }
        //            else if (e is WebDriverTimeoutException)
        //            {
        //                name1 = "";
        //                product1.Change = true;
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        foreach (Product product2 in Product.products)
        //        {
        //            String name2;

        //            driver.Navigate().GoToUrl(product2.URL);

        //            try
        //            {
        //                name2 = driver.FindElement(By.XPath("//span [@id = 'productTitle']")).Text;
        //            }
        //            catch (Exception e)
        //            {
        //                if (e is NoSuchElementException)
        //                {
        //                    name2 = "";
        //                    product2.Change = true;
        //                }
        //                else if (e is WebDriverTimeoutException)
        //                {
        //                    name2 = "";
        //                    product2.Change = true;
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }

        //            if (name1 == name2)
        //            {
        //                product2.Change = true;
        //            }
        //        }
        //    }

        //    restart:
        //    foreach (Product product in Product.products)
        //    {
        //        if (product.Change)
        //        {
        //            Product.products.Remove(product);
        //            Wordpress.RemoveProduct(product).Wait();
        //            goto restart;
        //        }
        //    }
        //}

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
                    if (element.Text == tag)
                    {
                        return driver.FindElement(By.XPath("(//span [@class = 'url'])[" + (elements.IndexOf(element) + 1).ToString() + "]")).GetAttribute("innerHTML");
                    }
                }
                if (x != totalPages)
                {
                    driver.FindElement(By.XPath("//a [@class = 'next-page button']")).Click();
                    wait.Until(element => element.FindElement(By.XPath("//td [@id = 'cb']")));
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
        public static void GetPostData(IWebDriver driver, WebDriverWait wait, List<Product> products)
        {
            foreach (Product product in products)
            {
                driver.Navigate().GoToUrl(product.URL);

                AutoResetEvent[] evs = { new AutoResetEvent(false), new AutoResetEvent(false) };
                Thread TitleThread = new Thread(() => WaitForPageTitle(driver, wait, evs[0]));
                Thread DogThread = new Thread(() => WaitForDogPage(driver, wait, evs[1]));

                TitleThread.Start();
                DogThread.Start();

                int first = WaitHandle.WaitAny(evs);

                //remove product cuz it no longer exists
                if (first == 1)
                {
                    product.Change = 2;
                    TitleThread.Abort();
                    continue;
                }
                DogThread.Abort();


                //Grab Price. Many times amazon uses different elements for the price so check for all possibilities
                try
                {
                    int NewPrice = Convert.ToInt32(double.Parse(driver.FindElement(By.XPath(ProductPriceXpath)).GetAttribute("innerText").Replace("$", "").Replace(",", "")));
                    if (product.Price != NewPrice)
                    {
                        product.Price = NewPrice;
                        product.Change = 1;
                    }
                }
                catch (NoSuchElementException)
                {
                    product.Change = 2;
                }

                //Grab Xprice
                try
                {
                    int NewXprice = Convert.ToInt32(double.Parse(driver.FindElement(By.XPath(ProductXpriceXpath)).GetAttribute("innerText").Replace("$", "").Replace(",", "")));
                    if (product.Xprice != NewXprice)
                    {
                        product.Xprice = NewXprice;
                        product.Change = 1;
                    }
                }
                catch (NoSuchElementException)
                {
                    product.Change = 2;
                }
            }
        }

        /// <summary>
        /// Waits for the amazon page of the product to appear
        /// </summary>
        /// <param name="are"></param>
        public static void WaitForPageTitle(IWebDriver driver, WebDriverWait wait, AutoResetEvent are)
        {
            try
            {
                wait.Until(element => element.FindElement(By.XPath("//span [@id = 'productTitle']")));
                are.Set();
            }
            catch (WebDriverTimeoutException)
            {
            }
        }

        /// <summary>
        /// Waits for the Dog Amazon page to appear signaling that the product is no longer on amazon
        /// </summary>
        /// <param name="are"></param>
        public static void WaitForDogPage(IWebDriver driver, WebDriverWait wait, AutoResetEvent are)
        {
            try
            {
                wait.Until(element => element.FindElement(By.XPath("//img[@alt = 'Dogs of Amazon']")));
                are.Set();
            }
            catch (WebDriverTimeoutException)
            {
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
            wait.Until(element => element.FindElement(By.XPath("//input[@id='user_login']")));

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
            wait.Until(element => element.FindElement(By.XPath("//div [@class = 'wp-menu-name' and contains(text(),'Posts')]")));

            driver.FindElement(By.XPath("//div [@class = 'wp-menu-name' and contains(text(),'Posts')]")).Click();
            wait.Until(element => element.FindElement(By.XPath("//a[contains(text(),'Add New')]")));

           
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
            wait.Until(element => element.FindElement(By.XPath("//a [@class='row-title']")));

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
                wait.Until(element => element.FindElement(By.XPath("//button [@data-label = 'Post']")));
            }

            //driver.FindElement(By.XPath("//button [@data-label = 'Document']")).Click();
            //wait.Until(element => element.FindElement(By.XPath("//button [contains(text(),'Page Links To')]")));
            Thread.Sleep(1000);

            if (driver.FindElements(By.XPath("//label [contains(text(),'A custom URL')]")).Count == 0)
            {
                driver.FindElement(By.XPath("//button [contains(text(),'Page Links To')]")).Click();
            }

            driver.FindElement(By.XPath("//label [contains(text(),'A custom URL')]")).Click();
            wait.Until(element => element.FindElement(By.XPath("// input  [@class = 'components-text-control__input']")));

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
    }
}
