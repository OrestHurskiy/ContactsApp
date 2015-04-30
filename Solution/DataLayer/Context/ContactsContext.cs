using System.Data.Entity;
using Models.Entities;

namespace DataLayer.Context
{
    public class ContactsContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Roles> Roles { get; set; }

        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Roles>().
                HasKey(r => new {r.PersonId, r.ProjectId});

            modelBuilder.Entity<Person>().HasMany(p => p.Roles).
                WithRequired().HasForeignKey(r => r.PersonId);

            modelBuilder.Entity<Project>().HasMany(pr => pr.Roles).
                WithRequired().HasForeignKey(r => r.ProjectId);

        }
    }
    
}
