﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AceTC.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AceDBEntities : DbContext
    {
        public AceDBEntities()
            : base("name=AceDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Parent> Parents { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Outstanding> Outstandings { get; set; }
    }
}
