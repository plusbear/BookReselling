using System.ComponentModel.DataAnnotations;

namespace Identity.DataTransferObjects
{
    public class UserForAuthenticationDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
