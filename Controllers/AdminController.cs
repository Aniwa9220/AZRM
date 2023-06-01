using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using AZRM2023v1.Models.SWD2;
using Microsoft.Build.Experimental.ProjectCache;
using AZRM2023v1.Models.Domain;
using Microsoft.Build.Framework;
using AZRM2023v1.Repositories.Implementation;
using AZRM2023v1.Repositories.Abstract;
using AZRM2023v1.Controllers;
using System.Linq;

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;


namespace AZRM2023v1.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        public IActionResult Error()
        {
            return View();
        }
        
        readonly UserManager<ApplicationUser> _userManager;
        public AdminController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Display()
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

        public IActionResult AdminDB()
        {
            try
            {
                  var manager = new AdminManager();
                 var BackUp = manager.BackUpList();
                manager.p2 = BackUp;
                 return View(manager);
                
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
        public IActionResult Backup()
        {
            try
            {
                var manager = new AdminManager();
                manager.BackupSWD();
                return View("Display");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }





        [HttpPost]
        public IActionResult LoadBackUp(string wybranyBak)
        {
            try
            {
                var manager = new AdminManager();
                manager.LoadBackUp(wybranyBak);
                return RedirectToAction("Display");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult ListaSzpitali()
        {
            try
            {
                var manager = new AdminManager();
                var szpital = manager.GetSzpitals();


                return View(szpital);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public IActionResult AddSzpital()
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
        
        public IActionResult AddSzpital(Szpital szpital)
        {
            try
            {
                var manager = new AdminManager();
                manager.AddSzpital(szpital);

                return RedirectToAction("ListaSzpitali");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }



        [HttpGet]
        public IActionResult RemoveSzpital(int id)
        {
            try
            {
                var manager = new AdminManager();
                var szpital = manager.GetSzpital(id);
                return View(szpital);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        public IActionResult RemoveConfirmSzpital(int id)
        {
            try
            {
                var manager = new AdminManager();
                manager.RemoveSzpital(id);
                return RedirectToAction("ListaSzpitali");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
        [HttpGet]
        public IActionResult EditSzpital(int id)
        {
            try
            {
                var manager = new AdminManager();
                var szpital = manager.GetSzpital(id);
                return View(szpital);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        public IActionResult EditSzpital(Szpital szpital)
        {
            try
            {
                var manager = new AdminManager();
                manager.UpdateSzpital(szpital);
                return RedirectToAction("ListaSzpitali");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult DetailsSzpital(int id)
        {
            try
            {
                var manager = new AdminManager();
                var szpital = manager.GetSzpital(id);

                return View(szpital);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public IActionResult RemovePracownik(string id)         {
            try
            {
                var user = _userManager.Users.FirstOrDefault(x => x.Id == id);


                return View(user);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult<IEnumerable<ApplicationUser>> ListaPracownikow()
        {
            try
            {
                var users = _userManager.Users.ToList<ApplicationUser>();

                return View(users);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> RemovePracownika(string id) 
        {
            try
            {

                ApplicationUser user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    IdentityResult result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("ListaPracownikow");
                    else
                        return View(result);
                }
                else
                    ModelState.AddModelError("", "User Not Found");
                return View("Display", _userManager.Users);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> UpdatePracownik(string id)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                if (user != null)
                    return View("EditPracownik");
                else
                    return RedirectToAction("ListaPracownikow");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditPracownik(string id, string email, string name, string username, string rola, int funkcja)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {

                    if (!string.IsNullOrEmpty(email))
                        user.Email = email;
                    else
                        ModelState.AddModelError("", "Uzupełnij pole mail");



                    if (!string.IsNullOrEmpty(email))
                    {
                        IdentityResult result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded)
                            return RedirectToAction("Index");
                        else
                            Errors(result);
                    }

                    if (!string.IsNullOrEmpty(name))
                        user.Name = name;
                    else
                        ModelState.AddModelError("", "Uzupełnij pole Name");



                    if (!string.IsNullOrEmpty(name))
                    {
                        IdentityResult result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded)
                            return RedirectToAction("ListaPracownikow");
                        else
                            Errors(result);
                    }

                    if (!string.IsNullOrEmpty(username))
                        user.UserName = username;
                    else
                        ModelState.AddModelError("", "Uzupełnij pole Username");



                    if (!string.IsNullOrEmpty(username))
                    {
                        IdentityResult result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded)
                            return RedirectToAction("ListaPracownikow");
                        else
                            Errors(result);
                    }

                    if (!string.IsNullOrEmpty(rola))
                        user.Rola = rola;
                    else
                        ModelState.AddModelError("", "Uzupełnij pole Rola");

                    if (!string.IsNullOrEmpty(rola))
                    {
                        IdentityResult result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded)
                            return RedirectToAction("ListaPracownikow");
                        else
                            Errors(result);
                    }

                    if (funkcja != 4)
                        user.Idfunkcji = funkcja;
                    else
                        ModelState.AddModelError("", "Uzupełnij pole funckja");

                    if (!string.IsNullOrEmpty(rola))
                    {
                        IdentityResult result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded)
                            return RedirectToAction("ListaPracownikow");
                        else
                            Errors(result);
                    }
                }
                else
                    ModelState.AddModelError("", "Użytkownika nie znaleziono");
                return View(user);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public async Task<IActionResult> DetailsPracownik(string id)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                return View(user);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }

        }

        private void Errors(IdentityResult result)
        {

            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }


        public IActionResult ListaSkładów()
        {
            try
            {
                var manager = new AdminManager();
                var skład = manager.GetSklads();


                return View(skład);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult SkladFiltering()
        {
            try { 
            return View();
                }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Skład>> IndexSklad()
        {
            try
            {
                var manager = new AdminManager();
                var index = manager.GetSklads();
                return View(index);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }

        }

        [HttpPost]
        public ActionResult<IEnumerable<Skład>> IndexSklad(string t)
        {
            try
            {
                var manager = new AdminManager();
                var index = manager.GetIndex(t);
                return View(index);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public ActionResult SkladFiltering_GetSklad(int? t)
        {
            try
            {

                var SWD = new SWD2Context();

                var sklad = SWD.Składs.Select(sklad => new Skład
                {
                    Idskład = sklad.Idskład,
                    Dzieńpracy = sklad.Dzieńpracy,
                    Idpracownika = sklad.Idpracownika,
                    Typskładu = sklad.Typskładu


                });
                if (t == 0 || t == null)
                { return RedirectToAction("Error"); }

                else if (t != 0)
                {
                    sklad = sklad.Where(s => s.Idskład.Equals(t));
                }
              

                return View(sklad);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public IActionResult AddSkład()
        {
            try
            {
                var UsersContext = new DataBaseContext();
                var Model = new Skład();
                Model.Squadcrew = UsersContext.Users.ToList();
                return View(Model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public IActionResult AddSkład(Skład skład)
        {
            try
            {
                var manager = new AdminManager();
                manager.AddSkład(skład);

                return RedirectToAction("IndexSklad");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }

        }



        [HttpGet]
        public IActionResult RemoveSkład(int id)
        {
            try
            {
                var manager = new AdminManager();
                var skład = manager.GetSklad(id);
                return View(skład);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        public IActionResult RemoveConfirmSkład(int id)
        {
            try
            {
                var manager = new AdminManager();
                manager.RemoveSkład(id);
                return RedirectToAction("IndexSklad");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
        [HttpGet]
        public IActionResult EditSkład(int id)
        {
            try
            {

                var manager = new AdminManager();

                var s = manager.GetSklad(id);
                return View(s);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        public IActionResult EditSkład(Skład skład)
        {
            try
            {
                var manager = new AdminManager();
                manager.EditSkład(skład);
                return RedirectToAction("IndexSklad");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult Grafik()
        {
            try
            {
                var manager = new AdminManager();
                var grafik = manager.GetGrafiks();


                return View(grafik);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
        [HttpGet]
        public IActionResult AddGrafik()
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
        
        public IActionResult AddGrafik(Grafik grafik)
        {
            try
            {
                var manager = new AdminManager();
                manager.AddGrafik(grafik);

                return RedirectToAction("Grafik");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }

        }
        [HttpGet]
        public IActionResult EditGrafik(int id)
        {
            try
            {
                var manager = new AdminManager();
                var grafik = manager.GetGrafik(id);
                return View(grafik);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }

        }

        [HttpPost]
        public ActionResult EditGrafik(Grafik grafik)
        {
            try
            {
                var manager = new AdminManager();
                manager.UpdateGrafik(grafik);

                return RedirectToAction("Grafik");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }

        }


        [HttpGet]
        public IActionResult RemoveGrafik(int id)
        {
            try
            {
                var manager = new AdminManager();
                var grafik = manager.GetGrafik(id);
                return View(grafik);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        public IActionResult RemoveConfirmGrafik(int id)
        {
            try
            {
                var manager = new AdminManager();
                manager.RemoveGrafik(id);
                return RedirectToAction("Grafik");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
    }

}





