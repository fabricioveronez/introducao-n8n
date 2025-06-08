using Microsoft.EntityFrameworkCore;
using Review_Filmes.Domain.Models;
using Review_Filmes.Web.Entity;

namespace ReviewFilmes.Api.Entity
{
    public class AppDbContext : DbContext
	{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Review> Reviews { get; set; }

    }
}

