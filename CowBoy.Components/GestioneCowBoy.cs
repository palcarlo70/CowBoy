using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Web.UI;
using CowBoy.Entities;
using CowBoy.DataAccess;
using CowBoy.Library;
using System.Transactions;

namespace CowBoy.Components
{
    public class GestioneCowBoy
    {
        private string ConnectionString { get; set; }

        public GestioneCowBoy(string nomeDB)
        {
            if (!String.IsNullOrWhiteSpace(nomeDB))
                ConnectionString = ConfigurationManager.ConnectionStrings[nomeDB].ConnectionString;
        }

        #region Anagrafica

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idAnagrafica"></param>
        /// <param name="sesso"></param>
        /// <param name="attivo">Può assumere i seguenti valori: = null (ritorna tutti i bovini presenti e non) = 1 (ritorna i bovini presenti) = 2 (ritorna i bovini venduti)</param>
        /// <returns></returns>
        public List<Anagrafica> GetAnagrafica(int? idAnagrafica, string sesso, int? attivo)
        {
            return new AnagraficaDAC(ConnectionString).GetAnagrafica(idAnagrafica, sesso, attivo);
        }

        /// <summary>
        /// Ritorna la lista dei vitelli registrati da questo parto
        /// </summary>
        /// <param name="idPartoSalto"></param>
        /// <returns></returns>
        public List<Anagrafica> GetAnagrafica(int idPartoSalto)
        {
            return new AnagraficaDAC(ConnectionString).GetAnagrafica(idPartoSalto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sesso">Filtro per il sesso in formato stringa M o F</param>
        /// <param name="attivo">Può assumere i seguenti valori: = null (ritorna tutti i bovini presenti e non) = 1 (ritorna i bovini presenti) = 2 (ritorna i bovini venduti)</param>
        /// <param name="manze">Se valorizato ritorna i soli bovini che non hanno artorito</param>
        /// <param name="lattazione">Bovini in lattazione</param>
        /// <param name="asciutta">Bovini in asciutta</param>
        /// <param name="riCerca">Filtrodi ricerca di tipo Like sui campi MatricolaAsl, MatricolaAziendale e nome</param>
        /// <returns></returns>
        public List<Anagrafica> GetAnagrafica(string sesso, int? attivo, bool? manze, bool? lattazione,
            bool? asciutta, string riCerca)
        {
            return new AnagraficaDAC(ConnectionString).GetAnagrafica(sesso, attivo, manze, lattazione, asciutta, riCerca);
        }

        public List<Anagrafica> GetAnagraficaCerca(string sesso, string riCerca)
        {
            return new AnagraficaDAC(ConnectionString).GetAnagraficaCerca(sesso, riCerca);
        }

        public int SaveAnagrafica(Anagrafica ana)
        {
            return new AnagraficaDAC(ConnectionString).SaveReturnId(ana);
        }


        public void DeleteAnagrafica(Anagrafica ana)
        {
            try
            {
                new AnagraficaDAC(ConnectionString).Delete(ana);
            }
            catch (Exception ex)
            {
                var dex = new DataException(ex.Message);
                throw new FaultException<DataException>(dex, new FaultReason(ex.Message), new FaultCode("Sender"));
            }
        }

        /// <summary>
        /// Rutin di salvataggio dei figli
        /// </summary>
        /// <param name="figlio">tipo Anagrafica con i dati del figlio</param>
        /// <param name="fotoFiglio">tipo Foto foto allegata di default</param>
        /// <param name="percorsoFileFoto">cartella di default dove vengono salvate le foto</param>
        /// <param name="bufferFoto">array di byte contenente la foto</param>
        public void SalvaFiglio(Anagrafica figlio, Foto fotoFiglio, string percorsoFileFoto, byte[] bufferFoto)
        {
            try
            {
                int idFiglio = new AnagraficaDAC(ConnectionString).SaveReturnId(figlio);
                var percorso = Path.Combine(percorsoFileFoto, idFiglio.ToString());
                if (fotoFiglio != null && fotoFiglio.Nome.Trim() != string.Empty)
                {
                    fotoFiglio.idAnagrafica = idFiglio;
                    var lstFoto = new List<Foto>();
                    lstFoto.Add(fotoFiglio);
                    SaveFotoList(lstFoto);
                    WriteToFile(percorso, fotoFiglio.Nome, bufferFoto);
                }
            }
            catch (Exception ex)
            {
                var dex = new DataException(ex.Message);
                throw new FaultException<DataException>(dex, new FaultReason(ex.Message), new FaultCode("Sender"));
            }
        }



        #endregion

        #region Foto

        public List<Foto> GetFoto(int? inAnagra)
        {
            return new FotoDAC(ConnectionString).GetFoto(inAnagra);
        }

        public Foto GetFotoByIdFoto(int idFoto)
        {
            return new FotoDAC(ConnectionString).Load(idFoto);
        }

        public void SaveFotoList(List<Foto> fotos)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                try
                {
                    foreach (var foto in fotos)
                    {
                        //se la foto è la principale devo settare le altre a false
                        if (foto.Principale == true)
                        {
                            var lstFoto = new FotoDAC(ConnectionString).GetFoto(foto.idAnagrafica);
                            foreach (var ft in lstFoto)
                            {
                                if (ft.Principale == true)
                                {
                                    ft.Principale = false;
                                    new FotoDAC(ConnectionString).Save(ft);
                                }

                            }
                        }
                        new FotoDAC(ConnectionString).Save(foto);
                    }
                    transaction.Complete();
                }
                catch (Exception ex)
                {
                    var dex = new DataException(ex.Message);
                    throw new FaultException<DataException>(dex, new FaultReason(ex.Message), new FaultCode("Sender"));
                }
            }

        }

