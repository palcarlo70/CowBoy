//------------------------------------------------------------------------------
// <auto-generated>
//     Codice generato da un modello.
//
//     Le modifiche manuali a questo file potrebbero causare un comportamento imprevisto dell'applicazione.
//     Se il codice viene rigenerato, le modifiche manuali al file verranno sovrascritte.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CowBoy.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Foto
    {
        public int idFoto { get; set; }
        public int idAnagrafica { get; set; }
        public string Nome { get; set; }
        public Nullable<bool> Principale { get; set; }
        public Nullable<System.DateTime> DataInserimento { get; set; }
    
        public virtual Anagrafica Anagrafica { get; set; }
    }
}