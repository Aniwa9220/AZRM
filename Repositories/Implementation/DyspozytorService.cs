using AZRM2023v1.Models.Domain;
using AZRM2023v1.Models.SWD2;
using AZRM2023v1.Repositories.Abstract;
using NuGet.DependencyResolver;

namespace AZRM2023v1.Repositories.Implementation
{
    public class DyspozytorService
    {
        public IEnumerable<ApplicationUser> Users { get; set; }
       
    }
}
