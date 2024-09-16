namespace Blog_Management_System.Services
{
    public interface IUserService
    {
        Task<bool> UserExists(int id);
        Task<List<User>> GetUsers();
        Task<User> GetUserByID(int id);
        Task<User> GetUserByName(string username);
        Task AddUser(User user);
        Task UpdateUser(int id ,User user); 
        Task DeleteUserByID(int id);
        Task<User> Authenticate(string username, string password);
        string GenerateToken(User user);
        
    }
    public class UserService : IUserService
    {
        private readonly BlogDBContext _Context;
        private readonly IConfiguration _config;
        public UserService(BlogDBContext context,IConfiguration config)
        {
            _Context = context;
            _config = config;
        }

        public async Task<bool> UserExists(int id)
        {
            return await _Context.Users.AnyAsync(u => u.UserID == id);

        }
        public async Task<List<User>> GetUsers()
        {
            return await _Context.Users
                .Include(u => u.Posts)
                .ToListAsync();
        }
        public async Task<User>  GetUserByID(int id)
        {

            var user = await _Context.Users
                .Include(u => u.Posts)
                .FirstOrDefaultAsync(u => u.UserID == id);
            return user;
        }
        public async Task<User> GetUserByName(String username)
        {
            var user = await _Context.Users
                .Include(u => u.Posts)
                .FirstOrDefaultAsync(u => u.Name == username);
            return user;
        }

        public async Task AddUser(User user)
        {
            await _Context.Users.AddAsync(user);

            await _Context.SaveChangesAsync();
        }
        public async Task UpdateUser(int id, User user)
        {
            var old_user = await GetUserByID(id);

            old_user.Name  = user.Name;
            old_user.Role = user.Role;
            old_user.Email = user.Email;  
            old_user.Password = user.Password;

            await _Context.SaveChangesAsync();
        }

        public async Task DeleteUserByID(int id)
        {   
            var user = await GetUserByID(id);
            _Context.Users.Remove(user);

            await _Context.SaveChangesAsync();
        }
        public async Task<User> Authenticate(string username,  string password)
        {
            
            return await _Context.Users.FirstOrDefaultAsync(u => u.Name == username
                                           && u.Password == password);
            
        }
        public  string GenerateToken(User user)
        {
            
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name , user.Name));
            claims.Add(new Claim(ClaimTypes.NameIdentifier,user.UserID.ToString()));
            claims.Add(new Claim(ClaimTypes.Role,user.Role.ToString()));

            var key = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_config["JWT:Key"]));

            var token = new JwtSecurityToken(
                claims: claims,
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: new SigningCredentials(key,SecurityAlgorithms.HmacSha256)

                );

            return  new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
