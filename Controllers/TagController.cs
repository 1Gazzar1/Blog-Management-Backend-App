namespace Blog_Management_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagservice;
        public TagController(ITagService tagservice)
        {
            _tagservice = tagservice;
        }
        [HttpGet]
        public async Task<ActionResult> GetTags()
        {
            var tags = await _tagservice.GetTags();
            return Ok(tags);
        }
        [HttpGet("id")]
        public async Task<ActionResult> GetTagByID(int id )
        {
            if (! await _tagservice.TagExists(id))
            {
                return BadRequest("Invalid ID");
            }
            var tag = await _tagservice.GetTagByID(id);
            return Ok(tag);
        }
        [HttpGet("name")]
        public async Task<ActionResult> GetTagByName(string name)
        {
            if (!(await _tagservice.GetTags()).Any(t => t.TagName == name))
            {
                return BadRequest("Invalid Name");
            }
            var tag = await _tagservice.GetTagByName(name);
            return Ok(tag);
        }
        [HttpPost]
        public async Task<ActionResult> AddTag(Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _tagservice.AddTag(tag);
            return Created();
        }
        [HttpPut]
        public async Task<ActionResult> UpdateTag(int id , Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!await _tagservice.TagExists(id))
            {
                return BadRequest("Invalid ID");
            }
            await _tagservice.UpdateTag(id,tag);
            return Created();
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteTag(int id)
        {
        
            if (!await _tagservice.TagExists(id))
            {
                return BadRequest("Invalid ID");
            }
            await _tagservice.DeleteTagByID(id);
            return NoContent();
        }
    }
}