        public void SaveFoto(Foto fotos, byte[] myBytes, string percorsoFileFoto)
        {
            try
            {
                //se la foto è la principale devo settare le altre a false
                if (fotos.Principale == true)
                {
                    var lstFoto = new FotoDAC(ConnectionString).GetFoto(fotos.idAnagrafica);
                    foreach (var ft in lstFoto)
                    {
                        if (ft.Principale == true)
                        {
                            ft.Principale = false;
                            new FotoDAC(ConnectionString).Save(ft);
                        }

                    }
                }
                new FotoDAC(ConnectionString).Save(fotos);

                var percorso = Path.Combine(percorsoFileFoto, fotos.idAnagrafica.ToString());
                WriteToFile(percorso, fotos.Nome, myBytes);
            }
            catch (Exception ex)
            {
                var dex = new DataException(ex.Message);
                throw new FaultException<DataException>(dex, new FaultReason(ex.Message), new FaultCode("Sender"));
            }
        }

        public void SalvaModificheFoto(int idFoto, bool elimina)
        {
            Foto foto = new FotoDAC(ConnectionString).Load(idFoto);
            if (elimina == false) //imposto la foto come di default
            {
                foto.Principale = true;
                var lstFoto = new List<Foto>();
                lstFoto.Add(foto);
                SaveFotoList(lstFoto);
            }
            else
            {
                //elimino il file dal percorso
                try
                {
                    string percorsoFileFoto = ConfigurationManager.AppSettings["PercorsoSalvataggioFoto"];
                    var percorso = Path.Combine(percorsoFileFoto, foto.idAnagrafica.ToString());
                    DeleteToFile(percorso, foto.Nome);
                }
                catch (Exception) //in caso di errore non effettuo nessun controllo o intervento
                { }
                try
                {
                    new FotoDAC(ConnectionString).Delete(foto);
                }
                catch (Exception ex)
                {
                    var dex = new DataException(ex.Message);
                    throw new FaultException<DataException>(dex, new FaultReason(ex.Message), new FaultCode("Sender"));
                }
            }
        }

        #endregion

        #region Partisalti

        public List<PartiSalti> GetListPartiSalti(int? idAnag, int? idPartSalti)
        {
            return new PartiSaltiDAC(ConnectionString).GetPartiSalti(idAnag, idPartSalti);
        }

        public int SavePartoSalto(PartiSalti entity)
        {
            return new PartiSaltiDAC(ConnectionString).SaveReturnId(entity);
        }


        public void DeletePartoSalto(PartiSalti entity)
        {
            new PartiSaltiDAC(ConnectionString).Delete(entity);
        }


        #endregion

        #region Salti

        public List<Salti> GetListSalti(int? idPartiSalto, int? idSalto)
        {
            return new SaltiDAC(ConnectionString).GetListSalti(idPartiSalto, idSalto);
        }

        public void SaveSalto(Salti salti)
        {

            if (salti.idPartoSalto == 0)
            {
                int idParto = 0;
                idParto = SavePartoSalto(new PartiSalti
                 {
                     idAnagrafica = salti.Anagrafica.idAnagrafica
                 });
                salti.idPartoSalto = idParto;
                salti.Anagrafica = null;
            }


            new SaltiDAC(ConnectionString).Save(salti);
        }

        public void DeleteSalto(Salti salti)
        {
            new SaltiDAC(ConnectionString).Delete(salti);
        }

        #endregion

        #region Utility

        private void WriteToFile(string strPath, string fileName, byte[] buffer)
        {
            try
            {
                CreaPercorsoCartella(strPath);
                
                var newFile = new FileStream(Path.Combine(strPath, fileName), FileMode.Create);
                newFile.Write(buffer, 0, buffer.Length);
                newFile.Close();
            }
            catch (Exception ex)
            {
                var dex = new DataException(ex.Message);
                throw new FaultException<DataException>(dex, new FaultReason(ex.Message), new FaultCode("Sender"));
            }
        }

        private void DeleteToFile(string strPath, string fileName)
        {
            try
            {
                if (File.Exists(Path.Combine(strPath, fileName)))
                    File.Delete(Path.Combine(strPath, fileName));
            }
            catch (Exception){}
        }

        /// <summary>
        /// Verifical'esistenza della cartella e se non esiste la crea
        /// </summary>
        /// <param name="percorso">percorso da verificare (solo directory)</param>
        public void CreaPercorsoCartella(string percorso)
        {
            try
            {
                if (!Directory.Exists(percorso))
                    Directory.CreateDirectory(percorso);
            }
            catch (Exception ex)
            {
                var dex = new DataException(ex.Message);
                throw new FaultException<DataException>(dex, new FaultReason(ex.Message), new FaultCode("Sender"));
            }
        }


        #endregion

    }
}
