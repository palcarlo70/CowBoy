using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using CowBoy.Components;
using CowBoy.Entities;
using System.IO;


namespace CowBoy.WF
{
    public partial class frmAnagrafica : Form
    {

        #region variabili
        public const string ConnectionString = "CowBoyEntities";
        private string PercorsoFoto = "";
        private List<Foto> FotoGrid = null;
        #endregion

        #region Metodi privati

        private void CaricaGrigliaAnagrafica()
        {
            try
            {
                PuliziaSele();
                gridAnag.Rows.Clear();
                var cow = new GestioneCowBoy(ConnectionString);

                //string sesso, int? attivo, bool? manze, bool? lattazione,bool? asciutta, string riCerca)
                string sesso = null;
                if (chbFemmine.Checked == true && chbMaschi.Checked == false) sesso = "F";
                else if (chbFemmine.Checked == false && chbMaschi.Checked == true) sesso = "M";

                int? attivo = null;
                if (chbPresenti.Checked == true && chbVenduti.Checked == false) attivo = 1;
                else if (chbPresenti.Checked == false && chbVenduti.Checked == true) attivo = 2;

                bool? manze = chbManze.Checked == true ? true : (bool?)null;
                bool? lattazione = chbLattazione.Checked == true ? true : (bool?)null;
                bool? asciutta = chbInAsciutta.Checked == true ? true : (bool?)null;
                string ricerca = txtRicerca.Text.Trim() != string.Empty ? txtRicerca.Text.Trim() : null;

                var anag = cow.GetAnagrafica(sesso, attivo, manze, lattazione, asciutta, ricerca);

                string[] riga;
                foreach (var an in anag)
                {
                    int secondTotF = 0;
                    int secondTotM = 0;
                    int contaParti = 0;




                    foreach (var cap in an.PartiSalti.Select(pa => pa.PartoNoParto).Where(cap => cap != null))
                    {
                        contaParti += 1;
                        secondTotF += (Convert.ToInt32(cap.Substring(3, 1)) - Convert.ToInt32(cap.Substring(2, 1)));
                        secondTotM += (Convert.ToInt32(cap.Substring(5, 1)) - Convert.ToInt32(cap.Substring(4, 1)));
                    }

                    var Stato = "Lattaz";
                    var dataSalto = Convert.ToDateTime("2090-01-01");
                    var dataPartoPross = Convert.ToDateTime("2090-01-01");
                    var dataAsciutPross = Convert.ToDateTime("2090-01-01");

                    if (!an.PartiSalti.Any())
                    {
                        Stato = an.Sesso == "M" ? "MANZO" : "MANZA";
                    }
                    //else if (an.PartiSalti.Where(c => c.DataParto == null && c.DataMessaAsciutta == null).Count() > 0)
                    else
                    {

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
                                        Stato = "Asciut";
                                        dataPartoPross = dataSalto.Date.AddMonths(9);
                                    }
                                }
                                dataSalto = Convert.ToDateTime("2090-01-01");
                            }
                        }
                        else
                        {
                            dataSalto =
                                         Convert.ToDateTime(an.PartiSalti.Max(c => c.DataParto)).AddDays(40);
                        }
                    }


                    if (an.DataFine != null) Stato = "Venduto";

                    int eta = Convert.ToInt32((DateTime.Now - (Convert.ToDateTime(an.DataNascita))).TotalDays / 365);

