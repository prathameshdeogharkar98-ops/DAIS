using System.ComponentModel.DataAnnotations;

namespace UserAccessManagement.Entities.DTOs
{
    public class UserDTO
    {
        public string CustomerId { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? MobileNumber { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsEmailVerified { get; set; }
        public bool IsMobileVerified { get; set; }
        public int Status { get; set; }
        public string LoggedInUser { get; set; } = string.Empty;
    }
}
