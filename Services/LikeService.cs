namespace Blog_Management_System.Services
{
    public interface ILikeService
    {
        Task<bool> LikeExists(int id);
        Task<List<Like>> GetLikes();
        Task<Like> GetLikeByID(int id);
        Task AddLike(Like like);
        Task DeleteLikeByID(int id);
    }

    public class LikeService : ILikeService
    {
        private readonly BlogDBContext _Context;
        public LikeService(BlogDBContext context)
        {
            _Context = context;
        }
        public async Task<bool> LikeExists(int id)
        {
            return await _Context.Likes.AnyAsync(l => l.LikeID == id);
        }
        public async Task<List<Like>> GetLikes()
        {
            return await _Context.Likes.ToListAsync();
        }
        public async Task<Like> GetLikeByID(int id)
        {
            return await _Context.Likes.FirstOrDefaultAsync(l => l.LikeID == id);
        }
        public async Task AddLike(Like like)
        {
            await _Context.Likes.AddAsync(like);
            await _Context.SaveChangesAsync();
        }
        public async Task DeleteLikeByID(int id)
        {
            var like = await GetLikeByID(id);
            _Context.Likes.Remove(like);
            await _Context.SaveChangesAsync();
        }

    }

    
}
