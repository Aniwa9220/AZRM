using System;
using System.Collections.Generic;

namespace AZRM2023v1.Models.SWD2
{
    public partial class AspNetUserClaim
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }

        public virtual AspNetUsers User { get; set; } = null!;
    }
}
