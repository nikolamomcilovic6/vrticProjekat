using Microsoft.EntityFrameworkCore;

namespace Models 
{
    public class VrticContext : DbContext
    {
        public DbSet<Administrator> Administratori {get; set;}
        public DbSet<Vrtic> Vrtici{get;set;}

        public DbSet<Aktivnost> Aktivnosti{get;set;}

        public DbSet<Vaspitac> Vaspitaci{get; set;}

        public DbSet<Ucestvuje> Ucestvuju{ get; set; }

        public DbSet<Odrzava> Odrzavaju { get; set; }

        public DbSet<Nadgleda> Nadgledaju { get; set; }

        public DbSet<Dete> Deca{get; set;}

        public VrticContext(DbContextOptions options) :base(options)
        {

        }

    }
}