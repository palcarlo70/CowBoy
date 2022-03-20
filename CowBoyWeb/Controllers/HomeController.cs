using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CowBoyEntityDto;
using CowBoyWeb.Models;
using Newtonsoft.Json;

namespace CowBoyWeb.Controllers
{
    public class HomeController : Controller
    {
        readonly JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult DettaglioBovini()
        //{

        //    return View();
        //}

        public ActionResult DettaglioBovini(int idBov)
        {
            var clCom = new ClassiComuni();
            var conn = new CowBoy.ComponentsNew.BoviniCom();
            var lst = conn.GetBovini(idBov, null, null, null, null, null, null, clCom.ConnectCbUniversal).FirstOrDefault();

            ViewBag.Bovino = new BoviniDto()
            {
                Id = lst.Id,
                MatricolaAsl = lst.MatricolaAsl,
                MatricolaAz = lst.MatricolaAz,
                Nome = lst.Nome,
                IdMadre = lst.IdMadre,
                MatricolaASLMadre = lst.MatricolaASLMadre,
                IdPadre = lst.IdPadre,
                MatricolaASLPadre = lst.MatricolaASLPadre,
                DataNascita = lst.DataNascita,
                DataNascitaStringa = lst.DataNascitaStringa,
                DataFine = lst.DataFine,
                DataFineStringa = lst.DataFineStringa,
                Note = lst.Note,
                IdParto = lst.IdParto,
                IdFoto = lst.IdFoto,
                ToroArtificiale = lst.ToroArtificiale,
                ToroDaMonta = lst.ToroDaMonta,
                Sesso = lst.Sesso,
                NomeFoto = string.IsNullOrEmpty(lst.NomeFoto) ? "imgNull.png" : lst.NomeFoto,
                FotoPrincipale = lst.FotoPrincipale,
                DataInAsciutta = lst.DataInAsciutta,
                DataInAsciuttaStringa = lst.DataInAsciuttaStringa,
                DataUltimoPartoStringa = lst.DataUltimoPartoStringa,
                MesiUltimoParto = lst.MesiUltimoParto,
                UltimoSalto = lst.UltimoSalto,
                UltimoSaltoStringa = lst.UltimoSaltoStringa,
                GiorniUltimoSalto = lst.GiorniUltimoSalto

            };

            return View();
        }




        public ContentResult GetBovini(int? idAnagrafica, string sesso, int? manze, int? inLattazione, int? inAsciutta, string ricercaLibera, int? inAzienda)
        {
            if (string.IsNullOrEmpty(sesso)) sesso = null;
            if (string.IsNullOrEmpty(ricercaLibera)) ricercaLibera = null;

            var clCom = new ClassiComuni();
            var conn = new CowBoy.ComponentsNew.BoviniCom();
            var lst = conn.GetBovini(idAnagrafica, sesso, manze, inLattazione, inAsciutta, ricercaLibera, inAzienda, clCom.ConnectCbUniversal);
            return Content(JsonConvert.SerializeObject(lst, _jsonSetting), "application/json");
        }

        public ContentResult GetProssimiSaltiAsciutteParti()
        {
            var clCom = new ClassiComuni();
            var conn = new CowBoy.ComponentsNew.BoviniCom();
            var lst = conn.GetProssimiSaltiAsciutteParti(clCom.ConnectCbUniversal);
            return Content(JsonConvert.SerializeObject(lst, _jsonSetting), "application/json");
        }

        public ContentResult UploadImage(HttpPostedFileBase file)
        {
            string[] lst = new string[2];

            try
            {
                string fileName = Path.GetFileName(file.FileName);
                string dest = Directory.CreateDirectory(Server.MapPath("~/img/FotoBov")).FullName;
                string filePath = Path.Combine(dest, fileName);
                file.SaveAs(filePath);
                lst[0] = fileName;
            }
            catch (Exception ex)
            {
                lst[1] = ex.Message;
            }
            return Content(JsonConvert.SerializeObject(lst, _jsonSetting), "application/json");
        }

    }
}