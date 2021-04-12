using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBAccess;

namespace CowBoyDataAccess
{
    public class AnagraficaDac : DBWork
    {
        public AnagraficaDac(string provider, string connectionString) : base(provider, connectionString)
        {
        }

        public DataSet GetAnagrafica(int? IdDoc, int? IdTipoDoc, string IdStato)
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
            base.SetParameter(cmd, "IdDoc", DbType.Int32, ParameterDirection.Input, (object)IdDoc ?? DBNull.Value);
            base.SetParameter(cmd, "IdTipoDoc", DbType.Int32, ParameterDirection.Input, (object)IdTipoDoc ?? DBNull.Value);
            base.SetParameter(cmd, "IdStato", DbType.String, ParameterDirection.Input, (object)IdStato ?? DBNull.Value);

            cmd.CommandType = CommandType.StoredProcedure;
            var lst = base.GetDataSet(cmd);

            return lst;
        }
    }
}
