
using AZRM2023v1.Models.DTO;
using AZRM2023v1.Models.SWD2;
using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace AZRM2023v1.Models.Domain
{
    public class DataBaseContext : IdentityDbContext<ApplicationUser>
    {



        public DbSet<ApplicationUser> Users { get; set; }
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        public DataBaseContext()
        {
        }
   

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
         
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                string connectionString = configuration.GetConnectionString("conn");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }


     
    }
}
    
         
    

