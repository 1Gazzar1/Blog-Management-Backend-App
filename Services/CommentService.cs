namespace Blog_Management_System.Services
{
    public interface ICommentService
    {
        Task<bool> CommentExists(int id);
        Task<List<Comment>> GetComments();
        Task<Comment> GetCommentByID(int id);
        Task AddComment(Comment comment);
        Task UpdateComment(int id, Comment comment);
        Task DeleteCommentByID(int id);
        Task ApproveComment(int id);
        Task RejectComment(int id);
        Task <List<Comment>>GetApprovedComment();
        Task<List<Comment>> Search_or_Filter_Comments(string content="",DateTime? createdtime = null);
    }
    public class CommentService : ICommentService
    {
        private readonly BlogDBContext _Context;
        public CommentService(BlogDBContext context)
        {
            _Context = context;
        }
        public async Task<bool> CommentExists(int id)
        {
            return await _Context.Comments.AnyAsync(c => c.CommentID == id);
        }
        public async Task<List<Comment>> GetComments()
        {
            return await _Context.Comments.ToListAsync();
        }
        public async Task<Comment> GetCommentByID(int id )
        {
            return await _Context.Comments.FirstOrDefaultAsync(c => c.CommentID == id);
        }
        public async Task AddComment(Comment comment)
        {
            await _Context.Comments.AddAsync(comment);
            await _Context.SaveChangesAsync();
        }
        public async Task UpdateComment(int id,Comment comment)
        {
            var old_comment = await GetCommentByID(id);

            old_comment.Content = comment.Content;
            
            await _Context.SaveChangesAsync();
        }
        public async Task DeleteCommentByID(int id)
        {
            var comment = await GetCommentByID(id);

            _Context.Comments.Remove(comment);

            await _Context.SaveChangesAsync();
        }
        public async Task ApproveComment(int id)
        {
            var comment = await GetCommentByID(id);

            comment.Status = CommentStatus.Approved;
            await _Context.SaveChangesAsync();
        }
        public async Task RejectComment(int id)
        {
            var comment = await GetCommentByID(id);

            comment.Status = CommentStatus.Rejected;
            await _Context.SaveChangesAsync();
        }
        public async Task<List<Comment>> GetApprovedComment()
        {
            return await _Context.Comments
                                .Where(c => c.Status == CommentStatus.Approved)
                                .ToListAsync();
        }
        public async Task<List<Comment>> Search_or_Filter_Comments(string content = "", DateTime? createdtime = null)
        {
            var comments = await GetComments();
            if (!String.IsNullOrWhiteSpace(content))
            {
                comments = comments.Where(c => c.Content.Contains(content) ).ToList();
            }
            if (createdtime != null)
            {
                comments = comments.Where(c => c.CreatedTime >= createdtime).ToList();
            }
            return comments;
            
        }

    }   
}