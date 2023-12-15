using SimpleBlog.Domain.Common;

namespace SimpleBlog.Domain.Entities
{
    public partial class User : BaseEntity
    {
        public string Id { get; set; }

        public string RoleId { get; set; }

        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

        public virtual UserRole Role { get; set; }
    }
}
