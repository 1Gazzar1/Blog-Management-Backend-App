namespace Blog_Management_System.Services
{
    public interface IPostService
    {
        Task<bool> PostExists(int id);
        Task<List<Post>> GetPosts();
        Task<Post> GetPostByID(int id);
        Task<Post> GetPostByName(string Title);
        Task AddPost(Post post);
        Task UpdatePost(int id, Post post);
        Task DeletePostByID(int id);
        Task<List<Comment>> GetApprovedCommentsByPostID(int postid);

    }
    public class PostService : IPostService
    {
        private readonly BlogDBContext _Context;
        public PostService(BlogDBContext context)
        {
             _Context = context;
        }
        public async Task<bool> PostExists(int id)
        {
            return await _Context.Posts.AnyAsync(p => p.PostID == id);
        }
        public async Task<List<Post>> GetPosts()
        {

           return await _Context.Posts
                        .Include(p=>p.Comments)
                        .Include(p => p.Tags)
                        .Include(p => p.Likes)
                        .ToListAsync();
        }
        public async Task<Post> GetPostByID(int id)
        {
            return await _Context.Posts
                        .Include(p => p.Comments)
                        .Include(p => p.Tags)
                        .Include(p => p.Likes)
                        .FirstOrDefaultAsync(p => p.PostID == id);
        }
        public async Task<Post> GetPostByName(string Title)
        {
            return await _Context.Posts
                        .Include(p => p.Comments)
                        .Include(p => p.Tags)
                        .Include(p => p.Likes)
                        .FirstOrDefaultAsync(p => p.Title == Title);
        }
        public async Task AddPost(Post post)
        {
            await _Context.Posts.AddAsync(post);
            await _Context.SaveChangesAsync();
        }
        public async Task UpdatePost(int id, Post post)
        {
            var old_post = await GetPostByID(id);

            old_post.Title = post.Title;
            old_post.Content = post.Content;
            
            await _Context.SaveChangesAsync();
        }
        public async Task DeletePostByID(int id)
        {   
            var post = await GetPostByID(id);
            _Context.Posts.Remove(post);
            await _Context.SaveChangesAsync();
        }
        public async Task<List<Comment>> GetApprovedCommentsByPostID(int postid)
        {
            var post = await GetPostByID(postid);
            var comments = post.Comments
                                      .Where(c => c.Status == CommentStatus.Approved)
                                      .ToList();
            return comments;
        }
    }
}
