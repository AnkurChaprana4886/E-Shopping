using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_BAL.Models
{
    public class UserModel
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

    }
}
