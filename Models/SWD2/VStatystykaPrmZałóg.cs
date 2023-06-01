using System;
using System.Collections.Generic;

namespace AZRM2023v1.Models.SWD2
{
    public partial class VStatystykaPrmZałóg
    {
        public int? LiczbaInterwencji { get; set; }
        public int NumerZałogi { get; set; }
        public string JednostkaChorobowa { get; set; } = null!;
        public string? SzpitalDocelowy { get; set; }
    }
}
