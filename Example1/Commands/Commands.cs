using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CQRSExample
{
    public class CreatePostCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public CreatePostCommand(string title, string content)
        {
            Title = title;
            Content = content;
        }
    }

    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, int>
    {
        public Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var newPost = new BlogPost
            {
                Id = BlogPostDB.GetNextId(),
                Title = request.Title,
                Content = request.Content,
                PublishedDate = DateTime.Now
            };

            BlogPostDB.BlogPosts.Add(newPost);
            return Task.FromResult(newPost.Id);
        }
    }
    

}