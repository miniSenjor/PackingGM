using PackingGM.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Windows;

namespace PackingGM.Data
{
    public class AppDb : DbContext
    {
        public AppDb() : base("name=DefaultConnection")
        {
            //Database.EnsureCreated();
            //Database.Migrate();
            //MessageBox.Show(Convert.ToString(Database.Exists));
            //Database.CreateIfNotExists();
            //Database.SetInitializer(new CreateDatabaseIfNotExists<AppDb>());
            //Database.Connection.Open();
            //var result = Database.SqlQuery<int>("SELECT 1").FirstOrDefault();
            //MessageBox.Show(result.ToString());
            //bool isDatabaseExists = Database.CompatibleWithModel(throwIfNoMetadata: false);
            //if (isDatabaseExists)
            //{
            //    MessageBox.Show("База данных доступна.");
            //}
            //else
            //{
            //    MessageBox.Show("База данных недоступна.");
            //}
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                        .Property(r => r.Version)
                        .IsConcurrencyToken();

            modelBuilder.Entity<OrderAggregate>().HasKey(oa => new { oa.OrderId, oa.AggregateId });
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderAggregates)
                .WithRequired(oa => oa.Order)
                .HasForeignKey(oa => oa.OrderId);
            modelBuilder.Entity<Aggregate>()
                .HasMany(a => a.OrderAggregates)
                .WithRequired(oa => oa.Aggregate)
                .HasForeignKey(oa => oa.AggregateId);

            modelBuilder.Entity<DrawingNameD3>().HasKey(dd => new { dd.DrawingNameVersionId, dd.D3Id });
            modelBuilder.Entity<DrawingNameVersion>()
                .HasMany(d => d.DrawingNameD3s)
                .WithRequired(dd => dd.DrawingNameVersion)
                .HasForeignKey(dd => dd.DrawingNameVersionId);
            modelBuilder.Entity<D3>()
                .HasMany(d => d.DrawingNameD3s)
                .WithRequired(dd => dd.D3)
                .HasForeignKey(dd => dd.D3Id);

            modelBuilder.Entity<ManufactoryGM>().HasKey(mg => new { mg.ManyfactoryId, mg.GMId });
            modelBuilder.Entity<Manufactory>()
                .HasMany(m => m.ManufactoryGMs)
                .WithRequired(mg => mg.Manufactory)
                .HasForeignKey(mg => mg.ManyfactoryId);
            modelBuilder.Entity<GM>()
                .HasMany(g => g.ManufactoryGMs)
                .WithRequired(mg => mg.GM)
                .HasForeignKey(mg => mg.GMId);
            //modelBuilder.Entity<Role>()
            //.Property(u => u.RowVersion)
            //.IsRowVersion();
            //modelBuilder.Entity<User>().ToTable("Useres");
            // Указываем схему "public" по умолчанию для всех таблиц
            //modelBuilder.HasDefaultSchema("public");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Contragent> Contragents { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderAggregate> OrderAggregates { get; set; }
        public DbSet<AggregateType> AggregateTypes { get; set; }
        public DbSet<DrawingName> DrawingNames { get; set; }
        public DbSet<Aggregate> Aggregates { get; set; }
        public DbSet<D3> D3s { get; set; }
        public DbSet<DrawingNameD3> DrawingNameD3s { get; set; }
        public DbSet<SPU> SPUs { get; set; }
        public DbSet<Manufactory> Manufactories { get; set; }
        public DbSet<GM> GMs { get; set; }
        public DbSet<ManufactoryGM> ManufactoryGMs { get; set; }
        public DbSet<Tare> Tares { get; set; }
        public DbSet<SPUTare> SPUTares { get; set; }
        public DbSet<GMNumber> GMNumbers { get; set; }
        public DbSet<DrawingNameVersion> DrawingNameVersions { get; set; }
        public DbSet<D3Version> D3Versions { get; set; }
        public DbSet<SPUVersion> SPUVersions { get; set; }
        //public DbSet<Test> Test { get; set; }
    }
}
