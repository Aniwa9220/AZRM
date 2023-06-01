using AZRM2023v1.Models.Domain;
using AZRM2023v1.Models.SWD2;
using AZRM2023v1.Repositories.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data.SqlTypes;
using System.Net;
using System.Net.NetworkInformation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace AZRM2023v1.Controllers
{
    public class DyspozytorController : Controller
    {

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Display()
        {
            return View();
        }

       
            
        
        public IActionResult Int()
        {
            try
            {
                var manager = new AnalitykManager();
                var index = manager.GetVW().ToList();
                return View(index);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        
        public IActionResult asap24h()
        {
            try
            {

                var manager = new DyspozytorManager();
                var index = manager.GetZlecenia24(); 
                return View(index);

           }
           catch (Exception ex)
           {
               return RedirectToAction("Error");
            }
        }
       
       
       
        public ActionResult Complete(int id)
        {
            try
            {
                var manager = new DyspozytorManager();
                var call = manager.GetZlecenie(id);
                call.Datazamkniecia = DateTime.Now;
                manager.UpdateZlecenie(call);
                return RedirectToAction("asap24h");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
        [HttpGet]
        public IActionResult AddZlecenie()
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
        [HttpGet]
        public IActionResult Find1()
        {
            try
            {
                using (var context = new SWD2Context())
                {


                    var v = context.views.FromSqlRaw("select P.nazwisko as nazwisko, P.Imie as imie, I.Opis as choroba, Z.datazgłoszenia as Przyjecie\r\n\t\tfrom Zgłoszenie Z inner join Szpital Sz on Sz.IDszpitala=Z.IDszpitala \r\n\t\tinner join Pacjent P on Z.IDpacjenta=P.IDpacjenta \r\n\t\tinner join KARTACHOROBY K on K.iDpacjenta=P.IDpacjenta\r\n\t\tinner join ICD10 I on I.IDicd10=K.IDicd10").ToList();

                    return View(v);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult Find0()
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
        public IActionResult Find1(string szukana)
        {
            try
            {
                using (var context = new SWD2Context())
                {


                    var v = context.views.FromSqlRaw("select * from Pac_Szp('" + szukana + "');").ToList();

                    return View(v);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
        [HttpGet]
        public IActionResult Find2()
        {
            try
            {
                using (var context = new SWD2Context())
                {


                    var v = context.views2.FromSqlRaw("select P.nazwisko as nazwisko, P.Imie as imie, I.Opis as choroba, Z.datazgłoszenia as Przyjecie,\r\n\t\tsz.nazwa as nazwa_szpitala, Z.danezgłoszenia as szczegóły_pogotowia\r\n\t\tfrom Zgłoszenie Z inner join Szpital Sz on Sz.IDszpitala=Z.IDszpitala \r\n\t\tinner join Pacjent P on Z.IDpacjenta=P.IDpacjenta \r\n\t\tinner join KARTACHOROBY K on K.iDpacjenta=P.IDpacjenta\r\n\t\tinner join ICD10 I on I.IDicd10=K.IDicd10").ToList();

                    return View(v);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }


        [HttpPost]
        public IActionResult Find2(string szukana)
        {
            try
            {
                using (var context = new SWD2Context())
                {


                    var v = context.views2.FromSqlRaw("select * from znajdz_slowo('" + szukana + "');").ToList();

                    return View(v);
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public IActionResult Find3()
        {
            try
            {
                using (var context = new SWD2Context())
                {


                    var v = context.views3.FromSqlRaw("select P.Name as Imie_Nazwisko, P.UserName as Log_in, F.stanowisko as stanowisko,\r\n\t\tS.IDskład as ID_składu, S.typskładu as typ_składu, S.dzieńpracy as Dzień_pracy\r\n\t\tfrom Skład S inner join AspNetUsers P on S.IDpracownika=P.IDpracownika \r\n\t\tinner join Funkcja F on F.IDfunkcji=P.IDfunkcji\r\n\t\tgroup by S.typskładu, S.IDskład, S.dzieńpracy, P.Name, P.UserName, F.stanowisko").ToList();

                    return View(v);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }


        [HttpPost]
        public IActionResult Find3(string szukana)
        {

            try
            {

                using (var context = new SWD2Context())
                {


                    var v = context.views3.FromSqlRaw("select * from Kto_na_miasto('" + szukana + "');").ToList();

                    return View(v);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
        [HttpGet]
        public IActionResult GetZlecenieSingle(int id)

        {
            try
            {
                using (var context = new SWD2Context())
                {

                    var manager = new DyspozytorManager();
                    var call = manager.GetZlecenie(id);

                    return View(call);

                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }


    
        public ActionResult UpdateZgloszenie2(Zgłoszenie call)
        {
            try
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .Build();

                string connectionString = configuration.GetConnectionString("conn");
                SqlConnection sqlconn = new SqlConnection(connectionString);
                SqlCommand sqlcmd = new SqlCommand();
                sqlconn.Open();
                sqlcmd = new SqlCommand("UPDATE ZGŁOSZENIE SET IDSKŁAD=" + call.Idskład + "WHERE IDzgłoszenia=" + call.Idzgłoszenia + ";", sqlconn);
                sqlcmd.ExecuteNonQuery();
                sqlconn.Close();

                return RedirectToAction("asap24h");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }


        }
       

        public IActionResult UpdateZgloszenie(int id)
        {
            try
            {
                using (var context = new SWD2Context())
                {

                    var manager = new DyspozytorManager();
                    var call = manager.GetZlecenie(id);


                    var index = manager.UpdateZlecenie(call);
                    return RedirectToAction("asap24h");

                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult DetailsZgloszenie(int id)
        {
            try
            {
                var manager = new DyspozytorManager();
                var zgloszenie = manager.GetZlecenie(id);

                return View(zgloszenie);

            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
        [HttpGet]
        public IActionResult RemoveZgloszenie(int id)
        {
            try
            {
                var manager = new DyspozytorManager();
                var zgloszenie = manager.GetZlecenie(id);

                return View(zgloszenie);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        public ActionResult ConfirmRemoveZgloszenie(int id)
        {
            try
            {
                var manager = new DyspozytorManager();
                manager.RemoveZlecenie(id);

                return RedirectToAction("asap24h");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult SWDv1()
        {
            return View();
        }

        public IActionResult SWDv2()
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

        public IActionResult GeoMethod()
        {
            try
            {
                var dyspozytor = new DyspozytorManager();

                var json = dyspozytor.CreateGeoJson();

                return new ContentResult
                {
                    Content = json,
                    ContentType = "application/json"
                };
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }



        public ActionResult Rejestrujsie(Pacjent pacjent, Kartachoroby karta, Zgłoszenie zgłoszenie) 
        {
            try
            {
                var manager = new DyspozytorManager();

                manager.AddPax(pacjent);



                karta.IDpacjenta = pacjent.Idpacjenta;
                manager.AddKarta(karta);
                zgłoszenie.Datarejestracji = DateTime.Now;
                zgłoszenie.Idpacjenta = karta.IDpacjenta;
                zgłoszenie.Idkartychoroby = karta.Idkarty;
                zgłoszenie.Idskład = 100;
                zgłoszenie.Datazgłoszenia = DateTime.Now;
                manager.AddZlecenie(zgłoszenie);

                return RedirectToAction("asap24h");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
        }
    }
