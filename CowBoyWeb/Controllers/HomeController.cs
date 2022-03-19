using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult DettaglioBovini()
        {
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