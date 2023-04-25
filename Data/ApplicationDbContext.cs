using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRest.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.Data
{
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){
        }

        public DbSet<Villa> Villas {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Villa>().HasData(
                new Villa(){
                    Id=1,
                    Name="Villa Real"
                },
                new Villa(){
                    Id = 2,
                    Name = "Vista a la playa"
                }
            );
        }
    }
}