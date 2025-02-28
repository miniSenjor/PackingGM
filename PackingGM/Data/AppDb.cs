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
            //modelBuilder.Entity<User>().ToTable("Useres");
            // Указываем схему "public" по умолчанию для всех таблиц
            //modelBuilder.HasDefaultSchema("public");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        //public DbSet<Test> Test { get; set; }
    }
}
