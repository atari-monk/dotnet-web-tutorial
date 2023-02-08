using Commander.Models;

namespace Commander.Data;

public class SqlCommanderRepo : ICommanderRepo
{
	private readonly CommanderContext context;

	public SqlCommanderRepo(
		CommanderContext context)
	{
		this.context = context;
	}

	public IEnumerable<Command> GetCommands()
	{
        ArgumentNullException.ThrowIfNull(context.Commands);
		return context.Commands.ToList();
	}

	public Command GetCommandById(int id)
	{
        ArgumentNullException.ThrowIfNull(context.Commands);
        var command = context.Commands.FirstOrDefault(c => c.Id == id);
		return command ?? new Command{  };
	}

	public void CreateCommand(Command cmd)
	{
		if (cmd == null)
			throw new ArgumentNullException(nameof(cmd));
        ArgumentNullException.ThrowIfNull(context.Commands);
		context.Commands.Add(cmd);
	}

	public void UpdateCommand(Command cmd)
	{
		//Nothing in ef core
	}

	public void DeleteCommand(Command cmd)
	{
		if (cmd == null)
			throw new ArgumentNullException(nameof(cmd));
        ArgumentNullException.ThrowIfNull(context.Commands);
		context.Commands.Remove(cmd);
	}

	public bool SaveChanges()
	{
		return (context.SaveChanges() >= 0);
	}
}