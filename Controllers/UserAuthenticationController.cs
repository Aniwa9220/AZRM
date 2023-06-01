
using AZRM2023v1.Models.Domain;
using AZRM2023v1.Models.DTO;
using AZRM2023v1.Repositories.Abstract;
using AZRM2023v1.Repositories.Implementation;
using Azure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Threading.Tasks;


namespace AZRM2023v1.Controllers
{
    public class UserAuthentication : Controller
    {

        public IActionResult Error()
        {
            return View();
        }
        private readonly IUserAuthenticationService _service;
       
        public UserAuthentication(IUserAuthenticationService service)
        {
            this._service = service;
        }

        public IActionResult Registration()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                { return View(model); }

                var result = await _service.RegisterAsync(model);
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Registration));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }


        }

        public IActionResult Login()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var result = await _service.LoginAsync(model);
                if (result.StatusCode == 1)
                {
                    TempData["UserName"] = model.UserName;
                    TempData.Keep("UserName");
                    return RedirectToAction("Display", "Dashboard");
                }
                else
                {
                    TempData["msg"] = result.Message;
                    return RedirectToAction(nameof(Login));
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _service.LogoutAsync();
                return RedirectToAction(nameof(Login));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

       

      
        [Authorize]
        public IActionResult ChangePassword()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        [Authorize]
        [HttpPost]        
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);
                var result = await _service.ChangePasswordAsync(model, User.Identity.Name);
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(ChangePassword));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        [Authorize]
        public IActionResult ResetPassword()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(string username)
        {
            try
            { 
                 var model = new ChangePasswordModel();

                var result = await _service.ResetPasswordAsync(model, username);
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(ResetPassword));
            }
             catch (Exception ex)
            {
                return RedirectToAction("Error");
    }
}


    }
}