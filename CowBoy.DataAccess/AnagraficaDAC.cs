using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Migrations;
using System.Linq;
using System.ServiceModel;
using CowBoy.Entities;
using CowBoy.Library;

namespace CowBoy.DataAccess
{
    public class AnagraficaDAC : BaseDAC<Anagrafica>
    {

        #region Metodi pubblici

        public AnagraficaDAC(string conn)
            : base(conn)
        {
        }

        public List<Anagrafica> GetAnagrafica(int? idAnagrafica, string sesso, int? attivo)
        {
            //il paramentro attivo se è null ritorna la lista di tutti i bovini
            //se è 1 ritorna tutti i bovini ancora presenti in azienda
            //se è 2 ritorna tutti i bovini venduti

            string presenti = null;
            string venduti = null;

            if (attivo == 1)
                presenti = "1";
            else if (attivo == 2)
                venduti = "2";

            List<Anagrafica> ret = new List<Anagrafica>();
            try
            {
                var n = new EntityConnection(connectionString);
                using (var ctx = new CowBoyEntities(n))
                {
                    ret = (from c in ctx.Anagrafica.Include("PartiSalti.Salti").Include("Foto")
                           where (idAnagrafica == null || c.idAnagrafica == idAnagrafica)
                           && (sesso == null || c.Sesso == sesso)
                           && (presenti == null || c.DataFine == null)
                           && (venduti == null || c.DataFine != null)
                           orderby c.idAnagrafica
                           select c).Include(m => m.Anagrafica2).Include(m => m.Anagrafica3).ToList();
                }

            }
            catch (Exception ex)
            {
                var dex = new DataException(ex.Message);
                throw new FaultException<DataException>(dex, new FaultReason(ex.Message), new FaultCode("Sender"));
            }
            return ret;
        }


        public List<Anagrafica> GetAnagrafica(string sesso, int? attivo, bool? manze, bool? lattazione,
            bool? asciutta, string riCerca)
        {
            //il paramentro attivo se è null ritorna la lista di tutti i bovini
            //se è 1 ritorna tutti i bovini ancora presenti in azienda
            //se è 2 ritorna tutti i bovini venduti

            string presenti = null;
            string venduti = null;

            switch (attivo)
            {
                case 1:
                    presenti = "1";
                    break;
                case 2:
                    venduti = "2";
                    break;
            }

            List<Anagrafica> ret = new List<Anagrafica>();
            try
            {
                var n = new EntityConnection(connectionString);
                using (var ctx = new CowBoyEntities(n))
                {

                    //&& (c.PartiSalti.Any(d => (d.DataMessaAsciutta != null && d.DataParto != null) && !(d.DataMessaAsciutta != null && d.DataParto == null)) || (c.PartiSalti.Any(d => d.DataMessaAsciutta != null && d.DataParto == null)))
                    if (lattazione != null && asciutta != null)
                    {
                        ret = (from c in ctx.Anagrafica.Include("PartiSalti.Salti").Include("Foto")
                               where (manze == null || manze == true || c.PartiSalti.Count == 0)
                               && (manze == false || c.PartiSalti.Count > 0)
                               && (c.PartiSalti.Count > 0 &&
                                  (c.PartiSalti.Any(d => (d.DataMessaAsciutta == null && d.DataParto == null))
                                    ||
                                   c.PartiSalti.Any(d => d.DataMessaAsciutta != null && d.DataParto == null)))
                               && (sesso == null || c.Sesso == sesso)
                               && (presenti == null || c.DataFine == null)
                               && (venduti == null || c.DataFine != null)
                               && (riCerca == null || c.MatricolaASL.Contains(riCerca) || c.MatricolaAzienda.Contains(riCerca) || c.Nome.Contains(riCerca))
                               orderby c.idAnagrafica
                               select c).Include(m => m.Anagrafica2).Include(m => m.Anagrafica3).ToList();
                    }
                    else
                    {
                        /*
                        ret = (from c in ctx.Anagrafica.Include("PartiSalti.Salti").Include("Foto")
                               //where (manze == null || c.PartiSalti.Count == 0)
                               where (manze == null || manze == false || c.PartiSalti.Count == 0)
                               && (manze == true || c.PartiSalti.Count > 0)
                               && (lattazione == null || c.PartiSalti.Any(d => (d.DataMessaAsciutta != null && d.DataParto != null) && !(d.DataMessaAsciutta != null && d.DataParto == null)))
                               && (asciutta == null || c.PartiSalti.Any(d => d.DataMessaAsciutta != null && d.DataParto == null))
                               && (sesso == null || c.Sesso == sesso)
                               && (presenti == null || c.DataFine == null)
                               && (venduti == null || c.DataFine != null)
                               && (riCerca == null || c.MatricolaASL.Contains(riCerca) || c.MatricolaAzienda.Contains(riCerca) || c.Nome.Contains(riCerca))
                               orderby c.idAnagrafica
                               select c).Include(m => m.Anagrafica2).Include(m => m.Anagrafica3).ToList();
                        */
                        ret = (from c in ctx.Anagrafica
                               where (sesso == null || c.Sesso == sesso)
                                     && (manze == null || c.PartiSalti.Count == 0)
                                     && (lattazione == null || c.PartiSalti.Any(d => (d.DataMessaAsciutta != null && d.DataParto != null) && !(d.DataMessaAsciutta != null && d.DataParto == null)))
                                     && (asciutta == null || c.PartiSalti.Any(d => d.DataMessaAsciutta != null && d.DataParto == null))
                                     && (presenti == null || c.DataFine == null)
                                     && (venduti == null || c.DataFine != null)
                                     && (riCerca == null || c.MatricolaASL.Contains(riCerca) || c.MatricolaAzienda.Contains(riCerca) || c.Nome.Contains(riCerca))
                               orderby c.idAnagrafica
                               select c).Include(m => m.Anagrafica2).Include(m => m.Anagrafica3).ToList();

                    }


                }

            }
            catch (Exception ex)
            {
                var dex = new DataException(ex.Message);
                throw new FaultException<DataException>(dex, new FaultReason(ex.Message), new FaultCode("Sender"));
            }
            return ret;
        }




