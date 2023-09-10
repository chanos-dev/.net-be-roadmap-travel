using EntityFramework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFramework.Infrastructure.Config
{
    internal class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(post => post.Id);

            builder.HasOne(post => post.Blog)
                   .WithMany(blog => blog.Posts)
                   .HasForeignKey(post => post.BlogId);

            builder.Property(post => post.Title).HasMaxLength(255);
        }
    }
}
