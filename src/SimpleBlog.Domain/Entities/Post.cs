using SimpleBlog.Domain.Common;

namespace SimpleBlog.Domain.Entities
{
    public partial class Post : BaseEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public bool IsActive { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
