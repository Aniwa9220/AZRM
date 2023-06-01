using Microsoft.Build.Experimental.ProjectCache;
using AZRM2023v1.Models.Domain;
using AZRM2023v1.Models.SWD2;
using AZRM2023v1.Models.DTO;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlTypes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Data.SqlClient;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace AZRM2023v1.Repositories.Implementation
{
    public class AdminManager
    {

        
        public AdminManager() { }
        public List<string> p2;

        public string wybranyBak { get; set; }

        public async Task<Status> LoadBackUp(string wybranyBak)
        {
            var status = new Status();

           try
            {

                IConfigurationRoot configuration = new ConfigurationBuilder()
                            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .Build();

                string connectionString = configuration.GetConnectionString("conn");
                SqlConnection sqlconn = new SqlConnection(connectionString); 
                SqlCommand sqlcmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                sqlconn.Open();
                string timebak = wybranyBak.Substring(wybranyBak.Length - 23);
                //  string time = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                sqlcmd = new SqlCommand("USE [master]; ALTER DATABASE [SWD2] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; "// +
                 //    "BACKUP LOG [SWD2] TO DISK = 'C:\\SQLBackUpFolder\\SWD2_LogBackup_" + timebak +
                 //     "' WITH NOFORMAT, NOINIT, NAME = N'SWD2_LogBackup_" + timebak + "', NOSKIP, NOREWIND, NOUNLOAD,  NORECOVERY ,  STATS = 5;"    
                    +"RESTORE DATABASE [SWD2] FROM DISK = 'C:\\SQLBackUpFolder\\" + wybranyBak + "' WITH recovery, replace;" +//FILE = 1, NORECOVERY,  NOUNLOAD,  STATS = 5;" +
              //          "RESTORE LOG [SWD2] FROM DISK = 'C:\\SQLBackUpFolder\\SWD2_LogBackup_26022023_171429.Bak' WITH FILE = 1, NOUNLOAD, STATS = 5;" +
                     //   "RESTORE LOG [SWD2] FROM DISK = 'C:\\SQLBackUpFolder\\SWD2_TailLogBackup_" + timebak + "' WITH FILE = 1, NOUNLOAD, STATS = 5;" +
                        "ALTER DATABASE [SWD2] SET MULTI_USER;", sqlconn);
                // polecenia w Komentarzu daja opcje Backupu z Logiem transakcji niezakonczonych. 

                sqlcmd.ExecuteNonQuery();
              
                sqlconn.Close();
              status.Message = "Wczytanie backupu nastąpiło poprawnie";
                status.StatusCode = 1;
                return status;
            }
            catch (Exception ex)
            {
                status.Message = "Wczytanie backupu nie powidło się";
                status.StatusCode = 0;
                return status;
            }

            
        }


        public async Task<Status> BackupSWD()
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
                                      .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                      .Build();

            string connectionString = configuration.GetConnectionString("conn");
            SqlConnection sqlconn = new SqlConnection(connectionString);
            var status = new Status();
            SqlCommand sqlcmd, sqlcmd0 = new SqlCommand();

            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            
            string backupDestination = "C:\\SQLBackUpFolder";
           
            if (!System.IO.Directory.Exists(backupDestination))
            {
                System.IO.Directory.CreateDirectory("C:\\SQLBackUpFolder");
            }
            try
            {

                sqlconn.Open();
                string time = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

                sqlcmd = new SqlCommand("USE [master];" + "ALTER DATABASE [SWD2] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;"
                                         + "backup database [SWD2] to disk = N'" + backupDestination + "\\" +
                                        "AZRMDB-" + time + ".Bak' WITH NOFORMAT, NOINIT,  NAME = N'SWD2_AZRMDB_" + time + "', SKIP, NOREWIND, NOUNLOAD, STATS = 10;" +
                                        "ALTER DATABASE [SWD2] SET MULTI_USER;", sqlconn);
             
                sqlcmd.ExecuteNonQuery();
              
                sqlconn.Close();
                status.Message = "Backup AZRM DB został wykonany";
                status.StatusCode = 1;
                return status;
            }
            catch (Exception ex)
            {
                status.Message = "Backup database nie został wykonany";
                status.StatusCode = 0;
                return status;
            }



        }
   

        public List<string> BackUpList()
        {

            DirectoryInfo dirInfo = new DirectoryInfo(@"C:\\SQLBackUpFolder");
            List<FileInfo> files = dirInfo.GetFiles().ToList();
            List<string> pliki = files.Select(file => file.Name).ToList();
            
           
            return pliki;
        }
        public Szpital GetSzpital(int id)
        {
            using (var context = new SWD2Context())
            {
                var szpital = context.Szpitals.SingleOrDefault(x => x.Idszpitala == id);
                return szpital;

            }
        }

        public List<Szpital> GetSzpitals()
        {
            var context = new SWD2Context();
            var view = context.Szpitals.ToList<Szpital>();
            return view;
        }

        public AdminManager AddSzpital(Szpital szpital)
        {
            using (var context = new SWD2Context())
            {

                context.Szpitals.Add(szpital);
                context.SaveChanges();


            }
            return this;
        }
        public void RemoveSzpital(int id)
        {
            using (var context = new SWD2Context())
            {
                var SzpitalDoUsun = context.Szpitals.Single(x => x.Idszpitala == id);
                context.Szpitals.Remove(SzpitalDoUsun);
                context.SaveChanges();
            }
        }

        public AdminManager UpdateSzpital(Szpital szpital)
        {
            using (var context = new SWD2Context())
            {
                context.Szpitals.Update(szpital);
                context.SaveChanges();
            }
            return this;
        }

        public Skład GetSklad(int id)
        {
            using (var context = new SWD2Context())
            {
                
                var view = context.Składs.SingleOrDefault(x=> x.Porzadkowa==id);
                return(view);
             
            }
           
        }

        public List<Skład> GetSklads()
        {
           
            var context = new SWD2Context();
            var view = context.Składs.ToList();
            return view;
        }
      

            [HttpPost]
        public IEnumerable<Skład> GetIndex(string t)
        {
            var context = new SWD2Context();
            var view = context.Składs.Where(s=>s.Idskład==int.Parse(t)).AsEnumerable<Skład>();
            

            return view;

        }
        public AdminManager AddSkład(Skład skład)
        {
            using (var context = new SWD2Context())
            {
               
                context.Składs.Add(skład);
                context.SaveChanges();
            }


            return this;
        }
        public void RemoveSkład(int id)
        {
            using (var context = new SWD2Context())
            {
                var SkładDoUsun = context.Składs.Single(x => x.Porzadkowa == id);
                context.Składs.Remove(SkładDoUsun);
                context.SaveChanges();
            }
        }
        [HttpPost]
        public AdminManager EditSkład(Skład skład) 
        {

            using (var context = new SWD2Context())
            {
             
                context.Składs.Update(skład);
                context.SaveChanges();

            }
          
            return this;
        }

        public Grafik GetGrafik(int Lp)
        {
            using (var context = new SWD2Context())
            {
                var grafik = context.Grafiks.SingleOrDefault(x => x.Lp == Lp);
                return grafik;

            }
            
        }

        public List<Grafik> GetGrafiks()
        {
            var context = new SWD2Context();
            var view = context.Grafiks.ToList<Grafik>();
            return view;
        }

        public AdminManager AddGrafik(Grafik grafik)
        {
            using (var context = new SWD2Context())
            {

                context.Grafiks.Add(grafik);
                context.SaveChanges();


            }
            return this;
        }
        public void RemoveGrafik(int id)
        {
            using (var context = new SWD2Context())
            {
                var GDoUsun = context.Grafiks.Single(x => x.Lp == id);
                context.Grafiks.Remove(GDoUsun);
                context.SaveChanges();
            }
        }

        public AdminManager UpdateGrafik(Grafik grafik)
        {
            using (var context = new SWD2Context())
            {
                context.Grafiks.Update(grafik);
                context.SaveChanges();
            }
            return this;
        }
      
    }
}



