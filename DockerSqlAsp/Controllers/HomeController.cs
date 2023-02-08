using DockerSqlAsp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DockerSqlAsp.Controllers;

public class HomeController
	: Controller
{
	private readonly FilmContext ctx;
	private readonly ILogger<HomeController> logger;

	public HomeController(
		ILogger<HomeController> logger
		, FilmContext ctx)
	{
		this.logger = logger;
		this.ctx = ctx;
	}

	public IActionResult Index()
	{
		var film = ctx.Film!.ToList();
		return View(film);
	}

	public IActionResult AddNew()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Delete(int id)
	{
		Film film = new();
		var filmDb = ctx.Film!.Where(b => b.Id == id).FirstOrDefault();
		if (filmDb == null) RedirectToAction(nameof(Index));
		else film = filmDb;
		ctx.Remove(film);
		await ctx.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}

	[HttpPost]
	public async Task<IActionResult> Modify(Film film)
	{
		if (ModelState.IsValid)
		{
			ctx.Update(film);
			await ctx.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		else
			return View(film);
	}

	[HttpPost]
	public async Task<IActionResult> AddNew(Film film)
	{
		if (ModelState.IsValid)
		{
			ctx.Add(film);
			await ctx.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		else
			return View();
	}

	public IActionResult Modify(int id)
	{
		var film = ctx.Film!.Where(b => b.Id == id).FirstOrDefault();
		return View(film);
	}

	public IActionResult Privacy()
	{
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
