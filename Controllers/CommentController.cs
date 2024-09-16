
namespace Blog_Management_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentservice;
        public CommentController(ICommentService commentservice)
        {
            _commentservice = commentservice;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetComments()
        {
            var Comments = await _commentservice.GetComments();
            return Ok(Comments);
        }
        [HttpGet("id")]
        
        public async Task<ActionResult> GetCommentByID(int id)
        {
            if (!await _commentservice.CommentExists(id))
            {
                return BadRequest("InValid ID");
            }
            var comment = await _commentservice.GetCommentByID(id);
            return Ok(comment);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddComment(Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _commentservice.AddComment(comment);

            return Created();
        }
        [HttpPut]
        [Authorize(Roles = "Editor,Admin")]
        public async Task<ActionResult> UpdateComment(int id, Comment comment)
        {
            if (!await _commentservice.CommentExists(id))
            {
                return BadRequest("InValid ID");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _commentservice.UpdateComment(id, comment);

            return NoContent();
        }
        [HttpDelete]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> DeleteComment(int id)
        {
            if (!await _commentservice.CommentExists(id))
            {
                return BadRequest("InValid ID");
            }

            await _commentservice.DeleteCommentByID(id);


            return NoContent();
        }
        [HttpPut("approve_comment/id")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> ApproveComment(int id)
        {
            await _commentservice.ApproveComment(id);
            return NoContent();
        }
        [HttpPut("reject_comment/id")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> RejectComment(int id)
        {
            await _commentservice.RejectComment(id);
            return NoContent();
        }
        [HttpGet("approved_comments")]
        public async Task<ActionResult> GetApprovedComments()
        {
            var comments = await _commentservice.GetApprovedComment();
            return Ok(comments);
        }
        [HttpGet("FilterComments")]
        [AllowAnonymous]
        public async Task<ActionResult> FilterComments(string content, DateTime starting_time)
        {
            var comments = await _commentservice
                                            .Search_or_Filter_Comments(content,starting_time);
            if (comments == null)
            {
                return BadRequest("Comment doesn't exist");
            }
            return Ok(comments);
        }
        
    }
}