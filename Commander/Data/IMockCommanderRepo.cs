using Commander.Models;

namespace Commander.Data;

public interface IMockCommanderRepo
{
     IEnumerable<Command> GetCommands();
    Command GetCommandById(int id);
}