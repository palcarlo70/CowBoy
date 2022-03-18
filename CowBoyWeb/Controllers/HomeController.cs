using System;
using System.Collections.Generic;
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


        //public ContentResult GetBovini()
        //{
        //    var clCom = new ClassiComuni();
        //    var conn = new CowBoy.ComponentsNew.BoviniCom();
        //    var lst = conn.GetBovini(null,null,null,null,null,null,null, clCom.ConnectCbUniversal);
        //    return Content(JsonConvert.SerializeObject(lst, _jsonSetting), "application/json");
        //}

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

    }
}