                    riga = new string[] { 
                        an.idAnagrafica.ToString(), 
                        an.MatricolaASL, 
                        an.Nome, 
                        eta.ToString(),
                        an.Foto.Any( )? an.Foto.First(tt => tt.Principale == true).Nome:string.Empty, 
                        an.Sesso ,
                        Stato, 
                        contaParti.ToString( ),
                        (secondTotF).ToString( ),
                        (secondTotM).ToString( ),
                        dataSalto == Convert.ToDateTime("2090-01-01" ) ? "": String.Format("{0:dd/MM/yyyy}",  dataSalto), 
                        dataAsciutPross == Convert.ToDateTime("2090-01-01" ) ? "":  String.Format("{0:dd/MM/yyyy}",  dataAsciutPross), 
                        dataPartoPross == Convert.ToDateTime("2090-01-01" ) ? "":  String.Format("{0:dd/MM/yyyy}",  dataPartoPross)
                };
                    gridAnag.Rows.Add(riga);
                }
                gridAnag.ClearSelection();
                gridAnag.RowsDefaultCellStyle.BackColor = Color.Bisque;
                gridAnag.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
                lblContaRecord.Text = string.Format("{0} bovini", gridAnag.RowCount);
            }
            catch (Exception ex)
            {
                lblContaRecord.Text = string.Empty;
                MessageBox.Show(ex.Message);
            }
        }

        private void CaricaDettaglio(int idAnag)
        {
            try
            {
                PuliziaSele();
                var cow = new GestioneCowBoy(ConnectionString);
                var ana = cow.GetAnagrafica(idAnag, null, null).First();

                try
                {
                    pctFotoDf.Load(Path.Combine(PercorsoFoto, idAnag.ToString(), ana.Foto.First(c => c.Principale == true).Nome));
                }
                catch (Exception) { lblNoFoto.Visible = true; }
                pctFotoDf.Tag = idAnag;
                txtMatricolaUsl.Text = ana.MatricolaASL;
                txtMatricolaAz.Text = ana.MatricolaAzienda;
                txtNome.Text = ana.Nome;
                const string formatoDate = "dd/MM/yyyy";
                if (ana.DataFine != null)
                {
                    dtpDipartita.CustomFormat = formatoDate;
                    dtpDipartita.Value = Convert.ToDateTime(ana.DataFine);
                }
                if (ana.DataNascita != null)
                {
                    dtpNascita.CustomFormat = formatoDate;
                    dtpNascita.Value = Convert.ToDateTime(ana.DataNascita);
                }
                //recupero info madre padre

                txtMatricolaMadre.Text = ana.Anagrafica2 != null ? ana.Anagrafica2.MatricolaASL : string.Empty;
                txtMatricolaMadre.Tag = ana.Anagrafica2 != null ? ana.Anagrafica2.idAnagrafica : 0;
                txtMatricolaPadre.Text = ana.Anagrafica3 != null ? ana.Anagrafica3.MatricolaASL : string.Empty;
                txtMatricolaPadre.Tag = ana.Anagrafica3 != null ? ana.Anagrafica3.idAnagrafica : 0;

                rbF.Checked = true;
                if (ana.Sesso == "M") rbM.Checked = true;
                if (ana.ToroArtificiale != null) chToroArtificiale.Checked = (bool)ana.ToroArtificiale;
                if (ana.ToroDaMonta != null) chToroMonta.Checked = (bool)ana.ToroDaMonta;


                txtNote.Text = ana.Note;
                botCanc.Visible = true;

                //caricamento delle foto
                CaricaFoto(ana.Foto.ToList());

                //abilito i tab foto e parti
                tabFoto.Enabled = true;
                tabSalti.Enabled = true;

                //carico i parti
                CaricaPartiSalti(idAnag);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void CaricaFoto(List<Foto> fotos)
        {
            var foto = from c in fotos
                       orderby c.Principale descending
                       select new
                       {
                           idF = c.idFoto,
                           idAnag = c.idAnagrafica,
                           Foto = c.Nome,
                           Pr = c.Principale == true ? "Si" : "No",
                           Dat = c.DataInserimento
                       };

            gridFoto.DataSource = foto.ToList();
            gridFoto.Columns[0].Visible = false;
            gridFoto.Columns[1].Visible = false;
            gridFoto.Columns[2].Width = 130;
            gridFoto.Columns[3].Width = 55;
            gridFoto.Columns[4].Visible = false;
            gridFoto.ClearSelection();
        }

        private void CaricaPartiSalti(int idAnag)
        {
            try
            {
                var cow = new GestioneCowBoy(ConnectionString);
                var par = cow.GetListPartiSalti(idAnag, null);

                var lst = from c in par
                          orderby c.DataParto
                          select new
                          {
                              idPart = c.idPartoSalto,
                              DatAsciutta = c.DataMessaAsciutta,
                              DatParto = c.DataParto,
                              Figli = c.PartoNoParto,
                              NotP = c.Note,
                              F = c.PartoNoParto != null ? c.PartoNoParto.Substring(2, 1) : string.Empty,
                              M = c.PartoNoParto != null ? c.PartoNoParto.Substring(4, 1) : string.Empty,
                              Stato = c.DataParto == null ? "Aperto" : "Chiuso"
                          };
                gridParti.DataSource = lst.ToList();

                gridParti.Columns[0].Visible = false;
                gridParti.Columns[1].Visible = false;
                gridParti.Columns[2].Width = 90;
                gridParti.Columns[3].Visible = false;
                gridParti.Columns[4].Visible = false;
                gridParti.Columns[5].Width = 25;
                gridParti.Columns[6].Width = 25;
                gridParti.Columns[7].Width = 80;

                gridParti.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CaricaDettaglioParto(int idParto)
        {
            try
            {
                PuliziaPartiSalti();
                var cow = new GestioneCowBoy(ConnectionString);
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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CaricaSalti(List<Salti> saltis, int? idPartoSalto)
        {
            try
            {
                PuliziaSalti();
                var lSalti = new List<Salti>();

                if (saltis != null)
                {
                    lSalti = saltis;

                }
                else
                {
                    var cow = new GestioneCowBoy(ConnectionString);
                    lSalti = cow.GetListSalti(idPartoSalto, null);
                }

                var t = from v in lSalti
                        orderby v.DataSalto descending
                        select new
                        {
                            idS = v.idSalto,
                            DataSalti = v.DataSalto,
                            colMatricolaASL = v.Anagrafica != null ? v.Anagrafica.MatricolaASL : null,
                            colIdAnagrafica = v.Anagrafica != null ? v.Anagrafica.idAnagrafica : 0
                        };

                gridSalti.DataSource = t.ToList();
                txtContaSalti.Text = gridSalti.RowCount == 0 ? string.Format("Nessun Salto Registrato") : string.Format("Salti totali = {0}", gridSalti.RowCount);
                gridSalti.Columns[0].Visible = false;
                gridSalti.Columns[1].Width = 110;
                gridSalti.Columns[2].Visible = false;
                gridSalti.Columns[3].Visible = false;
                gridSalti.ClearSelection();
                pnlSalti.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CaricaFigli(int idPartoSalto)
        {
            //seleziono i figli registrati a questo parto
            try
            {
                var cow = new GestioneCowBoy(ConnectionString);
                var figli = cow.GetAnagrafica(idPartoSalto);
                var lstFigli = from c in figli
                               select new
                               {
                                   idAnag = c.idAnagrafica,
                                   MatrUsl = c.MatricolaASL,
                                   MatrAz = c.MatricolaAzienda,
                                   NomeFiglio = c.Nome,
                                   Fot = c.Foto.Count > 0 ? c.Foto.First(d => d.Principale == true).Nome : string.Empty,
                                   S = c.Sesso
                               };

                gridFigli.DataSource = lstFigli.ToList();
                gridFigli.Columns[0].Visible = false;
                gridFigli.Columns[1].Width = 110;
                gridFigli.Columns[2].Visible = false;
                gridFigli.Columns[3].Visible = false;
                gridFigli.Columns[4].Visible = false;//nome foto
                gridFigli.Columns[5].Width = 30;

                gridFigli.ClearSelection();
                pnlFigli.Visible = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void PuliziaSele()
        {
            //------------ANAGRAFICA 
            pctFotoDf.Tag = 0;
            pctFotoDf.Image = null;
            txtMatricolaUsl.Text = string.Empty;
            txtNote.Text = string.Empty;
            txtMatricolaAz.Text = string.Empty;
            txtNome.Text = string.Empty;
            txtMatricolaMadre.Text = string.Empty;
            txtMatricolaMadre.Tag = 0;
            txtMatricolaPadre.Text = string.Empty;
            txtMatricolaPadre.Tag = 0;
            dtpDipartita.CustomFormat = "  ";
            dtpNascita.CustomFormat = "  ";
            lblNoFoto.Visible = false;
            rbF.Checked = false;
            rbM.Checked = false;
            botCanc.Visible = false;
            gridFoto.DataSource = null;
            TabContainer.SelectedIndex = 0;
            tabFoto.Enabled = false;
            tabSalti.Enabled = false;
            chToroArtificiale.Visible = false;
            chToroMonta.Visible = false;
            //------------ANAGRAFICA FINE
            PuliziaFoto();
            PuliziaPartiSalti();
        }

        private void PuliziaFoto()
        {
            //-------FOTO
            pctFotoModifica.Image = null;
            txtNomeFoto.Text = string.Empty;
            txtDataFoto.Text = string.Empty;
            chFoto.Checked = false;
            pctFotoModifica.Tag = 0;
            btnDeleteFoto.Visible = false;
            chFoto.Enabled = true;
            //-------FOTO FINE
        }

        private void PuliziaPartiSalti()
        {
            PuliziaSalti();
            PuliziaRegFigli();

            dtpAsciuttaIns.CustomFormat = @" ";
            dtpPartoIns.CustomFormat = @" ";
            btnSaveParto.Tag = 0;

            pnlDetParto.Visible = false;
            rbFacileSi.Checked = false;
            rbFacileNo.Checked = false;
            rbNaturaleSi.Checked = false;
            rbNaturaleNo.Checked = false;
            txtFvivi.Text = @"0";
            txtFmorte.Text = @"0";
            txtMvivi.Text = @"0";
            txtMmorti.Text = @"0";
            txtNoteParto.Text = string.Empty;
            btnDelParto.Visible = false;
        }

        private void PuliziaSalti(bool puliGrid = true)
        {
            btnDelSalto.Visible = false;
            if (puliGrid) gridSalti.DataSource = null;
            txtMatriToro.Text = string.Empty;
            txtMatriToro.Tag = 0;//memorizzo id del toro
            dtpSalto.Tag = 0;//memorizzo id del salto
            dtpSalto.CustomFormat = @"  ";
            pnlSalti.Visible = !puliGrid;
        }

        private void PuliziaRegFigli(bool puliGrid = true)
        {
            txtMatriUslIns.Text = string.Empty;
            txtMatriUslIns.Tag = 0;
            txtMatriAziIns.Text = string.Empty;
            rbFins.Checked = false;
            rbMins.Checked = false;
            pnlFigli.Visible = !puliGrid;
            btnDelFiglio.Visible = false;
            btnOpenAnaFiglio.Visible = true;
            txtFotoInsFiglio.Text = string.Empty;
        }


        private void SeleRow(DataGridView grid, int iDValue, int colum)
        {
            for (int i = 0; i < grid.RowCount; i++)
            {
                if (Convert.ToInt32(grid.Rows[i].Cells[colum].Value) == iDValue)
                {

                    for (int r = 0; r < grid.ColumnCount; r++)
                    {
                        if (grid.Columns[r].Visible == true)//seleziono la cella visibile
                        {
                            grid.CurrentCell = grid.Rows[i].Cells[r];
                            break;
                        }
                    }
                    grid.Rows[i].Selected = true;
                    return;
                }
            }

        }


        #endregion

        public frmAnagrafica()
        {
            var f = new frmAvvio();
            f.Show(this);
            InitializeComponent();
            Location = new Point(50, 10);
            PercorsoFoto = ConfigurationManager.AppSettings["PercorsoFoto"];
            CaricaGrigliaAnagrafica();

        }

        private void frmAnagrafica_Load(object sender, EventArgs e)
        {


        }


        #region Griglie

        private void gridAnag_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectFotoGrid(sender, e);
        }

        private void gridFigli_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectFotoGrid(sender, e);
        }

        private void SelectFotoGrid(object sender, DataGridViewCellMouseEventArgs e)
        {

            DataGridView dg = (DataGridView)sender;

            Point prop = new Point(e.X, e.Y);
            FotoGrid = null;
            if ((e.ColumnIndex == 1)
                && e.RowIndex != -1)
            {
                var cellRectangle = dg.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                var y = cellRectangle.Y;
                try
                {
                    var altezzaTab = this.Size.Height;
                    var altezzaPnl = pnlFoto.Size.Height;

                    if (y + altezzaPnl > altezzaTab)
                    {
                        y = altezzaTab - altezzaPnl;
                    }

                    var foto = dg.Rows[e.RowIndex].Cells[4].Value;
                    var id = dg.Rows[e.RowIndex].Cells[0].Value;

                    btnDopo.Tag = dg.Rows[e.RowIndex].Cells[0].Value;
                    btnDefault.Tag = e.RowIndex;
                    lblFoto.Text = string.Format("{0}-{1}-{2}", dg.Rows[e.RowIndex].Cells[1].Value, dg.Rows[e.RowIndex].Cells[2].Value, dg.Rows[e.RowIndex].Cells[3].Value);
                    pnlFoto.Location = new Point(cellRectangle.X + 100, y);
                    pctFoto.Load(Path.Combine(PercorsoFoto, id.ToString(), foto.ToString()));

                }
                catch (Exception ex)
                {
                    btnDopo.Tag = -1;
                    btnDefault.Tag = -1;
                    lblFoto.Text = ex.Message;
                    pnlFoto.Location = new Point(cellRectangle.X + 100, y);
                    pctFoto.Image = null;
                }
                pnlFoto.Visible = true;
            }
            else
            {
                pnlFoto.Visible = false;
            }
        }

        private void gridAnag_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                CaricaDettaglio(Convert.ToInt32(gridAnag.Rows[e.RowIndex].Cells[0].Value.ToString()));
            }
        }


        private void gridParti_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            try
            {
                //recupero le informazione  dell'anagrafica del parto selezionata
                int idPartoSalto = Convert.ToInt32(gridParti.Rows[e.RowIndex].Cells[0].Value);
                //apro i salti
                CaricaDettaglioParto(idPartoSalto);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void gridTori_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            txtMatriToro.Tag = Convert.ToInt32(gridTori.Rows[e.RowIndex].Cells[0].Value);
            txtMatriToro.Text = gridTori.Rows[e.RowIndex].Cells[1].Value.ToString();
            pnlTori.Visible = false;
        }

        private void gridFoto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                pctFotoModifica.Tag = 0;
                pctFotoModifica.Image = null;
                txtNomeFoto.Text = string.Empty;
                txtDataFoto.Text = string.Empty;
                chFoto.Checked = false;
                btnDeleteFoto.Visible = false;

                pctFotoModifica.Load(Path.Combine(PercorsoFoto, gridFoto.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    gridFoto.Rows[e.RowIndex].Cells[2].Value.ToString()));
                txtNomeFoto.Text = gridFoto.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtDataFoto.Text = string.Format("{0:dd/MM/yyyy}", gridFoto.Rows[e.RowIndex].Cells[4].Value);
                chFoto.Checked = gridFoto.Rows[e.RowIndex].Cells[3].Value.ToString() != "No";
                chFoto.Enabled = !chFoto.Checked;
                pctFotoModifica.Tag = Convert.ToInt32(gridFoto.Rows[e.RowIndex].Cells[0].Value);
                btnDeleteFoto.Visible = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Foto Non trovata, contattare l'amministratore");
            }
        }

        private void gridSalti_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            txtMatriToro.Text = string.Empty;
            txtMatriToro.Tag = 0;
            dtpSalto.Tag = 0;
            dtpSalto.Tag = (int)gridSalti.Rows[e.RowIndex].Cells[0].Value;
            dtpSalto.Value = Convert.ToDateTime("01/01/1950");
            dtpSalto.Value = (DateTime)gridSalti.Rows[e.RowIndex].Cells[1].Value;

            if (gridSalti.Rows[e.RowIndex].Cells[2].Value != null)
            {
                txtMatriToro.Text = gridSalti.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtMatriToro.Tag = gridSalti.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
            btnDelSalto.Visible = true;

        }

        private void gridFigli_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            try
            {
                PuliziaRegFigli(false);

                txtMatriUslIns.Tag = (int)gridFigli.Rows[e.RowIndex].Cells[0].Value;
                txtMatriUslIns.Text = gridFigli.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtMatriAziIns.Text = gridFigli.Rows[e.RowIndex].Cells[2].Value != null ? gridFigli.Rows[e.RowIndex].Cells[2].Value.ToString() : string.Empty;
                rbFins.Checked = gridFigli.Rows[e.RowIndex].Cells[5].Value.ToString() == "F";
                rbMins.Checked = gridFigli.Rows[e.RowIndex].Cells[5].Value.ToString() == "M";
                txtFotoInsFiglio.Text = gridFigli.Rows[e.RowIndex].Cells[4].Value != null ? gridFigli.Rows[e.RowIndex].Cells[4].Value.ToString() : string.Empty;
                btnDelFiglio.Visible = true;
                btnOpenAnaFiglio.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void gridAnag_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                var dataSalto = gridAnag.Rows[e.RowIndex].Cells["colSalto"].Value.ToString() != string.Empty
                    ? (int)(DateTime.Now - (Convert.ToDateTime(gridAnag.Rows[e.RowIndex].Cells["colSalto"].Value))).TotalDays
                    : (int?)null;
                var dataAsciutta = gridAnag.Rows[e.RowIndex].Cells["colAsciutta"].Value.ToString() != string.Empty
                    ? (int)(DateTime.Now - (Convert.ToDateTime(gridAnag.Rows[e.RowIndex].Cells["colAsciutta"].Value))).TotalDays
                    : (int?)null;
                var dataParto = gridAnag.Rows[e.RowIndex].Cells["colParto"].Value.ToString() != string.Empty
                    ? (int)(DateTime.Now - (Convert.ToDateTime(gridAnag.Rows[e.RowIndex].Cells["colParto"].Value))).TotalDays
                    : (int?)null;


                if (dataSalto > -2)
                    gridAnag.Rows[e.RowIndex].Cells["colSalto"].Style.BackColor = Color.Orange;

                if (dataAsciutta > -2)
                    gridAnag.Rows[e.RowIndex].Cells["colAsciutta"].Style.BackColor = Color.Red;

                if (dataParto > -2)
                    gridAnag.Rows[e.RowIndex].Cells["colParto"].Style.BackColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Pulsanti

        private void btnDefault_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(btnDefault.Tag) != -1)
            {
                try
                {
                    var id = gridAnag.Rows[Convert.ToInt32(btnDefault.Tag)].Cells[0].Value;
                    var foto = gridAnag.Rows[Convert.ToInt32(btnDefault.Tag)].Cells[4].Value;
                    pctFoto.Load(Path.Combine(PercorsoFoto, id.ToString(), foto.ToString()));
                }
                catch (Exception)
                { }
            }
        }

        private void btnDopo_Click(object sender, EventArgs e)
        {
            if (FotoGrid == null)
            {
                var cow = new GestioneCowBoy(ConnectionString);
                //chiamo il metodo per caricarmi tutte le foto associate al bovino
                if (Convert.ToInt32(btnDefault.Tag) != -1)
                {
                    FotoGrid =
                        new List<Foto>(
                            cow.GetFoto(Convert.ToInt32(gridAnag.Rows[Convert.ToInt32(btnDefault.Tag)].Cells[0].Value)));
                }
                var trovata = 0;
                if (FotoGrid != null)
                    foreach (var foto in FotoGrid)
                    {
                        if (foto.Principale == true)
                        {
                            trovata = foto.idFoto;
                        }
                        else if (trovata > 0)
                        {
                            btnDopo.Tag = foto.idFoto;
                            var id = gridAnag.Rows[Convert.ToInt32(btnDefault.Tag)].Cells[0].Value;
                            try
                            { pctFoto.Load(Path.Combine(PercorsoFoto, id.ToString(), foto.Nome)); }
                            catch (Exception)
                            { }
                            break;
                        }
                    }
            }
            else
            {
                if (Convert.ToInt32(btnDopo.Tag) != -1)
                {
                    var id = gridAnag.Rows[Convert.ToInt32(btnDefault.Tag)].Cells[0].Value;
                    var ok = false;
                    foreach (var foto in FotoGrid)
                    {
                        if (foto.idFoto > Convert.ToInt32(btnDopo.Tag))
                        {
                            ok = true;
                            btnDopo.Tag = foto.idFoto;

                            try
                            { pctFoto.Load(Path.Combine(PercorsoFoto, id.ToString(), foto.Nome)); }
                            catch (Exception)
                            { }
                            break;
                        }

                    }
                    if (ok == false)
                    {
                        btnDopo.Tag = FotoGrid.First().idFoto;
                        try
                        { pctFoto.Load(Path.Combine(PercorsoFoto, id.ToString(), FotoGrid.First().Nome)); }
                        catch (Exception)
                        { }
                    }
                }
            }
        }

        private void btnDeleteElimina_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Volete cancellare la data di uscita dall'azienda del Bovino?", "Cancellazione della Data", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                dtpDipartita.Value = Convert.ToDateTime("10/10/1950");
                dtpDipartita.CustomFormat = @"  ";
            }

        }

        private void label5_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.RotateTransform(30);
            e.Graphics.DrawString("Foto non presente", this.lblNoFoto.Font, new SolidBrush(this.lblNoFoto.ForeColor), new PointF(5, 0));
        }

        private void dtpDipartita_ValueChanged(object sender, EventArgs e)
        {
            const string formatoDate = "dd/MM/yyyy";
            dtpDipartita.CustomFormat = formatoDate;
        }

        private void dtpNascita_ValueChanged(object sender, EventArgs e)
        {
            const string formatoDate = "dd/MM/yyyy";
            dtpNascita.CustomFormat = formatoDate;
        }

        private void btnMadre_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtMatricolaMadre.Tag) != 0)
            {
                CaricaDettaglio(Convert.ToInt32(txtMatricolaMadre.Tag));
            }
            else
            {
                MessageBox.Show("Anagrafica non presente");
            }
        }

        private void btnPadre_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtMatricolaPadre.Tag) != 0)
            {
                CaricaDettaglio(Convert.ToInt32(txtMatricolaPadre.Tag));
            }
            else
            {
                MessageBox.Show("Anagrafica non presente");
            }
        }

        private void botNew_Click(object sender, EventArgs e)
        {
            PuliziaSele();

        }

        private void botSal_Click(object sender, EventArgs e)
        {
            string messaggio = string.Empty;

            if (txtMatricolaUsl.Text.Trim() == string.Empty)
                messaggio = "Inserire la matricola della ASL";
            if (rbM.Checked == false && rbF.Checked == false)
                messaggio = "Indicare il Sesso";
            if (dtpNascita.Text.Trim() == string.Empty)
                messaggio = "Inserire la data di nascita";
            //data troppo retrodatata
            if (dtpNascita.Value < DateTime.Now.AddYears(-55))
                messaggio = "La data di Nascita inserita è troppo indietro con gli anni verificarla";
            //data troppo avanti
            if (dtpNascita.Value > DateTime.Now)
                messaggio = "La data di Nascita inserita è superiore alla data odierna";


            if (messaggio != string.Empty)
            {
                MessageBox.Show(messaggio);
                return;
            }

            var cow = new GestioneCowBoy(ConnectionString);

            try
            {
                var ana = new Anagrafica();
                if (Convert.ToInt32(pctFotoDf.Tag) != 0)
                {
                    ana = cow.GetAnagrafica(Convert.ToInt32(pctFotoDf.Tag), null, null).First();
                    //verifico che se prime avevo sesso maschio e adesso lo sto cambiando non devo avere associato nessun salto con quel id 
                    if (ana.Sesso == "M" && rbF.Checked == true)
                    {
                        var toroSalto = cow.GetListPartiSalti(null, null);
                        if (toroSalto.Select(ddd => ddd.Salti.Count(d => d.idToro == Convert.ToInt32(pctFotoDf.Tag))).Any(c => c > 0))
                        {
                            messaggio = "Attenzione non potete cambiare il sesso al toro selezionato poichè risulta utilizzato in alcuni salti";
                            MessageBox.Show(messaggio);
                            return;
                        }
                    }
                }

                ana.MatricolaASL = txtMatricolaUsl.Text.Trim();
                ana.MatricolaAzienda = txtMatricolaAz.Text.Trim();
                ana.Nome = txtNome.Text.Trim();
                ana.Note = txtNote.Text.Trim();
                ana.Sesso = rbF.Checked == true ? "F" : "M";
                ana.DataNascita = dtpNascita.Value;
                ana.DataFine = dtpDipartita.Text.Trim() == string.Empty ? (DateTime?)null : dtpDipartita.Value;
                ana.ToroArtificiale = chToroArtificiale.Checked;
                ana.ToroDaMonta = chToroMonta.Checked;

                cow.SaveAnagrafica(ana);
                CaricaGrigliaAnagrafica();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void botCanc_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show("Attenzione volete procedere all'eliminazione del bovino selezionato", "Elimina Bovino",
                    MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                var cow = new GestioneCowBoy(ConnectionString);

                try
                {
                    var ana = new Anagrafica();
                    if (Convert.ToInt32(pctFotoDf.Tag) == 0)
                        return;

                    ana.idAnagrafica = Convert.ToInt32(pctFotoDf.Tag);
                    cow.DeleteAnagrafica(ana);
                    CaricaGrigliaAnagrafica();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnNewFoto_Click(object sender, EventArgs e)
        {
            pctFotoModifica.Tag = 0;
            pctFotoModifica.Image = null;
            txtNomeFoto.Text = string.Empty;
            txtDataFoto.Text = string.Empty;
            chFoto.Checked = false;
            btnDeleteFoto.Visible = false;
            chFoto.Enabled = true;
        }

        private void btnSaveFoto_Click(object sender, EventArgs e)
        {
            if (txtNomeFoto.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Inserire una immagine");
                return;
            }

            int idAna = Convert.ToInt32(pctFotoDf.Tag);

            try
            {
                var cowF = new GestioneCowBoy(ConnectionString);
                var foto = cowF.GetFoto(idAna);

                if (chFoto.Checked == true) //setto le altre foto tutte a false
                    foreach (var fo in foto.Where(fo => fo.idAnagrafica != Convert.ToInt32(pctFotoModifica.Tag) && fo.idFoto != Convert.ToInt32(pctFotoModifica.Tag)))
                    {
                        fo.Principale = false;
                    }

                //copia della nuova foto nella cartella predefinita
                if (txtNomeFoto.Text.Contains(@"\"))
                {
                    var percoFoto = Path.Combine(PercorsoFoto, pctFotoDf.Tag.ToString(), Path.GetFileName(txtNomeFoto.Text));
                    if (!Directory.Exists(Path.Combine(PercorsoFoto, pctFotoDf.Tag.ToString())))
                        Directory.CreateDirectory(Path.Combine(PercorsoFoto, pctFotoDf.Tag.ToString()));
                    File.Copy(txtNomeFoto.Text, percoFoto, true);
                }

                var f = new Foto();

                if (Convert.ToInt32(pctFotoModifica.Tag) != 0)
                    f = foto.First(fo => fo.idFoto == Convert.ToInt32(pctFotoModifica.Tag));
                f.Principale = chFoto.Checked;
                f.DataInserimento = DateTime.Now;
                f.Nome = txtNomeFoto.Text.Contains(@"\") ? Path.GetFileName(txtNomeFoto.Text) : txtNomeFoto.Text; ;
                f.idAnagrafica = Convert.ToInt32(pctFotoDf.Tag.ToString());
                if (Convert.ToInt32(pctFotoModifica.Tag) == 0)//aggiungo il nuovo ogetto
                    foto.Add(f);

                cowF.SaveFotoList(foto);
                PuliziaFoto();
                CaricaFoto(foto);
                //reimposto la foto di default 
                try
                {
                    lblNoFoto.Visible = false;
                    pctFotoDf.Load(Path.Combine(PercorsoFoto, pctFotoDf.Tag.ToString(), foto.First(c => c.Principale == true).Nome));
                }
                catch (Exception) { lblNoFoto.Visible = true; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCaricaFoto_Click(object sender, EventArgs e)
        {
            try
            {
                if (ofdFoto.ShowDialog() == DialogResult.OK)
                {
                    pctFotoModifica.Load(ofdFoto.FileName);
                    txtNomeFoto.Text = ofdFoto.FileName;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Inserire una immagine");
            }
        }

        private void TabContainer_MouseMove(object sender, MouseEventArgs e)
        {
            pnlFoto.Visible = false;
        }

        private void rbM_CheckedChanged(object sender, EventArgs e)
        {
            chToroArtificiale.Visible = rbM.Checked;
            chToroMonta.Visible = rbM.Checked;
            if (rbM.Checked) return;
            chToroArtificiale.Checked = false;
            chToroMonta.Checked = false;
        }

        private void dtpSalto_ValueChanged(object sender, EventArgs e)
        {
            const string formatoDate = "dd/MM/yyyy";
            dtpSalto.CustomFormat = formatoDate;
        }

        private void dtpPartoIns_ValueChanged(object sender, EventArgs e)
        {
            const string formatoDate = "dd/MM/yyyy";
            dtpPartoIns.CustomFormat = formatoDate;
            pnlDetParto.Visible = true;
        }

        private void dtpAsciuttaIns_ValueChanged(object sender, EventArgs e)
        {
            const string formatoDate = "dd/MM/yyyy";
            dtpAsciuttaIns.CustomFormat = formatoDate;
        }

        private void btnDelDataAsciutta_Click(object sender, EventArgs e)
        {
            //verifico se ho figli registrati non posso eliminare la data del parto
            if (gridFigli.RowCount > 0 && pnlFigli.Visible == true)
            {
                MessageBox.Show(
                    string.Format("Attenzione la data non può essere eliminata se sono stati registrati i figli"));
                return;
            }
            dtpAsciuttaIns.Value = Convert.ToDateTime("10/10/1950");
            dtpAsciuttaIns.CustomFormat = @" ";
        }

        private void btnDelDataParto_Click(object sender, EventArgs e)
        {
            //verifico se ho figli registrati non posso eliminare la data del parto
            if (gridFigli.RowCount > 0 && pnlFigli.Visible == true)
            {
                MessageBox.Show(
                    string.Format("Attenzione la data non può essere eliminata se sono stati registrati i figli"));
                return;
            }
            dtpPartoIns.Value = Convert.ToDateTime("10/10/1950");
            dtpPartoIns.CustomFormat = @" ";
            pnlDetParto.Visible = false;
            rbFacileSi.Checked = false;
            rbFacileNo.Checked = false;
            rbNaturaleSi.Checked = false;
            rbNaturaleNo.Checked = false;
            txtFvivi.Text = @"0";
            txtFmorte.Text = @"0";
            txtMvivi.Text = @"0";
            txtMmorti.Text = @"0";


        }

        private void btnNewParto_Click(object sender, EventArgs e)
        {
            if (rbM.Checked == true)
            {
                {
                    MessageBox.Show(string.Format("{0}",
                        "Attenzione un toro non può avere Salti/Parti"));
                    return;
                }
            }
            //var f = gridParti.DataSource;
            for (int i = 0; i < gridParti.RowCount; i++)
            {
                if (gridParti.Rows[i].Cells[2].Value == null)
                {
                    MessageBox.Show(string.Format("{0}",
                        "Attenzione prima di creare un nuovo procedimento di Parto chiudere il parto con lo stato aperto"));
                    return;
                }
            }
            PuliziaPartiSalti();
            //salvo subito un nuovo parto verificando prima che non ce ne siano ancora da chiudere
            var cow = new GestioneCowBoy(ConnectionString);
            try
            {
                var parto = new PartiSalti();
                parto.idPartoSalto = 0;
                parto.idAnagrafica = Convert.ToInt32(pctFotoDf.Tag);
                int idPartoSalto = cow.SavePartoSalto(parto);
                //ricarico la griglia selezionando il record appena salvato
                CaricaPartiSalti(parto.idAnagrafica);
                SeleRow(gridParti, idPartoSalto, 0);
                CaricaDettaglioParto(idPartoSalto);
                btnDelParto.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void botClosePnlTori_Click(object sender, EventArgs e)
        {
            pnlTori.Visible = false;
        }
        private void btnSeleToro_Click(object sender, EventArgs e)
        {
            pnlTori.Location = new Point(552, 457);
            pnlTori.Visible = true;
        }

        private void botDeleteMatricolaToro_Click(object sender, EventArgs e)
        {
            txtMatriToro.Tag = 0;
            txtMatriToro.Text = string.Empty;
        }


        private void btnNewSalto_Click(object sender, EventArgs e)
        {
            PuliziaSalti(false);
        }

        private void btnDelSalto_Click(object sender, EventArgs e)
        {

            try
            {
                //controllo che il parto non sia avvenuto 
                var cn = new GestioneCowBoy(ConnectionString);
                var lstParti = cn.GetListPartiSalti(null, (int)btnSaveParto.Tag).First().DataMessaAsciutta;

                if (dtpAsciuttaIns.CustomFormat.Trim() != String.Empty || lstParti != null)
                {
                    MessageBox.Show(string.Format("Non potete eliminare il salto poichè risulta che la bovina è stata asciugata"));
                    return;
                }
                //verifico se ho valorizzato id del salto
                if (dtpSalto.Tag.ToString().Trim() == "" || dtpSalto.Tag.ToString() == "0")
                {
                    MessageBox.Show(string.Format("Selezionare il salto"));
                    return;
                }

                //altrimenti elimino
                if (MessageBox.Show(string.Format("Volete procedere con l'eliminazione del record?"), string.Format("Eliminazione record"), MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                {
                    var salto = new Salti();
                    salto.idSalto = (int)dtpSalto.Tag;
                    cn.DeleteSalto(salto);
                    MessageBox.Show(string.Format("Cancellazione avvenuta con successo"));
                    CaricaSalti(null, (int)btnSaveParto.Tag);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSaveSalto_Click(object sender, EventArgs e)
        {
            try
            {
                var msg = (dtpSalto.Tag.ToString().Trim() == "" || dtpSalto.Tag.ToString() == "0") ? string.Format("Non potete modificare salto poichè la bovina è stota asciugata")
                    : "Non potete aggiungere un altro salto poichè la bovina è stota asciugata";


                //se inserisco un nuovo salto o modifico uno esistente controllo che il parto non sia avvenuto  
                var cn = new GestioneCowBoy(ConnectionString);
                var lstParti = cn.GetListPartiSalti(null, (int)btnSaveParto.Tag).First().DataMessaAsciutta;

                if (dtpAsciuttaIns.CustomFormat.Trim() != String.Empty || lstParti != null)
                {
                    MessageBox.Show(msg);
                    return;
                }
                //verifico se ho valorizzato la data
                if (dtpSalto.CustomFormat.Trim() == String.Empty)
                {
                    MessageBox.Show(string.Format("Selezionare la data"));
                    return;
                }

                //verifico se hanno selezionato il toro nel caso contrario chiedo se vogliono comunque continuare
                if (txtMatriToro.Tag.ToString() == "0")
                {
                    if (
                        MessageBox.Show(
                            string.Format("Non avete selezionato il toro. Volete coninuare il salvataggio?"),
                            string.Format("Salvataggio senza toro"), MessageBoxButtons.YesNo) != DialogResult.Yes)
                        return;
                }
                if ((int)dtpSalto.Tag == 0 && gridSalti.Rows.Cast<DataGridViewRow>().Any(r => string.Format("{0:d}", (DateTime)r.Cells[1].Value) == string.Format("{0:d}", (DateTime)dtpSalto.Value)))
                {
                    MessageBox.Show(string.Format("Il salto non può essere inserito perchè risulta già una registrazione con la stessa data"), string.Format("Impossibile inserire il salto"));
                    return;
                }
                //altrimenti salvo
                var salto = new Salti();
                salto.idSalto = (int)dtpSalto.Tag;
                salto.DataSalto = (DateTime)dtpSalto.Value;
                salto.idToro = txtMatriToro.Tag.ToString() != "0" ? Convert.ToInt32(txtMatriToro.Tag) : (int?)null;
                salto.idPartoSalto = Convert.ToInt32(btnSaveParto.Tag);
                cn.SaveSalto(salto);
                //MessageBox.Show(string.Format("Cancellazione avvenuta con successo"));
                CaricaSalti(null, (int)btnSaveParto.Tag);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnSaveParto_Click(object sender, EventArgs e)
        {
            //qui entro sempre in modifica poichè il nuovo record è stato inserito con il nuovo
            //verifico la congruenza dei dati

            if (btnSaveParto.Tag.ToString() == "0")
            {
                MessageBox.Show(string.Format("Attenzione selezionare il record"), string.Format("Attenzione mancanza dati"));
                PuliziaPartiSalti();
                return;
            }
            // se inserisco la data del parto devo avere anche la data di asciutta
            if (dtpAsciuttaIns.CustomFormat.Trim() == String.Empty && dtpPartoIns.CustomFormat.Trim() != String.Empty)
            {
                MessageBox.Show(string.Format("Attenzione prima di procedere inserire la data di messa in asciutta"), string.Format("Attenzione mancanza dati"));
                return;
            }
            int fV = Convert.ToInt32(txtFvivi.Text);
            int fM = Convert.ToInt32(txtFmorte.Text);

            int mV = Convert.ToInt32(txtMvivi.Text);
            int mM = Convert.ToInt32(txtMmorti.Text);

            if ((fV + fM + mV + mM) > 4)
            {
                {
                    MessageBox.Show(string.Format("Attenzione il numero dei vitelli nati (somma delle femmine e maschi vivi e morti) è esagerato."), string.Format("Attenzione mancanza dati"));
                    return;
                }
            }

            string partoNoParto = string.Format("{0}{1}{2}{3}{4}{5}", (fV + mV), (fV + mV + fM + mM), (fV), (fV + fM), (mV), (mV + mM));



            if (dtpPartoIns.CustomFormat.Trim() != String.Empty) //solo quando ho inserito la data del parto 
            {// verifico che abbia inserito almno un figlio e che abbia scelto i radio button
                if (partoNoParto == "000000")
                {
                    MessageBox.Show(string.Format("Attenzione prima di procedere inserire i vitelli nati"), string.Format("Attenzione mancanza dati"));
                    return;
                }
                //verifico che abbiamo valorizzato le radio button difficoltà e naturale
                if ((rbFacileNo.Checked == false && rbFacileSi.Checked == false) || (rbNaturaleNo.Checked == false && rbNaturaleSi.Checked == false))
                {
                    MessageBox.Show(string.Format("Attenzione prima di procedere inserire se è un parto Difficile e se è Naturale "), string.Format("Attenzione mancanza dati"));
                    return;
                }
            }



            var cow = new GestioneCowBoy(ConnectionString);
            try
            {
                var parto = new PartiSalti();
                parto.idPartoSalto = Convert.ToInt32(btnSaveParto.Tag);
                parto.idAnagrafica = Convert.ToInt32(pctFotoDf.Tag);
                parto.DataMessaAsciutta = dtpAsciuttaIns.CustomFormat.Trim() == String.Empty
                    ? (DateTime?)null
                    : dtpAsciuttaIns.Value;
                parto.DataParto = dtpPartoIns.CustomFormat.Trim() == String.Empty
                    ? (DateTime?)null
                    : dtpPartoIns.Value;
                parto.PartoNoParto = partoNoParto;
                parto.Facile = rbFacileSi.Checked;
                parto.Naturale = rbNaturaleSi.Checked;
                parto.Note = txtNoteParto.Text.Trim();
                cow.SavePartoSalto(parto);
                CaricaPartiSalti(parto.idAnagrafica);
                SeleRow(gridParti, parto.idPartoSalto, 0);
                CaricaDettaglioParto(parto.idPartoSalto);
                btnDelParto.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelParto_Click(object sender, EventArgs e)
        {
            var idPartoSalto = Convert.ToInt32(btnSaveParto.Tag);
            if (idPartoSalto == 0)
            {
                MessageBox.Show(string.Format("Attenzione selezionare il record"), string.Format("Attenzione mancanza dati"));
                PuliziaPartiSalti();
                return;
            }
            //verifico che i figli non siano stati registrati
            string msg =
                string.Format(
                    "Attenzione prima di eliminare i dati del parto eliminare dall'anagrafica i figli associati");
            var cow = new GestioneCowBoy(ConnectionString);

            var figliregistrati = cow.GetAnagrafica(idPartoSalto).Count();
            if (figliregistrati > 0)
            {
                MessageBox.Show(string.Format(msg), string.Format("Attenzione figli registrati"));
                return;
            }


            msg = "Sicuri di voler eliminare il parto selezionato?";
            if (MessageBox.Show(msg, "Eliminazione dati", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                var parto = new PartiSalti { idPartoSalto = idPartoSalto };
                cow.DeletePartoSalto(parto);
                PuliziaPartiSalti();
                CaricaPartiSalti(parto.idAnagrafica);
                msg = "Cancellazione avvenuta con successo";
                MessageBox.Show(msg);
            }



        }

        private void btnDelFiglio_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                string.Format("Per eliminare il figlio registrato entrare nel dettaglio dell'anagrafica e procedere"));
        }

        private void btnSaveFiglio_Click(object sender, EventArgs e)
        {
            try
            {
                var idPartosalto = Convert.ToInt32(btnSaveParto.Tag);
                if (Convert.ToInt32(txtMatriUslIns.Tag) != 0)
                {
                    MessageBox.Show(
                     string.Format("Per apportare modifiche al figlio registrato entrare nel dettaglio dell'anagrafica e procedere"));
                    return;
                }
                //procedura di inserimento in anagrafica del figlio 
                //txtMatriUslIns.Tag = (int)gridFigli.Rows[e.RowIndex].Cells[0].Value;
                if (txtMatriUslIns.Text.Trim() == string.Empty)
                {
                    MessageBox.Show(
                     string.Format("Inserire la Matricola Usl"));
                    return;
                }
                if (rbFins.Checked == false && rbMins.Checked == false)
                {
                    MessageBox.Show(
                     string.Format("Indicare il sesso"));
                    return;
                }

                var sesso = rbFins.Checked == false ? "M" : "F";
                //verifico che il numero di figli registrati più quello che si sta registrando equivalga al numero di figli 
                //recupero del umero di figli registrati con quel sesso
                var cow = new GestioneCowBoy(ConnectionString);
                var anagParto = cow.GetListPartiSalti(null, idPartosalto).First();
                var contaFigliReg = cow.GetAnagrafica(idPartosalto).Count(m => m.Sesso == sesso);
                var partiNoParti = anagParto.PartoNoParto;

                int viviF = Convert.ToInt32(partiNoParti.Substring(2, 1));
                int viviM = Convert.ToInt32(partiNoParti.Substring(4, 1));

                if ((sesso == "F" && ((contaFigliReg + 1 > viviF))) || (sesso == "M" && ((contaFigliReg + 1) > viviM)))
                {
                    MessageBox.Show(
                        string.Format("Attenzione il numero di figl{0} registrati per questo parto è superiore ai nati", sesso == "F" ? "ie Femmine" : "i Maschi"));
                    return;
                }

                if (anagParto.DataParto != null &&
                                      (dtpPartoIns.CustomFormat.Trim() != String.Empty &&
                                       (anagParto.DataParto.Value.ToString("d") != dtpPartoIns.Value.ToString("d"))))
                {
                    MessageBox.Show(
                    string.Format("Attenzione la data del parto salvata e la data presente nella casella Parto non corrispondono"));
                    return;
                }
                if (anagParto.DataParto == null || dtpPartoIns.CustomFormat.Trim() == String.Empty)
                {
                    MessageBox.Show(
                        string.Format("Attenzione Non risulta salvata la data del parto "));


                    return;
                }

                var anag = new Anagrafica();
                anag.idAnagrafica = 0;
                anag.MatricolaASL = txtMatriUslIns.Text.Trim();
                anag.MatricolaAzienda = txtMatriAziIns.Text.Trim() == string.Empty ? null : txtMatriAziIns.Text.Trim();
                anag.Sesso = sesso;
                anag.Madre = Convert.ToInt32(pctFotoDf.Tag); //id della madre 
                //id del toro dell'ultimo salto
                anag.Padre = cow.GetListSalti(idPartosalto, null).OrderByDescending(t => t.DataSalto).First().idToro;
                anag.idFiglio = idPartosalto;
                anag.DataNascita = dtpPartoIns.Value;
                int idAnag = cow.SaveAnagrafica(anag);

                //procedura di salvataggio della foto
                if (txtFotoInsFiglio.Text.Trim() != string.Empty)
                {

                    //copia della nuova foto nella cartella predefinita
                    if (txtFotoInsFiglio.Text.Contains(@"\"))
                    {
                        var percoFoto = Path.Combine(PercorsoFoto, idAnag.ToString(), Path.GetFileName(txtFotoInsFiglio.Text));
                        if (!Directory.Exists(Path.Combine(PercorsoFoto, idAnag.ToString())))
                            Directory.CreateDirectory(Path.Combine(PercorsoFoto, idAnag.ToString()));
                        File.Copy(txtFotoInsFiglio.Text, percoFoto, true);
                    }


                    var f = new Foto
                    {
                        Principale = true,
                        DataInserimento = DateTime.Now,
                        Nome =
                            txtFotoInsFiglio.Text.Contains(@"\")
                                ? Path.GetFileName(txtFotoInsFiglio.Text)
                                : txtFotoInsFiglio.Text
                    };


                    f.idAnagrafica = idAnag;

                    List<Foto> fotos = new List<Foto>();
                    fotos.Add(f);

                    cow.SaveFotoList(fotos);
                }

                CaricaFigli(idPartosalto);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNewFiglio_Click(object sender, EventArgs e)
        {

        }

        private void btnCaricaFotoFiglio_Click(object sender, EventArgs e)
        {
            try
            {
                if (ofdFoto.ShowDialog() == DialogResult.OK)
                {
                    txtFotoInsFiglio.Text = ofdFoto.FileName;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Inserire una immagine");
            }
        }

        private void btnOpenAnaFiglio_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtMatriUslIns.Tag) != 0)
            {
                CaricaDettaglio(Convert.ToInt32(txtMatriUslIns.Tag));
            }
            else
            {
                MessageBox.Show(string.Format("Figlio non registrato in anagrafica"));
            }
        }

        #endregion

        #region TextBox
        private void txtFvivi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtFvivi_Validated(object sender, EventArgs e)
        {
            TextBox tbBox = (TextBox)sender;
            if (tbBox.Text.Trim() == string.Empty)
                tbBox.Text = @"0";
        }


        private void txtRicerca_TextChanged(object sender, EventArgs e)
        {
            CaricaGrigliaAnagrafica();
        }
        #endregion


        private void pnlTori_VisibleChanged(object sender, EventArgs e)
        {
            if (pnlTori.Visible == false) return;
            //popolo la griglia dei tori

            var cow = new GestioneCowBoy(ConnectionString);
            var anag = cow.GetAnagrafica(null, "M", 1);
            var lstAnag = from c in anag
                          orderby c.idAnagrafica
                          select new
                          {
                              idana = c.idAnagrafica,
                              MatriAsl = c.MatricolaASL,
                              NomeToro = c.Nome,
                              DaMonta = c.ToroDaMonta == true ? "Si" : "No",
                              Artific = c.ToroArtificiale == true ? "Si" : "No"
                          };
            gridTori.DataSource = lstAnag.ToList();
            gridTori.Columns[0].Visible = false;
            gridTori.Columns[1].Width = 160; gridTori.Columns[1].SortMode = DataGridViewColumnSortMode.Automatic;
            gridTori.Columns[2].Width = 100; gridTori.Columns[2].SortMode = DataGridViewColumnSortMode.Automatic;
            gridTori.Columns[3].Width = 50; gridTori.Columns[3].SortMode = DataGridViewColumnSortMode.Automatic;
            gridTori.Columns[4].Width = 50; gridTori.Columns[4].SortMode = DataGridViewColumnSortMode.Automatic;

            gridTori.ClearSelection();
            gridTori.RowsDefaultCellStyle.BackColor = Color.Bisque;
            gridTori.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;

        }

        private void chbPresenti_CheckedChanged(object sender, EventArgs e)
        {
            CaricaGrigliaAnagrafica();
        }

       



















    }
}
