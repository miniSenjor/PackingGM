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

            modelBuilder.Entity<DrawingNameAggregateD3>().HasKey(dd => new { dd.DrawingNameAggregateId, dd.D3Id });
            modelBuilder.Entity<DrawingNameAggregate>()
                .HasMany(d => d.DrawingNameAggregateD3s)
                .WithRequired(dd => dd.DrawingNameAggregate)
                .HasForeignKey(dd => dd.DrawingNameAggregateId);
            modelBuilder.Entity<D3>()
                .HasMany(d => d.DrawingNameAggregateD3s)
                .WithRequired(dd => dd.D3)
                .HasForeignKey(dd => dd.D3Id);

            modelBuilder.Entity<GM>().HasKey(ds => new { ds.D3Id, ds.SPUId });
            modelBuilder.Entity<D3>()
                .HasMany(d => d.GMs)
                .WithRequired(ds => ds.D3)
                .HasForeignKey(ds => ds.D3Id);
            modelBuilder.Entity<SPU>()
                .HasMany(s => s.GMs)
                .WithRequired(ds => ds.SPU)
                .HasForeignKey(ds => ds.SPUId);

            modelBuilder.Entity<SPUTare>().HasKey(st => new { st.SPUId, st.TareId });
            modelBuilder.Entity<SPU>()
                .HasMany(s => s.SPUTares)
                .WithRequired(st => st.SPU)
                .HasForeignKey(st => st.SPUId);
            modelBuilder.Entity<Tare>()
                .HasMany(t => t.SPUTares)
                .WithRequired(st => st.Tare)
                .HasForeignKey(st => st.TareId);

            modelBuilder.Entity<ManufactoryGM>().HasKey(mds => new { mds.ManyfactoryId, mds.D3Id, mds.SPUId });
            modelBuilder.Entity<Manufactory>()
                .HasMany(m => m.ManufactoryGMs)
                .WithRequired(mds => mds.Manufactory)
                .HasForeignKey(mds => mds.ManyfactoryId);
            modelBuilder.Entity<GM>()
                .HasMany(ds => ds.ManufactoryGMs)
                .WithRequired(mds => mds.GM)
                .HasForeignKey(mds => new { mds.D3Id, mds.SPUId });
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
        public DbSet<DrawingNameAggregate> DrawingNameAggregates { get; set; }
        public DbSet<Aggregate> Aggregates { get; set; }
        public DbSet<D3> D3s { get; set; }
        public DbSet<DrawingNameAggregateD3> DrawingNameAggregateD3s { get; set; }
        public DbSet<SPU> SPUs { get; set; }
        public DbSet<Manufactory> Manufactories { get; set; }
        public DbSet<GM> GMs { get; set; }
        public DbSet<ManufactoryGM> ManufactoryGMs { get; set; }
        public DbSet<Tare> Tares { get; set; }
        public DbSet<SPUTare> SPUTares { get; set; }
        //public DbSet<Test> Test { get; set; }
    }
}
