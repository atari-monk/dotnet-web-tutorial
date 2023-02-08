using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers;

[Route("api/commands")]
[ApiController]
public class CommandController : ControllerBase
{
	private readonly ICommanderRepo repo;
	private readonly IMapper mapper;

	public CommandController(
		ICommanderRepo repo
		, IMapper mapper)
	{
		this.repo = repo;
		this.mapper = mapper;
	}

	//GET api/commands
	[HttpGet]
	public ActionResult<IEnumerable<CommandReadDto>> GetCommands()
	{
		var commandItems = repo.GetCommands();

		return Ok(mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
	}

	//GET api/commands/{id}
	[HttpGet("{id}", Name="GetCommandById")]
	public ActionResult<CommandReadDto> GetCommandById(int id)
	{
		var commandItem = repo.GetCommandById(id);
		if(commandItem != null)
		{
			return Ok(mapper.Map<CommandReadDto>(commandItem));
		}
		return NotFound();
	}

	//POST api/commands
	[HttpPost]
	public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
	{
		var commandModel = mapper.Map<Command>(commandCreateDto);
		repo.CreateCommand(commandModel);
		repo.SaveChanges();

		var commandReadDto = mapper.Map<CommandReadDto>(commandModel);

		return CreatedAtRoute(nameof(GetCommandById), new {Id = commandReadDto.Id}, commandReadDto);
	}

	//PUT api/commands/{id}
	[HttpPut("{id}")]
	public ActionResult UpdateCommand(
		int id
		, CommandUpdateDto commandUpdateDto)
	{
		var commandModel = repo.GetCommandById(id);
		if(commandModel == null)
		{
			return NotFound();
		}
		mapper.Map(commandUpdateDto, commandModel);
		
		repo.UpdateCommand(commandModel);

		repo.SaveChanges();

		return NoContent();
	}

	//PATCH api/commands/{id}
	[HttpPatch("{id}")]
	public ActionResult PartialCommandUpdate(
		int id
		, JsonPatchDocument<CommandUpdateDto> patchDoc)
	{
		var commandModel = repo.GetCommandById(id);
		if(commandModel == null)
		{
			return NotFound();
		}

		var commandToPatch = mapper.Map<CommandUpdateDto>(commandModel);
		patchDoc.ApplyTo(commandToPatch, ModelState);

		if(!TryValidateModel(commandToPatch))
		{
			return ValidationProblem(ModelState);
		}

		mapper.Map(commandToPatch, commandModel);

		repo.UpdateCommand(commandModel);

		repo.SaveChanges();

		return NoContent();
	}

	//DELETE api/commands/{id}
	[HttpDelete("{id}")]
	public ActionResult DeleteCommand(int id)
	{
		var commandModel = repo.GetCommandById(id);
		if(commandModel == null)
		{
			return NotFound();
		}
		repo.DeleteCommand(commandModel);
		repo.SaveChanges();
		return NoContent();
	}
}