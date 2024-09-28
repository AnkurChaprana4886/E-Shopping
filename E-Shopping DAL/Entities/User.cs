using System;
using System.Collections.Generic;

namespace E_Shopping_DAL.Entities;

public partial class User
{
    public long UserId { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string? Email { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public bool? IsPhoneVerified { get; set; }

    public bool? IsEmailVerified { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public string? Status { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public virtual Address? Address { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Vendor? Vendor { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
