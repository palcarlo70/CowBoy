﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Codice generato da un modello.
//
//     Le modifiche manuali a questo file potrebbero causare un comportamento imprevisto dell'applicazione.
//     Se il codice viene rigenerato, le modifiche manuali al file verranno sovrascritte.
// </auto-generated>
//------------------------------------------------------------------------------
using System.Data.Common;

namespace CowBoy.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CowBoyEntities : DbContext
    {
        public CowBoyEntities()
            : base("name=CowBoyEntities")
        {
        }
        public CowBoyEntities(DbConnection cn, bool conBool = false)
            : base(cn, conBool)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Anagrafica> Anagrafica { get; set; }
        public virtual DbSet<Foto> Foto { get; set; }
        public virtual DbSet<PartiSalti> PartiSalti { get; set; }
        public virtual DbSet<Salti> Salti { get; set; }
        public virtual DbSet<Stati> Stati { get; set; }
        public virtual DbSet<PrimaNota> PrimaNota { get; set; }
        public virtual DbSet<VociPrimaNota> VociPrimaNota { get; set; }
    }
}