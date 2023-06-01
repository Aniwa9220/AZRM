using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AZRM2023v1.Repositories.Implementation;
using ExcelDataReader;
using System.Data;
using Microsoft.EntityFrameworkCore;

using AZRM2023v1.Models;
using AZRM2023v1.Models.Domain;
using AZRM2023v1.Models.SWD2;
using AZRM2023v1.Models.DTO;
using Microsoft.Data.SqlClient;
using System.Drawing;




namespace AZRM2023v1.Controllers
{
    public class ImportIcd10Controller : Controller
    {
        IConfiguration configuration;
        IWebHostEnvironment _hostEnvironment;
        SWD2Context context= new SWD2Context();
        IExcelDataReader reader;

        public ImportIcd10Controller(IWebHostEnvironment hostEnvironment)
        {

            this._hostEnvironment = hostEnvironment;
        }
      
        public async Task<IActionResult> Icd10View()
        {
            var Icd10Details = await context.Icd10s.ToListAsync();
            return View(Icd10Details);
        }
        public async Task<IActionResult> Index()
        {
            var Icd10Details = await context.Icd10s.ToListAsync();
            return View(Icd10Details);
        }
        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {
           
            try
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                                   .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                   .Build();

                string connectionString = configuration.GetConnectionString("conn");

                if (file == null)
                    throw new Exception("nie odebrano pliku.");


             
                string dirPath = Path.Combine(path1: _hostEnvironment.WebRootPath, path2: "ICD10Slownik");
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

 
                string dataFileName = Path.GetFileName(file.FileName);

                string extension = Path.GetExtension(dataFileName);

                string[] allowedExtsnions = new string[] { ".xls", ".xlsx" };

                if (!allowedExtsnions.Contains(extension))
                    throw new Exception("NIeobsługiwany rodzaj pliku.");

                string saveToPath = Path.Combine(dirPath, dataFileName);

                using (FileStream stream = new FileStream(saveToPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var stream = new FileStream(saveToPath, FileMode.Open))
                {
                   
                    if (extension == ".xls")
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    else
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                    DataSet ds = new DataSet();
                    ds = reader.AsDataSet();
                    reader.Close();

                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable serviceDetails = ds.Tables[0];
                        SqlConnection con = new SqlConnection(connectionString);
                       con.Open();
                        string sql0 = @"EXEC ICDBEFORE";
                        string sql1 = @"EXEC ICDAFTER";
                        SqlCommand cmd = new SqlCommand(sql0, con) ;
                       cmd.ExecuteNonQuery();

                        for (int i = 1; i < serviceDetails.Rows.Count; i++)
                        {
                            Icd10 details = new Icd10();
                            details.Idicd10 = serviceDetails.Rows[i][0].ToString(); 
                            details.Opis = serviceDetails.Rows[i][1].ToString();
                            await context.Icd10s.AddAsync(details);
                            await context.SaveChangesAsync();
                            
                        }                    
                        cmd = new SqlCommand(sql1, con);
                        cmd.ExecuteNonQuery();
         
                        con.Close();
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel()
                {
                    ControllerName = this.RouteData.Values["controller"].ToString(),
                    ActionName = this.RouteData.Values["action"].ToString(),
                    ErrorMessage = ex.Message

                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateICD(IFormFile file)
        {

            try
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                .Build();

                string connectionString = configuration.GetConnectionString("conn");

                if (file == null)
                    throw new Exception("Plik nie został zwrócony");


                string dirPath = Path.Combine(path1: _hostEnvironment.WebRootPath, path2: "ICD10Slownik");
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

               
                string dataFileName = Path.GetFileName(file.FileName);

                string extension = Path.GetExtension(dataFileName);

                string[] allowedExtsnions = new string[] { ".xls", ".xlsx" };

                if (!allowedExtsnions.Contains(extension))
                    throw new Exception("Nieobsługiwany typ pliku.");

               
                string saveToPath = Path.Combine(dirPath, dataFileName);

                using (FileStream stream = new FileStream(saveToPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                
                using (var stream = new FileStream(saveToPath, FileMode.Open))
                {
                    if (extension == ".xls")
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    else
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                    DataSet ds = new DataSet();
                    ds = reader.AsDataSet();
                    reader.Close();

                    if (ds != null && ds.Tables.Count > 0)
                    {
                       
                        DataTable serviceDetails = ds.Tables[0];
                        SqlConnection con = new SqlConnection(connectionString);
                        con.Open();
                      
                        for (int i = 1; i < serviceDetails.Rows.Count; i++)
                        {
                            Icd10 details = new Icd10();
                            details.Idicd10 = serviceDetails.Rows[i][0].ToString();
                            details.Opis = serviceDetails.Rows[i][1].ToString();
                            context.Icd10s.Update(details);
                            await context.SaveChangesAsync();

                        }
                        con.Close();
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel()
                {
                    ControllerName = this.RouteData.Values["controller"].ToString(),
                    ActionName = this.RouteData.Values["action"].ToString(),
                    ErrorMessage = ex.Message

                });
            }
        }

    }
}
