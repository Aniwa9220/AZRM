﻿using System;
using System.Collections.Generic;

namespace AZRM2023v1.Models.SWD2
{
    public partial class AspNetRole
    {
        public AspNetRole()
        {
            AspNetRoleClaims = new HashSet<AspNetRoleClaim>();
            Users = new HashSet<AspNetUsers>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }

        public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; }

        public virtual ICollection<AspNetUsers> Users { get; set; }
    }
}
