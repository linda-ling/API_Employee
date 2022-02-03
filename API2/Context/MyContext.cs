using API2.Models;
using Microsoft.EntityFrameworkCore;

namespace API2.Context
{
    public class MyContext : DbContext//MENWARISKAN SIFAT DBCONTEXT : JEMBATAN KE DB
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profiling> Profilings { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ONE TO ONE FK NIK
            modelBuilder.Entity<Employee>()
                        .HasOne(a => a.Account)
                        .WithOne(b => b.Employee)
                        .HasForeignKey<Account>(b => b.NIK);

            //ONE TO ONE FK NIK
            modelBuilder.Entity<Account>()
                        .HasOne(a => a.Profiling)
                        .WithOne(b => b.Account)
                        .HasForeignKey<Profiling>(b => b.NIK);

            //ONE TO MANY FK EDUCATION_ID
            modelBuilder.Entity<Education>()
                        .HasMany(a => a.Profiling)
                        .WithOne(b => b.Education);

            //ONE TO MANY FK UNIVERSITY_ID
            modelBuilder.Entity<University>()
                        .HasMany(a => a.Education)
                        .WithOne(b => b.University);

            //MANY TO MANY
            modelBuilder.Entity<AccountRole>()
                        .HasKey(a => new { a.NIK, a.Role_Id });

            modelBuilder.Entity<Account>()
                        .HasMany(a => a.AccountRole)
                        .WithOne(b => b.Account);

            modelBuilder.Entity<Role>()
                        .HasMany(a => a.AccountRole)
                        .WithOne(b => b.Role);

        }

        /*public static MyContext Create()
        {
            return new MyContext();
        }*/

    }
}
