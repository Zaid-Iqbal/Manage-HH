using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                cmd.CommandText = $"INSERT INTO NewProducts VALUES (N'{product.ID}', N'{product.Name}', { product.Price}, {product.Xprice}, N'{product.Category}',N'{product.URL}');";
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

        public static void WriteProducts()
        {
            SqlCommand cmd = new SqlCommand("TRUNCATE TABLE Products",con);
            cmd.ExecuteNonQuery();
            List<Post> WP = Wordpress.SendGetPosts(Wordpress.GetPosts());
            foreach (Post post in WP)
            {
                String id = Wordpress.SendGetTag(Wordpress.GetTag(post));
                String name = post.Title.Rendered;
                int price = int.Parse(Formatting.GetPrice(post.Content.Rendered));
                int xprice = int.Parse(Formatting.GetXprice(post.Content.Rendered));
                String category = Formatting.CatIDtoCategory(post.Categories[0]);
                String url = post.Link;

                cmd.CommandText = $"INSERT INTO Products VALUES ('{id}', '{name}', {price}, {xprice}, '{category}', '{url}');";
                cmd.ExecuteNonQuery();
            }
        }
    }
}
