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
    
    public partial class Salti
    {
        public int idSalto { get; set; }
        public int idPartoSalto { get; set; }
        public Nullable<System.DateTime> DataSalto { get; set; }
        public Nullable<int> idToro { get; set; }
        public Nullable<bool> SaltoArtificiale { get; set; }
        public string MatrToroArt { get; set; }
        public string Note { get; set; }
    
        public virtual Anagrafica Anagrafica { get; set; }
        public virtual PartiSalti PartiSalti { get; set; }
    }
}
