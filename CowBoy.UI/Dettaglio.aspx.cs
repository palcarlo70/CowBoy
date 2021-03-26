using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CowBoy.Components;
using CowBoy.Entities;
using CowBoy.Library;

namespace CowBoy.UI
{
    public partial class Dettaglio : System.Web.UI.Page
    {
        public const string ConnectionString = "CowBoyEntities";
        private string PercorsoFoto = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String cat = Request.QueryString["ID"];
                PercorsoFoto = ConfigurationManager.AppSettings["PercorsoFoto"];
                CaricaDati(Convert.ToInt32(cat));
                
            }

        }

        #region Metodi privati
        protected void CaricaDati(int iDanag)
        {
            try
            {

                var cow = new GestioneCowBoy(ConnectionString);
                var anag = cow.GetAnagrafica(iDanag,null,null);
                
                Anagrafica myAnag = new Anagrafica();

                if (anag.Count > 0)
                    myAnag = anag.First();

              

                var lst = from c in myAnag.PartiSalti
                          orderby c.DataParto
                          select new
                          {
                              ID = c.idPartoSalto,
                              Data=c.DataParto,
                              F = c.PartoNoParto != null ? c.PartoNoParto.Substring(2, 1) : string.Empty,
                              M = c.PartoNoParto != null ? c.PartoNoParto.Substring(4, 1) : string.Empty,
                              Stato = c.DataParto == null ? "Aperto" : "Chiuso"
                          };
                gridParti.DataSource = lst;
                gridParti.DataBind();

                TxtMatricola.Text = myAnag.MatricolaASL;
                txtNome.Text = myAnag.Nome;
                txtData.Text = string.Format("{0:dd/MM/yyyy}", myAnag.DataNascita);

                PopolaFoto(myAnag.Foto.OrderByDescending(f=>f.Principale).ToList(), iDanag);


            }
            catch (Exception ex)
            {
                this.GetAlert(this.GetType(), ex.Message);
            }
        }


        protected void PopolaFoto(List<Foto> myFoto, int iDanag)
        {
            divImg.Controls.Clear();

            int cc = 1;

            if (myFoto.Count > 0)
            {
                foreach (var foto in myFoto)
                {
                    // <add key="PercorsoFoto" value="images\gallery" />
                    //PercorsoFoto = @"images\gallery";
                    divImg.InnerHtml +=
                        PopolaHtmlFoto(
                            Path.Combine(PercorsoFoto, iDanag.ToString(), foto.Nome).Replace(@"\","/"), cc,
                           myFoto.Count);
                    cc += 1;
                }
            }
            else
            {
                divImg.InnerHtml += String.Format("<img class=\"example-image-link\" src=\"images/default.jpg\" width=\"100px\" heght=\"100px\"  alt=\"Immagine non trovata\"/>");
            }
        }

        protected string PopolaHtmlFoto(string percorso,int conta, int contaTot)
        {
            var a = string.Empty; 
                
               if (conta == 1) 
               {
                   a= string.Format(
                    "<a class=\"example-image-link\" href=\"{0}\" data-lightbox=\"example-set\" title=\"Cliccare per vedere le immagini\"><img class=\"example-image\" src=\"{1}\" alt=\"Plants: image {2} 0f {3} thumb\" width=\"100\" height=\"100\" /></a>",
                    percorso, percorso,conta,contaTot);
               }
               else
               {
                   a = string.Format(
                        "<a class=\"example-image-link\"  style=\"display: none\" href=\"{0}\" data-lightbox=\"example-set\" title=\"Cliccare per vedere le immagini\"><img class=\"example-image\" src=\"{1}\" alt=\"Plants: image {2} 0f {3} thumb\" width=\"100\" height=\"100\" /></a>",
                        percorso, percorso, conta, contaTot);
               }
           return a;
        }


        protected void CaricaDettaglioPartoSalto(int idPartoSalto)
        {
            //divDettagliosalto.Visible = false; 
            var cow = new GestioneCowBoy(ConnectionString);
            var par = cow.GetListPartiSalti(null, idPartoSalto).First();
            
            
            /*
             var par = cow.GetListPartiSalti(null, idParto).First();
                btnSaveParto.Tag = idParto;
                const string formatoDate = "dd/MM/yyyy";
                if (par.DataMessaAsciutta != null)
                {
                    dtpAsciuttaIns.CustomFormat = formatoDate;
                    dtpAsciuttaIns.Value = (DateTime)par.DataMessaAsciutta;
                }
                if (par.DataParto != null)
                {
                    dtpPartoIns.CustomFormat = formatoDate;
                    dtpPartoIns.Value = (DateTime)par.DataParto;
                    pnlDetParto.Visible = true;
                }
                if (par.PartoNoParto != null)
                {
                    if (Convert.ToInt64(par.PartoNoParto) != 0)
                    {
                        string partiNoParti = par.PartoNoParto;
                        //femmine
                        int tot = Convert.ToInt32(partiNoParti.Substring(3, 1));
                        int vivi = Convert.ToInt32(partiNoParti.Substring(2, 1));
                        txtFvivi.Text = vivi.ToString();
                        txtFmorte.Text = (tot - vivi).ToString();
                        //machi
                        tot = Convert.ToInt32(partiNoParti.Substring(5, 1));
                        vivi = Convert.ToInt32(partiNoParti.Substring(4, 1));
                        txtMvivi.Text = vivi.ToString();
                        txtMmorti.Text = (tot - vivi).ToString();


                        if (par.Facile != null) rbFacileSi.Checked = (bool)par.Facile;
                        if (par.Facile != null) rbFacileNo.Checked = !(bool)par.Facile;

                        if (par.Naturale != null) rbNaturaleSi.Checked = (bool)par.Naturale;
                        if (par.Naturale != null) rbNaturaleNo.Checked = !(bool)par.Naturale;



                        //apro e popolo la griglia dei figli
                        CaricaFigli(par.idPartoSalto);
                    }
                }
                btnDelParto.Visible = true;
                //popolo la griglia dei salti
                CaricaSalti(par.Salti.ToList(), null);

             */

        }

        #endregion

        protected void gridParti_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
           
            try
            {
                
                var row = ((Control)(e.CommandSource)).NamingContainer as GridViewRow;
                if (row != null)
                {
                    DataKey dataKey = gridParti.DataKeys[row.RowIndex];
                    

                    if (dataKey != null)
                    {
                        var id = dataKey.Value.ToString();
                        switch (e.CommandName)
                        {
                            case "Dettaglio":
                                CaricaDettaglioPartoSalto(Convert.ToInt32(id));
                                divDettaglioParto.Visible = true;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.GetAlert(this.GetType(), ex.Message);
            }
        }
    }


}