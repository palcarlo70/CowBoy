using System;       
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBAccess;

namespace CowBoy.DataAccessNew
{
    public class BoviniDac : DBWork
    {
        public BoviniDac(string provider, string connectionString) : base(provider, connectionString)
        {
        }

        public DataSet GetBovini(int? idAnagrafica, string sesso, int? manze, int? inLattazione, int? inAsciutta, string ricercaLibera, int? inAzienda)
        {
            DbCommand cmd = CreateCommand("PR_GetAnagrafica", true);
            
            base.SetParameter(cmd, "IdAnagrafica", DbType.Int32, ParameterDirection.Input, (object)idAnagrafica ?? DBNull.Value);
            base.SetParameter(cmd, "Sesso", DbType.String, ParameterDirection.Input, (object)sesso ?? DBNull.Value);
            base.SetParameter(cmd, "Manze", DbType.Int32, ParameterDirection.Input, (object)manze ?? DBNull.Value);
            base.SetParameter(cmd, "Lattazione", DbType.Int32, ParameterDirection.Input, (object)inLattazione ?? DBNull.Value);
            base.SetParameter(cmd, "Asciutta", DbType.Int32, ParameterDirection.Input, (object)inAsciutta ?? DBNull.Value);
            base.SetParameter(cmd, "InAzienda", DbType.Int32, ParameterDirection.Input, (object)inAzienda ?? DBNull.Value);
            base.SetParameter(cmd, "RicercaLibera", DbType.String, ParameterDirection.Input, (object)ricercaLibera ?? DBNull.Value);

            cmd.CommandType = CommandType.StoredProcedure;
            var lst = base.GetDataSet(cmd);

            return lst;
        }
        public DataSet GetProssimiSaltiAsciutteParti() //ritorna 4 tabelle
        {
            DbCommand cmd = CreateCommand("PR_GetAsciuttePartiSalti", true);

            cmd.CommandType = CommandType.StoredProcedure;
            var lst = base.GetDataSet(cmd);

            return lst;
        }
    }
}
