using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CowBoy.Components;
using CowBoy.Entities;
using CowBoy.Library;
using Image = System.Drawing.Image;

namespace CowBoy.WEB_UI
{
    public partial class dettaglio : System.Web.UI.Page
    {
        public const string ConnectionString = "CowBoyEntities";
        private string PercorsoFoto = "";

        //  protected HtmlInputFile fileInputImage;

        protected void Page_Load(object sender, EventArgs e)
        {
            PercorsoFoto = ConfigurationManager.AppSettings["PercorsoFoto"];
            if (!IsPostBack)
            {
                String cat = Request.QueryString["ID"];
                hfPercorsoFoto.Value = PercorsoFoto;
                hfIdAnagrafica.Value = cat;
                CaricaDati(Convert.ToInt32(cat));

            }
        }

        #region Metodi privati
        protected void CaricaDati(int iDanag)
        {
            try
            {
                hfIdPartoSalto.Value = "";
                hfPartiChiusi.Value = "0"; //variabile utlizzata per verificare se abbiamo ancora procedure di parti ancora aperte
                gridParti.DataSource = null;
                gridParti.DataBind();
                TxtMatricola.Text = "";
                txtNome.Text = "";
                //Mycontrol.Value = "";
                txtDataNascita.Text = "";
                txtnNewMatricolaAzienda.Text = string.Empty;
                txtCercaMadre.Text = string.Empty;
                hfCercaMadreVal.Value = string.Empty;
                txtCercaPadre.Text = string.Empty;
                hfCercaMadreVal.Value = string.Empty;
                chToroArtificiale.Checked = false;
                chToroDaMonta.Checked = false;
                // divSaltoDettaglio.Visible = false;
                divSaltoDettaglio.Style.Add("visibility", "hidden");

                var cow = new GestioneCowBoy(ConnectionString);
                var anag = cow.GetAnagrafica(iDanag, null, null);

                Anagrafica myAnag = new Anagrafica();

                if (anag.Count > 0)
                    myAnag = anag.First();



                var lst = from c in myAnag.PartiSalti
                          orderby c.DataParto
                          select new
                          {
                              ID = c.idPartoSalto,
                              Data = c.DataParto,
                              F = c.PartoNoParto != null ? c.PartoNoParto.Substring(2, 1) : string.Empty,
                              M = c.PartoNoParto != null ? c.PartoNoParto.Substring(4, 1) : string.Empty,
                              Stato = c.DataParto == null ? "Aperto" : "Chiuso"
                          };
                gridParti.DataSource = lst;
                gridParti.DataBind();


                hfPartiChiusi.Value = "0"; //variabile utlizzata per verificare se abbiamo ancora procedure di parti ancora aperte
                int count = lst.Count(c => c.Stato == "Aperto");
                if (count > 0)
                {
                    hfPartiChiusi.Value = "1";
                }

                lblMatricola.Text = string.Format("MatricolaAsl = {0}", myAnag.MatricolaASL);
                TxtMatricola.Text = myAnag.MatricolaASL;
                txtnNewMatricolaAzienda.Text = myAnag.MatricolaAzienda;
                txtNome.Text = myAnag.Nome;
                if (myAnag.Sesso == "M")
                {
                    chMBovino.Checked = true;
                    //divSaltoDettaglio.Style.Add("visibility", "visible");
                    divTipoToro.Style.Add("visibility", "visible");
                }
                chFBovino.Checked = myAnag.Sesso == "F";
                chToroArtificiale.Checked = myAnag.ToroArtificiale == true;
                chToroDaMonta.Checked = myAnag.ToroDaMonta == true;

                if (myAnag.Anagrafica2 != null)
                {
                    txtCercaMadre.Text = myAnag.Anagrafica2.MatricolaASL;
                    hfCercaMadreVal.Value = myAnag.Anagrafica2.idAnagrafica.ToString();
                }
                if (myAnag.Anagrafica3 != null)
                {
                    txtCercaPadre.Text = myAnag.Anagrafica3.MatricolaASL;
                    hfCercaMadreVal.Value = myAnag.Anagrafica3.idAnagrafica.ToString();
                }
                
                txtDataNascita.Text = string.Format("{0:dd/MM/yyyy}", myAnag.DataNascita);
                PopolaFoto(myAnag.Foto.OrderByDescending(f => f.Principale).ToList(), iDanag);
                //carico la lista dei tori
                PopolaTori();

            }
            catch (Exception ex)
            {
                this.GetAlert(this.GetType(), ex.Message);
            }
        }

