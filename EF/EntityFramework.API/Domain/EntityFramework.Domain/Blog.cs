namespace EntityFramework.Domain
{
    public class Blog
    {
        public int Id { get; set; }
        public string URL { get; set; }

        public ICollection<Post> Posts { set;  get; }
    }

    public class BlogDto
    {
        public int Id { get; set; }
        public string URL { get; set; }
    }
}