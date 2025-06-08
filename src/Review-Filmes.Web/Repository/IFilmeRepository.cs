using Review_Filmes.Domain.Models;

namespace Review_Filmes.Web.Repository
{
	public interface IFilmeRepository
	{
		Filme GetById(int id);
		void SaveChanges();
	}
}
