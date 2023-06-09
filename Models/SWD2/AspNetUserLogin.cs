﻿using System;
using System.Collections.Generic;

namespace AZRM2023v1.Models.SWD2
{
    public partial class AspNetUserLogin
    {
        public string LoginProvider { get; set; } = null!;
        public string ProviderKey { get; set; } = null!;
        public string? ProviderDisplayName { get; set; }
        public string UserId { get; set; } = null!;

        public virtual AspNetUsers User { get; set; } = null!;
    }
}
