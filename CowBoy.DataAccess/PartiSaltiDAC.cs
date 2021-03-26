using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Migrations;
using System.Linq;
using System.ServiceModel;
using CowBoy.Entities;
using CowBoy.Library;


namespace CowBoy.DataAccess
{
    public class PartiSaltiDAC : BaseDAC<PartiSalti>
    {
        public PartiSaltiDAC(string conn)
            : base(conn)
        {
        }

        public List<PartiSalti> GetPartiSalti(int? idAnag, int? idPartoSalti)
        {
            List<PartiSalti> ret = new List<PartiSalti>();
            try
            {
                var n = new EntityConnection(connectionString);
                using (var ctx = new CowBoyEntities(n))
                {
                    ret = (from c in ctx.PartiSalti.Include("Salti.Anagrafica")
                           where (idAnag == null || c.idAnagrafica == idAnag)
                           && (idPartoSalti == null || c.idPartoSalto == idPartoSalti)
                           select c).ToList();
                    return ret;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void Create(PartiSalti entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(PartiSalti entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(PartiSalti entity)
        {
            try
            {
                var n = new EntityConnection(connectionString);
                using (var ctx = new CowBoyEntities(n))
                {
                    ctx.PartiSalti.Attach(entity);
                    ctx.PartiSalti.Remove(entity);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                var dex = new DataException(ex.Message);
                throw new FaultException<DataException>(dex, new FaultReason(ex.Message), new FaultCode("Sender"));
            }
        }

        public override void Save(PartiSalti entity)
        {
            throw new NotImplementedException();
        }
        public int SaveReturnId(PartiSalti entity)
        {
            //se sto inserendo un nuovo PartoSalto effettuo i seguenti controlli
            var lstPartiSalti = GetPartiSalti(entity.idAnagrafica, null);

            //verifico che i precedenti parti siano tutti chiusi
            if (lstPartiSalti.Count(c => c.DataParto == null) > 0 && entity.idPartoSalto == 0)
            {
                var mess =
                    string.Format(
                        "Attenzione prima di aggiungere nuovi Parti chiudere i precedenti che hanno lo stato aperto");

                var dex = new DataException(mess);
                throw new FaultException<DataException>(dex, new FaultReason(mess), new FaultCode("Sender"));
            }

            //verifico la data di parto che sia coungra con l'ultimo parto sempre che non sia un aborto
            if (entity.DataParto != null && entity.Abortito == false)
            {
                var ultimoPartoReg = lstPartiSalti.Where(c => c.idPartoSalto != entity.idPartoSalto).Select(d => d.DataParto);
                if (ultimoPartoReg.Where(dateTime => dateTime != null).Any(dateTime => (((Convert.ToDateTime(dateTime).Year - Convert.ToDateTime(entity.DataParto).Year)*
                                                                                         12) + Convert.ToDateTime(dateTime).Month -
                                                                                        Convert.ToDateTime(entity.DataParto).Month) > -7))
                {
                    var mess =
                        string.Format(
                            "Attenzione la data del parto risulta essere troppo vicina alla data del precedente parto/aborto. Verificare l'ultima registrazione effettuata");

                    var dex = new DataException(mess);
                    throw new FaultException<DataException>(dex, new FaultReason(mess), new FaultCode("Sender"));
                }
            }

            var n = new EntityConnection(connectionString);
            using (var ctx = new CowBoyEntities(n))
            {
                ctx.PartiSalti.AddOrUpdate(entity);
                ctx.SaveChanges();
                return entity.idPartoSalto;
            }
        }

        public override PartiSalti Load(int idEntity)
        {
            throw new NotImplementedException();
        }
    }
}
