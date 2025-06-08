using Review_Filmes.Domain.Models;

namespace Review_Filmes.Web.Repository
{
	public interface IReviewRepository
	{
		IEnumerable<Review> ListByFilmeId(int id);
	}
}
