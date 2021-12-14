using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordPressPCL;
using WordPressPCL.Models;
using System.Linq;

namespace Manage_HH
{
    class Wordpress
    {
        //Remebers all the tags that have already been on the site and the new ones coming in. Used to check if an incoming new listing has already been added
        public static List<String> tags = SendGetTags(GetTags()).Select(k => k.Name.ToUpper()).ToList();


        /// <summary>
        /// Remove empty tags on website
        /// </summary>
        public static async void RemoveEmptyTags()
        {
            var client = await GetClient();
            List<Tag> tags = SendGetTags(GetTags());
            foreach (var tag in tags)
            {
                if (tag.Count == 0)
                {
                    client.Tags.Delete(tag.Id);
                }
            }
        }

        public static List<String> FindDuplicates(List<String> lst)
        {
            return lst.GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .Select(y => y.Key)
              .ToList();
        }

        /// <summary>
        /// Converts the Task<list<Post>> to List<Post> and removes duplicates from Wordpress
        /// </summary>
        /// <param name="posts"></param>
        /// <returns></returns>
        public static List<Post> SendGetPosts(Task<List<Post>> Rawposts)
        {
            List<Post> posts = Rawposts.Result;
            var check = FindDuplicates(posts.Select(x => x.Title.Rendered).ToList());
            if (check.Count > 0)
            {
                var client = GetClient().Result;
                foreach (var duplicateName in check)
                {
                    var duplicates = posts.Where(y => y.Title.Rendered == duplicateName).ToList();
                    client.Tags.Delete(duplicates[0].Tags[0]);
                    client.Posts.Delete(duplicates[0].Id);
                    posts.Remove(duplicates[0]);
                }
            }
            return posts.ToList();
        }

        /// <summary>
        /// returns a list of all the posts from the site
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Post>> GetPosts()
        {
            var client = await GetClient();
            return client.Posts.GetAll().Result.ToList();
        }

        internal static async Task RemoveNoURLPostsAsync()
        {
            var client = await GetClient();
            List<Post> posts = GetPosts().Result;
            foreach (Post post in posts)
            {
                if(!post.Link.Contains("https://www.amazon.com"))
                {
                    client.Posts.Delete(post.Id).Wait();
                }
            }
        }

        /// <summary>
        /// Cleans the media library of uneeded photos
        /// </summary>
        /// <returns></returns>
        public static async Task CleanImagesFolder()
        {
            var client = await GetClient();
            Dictionary<String, MediaItem> MediaIDtoMedia = client.Media.GetAll().Result.ToDictionary(k => k.Title.Rendered);
            restart:
            foreach (String filePath in Directory.GetFiles(@"C:\Users\email\Desktop\Hardware Hub\images"))
            {
                try
                {
                    _ = MediaIDtoMedia[Formatting.GetIDfromFile(filePath)];
                }
                catch (KeyNotFoundException)
                {
                    File.Delete(filePath);
                    goto restart;
                }
            }
        }

        public static async Task CleanImagesFolder(List<String> IDs)
        {
            var client = await GetClient();
            Dictionary<String, MediaItem> MediaIDtoMedia = client.Media.GetAll().Result.ToDictionary(k => k.Title.Rendered);
            restart:
            foreach (String ID in IDs)
            {
                File.Delete(ID);
            }
        }

        /// <summary>
        /// Deletes the post from the website based of the product passed in
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static async Task RemoveProduct(Product product)
        {
            var client = await GetClient();
            Dictionary<String, Tag> tagsDict = SendGetTags(GetTags()).ToDictionary(p => p.Name);
            client.Posts.Delete(int.Parse(tagsDict[product.ID].Id.ToString())).Wait();
            //client.Posts.Delete(int.Parse(SendGetTag(GetTag(product)))).Wait();
        }

        /// <summary>
        /// Removes duplicate posts from wordpress
        /// </summary>
        /// <returns></returns>
        public static async Task RemoveDuplicates()
        {
            //TODO figure out why there are still duplicates
            var client = GetClient().Result;

            restart:
            Dictionary<int,Tag> tagsDict = SendGetTags(GetTags()).ToDictionary(p => p.Id);

            List<Post> posts = client.Posts.GetAll().Result.ToList();
            //Dictionary<String, Post> postsDict = posts.ToDictionary(p => tagsDict[p.Tags[0]].Name);

            //tracks products that have already been deleted so they aren't deleted twice
            List<Post> deleted = new List<Post>();

            foreach (Post post1 in posts)
            {
                if (deleted.Contains(post1))
                {
                    continue;
                }
                String title1 = post1.Title.Rendered;
                String id1 = tagsDict[post1.Tags[0]].Name;

                foreach (Post post2 in posts)
                {

                    if (deleted.Contains(post2))
                    {
                        continue;
                    }

                    String title2 = post2.Title.Rendered;
                    String id2 = tagsDict[post2.Tags[0]].Name;

                    if (post1.Id == post2.Id)
                    {
                        continue;
                    }

                    if (post1.Id != post2.Id && (title1 == title2 || id1 == id2))
                    {
                        if (post2.FeaturedMedia.Value != 0)
                        {
                            client.Media.Delete(post2.FeaturedMedia.Value).Wait();
                        }
                        client.Tags.Delete(post2.Tags[0]).Wait();
                        client.Posts.Delete(post2.Id).Wait();

                        deleted.Add(post2);
                        goto restart;                     
                    }
                }
            }
        }

