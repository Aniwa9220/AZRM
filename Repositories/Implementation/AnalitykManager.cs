using Microsoft.Build.Experimental.ProjectCache;
using AZRM2023v1.Models.Domain;
using AZRM2023v1.Models.SWD2;
using AZRM2023v1.Models.DTO;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Collections.Generic;
using NuGet.Protocol;

namespace AZRM2023v1.Repositories.Implementation
{
    public class AnalitykManager
    {
        public List<VChorobyBezdomni> GetNN()
        {
            var context = new SWD2Context();
            var view = context.VChorobyBezdomnis.ToList<VChorobyBezdomni>();
            return view;
        }

        public List<VChorobyWszyscy> GetAll()
        {
            var context = new SWD2Context();
            var view = context.VChorobyWszyscies.ToList<VChorobyWszyscy>();
            return view;
        }
        public List<ViewWord> GetVW()
        {
            var context = new SWD2Context();
            var view = context.views2.ToList<ViewWord>();
            return view;
        }

        public List<VIleRazyPacjent> GetPax()
        {
            var context = new SWD2Context();
            var view = context.VIleRazyPacjents.ToList<VIleRazyPacjent>();
            return view;
        }

        public List<VDaneWyjazdSexualAbuse> GetSA()
        {
            var context = new SWD2Context();
            var view = context.VDaneWyjazdSexualAbuses.ToList<VDaneWyjazdSexualAbuse>();
            return view;
        }

        public List<VPracownicyWgdatyUzytoRezerwa> GetPR()
        {
            var context = new SWD2Context();
            var view= context.VPracownicyWgdatyUzytoRezerwas.ToList<VPracownicyWgdatyUzytoRezerwa>();

            return view;
        }

        public List<VObsadaStanowiskWgdaty> GetOB()
        {
            var context = new SWD2Context();
            var view = context.VObsadaStanowiskWgdaties.ToList<VObsadaStanowiskWgdaty>();

            return view;
        }

        public List<VHistoriaTransportu> GetHT()
        {
            var context = new SWD2Context();
            var view = context.VHistoriaTransportus.ToList<VHistoriaTransportu>();

            return view;
        }

        public List<VIlInterwencjiPracownika> GetIleP()
        {
            var context = new SWD2Context();
            var view = context.VIlInterwencjiPracownikas.ToList<VIlInterwencjiPracownika>();

            return view;
        }

        public List<VIlIntPracownikSklad> GetSP()
        {
            var context = new SWD2Context();
            var view = context.VIlIntPracownikSklads.ToList<VIlIntPracownikSklad>();

            return view;
        }

        public List<VIlPracyPracownika> GetPP()
        {
            var context = new SWD2Context();
            var view = context.VIlPracyPracownikas.ToList<VIlPracyPracownika>();

            return view;
        }

        public List<VIleRazyPacjent> GetPPax()
        {
            var context = new SWD2Context();
            var view = context.VIleRazyPacjents.ToList<VIleRazyPacjent>();

            return view;
        }

        public List<VListazgłoszeń> GetZG()
        {
            var context = new SWD2Context();
            var view = context.VListazgłoszeńs.ToList<VListazgłoszeń>();

            return view;
        }

        public List<VStatystykaPrmZałóg> GetSPRM()
        {
            var context = new SWD2Context();
            var view = context.VStatystykaPrmZałógs.ToList<VStatystykaPrmZałóg>();

            return view;
        }
    }
}
