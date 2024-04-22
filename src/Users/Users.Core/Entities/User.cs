using Shared.Common;
using Users.Core.Enums;

namespace Users.Core.Entities;

public sealed class User : BaseEntity
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LatName { get; set; }
    public string Email { get; set; }
    public string HashedPassword { get; set; }
    public bool EmailConfirmed { get; set; }
    public Role Role { get; set; }
}