using Review_Filmes.Domain.Models;
using ReviewFilmes.Api.Entity;

namespace Review_Filmes.Web.Repository
{
	public class FilmeRepository : IFilmeRepository
	{
		private AppDbContext _context;
		public FilmeRepository(AppDbContext context)
		{
			this._context = context;
		}
		public Filme GetById(int id)
		{
			return this._context.Filmes.Where(x => x.FilmeId == id).FirstOrDefault();
		}

		public void SaveChanges()
		{
			this._context.SaveChanges();
		}
	}
}
