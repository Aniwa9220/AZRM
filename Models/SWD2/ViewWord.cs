using AZRM2023v1.Repositories.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace AZRM2023v1.Models.SWD2
{
    public partial class ViewWord
    {
        public DateTime? Przyjecie { get; set; }
        public string? nazwisko { get; set; }
        public string? imie { get; set; }
        public string? choroba { get; set; }
        public string? nazwa_szpitala { get; set; }
        public string? szczegóły_pogotowia { get; set; }
        

       public virtual List<ViewWord>? view2s { get; set; }



    }






}
