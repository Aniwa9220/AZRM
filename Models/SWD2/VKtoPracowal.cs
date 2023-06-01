using System;
using System.Collections.Generic;

namespace AZRM2023v1.Models.SWD2
{
    public partial class VKtoPracowal
    {
        public string Status { get; set; } = null!;
        public int? Id { get; set; }
        public string? Pracownik { get; set; }
        public int? IlePracował { get; set; }
    }
}
