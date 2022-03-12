using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowBoyEntityDto
{

    /*
     a.idAnagrafica,
        a.Nome,
        a.Madre,
		am.MatricolaASL MatricolaASLMadre,
        a.Padre,
		ap.MatricolaASL MatricolaASLPadre,
        a.DataNascita,
        a.DataFine,
        a.Note,
        a.ToroDaMonta,
        a.Sesso,
        a.MatricolaASL,
        a.MatricolaAzienda,
        a.idFiglio,
        a.ToroArtificiale ,
		f.idFoto,
        f.Nome AS NomeFoto,
        f.Principale     
     */

    public class BoviniDto
    {
        public int? Id { get; set; }
        public string MatricolaAsl { get; set; }
        public string MatricolaAz { get; set; }
        public string Nome { get; set; }
        public int? IdMadre { get; set; }
        public string MatricolaASLMadre { get; set; }
        public int? IdPadre { get; set; }
        public string MatricolaASLPadre { get; set; }
        public DateTime? DataNascita { get; set; }
        public string DataNascitaStringa { get; set; }
        public DateTime? DataFine { get; set; }
        public string DataFineStringa { get; set; }
        public string Note { get; set; }
        public int? IdParto { get; set; } //collegamento con il parto 
        public int? IdFoto { get; set; }
        public int? ToroArtificiale { get; set; }
        public int? ToroDaMonta { get; set; }
        public string Sesso { get; set; }
        public string NomeFoto { get; set; }
        public int? FotoPrincipale { get; set; }
        public DateTime? DataInAsciutta { get; set; }
        public string DataInAsciuttaStringa { get; set; }
        public DateTime? DataUltimoParto { get; set; }
        public string DataUltimoPartoStringa { get; set; }
        public int? MesiUltimoParto { get; set; }
        public DateTime? UltimoSalto { get; set; }
        public string UltimoSaltoStringa { get; set; }
        public int? GiorniUltimoSalto { get; set; }
       
    }
}
