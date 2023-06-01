using AZRM2023v1.Models.Domain;
using AZRM2023v1.Models.DTO;
using AZRM2023v1.Models.SWD2;
using AZRM2023v1.Repositories.Abstract;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;


namespace AZRM2023v1.Repositories.Implementation
{
    public class UserAuthenticationService : IUserAuthenticationService
    {


        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserAuthenticationService(RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        public async Task<Status> LoginAsync(LoginModel model)
        {


            var status = new Status();
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "niepoprawne dane";
                return status;
            }
          
            if (!await userManager.CheckPasswordAsync(user, model.Password))
            {
                status.StatusCode = 0;
                status.Message = "niewłaściwe hasło";
                return status;
            }

            var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, false, true);
            if (signInResult.Succeeded)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName)
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                status.StatusCode = 1;
                status.Message = "Logowanie zakończone sukcesem";
                return status;
            }
            else if (signInResult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "Użytkownik zablokowany";
                return status;
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "błąd logowania";
                return status;
            }



        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<Status> RegisterAsync(RegistrationModel model)
        {
            var status = new Status();
            var userExists = await userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "Użytkownik już istnieje";
                return status;
            }
            SWD2Context db = new SWD2Context();
            ApplicationUser user = new ApplicationUser
            {

                SecurityStamp = Guid.NewGuid().ToString(),
                Name = model.Name,
                Mail = model.Mail,
                UserName = model.UserName,
                EmailConfirmed = true,
                Rola = model.Rola,
                Password = model.Password,

                Idpracownika = db.AspNetUsers.Max(u => u.Idpracownika) + 1,
                Idfunkcji = model.Idfunkcji,
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "Tworzenie użytkownika nie udało się";
                return status;
            }
          

            if (!await roleManager.RoleExistsAsync(model.Rola))
                await roleManager.CreateAsync(new IdentityRole(model.Rola));
            if (await roleManager.RoleExistsAsync(model.Rola))
            {
                await userManager.AddToRoleAsync(user, model.Rola);
            }
            status.StatusCode = 1;
            status.Message = "Użytkownika zarejestrowano";
            return status;
        }

        public async Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username)
        {


            var status = new Status();

            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                status.Message = "Użytkownik nie istnieje";
                status.StatusCode = 0;
                return status;
            }
            var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                status.Message = "Zaktualizowano hasło";
                status.StatusCode = 1;

            }
            else
            {
                status.Message = "Błąd";
                status.StatusCode = 0;
            }
            return status;

        }
        
        public async Task<Status> ResetPasswordAsync(ChangePasswordModel model, string username)
        {
            using (var context = new DataBaseContext())
            {
                var status = new Status();
               

                var user = await userManager.FindByNameAsync(username);
                if (user == null)
                {
                    status.Message = "Użytkownik nie istnieje";
                    status.StatusCode = 0;
                    return status;
                }
                
                var def = "Azrm123!@#"; 
                model.NewPassword = def;
                model.CurrentPassword = user.Password;
                model.PasswordConfirm = model.NewPassword;
                var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    status.Message = "Hasło zostało zresetowane do postaci domyślnej.";
                    status.StatusCode = 1;

                }
                else
                {
                    status.Message = "Reset nie powiódł się";
                    status.StatusCode = 0;
                }
                return status;
            }

        }
     
        }
    }

    
