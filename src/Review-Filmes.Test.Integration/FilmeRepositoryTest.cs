using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework.Legacy;
using Review_Filmes.Domain.Models;
using Review_Filmes.Web.Repository;
using ReviewFilmes.Api.Entity;

namespace Review_Filmes.Test.Integration
{
	public class FilmeRepositoryTest
	{

		IFilmeRepository _filmeRepository;
		IReviewRepository _reviewRepository;

		[SetUp]
		public void Setup()
		{
			AppDbContext _context;

			// Configuração para carregar o appsettings.json e/ou variáveis de ambiente
			var configuration = new ConfigurationBuilder()
					.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
					.AddJsonFile("appsettings.json", optional: true)
					.AddEnvironmentVariables()
					.Build();

			// Utiliza a string de conexão do appsettings.json ou variável de ambiente
			var options = new DbContextOptionsBuilder<AppDbContext>()
				.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
				.Options;

			_context = new AppDbContext(options);
			_context.Database.EnsureCreated(); // Cria o banco de dados se não existir

			// Adiciona dados de teste
			_context.Filmes.Add(new Filme
			{
				Titulo = "Bad Boys: Até O Fim",
				Resumo = "Os Bad Boys preferidos do mundo todo estão de volta com a mistura icônica de ação eletrizante e comédia escrachada mas, dessa vez, com uma virada: os melhores de Miami são agora os mais procurados.",
				Lancamento = "Junho de 2024",
				Categoria = "Ação",
				Elenco = "Will Smith, Martin Lawrence, Vanessa Hudgens",
				Direcao = "Adil El Arbi, Bilall Fallah",
				Slide = "/images/badboys-slide.jpg",
				Thumb = "/images/badboys-thumb.jpg"
			});

			_context.SaveChanges();

			_filmeRepository = new FilmeRepository(_context);
			_reviewRepository = new ReviewRepository(_context);

		}

		[Test]
		public void ObterFilmePorID()
		{
			var result = _filmeRepository.GetById(1);
			ClassicAssert.IsNotNull(result);
			Assert.That(result.FilmeId, Is.EqualTo(1));
		}

		//[TearDown]
		//public void TearDown()
		//{
		//	_context.Database.EnsureDeleted();
		//	_context.Dispose();
		//}
	}
}
