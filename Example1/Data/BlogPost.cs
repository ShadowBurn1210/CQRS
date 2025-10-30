using System;
using System.Collections.Generic;
using System.Linq;

namespace CQRSExample
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; }
    }

    public class BlogPostDB
    {
        // This is my fake database just for demonstration purposes
        public static List<BlogPost> BlogPosts = new()
        {
            new BlogPost { Id = 1, Title = "First Post", Content = "This is the content of the first post.", PublishedDate = DateTime.Now.AddDays(-10) },
            new BlogPost { Id = 2, Title = "Second Post", Content = "This is the content of the second post.", PublishedDate = DateTime.Now.AddDays(-5) },
            new BlogPost { Id = 3, Title = "Third Post", Content = "This is the content of the third post.", PublishedDate = DateTime.Now }
        };

        public static int GetNextId()
        {
            return BlogPosts.Max(bp => bp.Id) + 1;
        }
    }
}