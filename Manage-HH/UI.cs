using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordPressPCL.Models;

namespace Manage_HH
{
    public partial class UI : Form
    {
        //Initialize Lists
        public static List<String> prices = new List<String>();
        public static List<String> names = new List<String>();
        public static List<String> Xprices = new List<String>();
        public static List<String> picURLs = new List<String>();
        //public static List<String> URLs = new List<String>();
        public static List<Image> pics = new List<Image>();
        public static List<String> skippedURLs = new List<String>();

        //public static Dictionary<String, Post> posts = Wordpress.SendGetPosts(Wordpress.GetPosts()).ToDictionary(k => k.Title.Rendered);

        public UI()
        {
            InitializeComponent();
        }

        public void TurnOffButtons()
        {
            Label.Text = "Wait...";
            FullUpdateBtn.Enabled = false;
            UpdatePostsBtn.Enabled = false;
            AddBtn.Enabled = false;
            CleanPicsBtn.Enabled = false;
            CleanLibraryBtn.Enabled = false;
            PopulateSQLBtn.Enabled = false;
            PopulateSQLFromExcelBtn.Enabled = false;
            RemoveDuplicatesButton.Enabled = false;
            Refresh();
        }

        public void TurnOnButtons()
        {
            Label.Text = "What would you like to do?";
            FullUpdateBtn.Enabled = true;
            UpdatePostsBtn.Enabled = true;
            AddBtn.Enabled = true;
            CleanPicsBtn.Enabled = true;
            CleanLibraryBtn.Enabled = true;
            PopulateSQLBtn.Enabled = true;
            PopulateSQLFromExcelBtn.Enabled = true;
            RemoveDuplicatesButton.Enabled = true;
            Refresh();
        }

        private void FullUpdateBtn_Click(object sender, EventArgs e)
        {
            TurnOffButtons();

            //Reads website and populates site list with existing posts
            //List<Product> site = Wordpress.ReadWebsite().Result;

            //Cleanup becuase my laptop has limited storage
            Wordpress.CleanImagesFolder().Wait();
            Wordpress.CleanMedia().Wait();
            Wordpress.RemoveEmptyTags();
            //Wordpress.RemoveNoURLPostsAsync().Wait();

            //Begin the 3 browser threads lists
            String[] list1 = new String[] { "Laptops", "Desktops", "PC Gaming", "Monitors", "Computer Accessories" };
            String[] list2 = new String[] { "Networking", "Computer Components", "Storage", "TV & Video", "Cell Phones & Accessories" };
            String[] list3 = new String[] { "Speakers", "Headphones", "Bluetooth Earbuds", "Phones" };

            //Initialize browser threads
            //Grabs new info and stores it as Product in Product.products list
            //Products WRITE
            Thread thread1 = new Thread(() => Selenium.GetInfoByCategories(Selenium.drivers[0], Selenium.waits[0], list1));
            Thread thread2 = new Thread(() => Selenium.GetInfoByCategories(Selenium.drivers[1], Selenium.waits[1], list2));
            Thread thread3 = new Thread(() => Selenium.GetInfoByCategories(Selenium.drivers[2], Selenium.waits[2], list3));

            thread1.Start();
            thread2.Start();
            thread3.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();

            //Remove duplicates from unknown bug
            Formatting.RemoveDuplicates();

            //Correct the categories of Products list so they fit the website
            Formatting.CorrectCategories();

            //Goes to the amazon page of each existing product (in used list) and updates the sale information
            List<List<Product>> DividedUpdates = (List<List<Product>>)Product.SplitList(Product.posts.Values.ToList(), (Product.posts.Count / 3) + 1);
            if (DividedUpdates.Count > 0)
            {
                thread1 = new Thread(() => Selenium.GetPostData(Selenium.drivers[0], Selenium.waits[0], DividedUpdates[0]));
                thread2 = new Thread(() => Selenium.GetPostData(Selenium.drivers[1], Selenium.waits[1], DividedUpdates[1]));
                thread3 = new Thread(() => Selenium.GetPostData(Selenium.drivers[2], Selenium.waits[2], DividedUpdates[2]));

                thread1.Start();
                thread2.Start();
                thread3.Start();

                thread1.Join();
                thread2.Join();
                thread3.Join();

                Product.updates = DividedUpdates[0].Concat(DividedUpdates[1]).Concat(DividedUpdates[2]).ToList();

                //Updates the posts on the website with the updated sale information for each product
                Wordpress.UpdatePosts().Wait();
            }
            
            Selenium.drivers[0].Close();
            Selenium.drivers[1].Close();
            Selenium.drivers[2].Close();

            //Send selenium to the posts page on wordpress and await orders to manually add links cuz wordpresspcl is dumb and cant do it by itself
            Selenium.GoToPosts(Selenium.driver);

            ////Adds pictures of the the new products to the media library
            //Wordpress.AddPics(Selenium.driver).Wait();
            foreach (Product product in Product.products)
            {
                //Posts the new products to the site
                Wordpress.CreatePost(Selenium.driver, product).Wait();
            }

            //Another check to make sure none of the posts are duplicates
            Wordpress.RemoveDuplicates().Wait();

            //Removes images that are not in use on the hard drive
            Wordpress.CleanImagesFolder().Wait();
            Wordpress.CleanMedia().Wait();

            Wordpress.RemoveEmptyTags();

            SQL.WriteProducts();

            Selenium.driver.Close();
            Selenium.driver.Quit();
            TurnOnButtons();
        }

