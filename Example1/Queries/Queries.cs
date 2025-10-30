using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CQRSExample
{
    public class PostSummaryDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
    }

    public class PostDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; }
    }

    public class GetAllPostsQuery : IRequest<List<PostSummaryDto>> { }

    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, List<PostSummaryDto>>
    {
        public Task<List<PostSummaryDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var allPosts = BlogPostDB.BlogPosts
                .Select(bp => new PostSummaryDto
                {
                    Id = bp.Id,
                    Title = bp.Title
                })
                .ToList();

            return Task.FromResult(allPosts);
        }
    }

    public class GetPostByIdQuery : IRequest<PostDetailDto>
    {
        public int Id { get; set; }

        public GetPostByIdQuery(int id) => Id = id;
    }

    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostDetailDto>
    {
        public Task<PostDetailDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = BlogPostDB.BlogPosts
                .Where(bp => bp.Id == request.Id)
                .Select(bp => new PostDetailDto
                {
                    Id = bp.Id,
                    Title = bp.Title,
                    Content = bp.Content,
                    PublishedDate = bp.PublishedDate
                })
                .FirstOrDefault();

            return Task.FromResult(post);
        }
    }
}