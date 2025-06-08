using Review_Filmes.Domain.Models;
using ReviewFilmes.Api.Entity;

namespace Review_Filmes.Web.Repository
{
	public class ReviewRepository : IReviewRepository
	{
		private AppDbContext _context;
		public ReviewRepository(AppDbContext context)
		{
			this._context = context;
		}

		public IEnumerable<Review> ListByFilmeId(int id)
		{
			return this._context.Reviews.Where(x => x.FilmeId == id).ToList();
		}
	}
}
