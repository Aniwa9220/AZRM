using Aspose.Gis;
using Aspose.Gis.SpatialReferencing;
using AZRM2023v1.Models.Domain;
using AZRM2023v1.Models.DTO;
using AZRM2023v1.Models.SWD2;
using AZRM2023v1.Repositories.Abstract;
using Azure.Core.GeoJson;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace AZRM2023v1.Repositories.Implementation
{
    public class DyspozytorManager
    {


        public DyspozytorManager() { }
        public List<Zgłoszenie> GetZlecenias()
        {
            var context = new SWD2Context();
            var view = context.Zgłoszenies.ToList();
            return view;
        }

        public ZgłoszenieASAP GetZlecenia24()
        {
            ZgłoszenieASAP zg = new();
            var view = zg.get24();
            zg.Głos = view;
            return zg;
               
        }

        public DyspozytorManager AddZlecenie(Zgłoszenie call)
        {
            using (var context = new SWD2Context())
            {

                context.Zgłoszenies.Add(call);
                context.SaveChanges();


            }
            return this;
        }
        [HttpPost]
        public void RemoveZlecenie(int id)
        {
            using (var context = new SWD2Context())
            {
                var DoUsun = context.Zgłoszenies.Single(x => x.Idzgłoszenia == id);
                context.Zgłoszenies.Remove(DoUsun);
                context.SaveChanges();


            }
        }
        [HttpGet]
        public Zgłoszenie GetZlecenie(int id)
        {

            using (var context = new SWD2Context())
            {
                var call = context.Zgłoszenies.Single(x => x.Idzgłoszenia == id);
                return call;

            }

        }

        public Zgłoszenie GetZlecenie2(int id)
        {

            var context = new ZgłoszenieASAP();

            var call = context.Głos.SingleOrDefault(x => x.Idzgłoszenia == id);
            return call;

        }

        public DyspozytorManager UpdateZlecenie(Zgłoszenie call)
        {
            using (var context = new SWD2Context())
            {






                context.Zgłoszenies.Update(call);
                context.SaveChanges();
            }
            return this;
        }


        public DyspozytorManager FindPax(string szpital)

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
            sqlcmd = new SqlCommand("if exists dbo.view1 Drop view view1 ELSE CREATE VIEW view1 as select * from Pac_Szp('" + szpital + "');", sqlconn);
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            return this;



        }

        public List<ViewAll> GetView()
        {
            var context = new SWD2Context();
            var view = context.views.ToList<ViewAll>();
            return view;
        }

        public List<Skład> GetSklads()
        {
            var context = new SWD2Context();
            var view = context.Składs.ToList<Skład>();
            return view;
        }

        public DyspozytorManager AddPax(Pacjent pacjent)
        {
            using (var context = new SWD2Context())
            {

                context.Pacjents.Add(pacjent);
                context.SaveChanges();

            }
            return this;

        }

        public DyspozytorManager AddKarta(Kartachoroby karta)
        {
            using (var context = new SWD2Context())
            {

                context.Kartachorobies.Add(karta);
                context.SaveChanges();

            }
            return this;

        }
        public static string ConvertDataTableToString(DataTable dataTable)
        {
            var output = new StringBuilder();

            var columnsWidths = new int[dataTable.Columns.Count];

            
            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    var length = row[i].ToString().Length;
                    if (columnsWidths[i] < length)
                        columnsWidths[i] = length;
                }
            }

            
            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    var text = row[i].ToString();
                    output.Append( PadCenter(text, columnsWidths[i] + 2));
                }
        
            }
            return output.ToString();
        }

        private static string PadCenter(string text, int maxLength)
        {
            int diff = maxLength - text.Length;
            return new string(' ', diff / 2) + text + new string(' ', (int)(diff / 2.0 + 0.5));

        }
        private DataTable dataTable = new DataTable();
      
       static string ReplaceHexadecimalSymbols(string txt)
        {
            string r = "[\x00-\x08\x0B\x0C\x0E-\x1F\x26]";
            return Regex.Replace(txt, r, "", RegexOptions.Compiled);
        }

        
        public string CreateGeoJson()
        {

            ConversionOptions options = null;
            //Wgs84
            if (Drivers.Shapefile.SupportsSpatialReferenceSystem(SpatialReferenceSystem.Wgs84))
            {
                options = new ConversionOptions()
                {
                    DestinationSpatialReferenceSystem = SpatialReferenceSystem.Wgs84,
                };
            }
            var queryWithForJson = "use swd2;\r\nDECLARE @featureList nvarchar(max) =\r\n(\r\n\tSELECT \r\n\t\t'Point'\t\t\t\t\t\t\t\t\t\t\t\tas 'geometry.type',\r\n\r\n\t\tJSON_QUERY\r\n ( FORMATMESSAGE('[%s,%s]', dl, szer)) as [geometry.coordinates],\r\n\r\n \t\r\n\t\tCast(IDgps as varchar)                              as 'properties.ID',\r\n\t\tCast(Status as varchar)                             as 'properties.Status',\r\n\t\t'Feature'                                           as 'type'\r\n\tFROM viewDyspoPBI\r\n\t\tFOR JSON PATH\r\n)\r\n \r\nDECLARE @featureCollection nvarchar(max) = (\r\n\tSELECT 'FeatureCollection' as 'type',\r\n\tJSON_QUERY(@featureList)   as 'features'\r\n\tFOR JSON PATH, WITHOUT_ARRAY_WRAPPER\r\n)\r\n \r\nSELECT @featureCollection\r\n\r\n";

            IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .Build();

            string connectionString = configuration.GetConnectionString("conn");
        

            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(queryWithForJson, conn))
                {
                    conn.Open();
                   
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dataTable);
                    conn.Close();
                    da.Dispose();
                    
                }


              
                foreach (DataRow row in dataTable.Rows)
                {
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        if (dataTable.Columns[i].DataType == typeof(string))
                            row[i] = ReplaceHexadecimalSymbols((string)row[i]);
                    }
                }
                string jsonString = string.Empty;
                
                
                jsonString = ConvertDataTableToString(dataTable);
                
                File.WriteAllText(@"C:\SQLBackUpFolder\data.geojson", jsonString);

               
                return jsonString;
            }

            
        }
       

               
            


                
        
    }
}

           
        
    

