namespace Blog_Management_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserService _userservice;
        public AuthController(IUserService userservice)
        {
            _userservice = userservice;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(string username,string password)
        { 
            var user = await _userservice.Authenticate(username, password);

            if (user == null)
            {
                return BadRequest("Invalid username or password");
            }

            var token = _userservice.GenerateToken(user);
            return Ok(new
            {
                _token = token,
                expires = DateTime.UtcNow.AddDays(1)
            });
        }
    }
}
