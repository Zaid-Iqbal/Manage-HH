using System;
using System.Collections.Generic;


namespace Manage_HH
{
    class Product
    {

        public static List<Product> products = new List<Product>();
        public static List<Product> updates = new List<Product>();
        public static List<Product> removes = new List<Product>();

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

        public int WPID
        {
            get;
            set;
        }
        public int tagID
        {
            get;
            set;
        }
        public bool change
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
            change = false;
        }

        //used mostly with updates list
        public Product(String name, int category, String url, int id)
        {
            Name = name;
            CatID = category;
            tagID = id;
            URL = url;
            change = false;
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
    }
}
