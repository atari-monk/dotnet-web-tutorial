using Commander.Data;
using Commander.Models;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers;

//To test api
//enter project path
//execute command : dotnet run
//you shoud get info in console like:
//Now listening on: http://localhost:5027
//in postman use get
//type to test getall: http://localhost:5027/api/testcommands
//type to test getbyId: http://localhost:5027/api/testcommands/5
[Route("api/testcommands")]
[ApiController]
public class TestCommandController : ControllerBase
{
	private readonly IMockCommanderRepo repo;

	public TestCommandController(
		IMockCommanderRepo repo)
	{
		this.repo = repo;
	}

	//GET api/testcommands
	[HttpGet]
	public ActionResult<IEnumerable<Command>> GetCommands()
	{
		return Ok(repo.GetCommands());
	}

	//GET api/testcommands/{id}
	[HttpGet("{id}", Name="TestGetCommandById")]
	public ActionResult<Command> GetCommandById(int id)
	{
		return Ok(repo.GetCommandById(id));
	}
}