        protected void PopolaTori()
        {
            ddlTori.Items.Clear();
            try
            {
                var cow = new GestioneCowBoy(ConnectionString);
                var anag = cow.GetAnagrafica(null, "M", 1);
                var lstAnag = from c in anag
                              orderby c.idAnagrafica
                              select new
                              {
                                  idana = c.idAnagrafica,
                                  MatriAsl = string.Format("{0}-{1}-Artif {2}", c.MatricolaASL, c.Nome, c.ToroArtificiale == true ? "Si" : "No")
                              };
                ddlTori.DataSource = lstAnag.ToList();
                ddlTori.DataTextField = "MatriAsl";
                ddlTori.DataValueField = "idana";
                ddlTori.DataBind();
                ddlTori.Items.Insert(0, new ListItem(string.Empty));
                ddlTori.SelectedIndex = 0;

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

            gvFoto.DataSource = myFoto;
            gvFoto.DataBind();

            if (myFoto.Count > 0)
            {
                foreach (var foto in myFoto)
                {
                    // <add key="PercorsoFoto" value="images\gallery" />
                    //PercorsoFoto = @"images\gallery";
                    divImg.InnerHtml +=
                        PopolaHtmlFoto(
                            Path.Combine(PercorsoFoto, iDanag.ToString(), foto.Nome).Replace(@"\", "/"), cc,
                           myFoto.Count);
                    cc += 1;
                }
            }
            else
            {
                divImg.InnerHtml += String.Format("<img class=\"example-image-link\" src=\"images/default.jpg\" width=\"100px\" heght=\"100px\"  alt=\"Immagine non trovata\"/>");
            }
        }

        protected string PopolaHtmlFoto(string percorso, int conta, int contaTot)
        {
            var a = string.Empty;

            if (conta == 1)
            {
                a = string.Format(
                 "<a class=\"example-image-link\" href=\"{0}\" data-lightbox=\"example-set\" title=\"Cliccare per vedere le immagini\"><img class=\"example-image\" src=\"{1}\" alt=\"Plants: image {2} 0f {3} thumb\" width=\"100\" height=\"100\" /></a>",
                 percorso, percorso, conta, contaTot);
            }
            else
            {
                a = string.Format(
                     "<a class=\"example-image-link\"  style=\"display: none\" href=\"{0}\" data-lightbox=\"example-set\" title=\"Cliccare per vedere le immagini\"><img class=\"example-image\" src=\"{1}\" alt=\"Plants: image {2} 0f {3} thumb\" width=\"100\" height=\"100\" /></a>",
                     percorso, percorso, conta, contaTot);
            }
            return a;
        }

        protected void CaricaSalti(List<Salti> par)
        {
            try
            {
                PulisciSalto();


                //  var ceee = par.First().Anagrafica;

                //dati relativi ai salti
                var lstSalti = from c in par
                               orderby c.DataSalto descending
                               select new
                               {
                                   ID = c.idSalto,
                                   Data = c.DataSalto,
                                   idT = c.idToro,
                                   MatriToro = c.Anagrafica != null ?
                                   string.Format("{0}-{1}-Artif {2}", c.Anagrafica.MatricolaASL, c.Anagrafica.Nome, c.Anagrafica.ToroArtificiale == true ? "Si" : "No") : ""
                                  ,
                                   idSal = c.idSalto
                               };

                gridSalti.DataSource = lstSalti.ToList();
                gridSalti.DataBind();
            }
            catch (Exception ex)
            {
                this.GetAlert(this.GetType(), ex.Message);
            }
        }

        protected void CaricaDettaglioPartoSalto(int idPartoSalto)
        {
            try
            {
                PuliParto();
                PulisciFigli();

                var cow = new GestioneCowBoy(ConnectionString);
                var par = cow.GetListPartiSalti(null, idPartoSalto).First();

                hfIdPartoSalto.Value = par.idPartoSalto.ToString();

                CaricaSalti(par.Salti.ToList());
                txtDataAsciutta.Text = string.Format("{0:dd/MM/yyyy}", par.DataMessaAsciutta);
                txtDataParto.Text = string.Format("{0:dd/MM/yyyy}", par.DataParto);

                if (par.PartoNoParto != null)
                {
                    if (Convert.ToInt64(par.PartoNoParto) != 0)
                    {
                        string partiNoParti = par.PartoNoParto;
                        //femmine
                        int tot = Convert.ToInt32(partiNoParti.Substring(3, 1));
                        int vivi = Convert.ToInt32(partiNoParti.Substring(2, 1));
                        txtFv.Text = vivi.ToString();
                        txtFm.Text = (tot - vivi).ToString();
                        //machi
                        tot = Convert.ToInt32(partiNoParti.Substring(5, 1));
                        vivi = Convert.ToInt32(partiNoParti.Substring(4, 1));
                        txtMv.Text = vivi.ToString();
                        txtMm.Text = (tot - vivi).ToString();

                        if (par.Facile != null) chPartoFacile.Checked = (bool)par.Facile;

                        if (par.Naturale != null) chPartoNaturale.Checked = (bool)par.Naturale;

                        //apro e popolo la griglia dei figli
                        CaricaFigli(par.idPartoSalto);
                    }
                }
            }
            catch (Exception ex)
            {
                this.GetAlert(this.GetType(), ex.Message);
            }
        }

        protected void CaricaFigli(int idPartoSalto)
        {
            try
            {
                var cow = new GestioneCowBoy(ConnectionString);
                var figli = cow.GetAnagrafica(idPartoSalto);
                var lstFigli = from c in figli
                               select new
                               {
                                   ID = c.idAnagrafica,
                                   MatrUsl = c.MatricolaASL,
                                   MatrAz = c.MatricolaAzienda,
                                   IdFiglio = c.idAnagrafica,
                                   Fot = c.Foto.Count > 0 ? c.Foto.First(d => d.Principale == true).Nome : string.Empty,
                                   S = c.Sesso
                               };

                gridFigli.DataSource = lstFigli.ToList();
                gridFigli.DataBind();

            }
            catch (Exception ex)
            {
                this.GetAlert(this.GetType(), ex.Message);
            }
        }

        protected Foto RecuperaFoto(int idFoto)
        {
            try
            {
                var cow = new GestioneCowBoy(ConnectionString);
                return cow.GetFotoByIdFoto(idFoto);
            }
            catch (Exception ex)
            {
                this.GetAlert(this.GetType(), ex.Message);
                return null;
            }
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



                    for (var i = 0; i < gridParti.Rows.Count; i++)
                    {
                        gridParti.Rows[i].BackColor = i % 2 == 0 ? Color.White : Color.FromArgb(0xCCFFCC);
                    }


                    row.BackColor = Color.YellowGreen;
                    if (dataKey != null)
                    {
                        var id = Convert.ToInt32(dataKey.Value.ToString());
                        switch (e.CommandName)
                        {
                            case "Dettaglio":
                                CaricaDettaglioPartoSalto(id);
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


        #region Pulizia Controlli

        protected void PuliParto()
        {
            txtDataParto.Text = "";
            txtDataAsciutta.Text = "";
            //dpParto.Attributes["data-date"] = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
            txtFm.Text = "0";
            txtFv.Text = "0";
            txtMv.Text = "0";
            txtMm.Text = "0";
            chPartoFacile.Checked = false;
            chPartoNaturale.Checked = false;
        }

        protected void PulisciSalto()
        {
            divSaltoDettaglio.Attributes["style"] = "visibility: hidden;";
            txtDataSalto.Text = string.Empty;
            ddlTori.SelectedIndex = 0;
        }

        protected void PulisciFigli()
        {
            divEditFigli.Attributes["style"] = "visibility: hidden;";
            gridFigli.DataSource = null;
            gridFigli.DataBind();

        }
        #endregion

        protected void btnSalvaSalto_OnServerClick(object sender, EventArgs e)
        {

            try
            {
                var msg = (idSalto.Value.Trim() == "" || idSalto.Value.Trim() == "0") ? string.Format("Non potete modificare salto poichè la bovina è stota asciugata")
                         : "Non potete aggiungere un altro salto poichè la bovina è stata asciugata";


                //se inserisco un nuovo salto o modifico uno esistente controllo che il parto non sia avvenuto  
                var cn = new GestioneCowBoy(ConnectionString);
                var lstParti = cn.GetListPartiSalti(null, Convert.ToInt32(hfIdPartoSalto.Value)).First();

                if (lstParti.DataMessaAsciutta != null)
                {
                    this.GetAlert(this.GetType(), msg);
                    return;
                }

                //verifica che la data immessa non sia presente nei salti già registrati
                if (lstParti.Salti.Any(d => d.DataSalto == Convert.ToDateTime(txtDataSalto.Text) && d.idSalto != Convert.ToInt32(idSalto.Value)))
                {
                    msg = string.Format("Attenzione la data inserita risulta già salvata");
                    this.GetAlert(this.GetType(), msg);
                    return;
                }

                //salvo
                var salto = new Salti
                {
                    idSalto = Convert.ToInt32(idSalto.Value),
                    DataSalto = Convert.ToDateTime(txtDataSalto.Text),
                    idToro = ddlTori.SelectedValue != "0" ? Convert.ToInt32(ddlTori.SelectedValue) : (int?)null,
                    idPartoSalto = Convert.ToInt32(hfIdPartoSalto.Value)
                };
                cn.SaveSalto(salto);

                CaricaSalti(cn.GetListSalti(Convert.ToInt32(hfIdPartoSalto.Value), null));
            }
            catch (Exception ex)
            {
                this.GetAlert(this.GetType(), ex.Message);
            }


        }

        protected void btnDeleteSalto_OnServerClick(object sender, EventArgs e)
        {
            try
            {
                var msg = (idSalto.Value.Trim() == "" || idSalto.Value.Trim() == "0") ? string.Format("Non potete modificare salto poichè la bovina è stota asciugata")
                         : "Non potete aggiungere un altro salto poichè la bovina è stota asciugata";

                var cn = new GestioneCowBoy(ConnectionString);

                var salto = new Salti
                {
                    idSalto = Convert.ToInt32(idSalto.Value),
                    idPartoSalto = Convert.ToInt32(hfIdPartoSalto.Value)
                };
                cn.DeleteSalto(salto);

                CaricaSalti(cn.GetListSalti(Convert.ToInt32(hfIdPartoSalto.Value), null));
            }
            catch (Exception ex)
            {
                this.GetAlert(this.GetType(), ex.Message);
            }
        }

        protected void btnSalvaParto_OnClick(object sender, EventArgs e)
        {
            try
            {

                int fV = Convert.ToInt32(txtFv.Text);
                int fM = Convert.ToInt32(txtFm.Text);

                int mV = Convert.ToInt32(txtMv.Text);
                int mM = Convert.ToInt32(txtMm.Text);

                var partoNoParto = string.Format("{0}{1}{2}{3}{4}{5}", (fV + mV), (fV + mV + fM + mM), (fV), (fV + fM), (mV), (mV + mM));

                var cow = new GestioneCowBoy(ConnectionString);

                var parto = new PartiSalti
                {
                    idPartoSalto = Convert.ToInt32(hfIdPartoSalto.Value),
                    idAnagrafica = Convert.ToInt32(hfIdAnagrafica.Value),
                    DataMessaAsciutta = txtDataAsciutta.Text.Trim() == String.Empty
                        ? (DateTime?)null
                        : Convert.ToDateTime(txtDataAsciutta.Text),
                    DataParto = txtDataParto.Text.Trim() == String.Empty
                        ? (DateTime?)null
                        : Convert.ToDateTime(txtDataParto.Text),
                    PartoNoParto = partoNoParto,
                    Facile = chPartoFacile.Checked,
                    Naturale = chPartoNaturale.Checked,
                    Note = txtNoteParto.Text.Trim()
                };

                cow.SavePartoSalto(parto);
                //se registriamo la data del parto riaggiorniamo tutti i dati
                if (txtDataParto.Text.Trim() != String.Empty)
                    CaricaDati(Convert.ToInt32(hfIdAnagrafica.Value));
            }
            catch (Exception ex)
            {
                this.GetAlert(this.GetType(), ex.Message);
            }
        }

        protected void btnDeleteParto_OnClick(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                this.GetAlert(this.GetType(), ex.Message);
            }
        }

        protected void btnSalvaFiglio_OnServerClick(object sender, EventArgs e)
        {
            //posso salvare solo se la mucca ha partorito
            //inoltre posso salvare solo se ci sono ancora figli da salvare verificando il numero di figli maschi e femmine se sono nati 2 femmine e cerco di registrare un maschio o una terza femmina invio allert

            try
            {

                string baseImageLocation = ConfigurationManager.AppSettings["PercorsoSalvataggioFoto"];
                var idAnagrafica = Convert.ToInt32(hfIdAnagrafica.Value);
                var idParto = Convert.ToInt32(hfIdPartoSalto.Value);



                var cow = new GestioneCowBoy(ConnectionString);
                var parto = cow.GetListPartiSalti(null, idParto).FirstOrDefault();
                if (parto.DataParto == null)
                {
                    this.GetAlert(this.GetType(), "Per poter salvare il figlio salvare prima la data del parto");
                    return;
                }

                var salto = cow.GetListSalti(idParto, null).OrderByDescending(c => c.DataSalto).FirstOrDefault();
                var sesso = chFfiglio.Checked == true ? "F" : "M";

                var contaFigliReg = cow.GetAnagrafica(idParto).Count(m => m.Sesso == sesso);//recupero i figli registrati e associati a quel parto

                int viviF = Convert.ToInt32(parto.PartoNoParto.Substring(2, 1));
                int viviM = Convert.ToInt32(parto.PartoNoParto.Substring(4, 1));

                if ((sesso == "F" && ((contaFigliReg + 1 > viviF))) || (sesso == "M" && ((contaFigliReg + 1) > viviM)))
                {
                    this.GetAlert(this.GetType(),
                        string.Format("Attenzione il numero di figl{0} registrati per questo parto è superiore ai nati", sesso == "F" ? "ie Femmine" : "i Maschi"));
                    return;
                }


                Anagrafica figlio = new Anagrafica()
                {
                    idAnagrafica = 0,
                    Madre = idAnagrafica,
                    Padre = salto.idToro,
                    DataNascita = parto.DataParto,
                    MatricolaASL = txtMatriUslFiglio.Value.Trim().ToUpper(),
                    MatricolaAzienda = txtMatriAzFigli.Value.Trim(),
                    Sesso = sesso,
                    idFiglio = idParto
                };//il campo idFiglio è il collegamento con il parto

                byte[] myData = null;
                string nomeFoto = null;



                if (fileInputImage.PostedFile != null)
                {
                    HttpPostedFile myFile = fileInputImage.PostedFile;
                    int nFileLen = myFile.ContentLength;
                    myData = new byte[nFileLen];
                    myFile.InputStream.Read(myData, 0, nFileLen);
                    nomeFoto = myFile.FileName;
                    //WriteToFile(@"c:\Temp\prova1.jpg", myData);
                }

                Foto fotoFiglio = new Foto()
                {
                    Nome = nomeFoto,
                    Principale = true
                };

                cow.SalvaFiglio(figlio, fotoFiglio, baseImageLocation, myData);
                CaricaFigli(idParto);

                this.GetAlert(this.GetType(), "Figlio salvato con successo");
            }
            catch (Exception ex)
            {
                this.GetAlert(this.GetType(), ex.Message);
            }

        }

        //private void WriteToFile(string strPath, byte[] buffer)
        //{
        //    try
        //    {
        //        // Create a file

        //        FileStream newFile = new FileStream(strPath, FileMode.Create);

        //        // Write data to the file

        //        newFile.Write(buffer, 0, buffer.Length);
        //        newFile.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        this.GetAlert(this.GetType(), ex.Message);
        //    }
        //}

        protected void btnDettaglioFiglio_OnServerClick(object sender, EventArgs e)
        {
            hfIdAnagrafica.Value = hfIdFiglio.Value;
            CaricaDati(Convert.ToInt32(hfIdFiglio.Value));
        }


        #region GRIGLIA FOTO

        protected void gvFoto_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var foto = e.Row.DataItem as Foto;

                if (foto.Principale == true)
                {
                    (e.Row.FindControl("hlDeleteFoto") as LinkButton).Visible = false;
                }


                var percorso = Path.Combine(PercorsoFoto, foto.idAnagrafica.ToString(), foto.Nome).Replace(@"\", "/");
                // hfPercorsoFotoGridFoto
                (e.Row.FindControl("hfPercorsoFotoGridFoto") as HiddenField).Value = percorso;

                var a = string.Format(
                 "<a class=\"example-image-link\" href=\"{0}\" data-lightbox=\"example-set\" title=\"Cliccare per vedere le immagini\"><img class=\"example-image\" src=\"{1}\" alt=\"Plants: image {2} 0f {3} thumb\" width=\"100\" height=\"100\" /></a>",
                 percorso, percorso, 1, 0);

                (e.Row.FindControl("divImageGrid") as HtmlGenericControl).InnerHtml += a;

            }
        }

        protected void gvFoto_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                var row = ((Control)(e.CommandSource)).NamingContainer as GridViewRow;
                if (row != null)
                {
                    DataKey dataKey = gvFoto.DataKeys[row.RowIndex];


                    if (dataKey != null)
                    {
                        Int16 vedi = 0;
                        // var id = dataKey.Value.ToString();
                        switch (e.CommandName)
                        {
                            case "DettaglioFoto":
                                //nel dettaglio posso solo impoostare come predefinita la foto
                                vedi = 1;
                                break;
                            case "DeleteFoto":
                                //la foto può essere eliminata solo se non è impostata di default
                                vedi = 2;
                                break;
                        }
                        if (vedi > 0)
                        {
                            Foto foto = RecuperaFoto(Convert.ToInt32(dataKey.Value));
                            if (foto==null)
                            {
                                this.GetAlert(this.GetType(), "Errore nel recupero dati della foto, riprovare o contattare l'amministratore");
                                return;
                            }

                            if (vedi == 2) //elimino
                            {
                                if (foto.Principale == true)
                                {
                                    this.GetAlert(this.GetType(), "Per eliminare la foto impostarne un altra foto come di Default");
                                    return;
                                }
                                lblModificaFoto.Text = "Procedere con la cancellazione della foto?";
                            }
                            else
                            {
                                if (foto.Principale == true)
                                {
                                    this.GetAlert(this.GetType(), "Impossibile procedere in quanto la foto risulta gia di Default");
                                    return;
                                }
                                lblModificaFoto.Text = "Impostare la foto come di Default?";
                            }
                            hfIdFoto.Value = foto.idFoto.ToString();
                            hfModifica.Value = vedi.ToString();

                            var sb = new System.Text.StringBuilder();
                            sb.Append(@"<script type='text/javascript'>");
                            sb.Append("$('#modalChangeFoto').modal('show');");
                            sb.Append(@"</script>");
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", sb.ToString(), false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.GetAlert(this.GetType(), ex.Message);
            }
        }
        #endregion

        [WebMethod]
        public static List<AnagList> GetListFem(string prefixText, int count)
        {
            if (prefixText.Length > 0)
            {
                var srv = new GestioneCowBoy(ConnectionString);

                var anag = srv.GetAnagraficaCerca("F", prefixText);
                if (anag.Count == 0) return null;

                return anag.Select(a => new AnagList
                {
                    Id = a.idAnagrafica,
                    Matricola = a.MatricolaASL
                }).ToList();
            }

            return null;

        }

        [WebMethod]
        public static List<AnagList> GetListMas(string prefixText, int count)
        {
            //if (prefixText.Length > 0) //tolto per visualizzare anche senza filtro l'intera lista dei tori
            //{
                var srv = new GestioneCowBoy(ConnectionString);

                var anag = srv.GetAnagraficaCerca("M", prefixText);
                if (anag.Count == 0) return null;
                return anag.Select(a => new AnagList
                {
                    Id = a.idAnagrafica,
                    Matricola = a.MatricolaASL
                }).ToList();
            //}

          //  return null;

        }

        protected void btnSalvaAnagrafica_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (hfIdAnagrafica.Value == string.Empty || Convert.ToInt32(hfIdAnagrafica.Value) == 0)
                {
                    this.GetAlert(this.GetType(),
                        string.Format("Attenzione, ritornare sulla home page e riselezionare il bovino. Se il problema persiste contattare l'amministratore di sistema."));
                    return;
                }
                string baseImageLocation = ConfigurationManager.AppSettings["PercorsoSalvataggioFoto"];
                Anagrafica ana = new Anagrafica()
                {
                    idAnagrafica = Convert.ToInt32(hfIdAnagrafica.Value),
                    Madre = hfCercaMadreVal.Value != String.Empty ? Convert.ToInt32(hfCercaMadreVal.Value) : (int?)null,
                    Padre = hfCercaPadreVal.Value != String.Empty ? Convert.ToInt32(hfCercaPadreVal.Value) : (int?)null,
                    DataNascita = Convert.ToDateTime(txtDataNascita.Text),
                    MatricolaASL = TxtMatricola.Text.Trim().ToUpper(),
                    MatricolaAzienda = txtnNewMatricolaAzienda.Text.Trim().ToUpper(),
                    Nome = txtNome.Text.Trim(),
                    Sesso = chFBovino.Checked == true ? "F" : "M"
                };

              //  byte[] myData = null;


                //Foto fotoFiglio = null;

                //if (fuAllegatiNewBovino.PostedFile != null &&
                //    fuAllegatiNewBovino.PostedFile.FileName.Trim() != string.Empty)
                //{

                //   var nomeFoto = Path.GetFileName(fuAllegatiNewBovino.PostedFile.FileName);
                //    myData = fuAllegatiNewBovino.FileBytes;

                //    fotoFiglio = new Foto()
                //    {
                //        Nome = nomeFoto,
                //        Principale = true
                //    };
                //}
                var cow = new GestioneCowBoy(ConnectionString);
                cow.SaveAnagrafica(ana);
            }
            catch (Exception ex)
            {
                this.GetAlert(this.GetType(), ex.Message);
            }
        }

        protected void btnOpenPanelNewParto_OnClick(object sender, EventArgs e)
        {
            var sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#modalNewParto').modal('show');");

            sb.Append("$('.classData').datepicker({ dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true});");
            sb.Append("$('.datepicker').zIndex(99998);");

            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", sb.ToString(), false);
        }

        protected void btnSalvaNewParto_OnClick(object sender, EventArgs e)
        {
            //id del toro
            //txtRicercaHf
            //data del salto 
            //txtNewDatasalto
            try
            {
                //se inserisco un nuovo salto o modifico uno esistente controllo che il parto non sia avvenuto  
                var cn = new GestioneCowBoy(ConnectionString);
                var lstParti = cn.GetListPartiSalti(Convert.ToInt32(hfIdAnagrafica.Value), null);

                if (lstParti.Count > 0)
                {
                    //verifico che la data del salto immessa sia superiore all'ultimo parto 
                    var ult = lstParti.FirstOrDefault(c => c.idPartoSalto == lstParti.Max(d => d.idPartoSalto));

                    if (ult.DataParto != null && ult.DataParto > Convert.ToDateTime(txtNewDatasalto.Text))
                    {
                        this.GetAlert(this.GetType(), "Attenzione la data inserita è inferiore alla data dell'ultimo parto");
                        return;
                    }
                }


                //salvo
                var salto = new Salti
                {
                    //idSalto = Convert.ToInt32(idSalto.Value),
                    DataSalto = Convert.ToDateTime(txtNewDatasalto.Text),
                    idToro = Convert.ToInt32(hfCercaPadreNewPartoVal.Value),
                    Anagrafica = new Anagrafica { idAnagrafica = Convert.ToInt32(hfIdAnagrafica.Value) }
                };
                cn.SaveSalto(salto);

                if (hfIdPartoSalto.Value.Trim() != string.Empty)
                    CaricaSalti(cn.GetListSalti(Convert.ToInt32(hfIdPartoSalto.Value), null));
                else
                    CaricaDati(Convert.ToInt32(hfIdAnagrafica.Value));
            }
            catch (Exception ex)
            {
                this.GetAlert(this.GetType(), ex.Message);
            }
        }

        protected void btnSaveNewFoto_OnClick(object sender, EventArgs e)
        {
            try
            {
                string baseImageLocation = ConfigurationManager.AppSettings["PercorsoSalvataggioFoto"];
                var cn = new GestioneCowBoy(ConnectionString);
                byte[] myData = null;


                Foto fotoFiglio = null;

                if (fuNewFoto.PostedFile != null &&
                    fuNewFoto.PostedFile.FileName.Trim() != string.Empty)
                {

                    var nomeFoto = Path.GetFileName(fuNewFoto.PostedFile.FileName);
                    myData = fuNewFoto.FileBytes;

                    fotoFiglio = new Foto()
                    {
                        Nome = nomeFoto,
                        Principale = chNewPrincipaleFoto.Checked,
                        idAnagrafica = Convert.ToInt32(hfIdAnagrafica.Value)
                    };
                    cn.SaveFoto(fotoFiglio, myData, baseImageLocation);
                    //aggiorno la griglia delle foto
                    var lstFoto = cn.GetFoto(Convert.ToInt32(hfIdAnagrafica.Value));
                    PopolaFoto(lstFoto.OrderByDescending(f => f.Principale).ToList(), Convert.ToInt32(hfIdAnagrafica.Value));
                }
                else
                {
                    this.GetAlert(this.GetType(), "Il file immagine non è stato trovato");
                }

            }
            catch (Exception ex)
            {
                this.GetAlert(this.GetType(), ex.Message);
            }
            //SaveFotoList
        }

        protected void btnSalvaChangeFoto_OnClick(object sender, EventArgs e)
        {
            //hfIdFoto.Value = foto.idFoto.ToString();
            //hfModifica.Value = vedi.ToString();
            try
            {
                var cn = new GestioneCowBoy(ConnectionString);
                cn.SalvaModificheFoto(Convert.ToInt32(hfIdFoto.Value), hfModifica.Value == "2");
                var lstFoto = cn.GetFoto(Convert.ToInt32(hfIdAnagrafica.Value));
                PopolaFoto(lstFoto.OrderByDescending(f => f.Principale).ToList(), Convert.ToInt32(hfIdAnagrafica.Value));
            }
            catch (Exception ex)
            {
                this.GetAlert(this.GetType(), ex.Message);
            }
               
        }
    }
}