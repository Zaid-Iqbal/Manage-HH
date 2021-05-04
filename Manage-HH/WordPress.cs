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

namespace Manage_HH
{
    class Wordpress
    {
        /// <summary>
        /// Converts the Task<list<Post>> to List<Post>
        /// </summary>
        /// <param name="posts"></param>
        /// <returns></returns>
        public static List<Post> SendGetPosts(Task<List<Post>> posts)
        {
            posts.Wait();
            return posts.Result.ToList();
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

        /// <summary>
        /// Deletes the post from the website based of the product passed in
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static async Task RemoveProduct(Product product)
        {
            var client = await GetClient();
            client.Posts.Delete(int.Parse(SendGetTag(GetTag(product))));
        }

        /// <summary>
        /// Converts Task<List<String>> to List<String>
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<String> SendLinks(Task<List<String>> list)
        {
            return list.Result;
        }
        public static async Task<List<String>> GetLinks()
        {
            var client = await GetClient();
            List<String> Links = new List<string>();
            foreach (Post post in client.Posts.GetAll().Result)
            {
                Links.Add(post.Link);
            }
            return Links;
        }

        /// <summary>
        /// Removes duplicate posts from wordpress
        /// </summary>
        /// <returns></returns>
        public static async Task RemoveDuplicates()
        {
            var client = GetClient().Result;

            restart:
            List<Tag> tags = SendGetTags(GetTags());
            Dictionary<int,Tag> tagsDict = tags.ToDictionary(p => p.Id);

            List<Post> posts = client.Posts.GetAll().Result.ToList();
            Dictionary<String, Post> postsDict = posts.ToDictionary(p => tagsDict[p.Tags[0]].Name);

            foreach (Post post1 in posts)
            {
                String title1 = post1.Title.Rendered;
                String id1 = tagsDict[post1.Tags[0]].Name;
                foreach (Post post2 in posts)
                {
                    String title2 = post2.Title.Rendered;
                    String id2 = tagsDict[post2.Tags[0]].Name;

                    if (post1.Id == post2.Id)
                    {
                        continue;
                    }

                    if (post1.Id != post2.Id && (title1 == title2 || id1 == id2))
                    {
                        client.Media.Delete(post2.FeaturedMedia.Value).Wait();
                        client.Tags.Delete(post2.Tags[0]).Wait();
                        client.Posts.Delete(post2.Id).Wait();

                        //await client.Media.Delete(post2.FeaturedMedia.Value);
                        //await client.Tags.Delete(post2.Tags[0]);
                        //await client.Posts.Delete(post2.Id);

                        //try
                        //{
                        //    await client.Media.Delete(post2.FeaturedMedia.Value);
                        //}
                        //catch (Exception)
                        //{
                        //    MessageBox.Show("Unable to delete picture at ID: " + id2 + ".\nMight already be deleted");
                        //}

                        //try
                        //{
                        //    await client.Tags.Delete(post2.Tags[0]);
                        //}
                        //catch (Exception)
                        //{
                        //    MessageBox.Show("Unable to delete tag at ID: " + id2 + ".\nMight already be deleted");
                        //}

                        //try
                        //{
                        //    await client.Posts.Delete(post2.Id);
                        //}
                        //catch (Exception)
                        //{
                        //    MessageBox.Show("Unable to delete post at ID: " + id2 + ".\nMight already be deleted");
                        //}

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
        public static async Task<String> GetTag(Product product)
        {
            var client = await GetClient();
            List<Tag> tags = client.Tags.GetAll().Result.ToList();
            if (await client.IsValidJWToken())
            {
                foreach (Tag tag in tags)
                {
                    if (product.ID == tag.Name)
                    {
                        return tag.Id.ToString();
                    }
                }

                return "Tag Not Found";
            }
            return "client not approved";
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
            List<Tag> tags = client.Tags.GetAll().Result.ToList();
            if (await client.IsValidJWToken())
            {
                foreach (Tag tag in tags)
                {
                    if (post.Tags[0] == tag.Id)
                    {
                        return tag.Name;
                    }
                }
                //for (int x = 0; x < tags.Count; x++)
                //{
                //    if(post.Tags[0] == tags[x].Id)
                //    {
                //        Selenium.tag = tags[x].Name;
                //        goto end;
                //    }
                //    count++;
                //}
                return "Tag Not Found";
            }
            return "client not approved";
        }

        /// <summary>
        /// Cleans the media library of unneeded photos
        /// </summary>
        /// <returns></returns>
        public static async Task CleanMedia()
        {
            var client = await GetClient();
            if(await client.IsValidJWToken())
            {
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
                        await client.Media.Delete(pic.Id);
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
            
        }

        /// <summary>
        /// returns client for wordpress site
        /// </summary>
        /// <returns></returns>
        private static async Task<WordPressClient> GetClient()
        {
            //JWT authentication
            var client = new WordPressClient("https://zed.exioite.com/wp-json/");
            client.AuthMethod = AuthMethod.JWT;
            String[] creds = Formatting.getCreds();
            await client.RequestJWToken(creds[0], creds[1]);
            return client;
        }

        /// <summary>
        /// Grab all website posts and add them to updates list
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="wait"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static async Task ReadWebsite()
        {
            WordPressClient client = await GetClient();
            if (await client.IsValidJWToken())
            {
                foreach (Post post in client.Posts.GetAll().Result)
                {
                    Product.updates.Add(new Product(
                        post.Title.Rendered,
                        post.Categories[0],
                        post.Link,
                        post.Tags[0]
                        ));
                }
                FixTags(Product.updates).Wait();
            }
        }

        //public static async Task ReadLastWeek2(IWebDriver driver, WebDriverWait wait)
        //{
        //    WordPressClient client = await GetClient();
        //    if (await client.IsValidJWToken())
        //    {
        //        foreach (Post post in client.Posts.GetAll().Result)
        //        {
        //            Product.updates.Add(new Product(
        //                post.Title.Rendered,
        //                post.Categories[0],
        //                Selenium.getLink(driver, wait, post),
        //                post.Tags[0]
        //                ));
        //        }
        //        tagIDtoID(Product.updates).Wait();
        //    }
        //}

        /// <summary>
        /// removes or changes posts with sale changes
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static async Task UpdatePosts()
        {
            WordPressClient client = await GetClient();

            foreach (Post post in client.Posts.GetAll().Result)
            {
                foreach (Product update in Product.updates)
                {
                    if (post.Tags[0] == update.tagID)
                    {
                        //if update is no longer on sale, delete from wordpress
                        if (update.Price == -1 || update.Xprice == -1)
                        {
                            await client.Posts.Delete(post.Id);
                            await client.Tags.Delete(post.Tags[0]);
                            await client.Media.Delete(post.FeaturedMedia.Value);
                        }
                        else
                        {
                            //Otherwise update with the new prices
                            Post updatePost = new Post
                            {
                                Id = post.Id,
                                Content = new Content("$" + update.Xprice + "-->" + "$" + update.Price),
                            };
                            await client.Posts.Update(updatePost);
                        }
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
            if (await client.IsValidJWToken())
            {
                Dictionary<String, MediaItem> MediaIDtoMedia = client.Media.GetAll().Result.ToDictionary(k => k.Slug);
                int mediaID = MediaIDtoMedia[product.ID].Id;

                //add tag
                Tag newTag = new Tag
                {
                    Name = product.ID,
                    Slug = product.ID,
                };
                await client.Tags.Create(newTag);

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
                await client.Posts.Create(post);

                //Link attached
                Selenium.AddLink(driver, product.URL, product.ID);
            }
            

        }

        /// <summary>
        /// When grabbing tags from wordpress, they give the ID of the tag instead of the name of the tag. This method grabs the list of tags and changes the value to the ID we want
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static async Task FixTags(List<Product> list)
        {
            WordPressClient client = await GetClient();
            Dictionary<int, String> TagIDtoTag = client.Tags.GetAll().Result.ToDictionary(k => k.Id,v => v.Name);
            if (await client.IsValidJWToken())
            {
                foreach (Product product in list)
                {
                    product.ID = TagIDtoTag[product.tagID];
                    #region oldtagsearch
                    //foreach (Tag tag in client.Tags.GetAll().Result)
                    //{
                    //    if(tag.Id == product.tagID)
                    //    {
                    //        product.ID = tag.Name;
                    //    }
                    //}
                    #endregion
                }
            }
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
            if (await client.IsValidJWToken())
            {
                int mediaID = 0;
                foreach (MediaItem item in client.Media.GetAll().Result)
                {
                    if (item.Slug == "logo")
                    {
                        mediaID = item.Id;

                        Tag newTag = new Tag
                        {
                            Name = "test",
                            Slug = "test",
                        };
                        await client.Tags.Create(newTag);

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
                        await client.Posts.Create(post);
                    }
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
                if (await client.IsValidJWToken())
                {
                    Image pic = Image.FromFile(@"C:\Users\email\Desktop\Hardware Hub\images\" + product.ID + ".png");
                    await client.Media.Create(Formatting.ToStream(pic, ImageFormat.Png), product.ID, "image/png");
                }
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
