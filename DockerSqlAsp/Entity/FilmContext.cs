using Microsoft.EntityFrameworkCore;

namespace DockerSqlAsp.Models;

public class FilmContext
	: DbContext
{
	public DbSet<Film>? Film { get; set; }

	public FilmContext(DbContextOptions<FilmContext> options)
		: base(options)
	{
	}
}