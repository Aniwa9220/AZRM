using AZRM2023v1.Models.Domain;
using AZRM2023v1.Models.DTO;

namespace AZRM2023v1.Repositories.Abstract
{
    public interface IUserAuthenticationService
    {
        Task<Status> LoginAsync(LoginModel model);

        Task<Status> RegisterAsync(RegistrationModel model);

        Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username);
        Task<Status> ResetPasswordAsync(ChangePasswordModel model, string username);
        Task LogoutAsync();
   

    }
}
