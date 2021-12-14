using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WordPressPCL.Models;

namespace Manage_HH
{
    class Formatting
    {
        public static String sample = @"< p >$50 &#8211;&gt;$28</p>\n";

        /// <summary>
        /// Remove duplicate tags added from unknown bug. My guess is that running 3 threads at once causes some products to be added twice somehow
        /// </summary>
        public static void RemoveDuplicates()
        {
            //Remove duplicate tags from unknown bug
            List<String> duplicates = Wordpress.tags.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
            foreach (String str in duplicates)
            {
                //remove tag
                Wordpress.tags.Remove(str);
                //remove product
                for (int i = 0; i < Product.products.Count; i++)
                {
                    if (Product.products[i].ID == str)
                    {
                        Product.products.RemoveAt(i);
                        break;
                    }
                }
            }

            //remove the picture on hard drive
            Wordpress.CleanImagesFolder(duplicates).Wait();

            if (Wordpress.tags.Count != Wordpress.tags.Distinct().Count())
            {
                MessageBox.Show("Error: There are still duplicates");
            }
        }

        public static String[] getCreds()
        {
            String[] lines = System.IO.File.ReadAllLines(@"C:\Users\email\Desktop\Hardware Hub\Twitter code files\Wordpress Login.txt");
            return new String[]
            {
                lines[0].Substring(lines[0].IndexOf(':') + 2), 
                lines[1].Substring(lines[1].IndexOf(':') + 2)
            };
                                          
        }

        /// <summary>
        /// Turns the Category ID (assigned from wordpress) to the string equivalent of the category
        /// </summary>
        /// <param name="CatID"></param>
        /// <returns></returns>
        public static String CatIDtoCategory(int CatID)
        {
            if (CatID == 7)
            {
                return "Audio";
            }
            else if (CatID == 9)
            {
                return "Computers and Tablets";
            }
            else if (CatID == 13)
            {
                return "Displays";
            }
            else if (CatID == 5)
            {
                return "Gaming";
            }
            else if (CatID == 10)
            {
                return "Misc";
            }
            else if (CatID == 6)
            {
                return "PC Parts";
            }
            else if (CatID == 8)
            {
                return "Phones";
            }
            else
            {
                return "Uncategorized";
            }

        }

        /// <summary>
        /// Gets the neth index of the charecter in a string, instead of just the 1st instance
        /// </summary>
        /// <param name="s">the full string</param>
        /// <param name="t">the charecter being looked for</param>
        /// <param name="n">nth instance of charecter t</param>
        /// <returns></returns>
        public static int GetNthIndex(string s, char t, int n)
        {
            int count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == t)
                {
                    count++;
                    if (count == n)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Gets the ID of the product from the filepath of its photo
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static String GetIDfromFile(String file)
        {
            int slashIndex = GetNthIndex(file, '\\', 6) + 1;
            return file.Substring(slashIndex,file.IndexOf('.')-slashIndex);
        }

        /// <summary>
        /// Gets the Xprice from the string given from wordpress. Sometimes the wordpress api adds in unecessary stuff to the string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String GetXprice(String str)
        {
            int dollar = str.IndexOf("$");
            str = str.Substring(dollar + 1);
            for (int x = 1; x < str.Length - 1; x++)
            {
                foreach (char c in str.Substring(0, x))
                {
                    if (c != '1' && c != '2' && c != '3' && c != '4' && c != '5' && c != '6' && c != '7' && c != '8' && c != '9' && c != '0')
                    {
                        return str.Substring(0, x - 1);
                    }
                }
            }
            return "not found";
        }


        /// <summary>
        /// same as above but for price
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String GetPrice(String str)
        {
            int dollar = str.IndexOf("$");
            str = str.Substring(dollar + 1);
            dollar = str.IndexOf("$");
            str = str.Substring(dollar + 1);
            for (int x = 1; x < str.Length-1; x++)
            {
                foreach (char c in str.Substring(0,x))
                {
                    if (c != '1' && c != '2' && c != '3' && c != '4' && c != '5' && c != '6' && c != '7' && c != '8' && c != '9' && c != '0')
                    {
                        return str.Substring(0,x-1);
                    }
                }
            }
            return "not found";
        }
               
        /// <summary>
        /// Gives the int equivalent of a webelement's value
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static int ElementToInt(IWebDriver driver, String xpath)
        {
            try
            {
                return (int)Math.Round(Convert.ToDouble((driver.FindElement(By.XPath(xpath)).Text).Replace("$", "")));
            }
            catch (FormatException)
            {
                return -1;
            }
        }

        /// <summary>
        /// Assigns wordpress category ID values based off string category
        /// </summary>
        public static void CorrectCategories()
        {
            foreach (Product product in Product.products)
            {
                if (product.Category == "Laptops")
                {
                    product.CatID = 9;
                }
                else if (product.Category == "Desktops")
                {
                    product.CatID = 9;
                }
                else if (product.Category == "Monitors")
                {
                    product.CatID = 13;
                }
                else if (product.Category == "Networking")
                {
                    product.CatID = 10;
                }
                else if (product.Category == "Computer Components")
                {
                    product.CatID = 6;
                }
                else if (product.Category == "Storage")
                {
                    product.CatID = 6;
                }
                else if (product.Category == "TV & Video")
                {
                    product.CatID = 13;
                }
                else if (product.Category == "Speakers")
                {
                    product.CatID = 7;
                }
                else if (product.Category == "Headphones")
                {
                    product.CatID = 7;
                }
                else if (product.Category == "Bluetooth Earbuds")
                {
                    product.CatID = 7;
                }
                else if (product.Category == "Phones")
                {
                    product.CatID = 8;
                }
                else if (product.Category == "Misc")
                {
                    product.CatID = 10;
                }
                else if (product.Category == "Audio")
                {
                    product.CatID = 7;
                }
                else if (product.Category == "Computers & Tablets")
                {
                    product.CatID = 9;
                }
                else if (product.Category == "Displays")
                {
                    product.CatID = 13;
                }
                else if (product.Category == "Gaming")
                {
                    product.CatID = 5;
                }
                else if (product.Category == "PC parts")
                {
                    product.CatID = 6;
                }
                else if (product.Category == "PC Parts")
                {
                    product.CatID = 6;
                }
                else
                {
                    product.CatID = -1;
                }
            }
        }

        /// <summary>
        /// Converts image to stream
        /// </summary>
        /// <param name="image"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static Stream ToStream(Image image, ImageFormat format)
        {
            var stream = new System.IO.MemoryStream();
            image.Save(stream, format);
            stream.Position = 0;
            return stream;
        }

        
    }
}
