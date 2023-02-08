using Commander.Models;

namespace Commander.Data;

public class MockCommanderRepo : IMockCommanderRepo
{
    public IEnumerable<Command> GetCommands()
    {
        return new List<Command>
        {
            new Command
            { 
                Id=0
                , HowTo="Boil an egg"
                , Line="Boil water"
                , Platform="Kettle & Pan"
            }
            , new Command
            { 
                Id=0
                , HowTo="Cut bread"
                , Line="Get a knife"
                , Platform="knife & chopping board"
            }
            , new Command
            { 
                Id=0
                , HowTo="Make cup of tea"
                , Line="Place teabag in cup"
                , Platform="Kettle & cup"
            }
        };
    }

    public Command GetCommandById(int Id)
    {
        return new Command
        { 
            Id=0
            , HowTo="Boil an egg"
            , Line="Boil water"
            , Platform="Kettle & Pan"
        };
    }

    public void CreateCommand(Command cmd)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateCommand(Command cmd)
    {
        throw new System.NotImplementedException();
    }

    public void DeleteCommand(Command cmd)
    {
        throw new System.NotImplementedException();
    }

    public bool SaveChanges()
    {
        throw new System.NotImplementedException();
    }
}