using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CowBoy.Components;
using CowBoy.Library;

namespace CowBoy
{
    public partial class _default : System.Web.UI.Page
    {
        public const string ConnectionString = "CowBoyEntities";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CaricaPartiAsciutte();

            }
        }

        protected void gridResultSearch_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            grid_OnRowCommand(sender, e, gridResultSearch);

        }

        //protected void gridResultSearch_OnRowDataBound(object sender, GridViewRowEventArgs e)
        //{

        //}

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
                    dtParto.Columns.Add("Data", typeof(string));
                }


                DataTable dtAsciutta = new DataTable();
                if (dtAsciutta.Columns.Count == 0)
                {
                    dtAsciutta.Columns.Add("ID", typeof(int));
                    dtAsciutta.Columns.Add("MatrAsl", typeof(string));
                    dtAsciutta.Columns.Add("Data", typeof(string));
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
                              });

                    if (dd.Any())
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

                gridParti.DataSource = dtParto;
                gridParti.DataBind();
                gridAsciutta.DataSource = dtAsciutta;
                gridAsciutta.DataBind();

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
                if (txtSearch.Text.Trim() != string.Empty)
                {
                    var cow = new GestioneCowBoy(ConnectionString);

                    var anag = cow.GetAnagrafica("F", null, null, true, true, txtSearch.Text.Trim());
                    divGridSearch.Visible = true;

                    var SelAna = from c in anag
                                 select new
                                 {
                                     ID = c.idAnagrafica,
                                     MatrAsl = c.MatricolaASL,
                                     NomBovino = c.Nome
                                 };

                    gridResultSearch.DataSource = SelAna.ToList();
                    gridResultSearch.DataBind();
                    divGridSearch.Visible = true;
                }
                else
                {
                    divGridSearch.Visible = false;
                }
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
    }
}