using System;
using System.Collections.Generic;

namespace CQRSExample
{
    public class PostListViewModel
    {
        public List<PostSummaryDto> Posts { get; set; } = new();
    }

    public class PostDetailViewModel
    {
        public PostDetailDto Post { get; set; } = null!;
    }

    public class CreatePostViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}