using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRSExample
{
    public class BlogController : Controller
    {
        private readonly IMediator _mediator;

        public BlogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var query = new GetAllPostsQuery();
            var posts = await _mediator.Send(query);
            var viewModel = new PostListViewModel
            {
                Posts = posts
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var query = new GetPostByIdQuery(id);
            var post = await _mediator.Send(query);
            if (post == null)
            {
                return NotFound();
            }
            var viewModel = new PostDetailViewModel
            {
                Post = post
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreatePostViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePostViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var command = new CreatePostCommand(model.Title, model.Content);
            await _mediator.Send(command);

            // Redirect to Index instead of Details to avoid reading DB after command
            return RedirectToAction(nameof(Index));
        }
    }
}
