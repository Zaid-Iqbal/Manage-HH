using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Manage_HH
{
	class Search
	{
		public static void searchAllinOne(IWebDriver driver, WebDriverWait wait)
		{
			//Goto Desktop page 1
			driver.Navigate().GoToUrl("https://amazon.com");
			wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));
			wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

			//Search Electronics
			driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
			driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
			WaitForPageLoad(wait);

			//retry Search Electronics
			if (driver.FindElements(By.XPath("//img [@alt = 'Computers & Accessories']")).Count() == 0)
			{
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

				driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
				driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")));

				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				WaitForPageLoad(wait);
			}

			//Computers and Accessories button
			if (driver.FindElements(By.XPath("//a [@aria-label = '###**Desktops**']")).Count() == 0)
			{
				//if page not correct
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")));

				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//a [@aria-label = '###**Desktops**']")));

				driver.FindElement(By.XPath("//a [@aria-label = '###**Desktops**']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				//Click on Computers and Accessories button
				driver.FindElement(By.XPath("//a [@aria-label = '###**Desktops**']")).Click();
				WaitForPageLoad(wait);
			}

			driver.FindElement(By.XPath("//a [@title = 'All-in-Ones']")).Click();
		}
		public static void searchTower(IWebDriver driver, WebDriverWait wait)
		{
			//Goto Desktop page 1
			driver.Navigate().GoToUrl("https://amazon.com");
			wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

			//Search Electronics
			driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
			driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
			WaitForPageLoad(wait);

			//retry Search Electronics
			if (driver.FindElements(By.XPath("//img [@alt = 'Computers & Accessories']")).Count() == 0)
			{
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

				driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
				driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")));

				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				WaitForPageLoad(wait);
			}

			//Computers and Accessories button
			if (driver.FindElements(By.XPath("//a [@aria-label = '###**Desktops**']")).Count() == 0)
			{
				//if page not correct
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")));

				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//a [@aria-label = '###**Desktops**']")));

				driver.FindElement(By.XPath("//a [@aria-label = '###**Desktops**']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				//Click on Computers and Accessories button
				driver.FindElement(By.XPath("//a [@aria-label = '###**Desktops**']")).Click();
				WaitForPageLoad(wait);
			}

			driver.FindElement(By.XPath("//a [@title = 'Towers']")).Click();
		}
		public static void searchPhones(IWebDriver driver, WebDriverWait wait)
		{
			//Goto Phones page 1
			driver.Navigate().GoToUrl("https://amazon.com");
			wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=mobile']")));

			//Search Electronics
			driver.FindElement(By.XPath("//option [@value = 'search-alias=mobile']")).Click();
			driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
			WaitForPageLoad(wait);

			//retry Search Phones
			if (driver.FindElements(By.XPath("//a [@title = 'Cell Phones']")).Count() == 0)
			{
				//driver.Navigate().Back();
				//wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=mobile']")));


				//driver.FindElement(By.XPath("//option [@value = 'search-alias=mobile']")).Click(); ;
				//driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
				//wait.Until(element => element.FindElement(By.XPath("//a [@title = 'Cell Phones']")));

				driver.FindElement(By.XPath("//a[contains(text(),'Cell Phones')]")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				driver.FindElement(By.XPath("//a [@title = 'Cell Phones']")).Click();
				WaitForPageLoad(wait);
			}

			if (driver.FindElements(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Count() == 0)
			{
				//if page not correct
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//a [@title = 'Cell Phones']")));

				driver.FindElement(By.XPath("//a [@title = 'Cell Phones']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")));

				driver.FindElement(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				//Click on Phones button
				driver.FindElement(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Click();
				WaitForPageLoad(wait);
			}


		}

		public static void searchBluetoothBuds(IWebDriver driver, WebDriverWait wait)
		{
			//Goto PCGaming page 1
			driver.Navigate().GoToUrl("https://amazon.com");
			wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

			//Search Electronics
			driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click();
			driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
			WaitForPageLoad(wait);

			//retry Search Headphones
			if (driver.FindElements(By.XPath("//img [@alt = 'Headphones']")).Count() == 0)
			{
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));


				driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
				driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Headphones']")));

				driver.FindElement(By.XPath("//img [@alt = 'Headphones']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				driver.FindElement(By.XPath("//img [@alt = 'Headphones']")).Click();
				WaitForPageLoad(wait);
			}

			//retry Search OverEar
			if (driver.FindElements(By.XPath("//img [@alt = 'In Ear Earbuds']")).Count() == 0)
			{
				driver.Navigate().Back();
				driver.FindElement(By.XPath("//img [@alt = 'Headphones']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'In Ear Earbuds']")));


				driver.FindElement(By.XPath("//img [@alt = 'In Ear Earbuds']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				driver.FindElement(By.XPath("//img [@alt = 'In Ear Earbuds']")).Click();
				WaitForPageLoad(wait);
			}

			//Switcheroo featured and price low to high pages to get amazon page format correct and readable
			if (driver.FindElements(By.XPath("//select [@class = 'a-spacing-top-mini']")).Count() > 0)
			{
				driver.FindElement(By.XPath("//select [@class = 'a-spacing-top-mini']")).Click();
				driver.FindElement(By.XPath("//option [@value = 'price-asc-rank']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//span [@class = 'a-dropdown-prompt']")));


				driver.FindElement(By.XPath("//span [@class = 'a-dropdown-prompt']")).Click();
				driver.FindElement(By.XPath("//option [@value = 'featured-rank']")).Click();
				WaitForPageLoad(wait);
			}

		}

		public static void searchHeadphones(IWebDriver driver, WebDriverWait wait)
		{
			//Goto PCGaming page 1
			driver.Navigate().GoToUrl("https://amazon.com");
			wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

			//Search Electronics
			driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
			driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
			WaitForPageLoad(wait);


			//retry Search Headphones
			if (driver.FindElements(By.XPath("//img [@alt = 'Headphones']")).Count() == 0)
			{
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

				driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
				driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Headphones']")));

				driver.FindElement(By.XPath("//img [@alt = 'Headphones']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				driver.FindElement(By.XPath("//img [@alt = 'Headphones']")).Click();
				WaitForPageLoad(wait);
			}

			//retry Search OverEar
			if (driver.FindElements(By.XPath("//img [@alt = 'Over Ear Headphones']")).Count() == 0)
			{
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Headphones']")));


				driver.FindElement(By.XPath("//img [@alt = 'Headphones']")).Click();
				WaitForPageLoad(wait);

				driver.FindElement(By.XPath("//img [@alt = 'Over Ear Headphones']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				driver.FindElement(By.XPath("//img [@alt = 'Over Ear Headphones']")).Click();
				WaitForPageLoad(wait);
			}

			//Switcheroo featured and price low to high pages to get amazon page format correct and readable
			if (driver.FindElements(By.XPath("//select [@class = 'a-spacing-top-mini']")).Count() > 0)
			{
				driver.FindElement(By.XPath("//select [@class = 'a-spacing-top-mini']")).Click();
				driver.FindElement(By.XPath("//option [@value = 'price-asc-rank']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//span [@class = 'a-dropdown-prompt']")));


				driver.FindElement(By.XPath("//span [@class = 'a-dropdown-prompt']")).Click();
				driver.FindElement(By.XPath("//option [@value = 'featured-rank']")).Click();
				WaitForPageLoad(wait);
			}
		}


		public static void searchBluetoothSpeakers(IWebDriver driver, WebDriverWait wait)
		{
			//Goto PCGaming page 1
			driver.Navigate().GoToUrl("https://amazon.com");
			wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));


			//Search Electronics
			driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
			driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
			WaitForPageLoad(wait);

			//retry Search Electronics
			if (driver.FindElements(By.XPath("//img [@alt = 'Home Audio']")).Count() == 0)
			{
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

				driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
				driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Home Audio']")));

				driver.FindElement(By.XPath("//img [@alt = 'Home Audio']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				driver.FindElement(By.XPath("//img [@alt = 'Home Audio']")).Click();
				WaitForPageLoad(wait);
			}

			//retry Search Wireless audio
			if (driver.FindElements(By.XPath("//img [@alt = 'Wireless Audio']")).Count() == 0)
			{
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Home Audio']")));

				driver.FindElement(By.XPath("//img [@alt = 'Home Audio']")).Click(); ;
				WaitForPageLoad(wait);

				driver.FindElement(By.XPath("//img [@alt = 'Wireless Audio']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				driver.FindElement(By.XPath("//img [@alt = 'Wireless Audio']")).Click();
				WaitForPageLoad(wait);
			}

			//Try toprated
			if (driver.FindElements(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Count() == 0)
			{
				//if page not correct
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Wireless Audio']")));

				driver.FindElement(By.XPath("//img [@alt = 'Wireless Audio']")).Click();
				WaitForPageLoad(wait);

				try
				{
					driver.FindElement(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Click();
					WaitForPageLoad(wait);
				}
				catch (Exception)
				{
					//Switcheroo featured and price low to high pages to get amazon page format correct and readable
					if (driver.FindElements(By.XPath("//select [@class = 'a-spacing-top-mini']")).Count() > 0)
					{
						driver.FindElement(By.XPath("//select [@class = 'a-spacing-top-mini']")).Click();
						driver.FindElement(By.XPath("//option [@value = 'price-asc-rank']")).Click();
						WaitForPageLoad(wait);

						driver.FindElement(By.XPath("//span [@class = 'a-dropdown-prompt']")).Click();
						driver.FindElement(By.XPath("//option [@value = 'featured-rank']")).Click();
						WaitForPageLoad(wait);
					}

				}
			}
			else
			{
				driver.FindElement(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Click();
				WaitForPageLoad(wait);
			}
		}


		public static void searchCellAccessories(IWebDriver driver, WebDriverWait wait)
		{
			//Goto PCGaming page 1
			driver.Navigate().GoToUrl("https://amazon.com");
			wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

			//Search Electronics
			driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
			driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
			WaitForPageLoad(wait);

			//retry Search Electronics
			if (driver.FindElements(By.XPath("//img [@alt = 'Cell Phones & Accessories']")).Count() == 0)
			{
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

				driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
				driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Cell Phones & Accessories']")));

				driver.FindElement(By.XPath("//img [@alt = 'Cell Phones & Accessories']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				try
				{
					driver.FindElement(By.XPath("//img [@alt = 'Cell Phones & Accessories']")).Click();
					WaitForPageLoad(wait);
				}
				catch (ElementClickInterceptedException)
				{
					Thread.Sleep(1000);
					driver.FindElement(By.XPath("//img [@alt = 'Cell Phones & Accessories']")).Click();
					WaitForPageLoad(wait);
				}

			}

			//Try toprated
			if (driver.FindElements(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Count() == 0)
			{
				//if page not correct
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Cell Phones & Accessories']")));

				driver.FindElement(By.XPath("//img [@alt = 'Cell Phones & Accessories']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")));

				driver.FindElement(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				//Click on Cell Accessories button
				driver.FindElement(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Click();
				WaitForPageLoad(wait);
			}
		}


		public static void searchTV(IWebDriver driver, WebDriverWait wait)
		{
			//Goto PCGaming page 1
			driver.Navigate().GoToUrl("https://amazon.com");
			wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

			//Search Electronics
			driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
			driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
			WaitForPageLoad(wait);

			//retry Search Electronics
			if (driver.FindElements(By.XPath("//img [@alt = 'TV & Video']")).Count() == 0)
			{
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

				driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
				driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'TV & Video']")));

				driver.FindElement(By.XPath("//img [@alt = 'TV & Video']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				driver.FindElement(By.XPath("//img [@alt = 'TV & Video']")).Click();
				WaitForPageLoad(wait);
			}

			//Try toprated
			if (driver.FindElements(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Count() == 0)
			{
				//if page not correct
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'TV & Video']")));

				driver.FindElement(By.XPath("//img [@alt = 'TV & Video']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")));

				driver.FindElement(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				//Click on Computers and Accessories button
				driver.FindElement(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Click();
				WaitForPageLoad(wait);
			}
		}


		public static void searchStorage(IWebDriver driver, WebDriverWait wait)
		{
			//Goto PCGaming page 1
			driver.Navigate().GoToUrl("https://amazon.com");
			wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

			//Search Electronics
			driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
			driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
			WaitForPageLoad(wait);

			//retry Search Electronics
			if (driver.FindElements(By.XPath("//img [@alt = 'Computers & Accessories']")).Count() == 0)
			{
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

				driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
				driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")));

				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				WaitForPageLoad(wait);
			}

			//Computers and Accessories button
			if (driver.FindElements(By.XPath("//a [@aria-label = '###**Storage &amp; Hard Drives**']")).Count() == 0)
			{
				//if page not correct
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")));

				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//a [@aria-label = '###**Storage &amp; Hard Drives**']")));

				driver.FindElement(By.XPath("//a [@aria-label = '###**Storage &amp; Hard Drives**']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				//Click on Computers and Accessories button
				driver.FindElement(By.XPath("//a [@aria-label = '###**Storage &amp; Hard Drives**']")).Click();
				WaitForPageLoad(wait);
			}

			//Try toprated
			if (driver.FindElements(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Count() == 0)
			{
				//if page not correct
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//a [@aria-label = '###**Storage &amp; Hard Drives**']")));

				driver.FindElement(By.XPath("//a [@aria-label = '###**Storage &amp; Hard Drives**']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")));

				driver.FindElement(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				//Click on Computers and Accessories button
				driver.FindElement(By.XPath("//span[contains(text(),'See more')]")).Click();
				WaitForPageLoad(wait);
			}
		}


		public static void searchComputerComponents(IWebDriver driver, WebDriverWait wait)
		{
			//Goto PCGaming page 1
			driver.Navigate().GoToUrl("https://amazon.com");
			wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

			//Search Electronics
			driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
			driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
			WaitForPageLoad(wait);

			//retry Search Electronics
			if (driver.FindElements(By.XPath("//img [@alt = 'Computers & Accessories']")).Count() == 0)
			{
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

				driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
				driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Computers & Accessories'")));

				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				WaitForPageLoad(wait);
			}

			//Computers and Accessories button
			if (driver.FindElements(By.XPath("//a [@aria-label = '###**Computer Components**']")).Count() == 0)
			{
				//if page not correct
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")));

				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//a [@aria-label = '###**Computer Components**']")));

				driver.FindElement(By.XPath("//a [@aria-label = '###**Computer Components**']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				//Click on Computers and Components button
				try
				{
					driver.FindElement(By.XPath("//a [@aria-label = '###**Computer Components**']")).Click();
					WaitForPageLoad(wait);
				}
				catch (ElementClickInterceptedException)
				{
					Thread.Sleep(1000);
					driver.FindElement(By.XPath("//a [@aria-label = '###**Computer Components**']")).Click();
					WaitForPageLoad(wait);
				}

			}

			//Try toprated
			if (driver.FindElements(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Count() == 0)
			{
				//if page not correct
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//a [@aria-label = '###**Computer Components**']")));

				driver.FindElement(By.XPath("//a [@aria-label = '###**Computer Components**']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")));

				driver.FindElement(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				//Click on Computers and Accessories button
				driver.FindElement(By.XPath("//span[contains(text(),'See more')]")).Click();
				WaitForPageLoad(wait);
			}
		}


		public static void searchNetworking(IWebDriver driver, WebDriverWait wait)
		{
			//Goto PCGaming page 1
			driver.Navigate().GoToUrl("https://amazon.com");
			wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

			//Search Electronics
			driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
			driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
			WaitForPageLoad(wait);

			//retry Search Electronics
			if (driver.FindElements(By.XPath("//img [@alt = 'Computers & Accessories']")).Count() == 0)
			{
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

				driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
				driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")));

				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				WaitForPageLoad(wait);
			}

			//Computers and Accessories button
			if (driver.FindElements(By.XPath("//a [@aria-label = '###**Networking**']")).Count() == 0)
			{
				//if page not correct
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")));

				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//a [@aria-label = '###**Networking**']")));

				driver.FindElement(By.XPath("//a [@aria-label = '###**Networking**']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				//Click on Computers and Accessories button
				driver.FindElement(By.XPath("//a [@aria-label = '###**Networking**']")).Click();
				WaitForPageLoad(wait);
			}

			//Try toprated
			if (driver.FindElements(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Count() == 0)
			{
				//if page not correct
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//a [@aria-label = '###**Networking**']")));

				driver.FindElement(By.XPath("//a [@aria-label = '###**Networking**']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//div[@class = 'a-section a-spacing-none']")));

				if (driver.FindElements(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Count() > 0)
				{
					driver.FindElement(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Click();
				}
				else if (driver.FindElements(By.XPath("//span[contains(text(),'See all results')]")).Count() > 0)
				{
					driver.FindElement(By.XPath("//span[contains(text(),'See all results')]")).Click();
				}
				else
				{
					throw new ElementNotVisibleException();
				}
				WaitForPageLoad(wait);
			}
			else
			{
				//Click on Computers and Accessories button
				driver.FindElement(By.XPath("//span[contains(text(),'See more')]")).Click();
				WaitForPageLoad(wait);
			}
		}


		public static void searchComputerAccessories(IWebDriver driver, WebDriverWait wait)
		{
			//Goto PCGaming page 1
			driver.Navigate().GoToUrl("https://amazon.com");
			wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

			//Search Electronics
			driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
			driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
			WaitForPageLoad(wait);

			//retry Search Electronics
			if (driver.FindElements(By.XPath("//img [@alt = 'Computers & Accessories']")).Count() == 0)
			{
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

				driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
				driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")));

				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				WaitForPageLoad(wait);
			}

			//Computers and Accessories button
			if (driver.FindElements(By.XPath("//a [@aria-label = '###**Computer Accessories**']")).Count() == 0)
			{
				//if page not correct
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")));

				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//a [@aria-label = '###**Computer Accessories**']")));

				driver.FindElement(By.XPath("//a [@aria-label = '###**Computer Accessories**']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				//Click on Computers and Accessories button
				driver.FindElement(By.XPath("//a [@aria-label = '###**Computer Accessories**']")).Click();
				WaitForPageLoad(wait);
			}

			//Try toprated
			if (driver.FindElements(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Count() == 0)
			{
				//if page not correct
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//a [@aria-label = '###**Computer Accessories**']")));

				driver.FindElement(By.XPath("//a [@aria-label = '###**Computer Accessories**']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")));

				driver.FindElement(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				//Click on Computers and Accessories button
				driver.FindElement(By.XPath("//span[contains(text(),'See more')]")).Click();
				WaitForPageLoad(wait);
			}
		}


		public static void searchTablets(IWebDriver driver, WebDriverWait wait)
		{
			//Goto PCGaming page 1
			driver.Navigate().GoToUrl("https://amazon.com");
			wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

			//Search Electronics
			driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
			driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
			WaitForPageLoad(wait);

			//retry Search Electronics
			if (driver.FindElements(By.XPath("//img [@alt = 'Computers & Accessories']")).Count() == 0)
			{
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

				driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
				driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")));

				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				WaitForPageLoad(wait);
			}

			//Computers and Accessories button
			if (driver.FindElements(By.XPath("//a [@aria-label = '###**Tablets**']")).Count() == 0)
			{
				//if page not correct
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")));

				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//a [@aria-label = '###**Tablets**']")));

				driver.FindElement(By.XPath("//a [@aria-label = '###**Tablets**']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				//Click on Computers and Accessories button
				driver.FindElement(By.XPath("//a [@aria-label = '###**Tablets**']")).Click();
				WaitForPageLoad(wait);
			}

			//Try toprated
			if (driver.FindElements(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Count() == 0)
			{
				//if page not correct
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//a [@aria-label = '###**Tablets**']")));

				driver.FindElement(By.XPath("//a [@aria-label = '###**Tablets**']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")));

				driver.FindElement(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				//Click on Computers and Accessories button
				driver.FindElement(By.XPath("//span[contains(text(),'See more')]")).Click();
				WaitForPageLoad(wait);
			}
		}


		public static bool searchMonitors(IWebDriver driver, WebDriverWait wait)
		{
			//Goto  page 1
			driver.Navigate().GoToUrl("https://amazon.com");
			wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

			//Search 
			driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
			driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
			WaitForPageLoad(wait);

			//retry Search 
			if (driver.FindElements(By.XPath("//img [@alt = 'Computers & Accessories']")).Count() == 0)
			{
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

				driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
				driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")));

				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				WaitForPageLoad(wait);
			}

			//Computers and Accessories button
			if (driver.FindElements(By.XPath("//a [@aria-label = '###**Monitors**']")).Count() == 0)
			{
				//if page not correct
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")));

				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//a [@aria-label = '###**Monitors**']")));

				driver.FindElement(By.XPath("//a [@aria-label = '###**Monitors**']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				//Click on Computers and Accessories button
				driver.FindElement(By.XPath("//a [@aria-label = '###**Monitors**']")).Click();
				WaitForPageLoad(wait);
			}

			//Try toprated
			if (driver.FindElements(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Count() == 0)
			{
				//if page not correct
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//a [@aria-label = '###**Monitors**']")));

				driver.FindElement(By.XPath("//a [@aria-label = '###**Monitors**']")).Click();
				WaitForPageLoad(wait);

				if (driver.FindElements(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Count > 0)
				{
					driver.FindElement(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Click();
				}
				else
				{
					return false;
				}
				WaitForPageLoad(wait);
			}
			else
			{
				//Click on Computers and Accessories button
				try
				{
					driver.FindElement(By.XPath("//span[contains(text(),'See more')]")).Click();
					WaitForPageLoad(wait);
				}
				catch (ElementClickInterceptedException)
				{
					WaitForPageLoad(wait);
					driver.FindElement(By.XPath("//span[contains(text(),'See more')]")).Click();
					WaitForPageLoad(wait);
				}
			}
			return true;
		}


		public static void searchPCGaming(IWebDriver driver, WebDriverWait wait)
		{
			//Goto PCGaming page 1
			driver.Navigate().GoToUrl("https://amazon.com");
			wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

			//Search Electronics
			driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
			driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
			WaitForPageLoad(wait);

			//retry Search Electronics
			if (driver.FindElements(By.XPath("//img [@alt = 'Computers & Accessories']")).Count() == 0)
			{
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

				driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
				driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")));

				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				WaitForPageLoad(wait);
			}

			//Computers and Accessories button
			if (driver.FindElements(By.XPath("//a [@aria-label = '###**PC Gaming**']")).Count() == 0)
			{
				//if page not correct
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")));

				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//a [@aria-label = '###**PC Gaming**']")));

				driver.FindElement(By.XPath("//a [@aria-label = '###**PC Gaming**']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				//Click on Computers and Accessories button
				driver.FindElement(By.XPath("//a [@aria-label = '###**PC Gaming**']")).Click();
				WaitForPageLoad(wait);
			}

			//Try toprated
			if (driver.FindElements(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Count() == 0)
			{
				//if page not correct
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//a [@aria-label = '###**PC Gaming**']")));

				driver.FindElement(By.XPath("//a [@aria-label = '###**PC Gaming**']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")));

				driver.FindElement(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				//Click on Computers and Accessories button
				driver.FindElement(By.XPath("//span[contains(text(),'See more')]")).Click();
				WaitForPageLoad(wait);
			}
		}


		public static bool searchDesktop(IWebDriver driver, WebDriverWait wait)
		{
			//Goto Desktop page 1
			driver.Navigate().GoToUrl("https://amazon.com");
			wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

			//Search Electronics
			driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
			driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
			WaitForPageLoad(wait);

			//retry Search Electronics
			if (driver.FindElements(By.XPath("//img [@alt = 'Computers & Accessories']")).Count() == 0)
			{
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

				driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
				driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")));

				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				WaitForPageLoad(wait);
			}

			//Computers and Accessories button
			if (driver.FindElements(By.XPath("//a [@aria-label = '###**Desktops**']")).Count() == 0)
			{
				//if page not correct
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")));

				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//a [@aria-label = '###**Desktops**']")));

				driver.FindElement(By.XPath("//a [@aria-label = '###**Desktops**']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				try
				{
					//Click on Computers and Accessories button
					driver.FindElement(By.XPath("//a [@aria-label = '###**Desktops**']")).Click();
					WaitForPageLoad(wait);
				}
				catch (ElementClickInterceptedException)
				{
					Thread.Sleep(1000);
					//Click on Computers and Accessories button
					driver.FindElement(By.XPath("//a [@aria-label = '###**Desktops**']")).Click();
					WaitForPageLoad(wait);
				}

			}

			//Try toprated
			if (driver.FindElements(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Count() == 0)
			{
				//if page not correct
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//a [@aria-label = '###**Desktops**']")));

				driver.FindElement(By.XPath("//a [@aria-label = '###**Desktops**']")).Click();
				WaitForPageLoad(wait);

				if (driver.FindElements(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Count() == 0)
				{
					return false;
				}

				driver.FindElement(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				//Click on Computers and Accessories button
				driver.FindElement(By.XPath("//span[contains(text(),'See more')]")).Click();
				WaitForPageLoad(wait);
			}
			return true;
		}


		public static void searchLaptop(IWebDriver driver, WebDriverWait wait)
		{
			//Goto Desktop page 1
			driver.Navigate().GoToUrl("https://amazon.com");
			wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

			//Search Electronics
			driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
			driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
			WaitForPageLoad(wait);

			//retry Search Electronics
			if (driver.FindElements(By.XPath("//img [@alt = 'Computers & Accessories']")).Count() == 0)
			{
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")));

				driver.FindElement(By.XPath("//option [@value = 'search-alias=electronics']")).Click(); ;
				driver.FindElement(By.XPath("//input [@type = 'submit']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")));

				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				WaitForPageLoad(wait);
			}

			//Computers and Accessories button
			if (driver.FindElements(By.XPath("//a [@aria-label = '###**Laptops**']")).Count() == 0)
			{
				//if page not correct
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")));

				driver.FindElement(By.XPath("//img [@alt = 'Computers & Accessories']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//a [@aria-label = '###**Laptops**']")));

				driver.FindElement(By.XPath("//a [@aria-label = '###**Laptops**']")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				//Click on Computers and Accessories button
				driver.FindElement(By.XPath("//a [@aria-label = '###**Laptops**']")).Click();
				WaitForPageLoad(wait);

			}

			//Try toprated
			if (driver.FindElements(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Count() == 0)
			{
				//if page not correct
				driver.Navigate().Back();
				wait.Until(element => element.FindElement(By.XPath("//a [@aria-label = '###**Laptops**']")));

				driver.FindElement(By.XPath("//a [@aria-label = '###**Laptops**']")).Click();
				wait.Until(element => element.FindElement(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")));

				driver.FindElement(By.XPath("//span[contains(text(),'Top rated')]//parent::div[@class = 'a-section octopus-pc-card-title']//a[@class = 'a-link-normal']//span[contains(text(),'See more')]")).Click();
				WaitForPageLoad(wait);
			}
			else
			{
				//Click on Computers and Accessories button
				driver.FindElement(By.XPath("//span[contains(text(),'See more')]")).Click();
				WaitForPageLoad(wait);
			}
		}

		public static void WaitForPageLoad(WebDriverWait wait)
		{
			wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
		}

	}
}