        private void UpdatePostsBtn_Click(object sender, EventArgs e)
        {
            TurnOffButtons();

            //Grabs last week's posts straight from website
            Product.updates.Clear();

            //Reads website and populates updates list with existing posts
            Wordpress.ReadWebsite().Wait();

            //Goes to the amazon page of each existing product (in used list) and updates the sale information

            List<List<Product>> DividedUpdates = Product.SplitList(Product.updates, 3);
            Thread thread1 = new Thread(() => Selenium.GetPostData(Selenium.drivers[0], Selenium.waits[0], DividedUpdates[0]));
            Thread thread2 = new Thread(() => Selenium.GetPostData(Selenium.drivers[1], Selenium.waits[1], DividedUpdates[1]));
            Thread thread3 = new Thread(() => Selenium.GetPostData(Selenium.drivers[2], Selenium.waits[2], DividedUpdates[2]));

            thread1.Start();
            thread2.Start();
            thread3.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();

            Product.updates = DividedUpdates[0].Concat(DividedUpdates[1]).Concat(DividedUpdates[2]).ToList();
            //Selenium.GetOldPostData();

            //Updates the posts on the website with the updates sale information for each product
            Wordpress.UpdatePosts().Wait();

            TurnOnButtons();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            TurnOffButtons();

            Product.products.Clear();

            //Grabs New products from excel, adds to SQL, and populates products list
            Excel.ReadProducts();

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

            TurnOnButtons();
        }

        private void CleanPicsBtn_Click(object sender, EventArgs e)
        {
            TurnOffButtons();

            Wordpress.CleanImagesFolder().Wait();

            TurnOnButtons();
        }

        private void CleanLibraryBtn_Click(object sender, EventArgs e)
        {
            TurnOffButtons();

            Wordpress.CleanMedia().Wait();

            TurnOnButtons();
        }

        private void PopulateSQLFromExcelBtn_Click(object sender, EventArgs e)
        {
            TurnOffButtons();

            Product.products.Clear();
            Excel.ReadProducts();
            SQL.WriteNewProducts();

            TurnOnButtons();
        }

        private void PopulateSQLBtn_Click(object sender, EventArgs e)
        {
            TurnOffButtons();

            SQL.WriteProducts();

            TurnOnButtons();
        }

        private void RemoveDuplicatesButton_Click(object sender, EventArgs e)
        {
            TurnOffButtons();

            Wordpress.RemoveDuplicates();

            TurnOnButtons();
        }
    }
}
