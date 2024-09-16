namespace Blog_Management_System.Services
{   
    public interface ITagService
    {
        Task<bool> TagExists(int id);
        Task<List<Tag>> GetTags();
        Task<Tag> GetTagByID(int id);
        Task<Tag> GetTagByName(string tagname);
        Task AddTag(Tag tag);
        Task UpdateTag(int id, Tag tag);
        Task DeleteTagByID(int id);
    }
    public class TagService : ITagService
    {
        private readonly BlogDBContext _Context;
        public TagService(BlogDBContext context)
        {
             _Context = context;
        }
        public async Task<bool> TagExists(int id)
        {
            return await _Context.Tags.AnyAsync(t => t.TagID == id);
        }

        public async Task<List<Tag>> GetTags()
        {
            return await _Context.Tags.ToListAsync();
        }
        public async Task<Tag> GetTagByID(int id)
        {
            return await _Context.Tags.FirstOrDefaultAsync(t => t.TagID == id);
        }
        public async Task<Tag> GetTagByName(string tagname)
        {
            return await _Context.Tags.FirstOrDefaultAsync(t => t.TagName == tagname);
        }
        public async Task AddTag(Tag tag)
        {
            await _Context.Tags.AddAsync(tag);
            await _Context.SaveChangesAsync();
        }
        public async Task UpdateTag(int id, Tag tag)
        {
           var old_tag = await GetTagByID(id);

            old_tag.TagName = tag.TagName;

            await _Context.SaveChangesAsync();
        }
        public async Task DeleteTagByID(int id)
        {
            var tag = await GetTagByID(id);

            _Context.Tags.Remove(tag);

            await _Context.SaveChangesAsync();
        }

    }
}
