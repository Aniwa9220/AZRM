using System;
using System.Collections.Generic;

namespace AZRM2023v1.Models.SWD2
{
    public partial class VChorobyWszyscy
    {
        public int Idzgłoszenia { get; set; }
        public string? Pacjent { get; set; }
        public int NumerZałogi { get; set; }
        public DateTime DataInterwencji { get; set; }
        public string JednostkaChorobowa { get; set; } = null!;
        public string? SzpitalDocelowy { get; set; }
    }
}
