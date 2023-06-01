using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AZRM2023v1.Models.DTO
{
    public class ChangePasswordModel
    {
       
            [Required]
            public string? CurrentPassword { get; set; }
            [Required]
            public string? UserName { get; set; }   
            [Required]
            [RegularExpression("^(?=.*?[A-Z])(?=.*[a-z])(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Minimum 6 znaków, 1 Duża litera, 1 mała i 1 znak specjalny")]
            public string? NewPassword { get; set; }
            [Required]
            [Compare("NewPassword")]
            public string? PasswordConfirm { get; set; }

          


        
    }

}
