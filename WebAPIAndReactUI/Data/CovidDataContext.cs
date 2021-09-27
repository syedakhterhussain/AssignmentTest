using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIAndReactUI.Model;

namespace WebAPIAndReactUI.Data
{
    public class CovidDataContext : DbContext
    {
        public CovidDataContext(DbContextOptions<CovidDataContext> options) : base(options)
        {

        }


        public DbSet<GlobalStat> GlobalStats { get; set; }
        public DbSet<CountriesWiseSummery> CountryWiseSummeries { get; set; }
        public DbSet<CountryHistory> CountryHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
           
            modelBuilder.Entity<GlobalStat>().ToTable("GlobalStat");
            modelBuilder.Entity<CountriesWiseSummery>().ToTable("CountryWiseSummeries");
            modelBuilder.Entity<CountryHistory>().ToTable("CountryHistories");
            
        }

    }
}
