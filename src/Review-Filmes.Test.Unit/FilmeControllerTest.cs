using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework.Legacy;
using Review_Filmes.Domain.Models;
using Review_Filmes.Web.Controllers;
using Review_Filmes.Web.Entity;
using Review_Filmes.Web.Entity.DTO;
using Review_Filmes.Web.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review_Filmes.Test.Unit
{
	public class FilmeControllerTest
	{

		private Mock<IFilmeRepository> mockFilmeRepository;
		private Mock<IReviewRepository> mockReviewRepository;

		[Test]
		public void Cadastro_Review()
		{
			mockFilmeRepository = new(MockBehavior.Strict);

			mockFilmeRepository.Setup(s => s.GetById(1))
				.Returns(() => new Filme() { FilmeId = 1, Reviews = new List<Review>() });

			mockFilmeRepository.Setup(s => s.SaveChanges())
				.Verifiable();

			mockReviewRepository = new(MockBehavior.Strict);

			mockReviewRepository.Setup(s => s.ListByFilmeId(1))
				.Returns(() => new List<Review>() { new Review(1, "Teste", "Teste") });

			var controller = new FilmeController(null, mockFilmeRepository.Object, mockReviewRepository.Object);

			var result = controller.AddReview(new FilmeReview() { FilmeID = 1, Nome = "Teste", Avaliacao = "Foi bom" }) as RedirectToActionResult;

			ClassicAssert.NotNull(result);
			Assert.That(result.ActionName, Is.EqualTo("Get"));
			Assert.That(result.ControllerName, Is.EqualTo("Filme"));
			Assert.That((int)result.RouteValues["id"], Is.EqualTo(1));		
		}
	}
}
