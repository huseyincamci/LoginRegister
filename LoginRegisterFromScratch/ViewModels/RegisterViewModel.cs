using System.ComponentModel.DataAnnotations;

namespace LoginRegisterFromScratch.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public short Age { get; set; }
        public string Address { get; set; }
    }
}