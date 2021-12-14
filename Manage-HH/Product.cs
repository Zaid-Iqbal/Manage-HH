using System;
using System.Collections.Generic;
using System.Linq;
using WordPressPCL.Models;

namespace Manage_HH
{
    class Product
    {

        public static List<Product> products = new List<Product>();
        public static List<Product> updates = new List<Product>();
        //public static Dictionary<String, Product> posts = Wordpress.ReadWebsite().Result.ToDictionary(k => k.Name);
        public static Dictionary<String, Product> posts = PostToProduct(Wordpress.SendGetPosts(Wordpress.GetPosts())).ToDictionary(k => k.Name);


        public String Name 
        {
            get;
            set;
        }
        public int Price
        {
            get;
            set;
        }
        public int Xprice
        {
            get;
            set;
        }
        public String Category
        {
            get;
            set;
        }
        public int CatID
        {
            get;
            set;
        }
        public String ID
        {
            get;
            set;
        }
        public String URL
        {
            get;
            set;
        }

        public int tagID
        {
            get;
            set;
        }

        //0 = leave it alone
        //1 = update to new value
        //2 = remove it because it no longer exists.
        public int Change
        {
            get;
            set;
        }


        //used with products.xlsx
        public Product(String name, int price, int xprice, String category, String url, String id)
        {
            Name = name;
            Price = price;
            Xprice = xprice;
            Category = category;
            ID = id;
            URL = url;
            Change = 0;
        }

        //used mostly with updates list
        public Product(String name, int category, String url, int id)
        {
            Name = name;
            CatID = category;
            tagID = id;
            URL = url;
            Change = 0;
        }

        /// <summary>
        /// Any elements in the updates list should be removed from the products list
        /// </summary>
        public static void RemoveUpdates()
        {
            restart:
            foreach (Product update in updates)
            {
                foreach (Product product in products)
                {
                    if(update.Name == product.Name || update.URL == product.URL || update.ID == product.ID)
                    {
                        products.Remove(product);
                        goto restart;
                    }

                }
            }
        }

        /// <summary>
        /// Break a list of items into chunks of a specific size
        /// </summary>
        public static IEnumerable<IEnumerable<Product>> Chunk<T>(IEnumerable<Product> source, int chunksize)
        {
            while (source.Any())
            {
                yield return source.Take(chunksize);
                source = source.Skip(chunksize);
            }
        }

        public static List<List<Product>> TripleSplitList(List<Product> products)
        {
            var list = new List<List<Product>>();
            list.Add(new List<Product>());
            list.Add(new List<Product>());
            list.Add(new List<Product>());

            int count = products.Count;

            list[0] = products.GetRange(0, count / 3);
            list[1] = products.GetRange(0, count / 3);
            list[2] = products.GetRange(0, count / 3);


            for (int i = 0; i < (count / 3); i++)
            {
                list[0].Add(products[i]);
            }

            for (int i = count / 3; i < (count * (2.0 / 3.0)); i++)
            {
                list[1].Add(products[i]);
            }

            for (int i = Convert.ToInt32(count * (2.0 / 3.0)); i < count; i++)
            {
                list[2].Add(products[i]);
            }

            return list;
        }

        public static List<List<Product>> SplitList(List<Product> products, int nSize = 30)
        {
            var list = new List<List<Product>>();

            for (int i = 0; i < products.Count; i += nSize)
            {
                list.Add(products.GetRange(i, Math.Min(nSize, products.Count - i)));
            }

            return list;
        }

        // Function to remove duplicates from an ArrayList 
        public static List<Product> RemoveDuplicates(List<Product> list)
        {
        restart:
            // Traverse through the first list 
            foreach (Product element1 in list)
            {
                foreach (Product element2 in list)
                {
                    if ((element1 != element2) && (element1.Name == element2.Name) && (element1.Price == element2.Price) && (element1.Xprice == element2.Xprice))
                    {
                        list.Remove(element2);
                        goto restart;
                    }
                }
            }
            // return the new list 
            return list;
        }

        /// <summary>
		/// Removes some unwanted products and removes single quotes so that SQL updates properly
		/// </summary>
		/// <param name="products"></param>
        /// TODO grab words from a file rather than hardcode
		public static void RemoveBadProducts()
        {
            for (int i = 0; i < Product.products.Count; i++)
            {
                Product product = Product.products[i];
                product.Name = product.Name.Replace("'", "''");
                product.URL = product.URL.Replace("'", "''");

                if (product.Xprice - product.Price <= 10)
                {
                    Product.products.Remove(product);
                    i--;
                }

                if (product.Name.ToLower().Contains("mount") ||
                    product.Name.ToLower().Contains("screen protector") ||
                    product.Name.ToLower().Contains("gaems vanguard") ||
                    product.Name.ToLower().Contains("toner") ||
                    product.Name.ToLower().Contains("ethernet splitter") ||
                    product.Name.ToLower().Contains("flash drive") ||
                    product.Name.ToLower().Contains("antenna") ||
                    product.Name.ToLower().Contains("projection screen") ||
                    ((product.Category == "Bluetooth Earbuds") && product.Name.ToLower().Contains("wired")) ||
                    product.Name.ToLower().Contains("adapter") ||
                    product.Name.ToLower().Contains("live stream switcher"))
                {
                    Product.products.Remove(product);
                    i--;
                }
            }
        }

        /// <summary>
        /// Takes a list of posts and returns a list of products
        /// </summary>
        /// <param name="posts"></param>
        /// <returns></returns>
        public static List<Product> PostToProduct(List<Post> posts)
        {
            List<Product> siteProduct = new List<Product>();
            foreach (Post post in posts)
            {
                siteProduct.Add(new Product(
                    post.Title.Rendered,
                    post.Categories[0],
                    post.Link,
                    post.Tags[0]
                    ));
            }
            siteProduct = Wordpress.FixTags(siteProduct).Result;
            return siteProduct;
        }
    }
}
