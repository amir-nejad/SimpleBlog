using System;
using System.Collections.Generic;

namespace SimpleBlog.Infrastructure.Models;

public partial class UserRole
{
    public string Id { get; set; }

    public string RoleName { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
