namespace Blog_Management_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userservice;

        public UserController(IUserService userservice)
        {
            _userservice = userservice;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllUsers()
        {
            
            var users = await _userservice.GetUsers();

            return Ok(users);
        }
        [HttpGet("id")]
        public async Task<ActionResult> GetUserByID(int id )
        {
            if (!await _userservice.UserExists(id))
            {
                return BadRequest("Invalid ID");
            }
            var user = await _userservice.GetUserByID(id);

            return Ok(user);
        }
        [HttpGet("name")]
        public async Task<ActionResult> GetUserByName(String name)
        {
            if ((await _userservice.GetUsers()).FirstOrDefault(u => u.Name == name) == null) 
            {
                return BadRequest("Invalid Name");
            }
            
            var user = await _userservice.GetUserByName(name);

            return Ok(user);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AddUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _userservice.AddUser(user);

            return Created();
        }
        [HttpPut]
        [Authorize(Roles = "Admin,Editor")]
        public async Task<ActionResult> UpdateUser(int id , User user)
        {
            if (!await _userservice.UserExists(id))
            {
                return BadRequest("Invalid ID");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _userservice.UpdateUser(id, user);

            return NoContent();
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            if (!await _userservice.UserExists(id))
            {
                return BadRequest("Invalid ID");
            }
            await _userservice.DeleteUserByID(id);

            return NoContent();
        }
        [HttpGet("FilterUsers")]
        [AllowAnonymous]
        public async Task<ActionResult> FilterComments(string name)
        {
            var users = await _userservice
                                       .Search_or_Filter_Users(name);
            if (users == null)
            {
                return BadRequest("Comment doesn't exist");
            }
            return Ok(users);
        }
    }
}
