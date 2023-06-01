using System.ComponentModel.DataAnnotations;

namespace AZRM2023v1.Models.DTO
{
    public class LoginModel
    {
        [Required]
        public string UserName  { get; set; }
        [Required]
        public string Password { get; set; }
      

    }
}
