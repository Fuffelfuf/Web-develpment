using System;
using System.ComponentModel.DataAnnotations;

namespace MyApi.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "First name cannot be longer than 15 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Last name cannot be longer than 15 characters.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public DateTime LastLoginDate { get; set; }

        public int FailedLoginAttempts { get; set; }
    }
}