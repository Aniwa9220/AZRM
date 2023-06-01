using AZRM2023v1.Repositories.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace AZRM2023v1.Models.SWD2
{
    public partial class ViewCrew
    {
        public DateTime? Dzień_pracy { get; set; }
        public string? Imie_Nazwisko { get; set; }
        public string? Log_in { get; set; }
        public string? stanowisko { get; set; }
        public int? ID_składu { get; set; }
        public char? typ_składu { get; set; }
        
        

       public virtual List<ViewCrew>? view3s { get; set; }


    }






}
