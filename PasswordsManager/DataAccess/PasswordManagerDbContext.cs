using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace PasswordsManager.DataAccess
{
    public class PasswordManagerDbContext : DbContext
    {

        private static PasswordManagerDbContext _context = null;
        public static PasswordManagerDbContext Current
        {
            get
            {
                if (_context == null)
                    throw new Exception("Le contexte de base de données n'est pas initialisé !");
                return _context;
            }
        }
        public static async Task<PasswordManagerDbContext> Initialize()
        {
            if (_context == null)
            {
                _context = new PasswordManagerDbContext("PasswordManager.db");
               await _context.Database.MigrateAsync();
            }
            return _context;
        }


        internal PasswordManagerDbContext(DbContextOptions options) : base(options) { }
        private PasswordManagerDbContext(string databasePath) : base()
        {
            DatabasePath = databasePath;
        }

        public string DatabasePath { get; }


        public DbSet<Models.Password> Passwords { get; set; }
        public DbSet<Models.PasswordTag> PasswordTagLinks { get; set; }
        public DbSet<Models.Tag> Tags { get; set; }
        public DbSet<Models.User> User { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlite($"Filename={DatabasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Models.PasswordTag>()
                        .HasKey(pt => new { pt.PasswordId, pt.TagId });
        }

    }
}
