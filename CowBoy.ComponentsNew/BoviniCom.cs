using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CowBoy.DataAccessNew;
using CowBoyEntityDto;

namespace CowBoy.ComponentsNew
{
    public class BoviniCom
    {
        public List<BoviniDto> GetBovini(int? idAnagrafica, string sesso, int? manze, int? inLattazione, int? inAsciutta, string ricercaLibera, string conCowBoy)
        {
            List<BoviniDto> anods;

            try
            {
                var inDb = new BoviniDac("System.Data.SqlClient", conCowBoy);

                DataSet ds = inDb.GetBovini(idAnagrafica,sesso,manze, inLattazione,inAsciutta,ricercaLibera);

                anods = (from DataRow dr in ds.Tables[0].Rows
                         select new BoviniDto()
                         {
                             Id = Convert.ToInt32(dr["Id"].ToString()),
                             Banca = dr["Banca"].ToString(),
                             CC = dr["CC"].ToString(),
                             DataMovimento = !dr.IsNull("DataMovimento") ? DateTime.Parse(dr["DataMovimento"].ToString()) : (DateTime?)null,
                             DataMovimentoStringa = !dr.IsNull("DataMovimento") ? DateTime.Parse(dr["DataMovimento"].ToString()).ToString("dd/MM/yy HH:mm") : string.Empty,
                             Saldo = !dr.IsNull("Saldo") ? Convert.ToDecimal(dr["Saldo"].ToString()) : 0,
                             SaldoStringa = !dr.IsNull("Saldo") ? Convert.ToDecimal(dr["Saldo"].ToString()).ToString("C") : "0 €",
                             Note = dr["Note"].ToString(),
                             IdMovimento = !dr.IsNull("IdSaldo") ? Convert.ToInt32(dr["IdSaldo"].ToString()) : (int?)null,
                             TotSaldoBanche = !dr.IsNull("TotSaldo") ? Convert.ToDecimal(dr["TotSaldo"].ToString()) : 0,
                             TotSaldoBancheString = !dr.IsNull("TotSaldo") ? Convert.ToDecimal(dr["TotSaldo"].ToString()).ToString("C") : "0 €"
                         }).ToList();

            }
            catch (Exception e)
            {
                return null;
            }

            return anods;
        }

        public string[] InsertUpdateBovino(BancaDto cf, int idUte, string conAVdb)
        {
            string[] lst = new string[2];
            try
            {
                //InsertUpdateSaldoBanca(int? Id, int IdBanca, decimal Saldo, string note, int idUte
                var inDb = new BancheDac("System.Data.SqlClient", conAVdb);
                lst = inDb.InsertUpdateSaldoBanca(cf.IdMovimento, cf.Id, cf.Saldo, cf.Note, idUte);
            }
            catch (Exception e)
            {
                lst[1] = e.Message;
            }

            return lst;

        }
    }
}
