using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WordPressPCL.Models;

namespace Manage_HH
{
    class SQL
    {
        public static String cs = System.IO.File.ReadAllLines(@"C:\Users\email\Desktop\Hardware Hub\SQL Connection String.txt")[0];
        public static SqlConnection con = new SqlConnection(cs);

        public static void WriteNewProducts()
        {
            SqlCommand cmd = new SqlCommand("TRUNCATE TABLE NewProducts", con);
            cmd.ExecuteNonQuery();
            foreach (Product product in Product.products)
            {
                cmd.CommandText = $"INSERT INTO NewProducts VALUES (N'{product.ID}', N'{product.Name.Replace("'","")}', { product.Price}, {product.Xprice}, N'{product.Category}',N'{(product.URL.Contains("'") ? "URL had ' so we threw it out":product.URL)}');";
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Grabs New Products from SQL table
        /// </summary>
        public static void ReadNewProducts()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM NewProducts", con);
            SqlDataReader Reader = cmd.ExecuteReader();

            while(Reader.Read())
            {
                Product.products.Add(new Product
                    (
                        Reader[1].ToString(),               //name
                        int.Parse(Reader[2].ToString()),    //price
                        int.Parse(Reader[3].ToString()),    //xprice
                        Reader[4].ToString(),               //category
                        Reader[5].ToString(),               //URL
                        Reader[0].ToString()                //ID
                    ));
            }
            Reader.Close();
        }

        /// <summary>
        /// Creates record of all products from website
        /// </summary>
        public static void WriteProducts()
        {
            //TODO grab all the tags at once instead of getting each one every time. too long
            SqlCommand cmd = new SqlCommand("TRUNCATE TABLE Products",con);
            cmd.ExecuteNonQuery();
            List<Post> WP = Wordpress.SendGetPosts(Wordpress.GetPosts());
            Dictionary<int,Tag> tags = Wordpress.SendGetTags(Wordpress.GetTags()).ToDictionary(p => p.Id);
            foreach (Post post in WP)
            {
                //String id = Wordpress.SendGetTag(Wordpress.GetTag(post));
                String id = tags[post.Tags[0]].Name;
                String name = post.Title.Rendered;
                int price = int.Parse(Formatting.GetPrice(post.Content.Rendered));
                int xprice = int.Parse(Formatting.GetXprice(post.Content.Rendered));
                String category = Formatting.CatIDtoCategory(post.Categories[0]);
                String url = post.Link;

                cmd.CommandText = $"INSERT INTO Products VALUES ('{id}', '{name}', {price}, {xprice}, '{category}', '{url}');";
                cmd.ExecuteNonQuery();
            }
        }

        public static void WriteNewProducts(List<Product> products)
        {
            //TODO grab all the tags at once instead of getting each one every time. too long
            SqlCommand cmd = new SqlCommand("TRUNCATE TABLE NewProducts", con);
            cmd.ExecuteNonQuery();
            foreach (Product product in products)
            {
                //String id = Wordpress.SendGetTag(Wordpress.GetTag(post));
                String id = product.ID;
                String name = product.Name;
                int price = product.Price;
                int xprice = product.Xprice;
                String category = product.Category;
                String url = product.URL;

                cmd.CommandText = $"INSERT INTO NewProducts VALUES ('{id}', '{name}', {price}, {xprice}, '{category}', '{url}');";
                cmd.ExecuteNonQuery();
            }
        }
    }
}
