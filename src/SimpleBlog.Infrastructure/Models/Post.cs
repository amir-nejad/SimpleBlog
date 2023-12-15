namespace SimpleBlog.Infrastructure.Models;

public partial class Post
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Text { get; set; }

    public bool IsActive { get; set; }

    public string UserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}
