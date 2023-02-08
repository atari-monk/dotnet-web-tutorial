using Commander.Data;
using Commander.Models;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers;

[Route("api/test2commands")]
[ApiController]
public class Test2CommandController : ControllerBase
{
	private readonly ICommanderRepo repo;

	public Test2CommandController(
		ICommanderRepo repo)
	{
		this.repo = repo;
	}

	//GET api/test2commands
	[HttpGet]
	public ActionResult<IEnumerable<Command>> GetCommands()
	{
		return Ok(repo.GetCommands());
	}

	//GET api/test2commands/{id}
	[HttpGet("{id}", Name="Test2GetCommandById")]
	public ActionResult<Command> GetCommandById(int Id)
	{
		var commandItem = repo.GetCommandById(Id);
		if(commandItem != null)
		{
			return Ok(commandItem);
		}
		return NotFound();
	}
}