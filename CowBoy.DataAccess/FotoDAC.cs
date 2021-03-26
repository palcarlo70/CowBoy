using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Migrations;
using System.Linq;
using CowBoy.Entities;
//using System.Data.EntityClient;
//using System.Data.Objects;
//using System.Data;
//using System.Data.SqlClient;

namespace CowBoy.DataAccess
{
    public class FotoDAC:BaseDAC<Foto>
    {

        #region Metodi pubblici


        public FotoDAC(string conn) : base(conn)
        {
        }

        public List<Foto> GetFoto(int? idAnagrafica)
        {
            var ret = new List<Foto>();
            var n = new EntityConnection(connectionString);
            using (CowBoyEntities ctx = new CowBoyEntities(n))
            {
                
                //ret = SGetFoto(ctx, idAnagrafica).ToList();
                ret = (from c in ctx.Foto
                    where (idAnagrafica == null || c.idAnagrafica == idAnagrafica)
                    orderby c.idFoto
                    select c).ToList();
                return ret;
            }
        }

        #endregion


        public override void Create(Foto entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(Foto entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Foto entity)
        {
            try
            {
                var n = new EntityConnection(connectionString);
                using (var ctx = new CowBoyEntities(n))
                {
                    ctx.Foto.Attach(entity);
                    ctx.Foto.Remove(entity);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void Save(Foto entity)
        {
            var n = new EntityConnection(connectionString);
            using (var ctx = new CowBoyEntities(n))
            {
                ctx.Foto.AddOrUpdate(entity);
                ctx.SaveChanges();
            }
        }

        public override Foto Load(int idEntity)
        {
            var ret = new Foto();
            var n = new EntityConnection(connectionString);
            using (var ctx = new CowBoyEntities(n))
            {
                ret = (from c in ctx.Foto
                    where (c.idFoto == idEntity)
                    select c).FirstOrDefault();
                return ret;
            }
        }
    }
}

