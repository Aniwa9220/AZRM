using System;
using System.Collections.Generic;

namespace AZRM2023v1.Models.SWD2
{
    public partial class VListazgłoszeń
    {
        public DateTime DataZgłoszenia { get; set; }
        public int IdZgłoszenia { get; set; }
        public int IdSkładuPrm { get; set; }
        public string? ImiePacjenta { get; set; }
        public string? NazwiskoPacjenta { get; set; }
    }
}
