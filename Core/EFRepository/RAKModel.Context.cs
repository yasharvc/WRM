﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFRepository
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RAKEntities : DbContext
    {
        public RAKEntities()
            : base("name=RAKEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DeliveryRegistration> DeliveryRegistrations { get; set; }
        public virtual DbSet<DeliveryRegistrationDetail> DeliveryRegistrationDetails { get; set; }
        public virtual DbSet<DetailModification> DetailModifications { get; set; }
        public virtual DbSet<PremiumBanking> PremiumBankings { get; set; }
        public virtual DbSet<PremiumModification> PremiumModifications { get; set; }
        public virtual DbSet<RegistrationModification> RegistrationModifications { get; set; }
        public virtual DbSet<ProcessedFile> ProcessedFiles { get; set; }
        public virtual DbSet<DeliveryRegistrationHistory> DeliveryRegistrationHistories { get; set; }
        public virtual DbSet<Path> Paths { get; set; }
        public virtual DbSet<GoupMenu> GoupMenus { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
    }
}
