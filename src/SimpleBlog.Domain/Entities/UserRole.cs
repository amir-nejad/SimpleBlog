using SimpleBlog.Domain.Common;

namespace SimpleBlog.Domain.Entities
{
    public partial class UserRole : BaseEntity
    {
        public string Id { get; set; }

        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