        public List<Anagrafica> GetAnagraficaCerca(string sesso, string riCerca)
        {

            List<Anagrafica> ret;
            try
            {
                var n = new EntityConnection(connectionString);
                using (var ctx = new CowBoyEntities(n))
                {
                    if (sesso != null && sesso == "F")
                    {
                        ret = (from c in ctx.Anagrafica
                               where c.Sesso == sesso
                               && (c.PartiSalti.Count > 0)
                               && (riCerca == null || c.MatricolaASL.Contains(riCerca) || c.MatricolaAzienda.Contains(riCerca) || c.Nome.Contains(riCerca))
                               orderby c.idAnagrafica
                               select c).ToList();
                    }
                    else if (sesso != null && sesso == "M")
                    {
                        ret = (from c in ctx.Anagrafica
                               where (sesso == null || c.Sesso == sesso)
                               && (riCerca == null || c.MatricolaASL.Contains(riCerca) || c.MatricolaAzienda.Contains(riCerca) || c.Nome.Contains(riCerca))
                               orderby c.idAnagrafica
                               select c).ToList();
                    }
                    else //verifico che la matricola è univoca
                    {
                        ret = (from c in ctx.Anagrafica
                               where (c.MatricolaASL == riCerca)
                               orderby c.idAnagrafica
                               select c).ToList();
                    }
                }

            }
            catch (Exception ex)
            {
                var dex = new DataException(ex.Message);
                throw new FaultException<DataException>(dex, new FaultReason(ex.Message), new FaultCode("Sender"));
            }
            return ret;
        }


        public List<Anagrafica> GetAnagrafica(int idPartoSalto)
        {
            //ritorna la lista dei figli associati e registrati con quel parto
            List<Anagrafica> ret;
            try
            {
                var n = new EntityConnection(connectionString);
                using (var ctx = new CowBoyEntities(n))
                {
                    ret = (from c in ctx.Anagrafica.Include("Foto")
                           where (c.idFiglio == idPartoSalto)
                           orderby c.idAnagrafica
                           select c).ToList();
                }
            }
            catch (Exception ex)
            {
                var dex = new DataException(ex.Message);
                throw new FaultException<DataException>(dex, new FaultReason(ex.Message), new FaultCode("Sender"));
            }
            return ret;
        }



        #endregion

        public override void Create(Anagrafica entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(Anagrafica entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Anagrafica entity)
        {
            try
            {
                var n = new EntityConnection(connectionString);
                using (var ctx = new CowBoyEntities(n))
                {
                    ctx.Anagrafica.Attach(entity);
                    ctx.Anagrafica.Remove(entity);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void Save(Anagrafica entity)
        {

        }

        public int SaveReturnId(Anagrafica entity)
        {
            var n = new EntityConnection(connectionString);
            using (CowBoyEntities ctx = new CowBoyEntities(n))
            {
                ctx.Anagrafica.AddOrUpdate(entity);
                _ = ctx.SaveChanges();
                return entity.idAnagrafica;
            }
        }

        public override Anagrafica Load(int idEntity)
        {
            throw new NotImplementedException();
        }
    }
}
