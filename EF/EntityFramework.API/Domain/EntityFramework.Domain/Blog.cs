namespace EntityFramework.Domain
{
    public class Blog
    {
        public int Id { get; set; }
        public string URL { get; set; }

        public List<Post> Posts { set;  get; }
    }
}