namespace Blog_Management_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postservice;
        public PostController(IPostService postService)
        {
            _postservice = postService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetPosts()
        {
            var posts = await _postservice.GetPosts();
            return Ok(posts);
        }
        [HttpGet("id")]
        public async Task<ActionResult> GetPostByID(int id)
        {
            if (!await _postservice.PostExists(id))
            {
                return BadRequest("InValid ID");
            }
            var post = await _postservice.GetPostByID(id);
            return Ok(post);
        }
        [HttpGet("Title")]
        public async Task<ActionResult> GetPostByName(string title)
        {
            if ((await _postservice.GetPosts()).FirstOrDefault(p => p.Title == title) == null)
            {
                return BadRequest("Invalid Name");
            }
            var post = await _postservice.GetPostByName(title);
            return Ok(post);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddPost(Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _postservice.AddPost(post);

            return Created();
        }
        [HttpPut]
        [Authorize(Roles = "Admin,Editor")]
        public async Task<ActionResult> UpdatePost(int id, Post post)
        {
            if (!await _postservice.PostExists(id))
            {
                return BadRequest("InValid ID");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _postservice.UpdatePost(id, post);

            return NoContent();
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeletePost(int id)
        {
            if (!await _postservice.PostExists(id))
            {
                return BadRequest("InValid ID");
            }

            await _postservice.DeletePostByID(id);


            return NoContent();
        }
        [HttpGet("comments/postid")]
        public async Task<ActionResult> GetApprovedCommentsByPostID(int postid)
        {
            if (!await _postservice.PostExists(postid))
            {
                return BadRequest("InValid ID");
            }

            var comments = await _postservice.GetApprovedCommentsByPostID(postid);

            return Ok(comments);
        }
    }
}
