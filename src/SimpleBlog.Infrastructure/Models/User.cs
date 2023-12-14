using System;
using System.Collections.Generic;

namespace SimpleBlog.Infrastructure.Models;

public partial class User
{
    public string Id { get; set; }

    public string RoleId { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual UserRole Role { get; set; }
}