        /// <summary>
        /// converts Task<String> to String used for getTags() method
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String SendGetTag(Task<String> str)
        {
            return str.Result;
        }


        /// <summary>
        /// gets the tag of the product passed in from the site
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static async Task<List<Tag>> GetTags()
        {
            var client = GetClient().Result;
            return client.Tags.GetAll().Result.ToList();
        }

        /// <summary>
        /// converts Task<String> to String used for getTags() method
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<Tag> SendGetTags(Task<List<Tag>> tags)
        {
            return tags.Result;
        }

        /// <summary>
        /// Gets the tag based off the post passed in
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public static async Task<String> GetTag(Post post)
        {
            var client = await GetClient();
            Dictionary<int, Tag> tagsDict = SendGetTags(GetTags()).ToDictionary(p => p.Id);
            return tagsDict[post.Tags[0]].Name;

            //List<Tag> tags = client.Tags.GetAll().Result.ToList();
            //if (await client.IsValidJWToken())
            //{
            //    foreach (Tag tag in tags)
            //    {
            //        if (post.Tags[0] == tag.Id)
            //        {
            //            return tag.Name;
            //        }
            //    }
            //    //for (int x = 0; x < tags.Count; x++)
            //    //{
            //    //    if(post.Tags[0] == tags[x].Id)
            //    //    {
            //    //        Selenium.tag = tags[x].Name;
            //    //        goto end;
            //    //    }
            //    //    count++;
            //    //}
            //    return "Tag Not Found";
            //}
            //return "client not approved";
        }

        /// <summary>
        /// Cleans the media library of unneeded photos
        /// </summary>
        /// <returns></returns>
        public static async Task CleanMedia()
        {
            var client = await GetClient();
            
            //List of pics being used and need to be kept
            List<int> used = new List<int>();

            //Enter in a media ID to get back a Post assosiated with that ID
            Dictionary<int?,Post> mediaIDtoPost = client.Posts.GetAll().Result.ToDictionary(k => k.FeaturedMedia);
            List<MediaItem> media = client.Media.GetAll().Result.ToList();

            //If picID cannot be matched to a post, that must mean that pic is no longer being used and is then deleted
            foreach (MediaItem pic in media)
            {
                try
                {
                    _ = mediaIDtoPost[pic.Id];
                }
                catch (KeyNotFoundException)
                {
                    client.Media.Delete(pic.Id).Wait();
                }
            }
                
            ////Grabs Posts and Media. Compares the ID of each. If true, add to the used list
            //foreach (Post post in posts)
            //{
            //    foreach (MediaItem pic in media)
            //    {
            //        if(post.FeaturedMedia == pic.Id)
            //        {
            //            used.Add(pic.Id);
            //            break;
            //        }
            //    }
            //}

            ////Delete all picture from media library that arent in the used list
            //foreach (MediaItem pic in client.Media.GetAll().Result.ToList())
            //{
            //    if (!used.Contains(pic.Id) && !pic.Title.Rendered.Contains("HH-logo") && !pic.Title.Rendered.Contains("favicon.png") && !pic.Title.Rendered.Contains("HH logo invert"))
            //    {
            //        await client.Media.Delete(pic.Id);
            //    }
            //}
            
            
        }

        /// <summary>
        /// returns client for wordpress site
        /// </summary>
        /// <returns></returns>
        public static async Task<WordPressClient> GetClient()
        {
            //JWT authentication
            var client = new WordPressClient("https://zed.exioite.com/wp-json/");
            client.AuthMethod = AuthMethod.JWT;
            String[] creds = Formatting.getCreds();
            client.RequestJWToken(creds[0], creds[1]).Wait();
            //await client.RequestJWToken("zaid@exioite.com", "*xuFKWOX@t8Oc$8fgALK4HLh");
            return client;
        }

        /// <summary>
        /// Grab all website posts and add them to updates list
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="wait"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static async Task<List<Product>> ReadWebsite()
        {
            List<Product> siteWideProducts = new List<Product>();
            WordPressClient client = await GetClient();
            foreach (Post post in client.Posts.GetAll().Result)
            {
                siteWideProducts.Add(new Product(
                    post.Title.Rendered,
                    post.Categories[0],
                    post.Link,
                    post.Tags[0]
                    ));
            }
            siteWideProducts = FixTags(siteWideProducts).Result;
            return siteWideProducts;
        }


