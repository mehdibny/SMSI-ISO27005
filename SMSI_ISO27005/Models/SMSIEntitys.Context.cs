﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SMSI_ISO27005.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SMSIEntities1 : DbContext
    {
        public SMSIEntities1()
            : base("name=SMSIEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<actif> actif { get; set; }
        public virtual DbSet<activite> activite { get; set; }
        public virtual DbSet<CID_actif> CID_actif { get; set; }
        public virtual DbSet<collaborateur> collaborateur { get; set; }
        public virtual DbSet<confid> confid { get; set; }
        public virtual DbSet<disponibilte> disponibilte { get; set; }
        public virtual DbSet<impact> impact { get; set; }
        public virtual DbSet<integrite> integrite { get; set; }
        public virtual DbSet<menace> menace { get; set; }
        public virtual DbSet<prob_occurrence> prob_occurrence { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<user_table> user_table { get; set; }
        public virtual DbSet<vulnerabilte> vulnerabilte { get; set; }
        public virtual DbSet<action> action { get; set; }
        public virtual DbSet<gestion_risque> gestion_risque { get; set; }
    }
}