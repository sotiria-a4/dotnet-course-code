using HelloWorld.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HelloWorld.Data
{
    public class DataContextDapperEF : DbContext //ο DbContext είναι η βασική κλάση του Entity Framework Core που χρησιμοποιούμε για να επικοινωνούμε με τη βάση δεδομένων.
    {
        private IConfiguration _config;
        public DataContextDapperEF(IConfiguration config)
        {
            _config = config;
        
        }
        public DbSet<Computer>? Computer { get; set; } // Το DbSet<T> αντιπροσωπεύει ένα σύνολο από entities ενός συγκεκριμένου τύπου στη βάση δεδομένων. Sthn periptwsh msa DbSet<Computer>
        protected override void OnConfiguring(DbContextOptionsBuilder options) //OnConfiguring() χρησιμοποιείται για να ρυθμίσουμε τον τρόπο με τον οποίο το DbContext επικοινωνεί με τη βάση δεδομένων.
        {
            if (!options.IsConfigured) // Αν δεν έχει οριστεί ακόμα database provider, τότε ρύθμισε το Entity Framework να χρησιμοποιεί SQL Server.
            {// if it hasn't been configured yet, then we are going to configure it by passing in our connection string and that the basic idea
               // options.UseSqlServer("Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=True;Trusted_Connection=True;",
                options.UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                options => options.EnableRetryOnFailure());// shift+end
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");
            modelBuilder.Entity<Computer>() //Θέλω το Computer να αποτελεί entity του μοντέλου μου».ο EF Core θα συμπεριλάβει την κλάση Computer στο μοντέλο του.
             //.HasNoKey();  
            .HasKey(c => c.ComputerId);                                
                                              // .ToTable("Computer", "TutorialAppSchema");  .ToTable("TableName", "SchemaName");
                                             
          
        }


    }
}