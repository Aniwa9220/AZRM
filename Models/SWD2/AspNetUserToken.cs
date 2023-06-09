﻿using System;
using System.Collections.Generic;

namespace AZRM2023v1.Models.SWD2
{
    public partial class AspNetUserToken
    {
        public string UserId { get; set; } = null!;
        public string LoginProvider { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Value { get; set; }

        public virtual AspNetUsers User { get; set; } = null!;
    }
}
