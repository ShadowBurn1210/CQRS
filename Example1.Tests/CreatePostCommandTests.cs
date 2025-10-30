using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSExample;
using Xunit;

namespace Example1.Tests
{
    public class CreatePostCommandTests
    {
        // Helper to reset the fake DB to a known state for each test
        private void ResetBlogPosts()
        {
            BlogPostDB.BlogPosts = new List<BlogPost>
            {
                new BlogPost { Id = 1, Title = "First Post", Content = "First", PublishedDate = DateTime.Now.AddDays(-10) },
                new BlogPost { Id = 2, Title = "Second Post", Content = "Second", PublishedDate = DateTime.Now.AddDays(-5) }
            };
        }

        [Fact]
        public async Task Handler_AddsNewPost_AndReturnsId()
        {
            // Arrange
            ResetBlogPosts();
            var handler = new CreatePostCommandHandler();
            var cmd = new CreatePostCommand("New Title", "New Content");

            var beforeCount = BlogPostDB.BlogPosts.Count;

            // Act
            var returnedId = await handler.Handle(cmd, System.Threading.CancellationToken.None);

            // Assert
            Assert.Equal(beforeCount + 1, BlogPostDB.BlogPosts.Count);

            var added = BlogPostDB.BlogPosts.Last();
            Assert.Equal("New Title", added.Title);
            Assert.Equal("New Content", added.Content);
            Assert.Equal(returnedId, added.Id);
        }

        [Fact]
        public async Task Handler_AssignsIncrementalIds()
        {
            // Arrange
            ResetBlogPosts();
            var handler = new CreatePostCommandHandler();
            var cmd1 = new CreatePostCommand("A", "a");
            var cmd2 = new CreatePostCommand("B", "b");

            // Act
            var id1 = await handler.Handle(cmd1, System.Threading.CancellationToken.None);
            var id2 = await handler.Handle(cmd2, System.Threading.CancellationToken.None);

            // Assert ids are increasing and correspond to posts in DB
            Assert.True(id2 > id1);
            var post1 = BlogPostDB.BlogPosts.Single(p => p.Id == id1);
            var post2 = BlogPostDB.BlogPosts.Single(p => p.Id == id2);
            Assert.Equal("A", post1.Title);
            Assert.Equal("B", post2.Title);
        }
    }
}