        /// <summary>
        /// removes or changes posts with sale changes
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static async Task UpdatePosts()
        {
            WordPressClient client = await GetClient();
            Dictionary<int, String> TagsIDtoName = SendGetTags(GetTags()).ToDictionary(k => k.Id, v => v.Name);
            Dictionary<int, Post> PostsDict = SendGetPosts(GetPosts()).ToDictionary(p => p.Tags[0]);

            foreach (Product update in Product.updates)
            {
                if (PostsDict.ContainsKey(update.tagID))
                {
                    Post post = PostsDict[update.tagID];
                    //if update is no longer on sale, delete from wordpress
                    if (update.Change == 2)
                    {
                        client.Posts.Delete(post.Id).Wait();
                        client.Tags.Delete(post.Tags[0]).Wait();
                        client.Media.Delete(post.FeaturedMedia.Value).Wait();
                        tags.Remove(TagsIDtoName[update.tagID]);
                    }
                    else if(update.Change == 1)
                    {
                        //Otherwise update with the new prices
                        Post updatePost = new Post
                        {
                            Id = post.Id,
                            Content = new Content("$" + update.Xprice + "-->" + "$" + update.Price),
                        };
                        client.Posts.Update(updatePost).Wait();
                    }
                }
            }
        }

        /// <summary>
        /// Creates a post based off the product object passed
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        public static async Task CreatePost(IWebDriver driver, Product product)
        {
            int[] catID = new int[1] { product.CatID };

            WordPressClient client = await GetClient();

            //upload image to media library
            Image pic = Image.FromFile(@"C:\Users\email\Desktop\Hardware Hub\images\" + product.ID + ".png");
            client.Media.Create(Formatting.ToStream(pic, ImageFormat.Png), product.ID, "image/png").Wait();

            Dictionary<String, MediaItem> MediaIDtoMedia = client.Media.GetAll().Result.ToDictionary(k => k.Slug.ToUpper());
            int mediaID = MediaIDtoMedia[product.ID].Id;

            //add tag
            Tag newTag = new Tag
            {
                Name = product.ID,
                Slug = product.ID,
            };
            client.Tags.Create(newTag).Wait();

            Dictionary<String, Tag> IDtoTag = client.Tags.GetAll().Result.ToDictionary(k => k.Name);

            //tags to be tied to post
            int[] tagID = new int[1] { IDtoTag[product.ID].Id };

            //post created and uploaded
            Post post = new Post
            {
                Title = new Title(product.Name),
                Content = new Content("$" + product.Xprice + "-->" + "$" + product.Price),
                Categories = catID,
                FeaturedMedia = mediaID,
                Tags = tagID
            };
            client.Posts.Create(post).Wait();

            //Link attached
            Selenium.AddLink(driver, product.URL, product.ID);

        }

        /// <summary>
        /// When grabbing tags from wordpress, they give the ID of the tag instead of the name of the tag. This method grabs the list of tags and changes the value to the ID we want
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static async Task<List<Product>> FixTags(List<Product> list)
        {
            WordPressClient client = await GetClient();
            Dictionary<int, String> TagIDtoTag = client.Tags.GetAll().Result.ToDictionary(k => k.Id,v => v.Name);
            foreach (Product product in list)
            {
                product.ID = TagIDtoTag[product.tagID];
            }
            return list;
        }

        /// <summary>
        /// Tests a post with the wordpresspcl api
        /// </summary>
        /// <returns></returns>
        public static async Task TestPost()
        {
            int[] catID = new int[1];
            catID[0] = 7;

            WordPressClient client = await GetClient();
            
            int mediaID = 0;
            foreach (MediaItem item in client.Media.GetAll().Result)
            {
                if (item.Title.Rendered == "HH logo invert")
                {
                    mediaID = item.Id;

                    Tag newTag = new Tag
                    {
                        Name = "test",
                        Slug = "test",
                    };
                    client.Tags.Create(newTag).Wait();

                    int[] tagID = new int[1];
                    foreach (Tag tag in client.Tags.GetAll().Result)
                    {
                        if (tag.Name == "test")
                        {
                            tagID[0] = tag.Id;
                        }
                    }
                    Post post = new Post
                    {
                        Title = new Title("Test Post"),
                        Content = new Content("Test Content"),
                        Categories = catID,
                        FeaturedMedia = mediaID,
                        Tags = tagID
                    };
                    client.Posts.Create(post).Wait();
                    MessageBox.Show("success");
                }
            }
            

        }

        /// <summary>
        /// Add photos to media library in wordpress site
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static async Task AddPics(IWebDriver driver)
        {
            var client = await GetClient();
            foreach (Product product in Product.products)
            {
                
                Image pic = Image.FromFile(@"C:\Users\email\Desktop\Hardware Hub\images\" + product.ID + ".png");
                client.Media.Create(Formatting.ToStream(pic, ImageFormat.Png), product.ID, "image/png").Wait();
                
            }
            
        }


        /// <summary>
        /// Tests the tag fucntion of the wrodpresspcl api
        /// </summary>
        /// <returns></returns>
        public static async Task TestTag()
        {
            try
            {
                WordPressClient client = await GetClient();
                if (await client.IsValidJWToken())
                {
                    foreach (Tag tag in client.Tags.GetAll().Result)
                    {
                        MessageBox.Show(tag.Name);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
            }
        }

    }
}
