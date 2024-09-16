namespace Blog_Management_System
{
    public class BlogDBContext : DbContext
    {
        public BlogDBContext(DbContextOptions<BlogDBContext> options) : base(options) 
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          
            //Primary keys
            modelBuilder.Entity<User>()
                        .HasKey(k => k.UserID);
            modelBuilder.Entity<Comment>()
                        .HasKey(k => k.CommentID);
            modelBuilder.Entity<Like>()
                        .HasKey(k => k.LikeID);
            modelBuilder.Entity<Post>()
                        .HasKey(k => k.PostID);
            modelBuilder.Entity<Tag>()
                        .HasKey(k => k.TagID);
            //Foreign Keys

            modelBuilder.Entity<User>()
                        .HasMany(u => u.Posts)
                        .WithOne(p => p.User)
                        .HasForeignKey(p => p.UserID)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                        .HasMany(u => u.Likes)
                        .WithOne(l => l.User)
                        .HasForeignKey(l => l.UserID)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                        .HasMany(u => u.Comments)
                        .WithOne(c => c.User)
                        .HasForeignKey(c => c.UserID)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Post>()
                        .HasMany(p => p.Comments)
                        .WithOne(c => c.Post)
                        .HasForeignKey(c => c.PostID)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                        .HasMany(p => p.Likes)
                        .WithOne(l => l.Post)
                        .HasForeignKey(l => l.PostID)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                        .HasMany(p => p.Tags)
                        .WithOne(t => t.Post)
                        .HasForeignKey(t => t.PostID)
                        .OnDelete(DeleteBehavior.Cascade);



        }

    }
}
