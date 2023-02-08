using System.ComponentModel.DataAnnotations;

namespace DockerSqlAsp.Models;

public class Film
{
	[Required]
	[Key]
	public int Id { get; set; }

	[Required]
	[StringLength(50, ErrorMessage = "ReleaseYear cannot exceed 50 characters.")]
	public string? ReleaseYear { get; set; }

	[Required]
	[StringLength(150, ErrorMessage = "Movie name cannot exceed 150 characters.")]
	public string? MovieName { get; set; }
}