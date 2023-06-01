using System.ComponentModel.DataAnnotations;

namespace AZRM2023v1.Models.DTO
{
    public class RegistrationModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*[a-z])(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Minimum 6 znaków, 1 Duża litera, 1 mała i 1 znak specjalny")]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
        [Required]
        public string? Rola { get; set; }
        [Required]
        public int? Idfunkcji { get; set; }
      
    }
}
