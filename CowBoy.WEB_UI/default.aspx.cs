using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using CowBoy.Components;
using CowBoy.Entities;
using CowBoy.Library;


namespace CowBoy.WEB_UI
{
    public partial class _default : System.Web.UI.Page
    {
        public const string ConnectionString = "CowBoyEntities";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CaricaPartiAsciutte();
                //txtSearch.Attributes.Add("onKeyPress",
                //   "SerchClick('" + btnSearh.ClientID + "',event)");

            }
        }

        protected void gridResultSearch_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            grid_OnRowCommand(sender, e, gridResultSearch);

        }

        #region Metodi Privati

        protected void CaricaPartiAsciutte()
        {
            try
            {

                var cow = new GestioneCowBoy(ConnectionString);

                var anag = cow.GetAnagrafica("F", 1, null, true, true, null);

                DataTable dtParto = new DataTable();
                if (dtParto.Columns.Count == 0)
                {
                    dtParto.Columns.Add("ID", typeof(int));
                    dtParto.Columns.Add("MatrAsl", typeof(string));
                    dtParto.Columns.Add("Data", typeof(DateTime));
                }


                DataTable dtAsciutta = new DataTable();
                if (dtAsciutta.Columns.Count == 0)
                {
                    dtAsciutta.Columns.Add("ID", typeof(int));
                    dtAsciutta.Columns.Add("MatrAsl", typeof(string));
                    dtAsciutta.Columns.Add("Data", typeof(DateTime));
                }

                DataTable dtPartorite = new DataTable();
                if (dtPartorite.Columns.Count == 0)
                {
                    dtPartorite.Columns.Add("ID", typeof(int));
                    dtPartorite.Columns.Add("MatrAsl", typeof(string));
                    dtPartorite.Columns.Add("Data", typeof(DateTime));
                }

                foreach (var an in anag)
                {


                    DateTime dataSalto = Convert.ToDateTime("2090-01-01");
                    DateTime dataPartoPross = Convert.ToDateTime("2090-01-01");// = Convert.ToDateTime("2090-01-01");
                    DateTime dataAsciutPross = Convert.ToDateTime("2090-01-01");// = Convert.ToDateTime("2090-01-01");

                    var dd = (from d in an.PartiSalti
                              where d.DataParto == null && d.Salti.Count != 0
                              select new
                              {
                                  DataSalto = d.Salti.Max(ds => ds.DataSalto),
                                  DataAsciutte = d.DataMessaAsciutta
                              }).ToList();

                   
                    if (dd.Count>0)
                    {

                        foreach (var parti in dd)
                        {
                            if (parti.DataSalto != null)
                            {
                                dataSalto = (DateTime)parti.DataSalto;
                                if (parti.DataAsciutte == null)
                                {
                                    dataAsciutPross = dataSalto.Date.AddMonths(7);
                                }
                                else
                                {
                                    dataPartoPross = dataSalto.Date.AddMonths(9);
                                }
                            }

                        }

                    }

                    if (dataPartoPross != Convert.ToDateTime("2090-01-01"))//prossimi parti
                    {
                        DataRow NewRow = dtParto.NewRow();
                        NewRow[0] = an.idAnagrafica;
                        NewRow[1] = an.MatricolaASL;
                        NewRow[2] = dataPartoPross == Convert.ToDateTime("2090-01-01") ? "" : String.Format("{0:dd/MM/yyyy}", dataPartoPross);
                        dtParto.Rows.Add(NewRow);
                    }

                    if (dataAsciutPross != Convert.ToDateTime("2090-01-01"))//prossimi parti
                    {
                        DataRow NewRowA = dtAsciutta.NewRow();
                        NewRowA[0] = an.idAnagrafica;
                        NewRowA[1] = an.MatricolaASL;
                        NewRowA[2] = String.Format("{0:dd/MM/yyyy}", dataAsciutPross);
                        dtAsciutta.Rows.Add(NewRowA);
                    }
                }


                /*
                  DataView dv = ft.DefaultView;
                  dv.Sort = "occr desc";
                  DataTable sortedDT = dv.ToTable();
                 */
                DataView dv = dtParto.DefaultView;
                dv.Sort = "Data";
                DataTable sortedParto = dv.ToTable();

                gridParti.DataSource = sortedParto;
                gridParti.DataBind();

                DataView dvAsc = dtAsciutta.DefaultView;
                dvAsc.Sort = "Data";
                DataTable sortedAsciutte = dvAsc.ToTable();
                gridAsciutta.DataSource = sortedAsciutte;

                gridAsciutta.DataBind();
                gridCoprire.DataSource = dtPartorite;
                gridCoprire.DataBind();
            }
            catch (Exception ex)
            {
                this.GetAlert(this.GetType(), ex.Message);
            }

        }

        protected void grid_OnRowCommand(object sender, GridViewCommandEventArgs e, GridView grid)
        {
            try
            {
                var row = ((Control)(e.CommandSource)).NamingContainer as GridViewRow;
                if (row != null)
                {
                    DataKey dataKey = grid.DataKeys[row.RowIndex];


                    if (dataKey != null)
                    {
                        var id = dataKey.Value.ToString();
                        switch (e.CommandName)
                        {
                            case "Dettaglio":
                                Response.Redirect("~/Dettaglio.aspx?ID=" + id);
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

        #endregion

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            //metodo di ricerca 
            try
            {
                hfAccordionAperto.Value = "accordion-header-Ricerche";
                var cow = new GestioneCowBoy(ConnectionString);

                string sesso = null;
              

                int? attivo = !chVenduti.Checked ? 1 : (int?) 2;
                var lattazione = chLattazione.Checked ? true : (bool?)null;
                var manze = chManze.Checked ? true : (bool?)null;
                var asciutta = chAsciutta.Checked ? true : (bool?)null;
                var cerca = txtSearch.Text.Trim() != string.Empty ? txtSearch.Text.Trim() : null;
                
                

                if (chBovine.Checked && chBovini.Checked == false)
                    sesso = "F";
                else if (chBovini.Checked && chBovine.Checked == false)
                    sesso = "M";

                
                var anag = cow.GetAnagrafica(sesso, attivo, manze, lattazione, asciutta, cerca);
                divGridSearch.Visible = true;

                var selAna = from c in anag
                             select new
                             {
                                 ID = c.idAnagrafica,
                                 MatrAsl = c.MatricolaASL,
                                 NomBovino = c.Nome,
                                 c.Sesso
                             };
                lblContaRecord.Text = selAna.Count().ToString();
                gridResultSearch.DataSource = selAna.ToList();
                gridResultSearch.DataBind();
                divGridSearch.Visible = true;
                //}
                //else
                //{
                //    divGridSearch.Visible = false;
                //}
            }
            catch (Exception ex)
            {
                this.GetAlert(this.GetType(), ex.Message);
            }
        }

        protected void gridParti_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            grid_OnRowCommand(sender, e, gridParti);
        }

        protected void gridParti_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    var c = ((Literal)e.Row.FindControl("ltlData")).Text;
                    if (c.Trim() != string.Empty)
                    {
                        if (Convert.ToDateTime(c) < DateTime.Now)
                        {
                            e.Row.Cells[2].ForeColor = Color.Red;
                            e.Row.Cells[2].Font.Bold = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.GetAlert(this.GetType(), ex.Message);
            }
        }

        protected void gridAsciutta_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            grid_OnRowCommand(sender, e, gridAsciutta);
        }

        protected void gridAsciutta_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    var c = ((Literal)e.Row.FindControl("ltlData")).Text;
                    //  (e.Row.FindControl("ltlData") as Literal).Attributes.Add("onkeyup", "extractNumber(this, 2, false)");
                    if (c.Trim() != string.Empty)
                    {
                        if (Convert.ToDateTime(c) < DateTime.Now)
                        {
                            e.Row.Cells[2].ForeColor = Color.Red;
                            e.Row.Cells[2].Font.Bold = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                this.GetAlert(this.GetType(), ex.Message);
            }
        }

        protected void btnAddBovino_OnClick(object sender, EventArgs e)
        {
            var sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#modalNewBovino').modal('show');");

            sb.Append("$('.classData').datepicker({ dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true});");
            sb.Append("$('.datepicker').zIndex(9999);");

            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", sb.ToString(), false);
        }

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
            //if (prefixText.Length > 0)
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

            //return null;

        }

        [WebMethod]
        public static bool CheckedMatricolaOnly(string prefixText)
        {
            try
            {
                var srv = new GestioneCowBoy(ConnectionString);
                var anag = srv.GetAnagraficaCerca(null, prefixText);
                if (anag.Count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected void btnSalvaNewBovino_OnClick(object sender, EventArgs e)
        {
            try
            {
                string baseImageLocation = ConfigurationManager.AppSettings["PercorsoSalvataggioFoto"];
                Anagrafica figlio = new Anagrafica()
                {
                    idAnagrafica = 0,
                    Madre = hfCercaMadreVal.Value != String.Empty ? Convert.ToInt32(hfCercaMadreVal.Value) : (int?)null,
                    Padre = hfCercaPadreVal.Value != String.Empty ? Convert.ToInt32(hfCercaPadreVal.Value) : (int?)null,
                    DataNascita = Convert.ToDateTime(txtDataNascita.Text),
                    MatricolaASL = txtNewMatricolaASL.Text.Trim().ToUpper(),
                    MatricolaAzienda = txtnNewMatricolaAzienda.Text.Trim().ToUpper(),
                    Nome = txtNewName.Text.Trim(),
                    Sesso = chFfiglio.Checked == true ? "F" : "M",
                    ToroArtificiale = chToroArtificiale.Checked,
                    ToroDaMonta = chToroDaMonta.Checked
                };

                byte[] myData = null;


                Foto fotoFiglio = null;

                if (fuAllegatiNewBovino.PostedFile != null &&
                    fuAllegatiNewBovino.PostedFile.FileName.Trim() != string.Empty)
                {

                    var nomeFoto = Path.GetFileName(fuAllegatiNewBovino.PostedFile.FileName);
                    myData = fuAllegatiNewBovino.FileBytes;

                    fotoFiglio = new Foto()
                    {
                        Nome = nomeFoto,
                        Principale = true
                    };
                }
                var cow = new GestioneCowBoy(ConnectionString);
                cow.SalvaFiglio(figlio, fotoFiglio, baseImageLocation, myData);


            }
            catch (Exception ex)
            {
                this.GetAlert(this.GetType(), ex.Message);
            }
        }

        protected void gridCoprire_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            grid_OnRowCommand(sender, e, gridCoprire);
        }

        protected void gridCoprire_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    var c = ((Literal)e.Row.FindControl("ltlData")).Text;
                    //  (e.Row.FindControl("ltlData") as Literal).Attributes.Add("onkeyup", "extractNumber(this, 2, false)");
                    if (c.Trim() != string.Empty)
                    { 
                        if ((DateTime.Now - Convert.ToDateTime(c)).TotalDays > 40)
                        {
                            e.Row.Cells[2].ForeColor = Color.Red;
                            e.Row.Cells[2].Font.Bold = true;
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

    [DataContract, Serializable]
    public class AnagList
    {
        [DataMember]
        public int Id
        {
            set;
            get;
        }
        [DataMember]
        public string Matricola
        {
            set;
            get;
        }
    }
}