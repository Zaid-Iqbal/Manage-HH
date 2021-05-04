namespace Manage_HH
{
    class Program
    {
        public static void Main(string[] args)
        {
            SQL.con.Open();

            #region populate Products Only
            //SQL.WriteProducts();
            #endregion

            //Grabs last week's posts straight from website
            Product.updates.Clear();

            //Reads website and populates updates list with existing posts
            Wordpress.ReadWebsite().Wait();

            //Goes to the amazon page of each existing product (in used list) and updates the sale information
            Selenium.GetOldPostData();

            //Updates the posts on the website with the updates sale information for each product
            Wordpress.UpdatePosts().Wait();

            Product.products.Clear();

            //Grabs New products from excel, adds to SQL, and populates products list
            Excel.ReadProducts();
            SQL.WriteNewProducts();

            //populate products list with new products to be added
            //SQL.ReadNewProducts();
            //excel.ReadProducts();

            //Correct the categories so they fit the website
            Formatting.CorrectCategories();

            //// Remove any duplicate products in the new Products list that are already on the website
            //Product.RemoveUpdates();

            //Send selenium to the posts page on wordpress and await orders to manually add links cuz wordpresspcl is dumb and cant do it by itself
            Selenium.GoToPosts(Selenium.driver);

            //Adds picture of the the new products to the media library
            Wordpress.AddPics(Selenium.driver).Wait();
            foreach (Product product in Product.products)
            {
                //Posts the new products to the site
                Wordpress.CreatePost(Selenium.driver, product).Wait();
            }

            //Another check to make sure none of the posts are duplicates
            Wordpress.RemoveDuplicates();

            //Removes images that are not in use on the hard drive
            Wordpress.CleanImagesFolder().Wait();
            Wordpress.CleanMedia().Wait();

            SQL.WriteProducts();

            //excel.WriteHHPosts();

            Selenium.driver.Close();
            Selenium.driver.Quit();

            SQL.con.Close();
        }


    }
}
