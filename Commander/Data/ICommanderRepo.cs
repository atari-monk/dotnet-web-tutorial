using Commander.Models;

namespace Commander.Data;

public interface ICommanderRepo
{
    IEnumerable<Command> GetCommands();
    Command GetCommandById(int Id);
    void CreateCommand(Command cmd);
    void UpdateCommand(Command cmd);
    void DeleteCommand(Command cmd);
    bool SaveChanges();
}