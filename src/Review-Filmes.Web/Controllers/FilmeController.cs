using Microsoft.AspNetCore.Mvc;
using Review_Filmes.Domain.Models;
using Review_Filmes.Web.Entity;
using Review_Filmes.Web.Entity.DTO;
using Review_Filmes.Web.Repository;
using ReviewFilmes.Api.Entity;

namespace Review_Filmes.Web.Controllers
{
	public class FilmeController : Controller
	{

		private readonly ILogger<FilmeController> _logger;
		IFilmeRepository filmeRepo;
		IReviewRepository reviewRepo;

		public FilmeController(ILogger<FilmeController> logger, IFilmeRepository filmeRepo, IReviewRepository reviewRepo)
		{
			_logger = logger;
			this.filmeRepo = filmeRepo;
			this.reviewRepo = reviewRepo;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet("filme/{id}", Name = "GetFilme")]
		public IActionResult Get(int id)
		{
			FilmeReview filmeReview = new FilmeReview();
			var filme = this.filmeRepo.GetById(id);
			if (filme != null)
			{
				filme.Reviews = this.reviewRepo.ListByFilmeId(id).ToList();
				filmeReview.Filme = filme;
				filmeReview.FilmeID = filme.FilmeId;
			}

			return View(filmeReview);
		}

		[HttpPost("filme", Name = "AddReview")]
		[AutoValidateAntiforgeryToken]
		public IActionResult AddReview([FromForm] FilmeReview review)
		{
			Filme filme = this.filmeRepo.GetById(review.FilmeID);

			if (filme != null)
			{
				filme.AddReview(new Review(review.FilmeID, review.Nome, review.Avaliacao));
				this.filmeRepo.SaveChanges();
			}

			return RedirectToAction(actionName: "Get", controllerName: "Filme", new { id = review.FilmeID });
		}
	}

}
