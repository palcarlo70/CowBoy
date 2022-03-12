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
        public List<BoviniDto> GetBovini(int? idAnagrafica, string sesso, int? manze, int? inLattazione, int? inAsciutta, string ricercaLibera, int? inAzienda, string conCowBoy)
        {
            List<BoviniDto> nods;

            try
            {
                var inDb = new BoviniDac("System.Data.SqlClient", conCowBoy);

                DataSet ds = inDb.GetBovini(idAnagrafica,sesso,manze, inLattazione,inAsciutta,ricercaLibera, inAzienda);

                nods = (from DataRow dr in ds.Tables[0].Rows
                         select new BoviniDto()
                         {
                             Id = Convert.ToInt32(dr["idAnagrafica"].ToString()),
                             Nome = dr["Nome"].ToString(),
                             MatricolaAsl = dr["MatricolaASL"].ToString(),
                             MatricolaAz = dr["MatricolaAzienda"].ToString(),
                             IdMadre = !dr.IsNull("Madre") ? Convert.ToInt32(dr["Madre"].ToString()) : (int?)null,
                             MatricolaASLMadre = dr["MatricolaASLMadre"].ToString(),
                             IdPadre = !dr.IsNull("Padre") ? Convert.ToInt32(dr["Padre"].ToString()) : (int?)null,
                             MatricolaASLPadre = dr["MatricolaASLPadre"].ToString(),
                             DataNascita = !dr.IsNull("DataNascita") ? DateTime.Parse(dr["DataNascita"].ToString()) : (DateTime?)null,
                             DataNascitaStringa = !dr.IsNull("DataNascita") ? DateTime.Parse(dr["DataNascita"].ToString()).ToString("dd/MM/yy") : string.Empty,
                             DataFine = !dr.IsNull("DataFine") ? DateTime.Parse(dr["DataFine"].ToString()) : (DateTime?)null,
                             DataFineStringa = !dr.IsNull("DataFine") ? DateTime.Parse(dr["DataFine"].ToString()).ToString("dd/MM/yy") : string.Empty,
                             Note = dr["Nome"].ToString(),
                             IdParto = !dr.IsNull("idParto") ? Convert.ToInt32(dr["idParto"].ToString()) : (int?)null, // collegamento alla nascita in azienda per risalire ai dati sulla nascita
                             Sesso = dr["Sesso"].ToString(),
                             ToroDaMonta = !dr.IsNull("ToroDaMonta") ? Convert.ToInt32(dr["ToroDaMonta"].ToString()) : 0,
                             ToroArtificiale = !dr.IsNull("ToroArtificiale") ? Convert.ToInt32(dr["ToroArtificiale"].ToString()) : 0,
                             IdFoto = !dr.IsNull("idFoto") ? Convert.ToInt32(dr["idFoto"].ToString()) : (int?)null,
                             NomeFoto = dr["NomeFoto"].ToString(),
                             FotoPrincipale = 1,
                             DataInAsciutta = !dr.IsNull("DataInAsciutta") ? DateTime.Parse(dr["DataInAsciutta"].ToString()) : (DateTime?)null,
                             DataInAsciuttaStringa = !dr.IsNull("DataInAsciutta") ? DateTime.Parse(dr["DataInAsciutta"].ToString()).ToString("dd/MM/yy") : string.Empty,
                             DataUltimoParto = !dr.IsNull("DataUltimoParto") ? DateTime.Parse(dr["DataUltimoParto"].ToString()) : (DateTime?)null,
                             DataUltimoPartoStringa = !dr.IsNull("DataUltimoParto") ? DateTime.Parse(dr["DataUltimoParto"].ToString()).ToString("dd/MM/yy") : string.Empty,
                             MesiUltimoParto = !dr.IsNull("idFoto") ? Convert.ToInt32(dr["idFoto"].ToString()) : 0,
                             UltimoSalto = !dr.IsNull("UltimoSalto") ? DateTime.Parse(dr["UltimoSalto"].ToString()) : (DateTime?)null,
                             UltimoSaltoStringa = !dr.IsNull("UltimoSalto") ? DateTime.Parse(dr["UltimoSalto"].ToString()).ToString("dd/MM/yy") : string.Empty,
                             GiorniUltimoSalto = !dr.IsNull("idFoto") ? Convert.ToInt32(dr["idFoto"].ToString()) : 0

                         }).ToList();

            }
            catch (Exception e)
            {
                return null;
            }

            return nods;
        }

        //public string[] InsertUpdateBovino(BancaDto cf, int idUte, string conAVdb)
        //{
        //    string[] lst = new string[2];
        //    try
        //    {
        //        //InsertUpdateSaldoBanca(int? Id, int IdBanca, decimal Saldo, string note, int idUte
        //        var inDb = new BancheDac("System.Data.SqlClient", conAVdb);
        //        lst = inDb.InsertUpdateSaldoBanca(cf.IdMovimento, cf.Id, cf.Saldo, cf.Note, idUte);
        //    }
        //    catch (Exception e)
        //    {
        //        lst[1] = e.Message;
        //    }

        //    return lst;

        //}
    }
}
