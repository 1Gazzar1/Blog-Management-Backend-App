namespace Blog_Management_System.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeservice;
        public LikeController(ILikeService Likeservice)
        {
            _likeservice = Likeservice;
        }
        [HttpGet]
        public async Task<ActionResult> GetLikes()
        {
            var likes = await _likeservice.GetLikes();
            return Ok(likes);
        }
        [HttpGet("id")]
        public async Task<ActionResult> GetLikeByID(int id)
        {
            if (!await _likeservice.LikeExists(id))
            {
                return BadRequest("Invalid ID");
            }
            var like = await _likeservice.GetLikeByID(id);
            return Ok(like);
        }
        [HttpPost]
        public async Task<ActionResult> AddLike(Like like)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _likeservice.AddLike(like);
            return Created();
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteLikeByID(int id)
        {
            if (!await _likeservice.LikeExists(id))
            {
                return BadRequest("Invalid ID");
            }

            await _likeservice.DeleteLikeByID(id);
            
            return NoContent();


        }
    }
}
