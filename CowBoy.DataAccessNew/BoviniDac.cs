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

        public DataSet GetBovini(int? idAnagrafica, string sesso, int? manze, int? inLattazione, int? inAsciutta, string ricercaLibera)
        {
            DbCommand cmd = CreateCommand("PR_GetAnagrafica", true);
            /*
             @IdAnagrafica Int = NULL,
	@Sesso CHAR(1) = NULL,
	@Manze INT = NULL,
	@Lattazione INT = NULL,
    @Asciutta INT = NULL,
	@RicercaLibera NVARCHAR(50) = NULL
             */
            base.SetParameter(cmd, "IdAnagrafica", DbType.Int32, ParameterDirection.Input, (object)idAnagrafica ?? DBNull.Value);
            base.SetParameter(cmd, "Sesso", DbType.String, ParameterDirection.Input, (object)sesso ?? DBNull.Value);
            base.SetParameter(cmd, "Manze", DbType.Int32, ParameterDirection.Input, (object)manze ?? DBNull.Value);
            base.SetParameter(cmd, "Lattazione", DbType.Int32, ParameterDirection.Input, (object)inLattazione ?? DBNull.Value);
            base.SetParameter(cmd, "Asciutta", DbType.Int32, ParameterDirection.Input, (object)inAsciutta ?? DBNull.Value);
            base.SetParameter(cmd, "RicercaLibera", DbType.String, ParameterDirection.Input, (object)ricercaLibera ?? DBNull.Value);

            cmd.CommandType = CommandType.StoredProcedure;
            var lst = base.GetDataSet(cmd);

            return lst;
        }
    }
}
