using Microsoft.AspNetCore.Http;

namespace Medium.DTOs
{
    public class EditProfileDTo
    {
        
        public string Username { get; set; }

        public string Password { get; set; }
   
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
        public string? Website { get; set; }
        public string? AboutMe { get; set; }
        public string? Image { get; set; }
        public IFormFile? UploadImage { get; set; }
    }
}