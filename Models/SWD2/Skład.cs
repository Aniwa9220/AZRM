using AZRM2023v1.Models.Domain;
using MessagePack;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using NuGet.Packaging.Signing;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace AZRM2023v1.Models.SWD2
{
    public class Skład
    {

        public int Porzadkowa { get; set; }
        public int Idskład { get; set; }
        public string? Typskładu { get; set; }
        public DateTime Dzieńpracy { get; set; }
        public int Idpracownika { get; set; }
        

        public virtual Grafik Grafik { get; set; } = null!;
        public virtual AspNetUsers IdpracownikaNavigation { get; set; } = null!;
        public virtual List<ApplicationUser> Squadcrew { get; set;}
       
        public Skład squadcrew ()
        {
            var UsersContext = new DataBaseContext();
            var Model = new Skład();
           Model.Squadcrew = UsersContext.Users.ToList();

            return Model;

        }

    }
    
}
