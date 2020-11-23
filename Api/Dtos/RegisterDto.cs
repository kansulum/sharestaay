using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class RegisterDto
    {
        // [Required]
        // public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
         ErrorMessage = "Password must have 1 Uppercase, 1 Lowercase, 1 number, 1 non alphanumeric and at least 6 characters")]
        public string Password { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        // public DateTime Birthday { get; set; }

        public string Gender { get; set; }

        public string Phone { get; set; }
        
        public string ContactEmail { get; set; }
    }
}