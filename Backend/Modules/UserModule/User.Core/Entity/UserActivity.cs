using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Core.Entity;

public class UserActivity
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public UserModel User { get; set; } = default!;

    public DateOnly Date { get; set; }

    public int ActivityCount { get; set; }
    public int EarnedExperience { get; set; }
}