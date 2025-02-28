using BlogApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}
		[NonAction]
		[Route("Error")]
		public IActionResult Error(int? statusCode = null)
		{
			if (statusCode.HasValue)
			{
				if (statusCode == 404 || statusCode == 403|| statusCode==500)
				{
					var viewName = statusCode.ToString();
					_logger.LogWarning($"Произошла ошибка - {statusCode}\n{viewName}");
					return View(viewName);
				}
				else
					return View("500");
			}
			return View("500");
		}
	}
}
