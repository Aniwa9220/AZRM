using Microsoft.AspNetCore.Identity;
using NuGet.DependencyResolver;

namespace AZRM2023v1.Models.Domain
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }    
        public string Mail { get; set; }    
        public string Password { get; set; }
        public string? Rola { get; set; }
        public int? Idpracownika { get; set; }
        public int? Idfunkcji { get; set; }  
    }
}
