using System;
using System.Collections.Generic;

namespace AZRM2023v1.Models.SWD2
{
    public partial class VIlInterwencjiPracownika
    {
        public int? Id { get; set; }
        public string Pracownik { get; set; } = null!;
        public int? IleInterwencji { get; set; }
    }
}
