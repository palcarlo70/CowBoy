using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Migrations;
using System.Linq;
using System.ServiceModel;
using CowBoy.Entities;
using CowBoy.Library;

namespace CowBoy.DataAccess
{
   public class SaltiDAC:BaseDAC<Salti>
   {
       public SaltiDAC(string conn) : base(conn)
       {
       }

       public List<Salti> GetListSalti(int? idPartoSalto, int? idSalto)
       {
           try
           {
               var n = new EntityConnection(connectionString);
               using (var ctx = new CowBoyEntities(n))
               {
                   List<Salti> ret = (from c in ctx.Salti.Include("Anagrafica")
                       where (idPartoSalto == null || c.idPartoSalto == idPartoSalto) &&
                             (idSalto == null || c.idSalto == idSalto)
                       select c).ToList();
                   return ret;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public override void Create(Salti entity)
       {
           throw new NotImplementedException();
       }

       public override void Update(Salti entity)
       {
           throw new NotImplementedException();
       }

       public override void Delete(Salti entity)
       {
           try
           {
               var n = new EntityConnection(connectionString);
               using (var ctx = new CowBoyEntities(n))
               {
                   ctx.Salti.Attach(entity);
                   ctx.Salti.Remove(entity);
                   ctx.SaveChanges();
               }
           }
           catch (Exception ex)
           {
               var dex = new DataException(ex.Message);
               throw new FaultException<DataException>(dex, new FaultReason(ex.Message), new FaultCode("Sender"));
           }
       }

       public override void Save(Salti entity)
       {
           try
           {
               var n = new EntityConnection(connectionString);
               using (var ctx = new CowBoyEntities(n))
               {
                   ctx.Salti.AddOrUpdate(entity);
                   ctx.SaveChanges();
               }
           }
           catch (Exception ex)
           {
               var dex = new DataException(ex.Message);
               throw new FaultException<DataException>(dex, new FaultReason(ex.Message), new FaultCode("Sender"));
           }
       }

       public override Salti Load(int idEntity)
       {
           throw new NotImplementedException();
       }
   }
}
