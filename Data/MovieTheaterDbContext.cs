using BárdiHomework.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BárdiHomework.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Reservation> Reservations { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
