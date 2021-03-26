//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClassLibrary1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Anagrafica
    {
        public Anagrafica()
        {
            this.Anagrafica1 = new HashSet<Anagrafica>();
            this.Anagrafica11 = new HashSet<Anagrafica>();
            this.Foto = new HashSet<Foto>();
            this.PartiSalti = new HashSet<PartiSalti>();
            this.Salti = new HashSet<Salti>();
        }
    
        public int idAnagrafica { get; set; }
        public string Nome { get; set; }
        public Nullable<int> Madre { get; set; }
        public Nullable<int> Padre { get; set; }
        public Nullable<System.DateTime> DataNascita { get; set; }
        public Nullable<System.DateTime> DataFine { get; set; }
        public string Note { get; set; }
        public Nullable<bool> ToroDaMonta { get; set; }
        public Nullable<bool> ToroArtificiale { get; set; }
        public string Sesso { get; set; }
        public string MatricolaASL { get; set; }
        public string MatricolaAzienda { get; set; }
        public Nullable<int> idFiglio { get; set; }
    
        public virtual ICollection<Anagrafica> Anagrafica1 { get; set; }
        public virtual Anagrafica Anagrafica2 { get; set; }
        public virtual ICollection<Anagrafica> Anagrafica11 { get; set; }
        public virtual Anagrafica Anagrafica3 { get; set; }
        public virtual ICollection<Foto> Foto { get; set; }
        public virtual ICollection<PartiSalti> PartiSalti { get; set; }
        public virtual ICollection<Salti> Salti { get; set; }
    }
}
