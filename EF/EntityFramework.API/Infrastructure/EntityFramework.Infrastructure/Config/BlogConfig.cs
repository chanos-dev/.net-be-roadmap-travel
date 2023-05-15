using EntityFramework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFramework.Infrastructure.Config
{
    internal class BlogConfig : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(blog => blog.Id);

            builder.HasMany(blog => blog.Posts)
                   .WithOne(post => post.Blog)
                   .HasForeignKey(post => post.BlogId)
                   .IsRequired();
        }
    }
}
