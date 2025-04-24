using Avalonia.Input;
using Digger.Architecture;
using System;

namespace Digger;

public class Terrain : ICreature
{
    public CreatureCommand Act(int x, int y)
    {
        return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
    }

    public bool DeadInConflict(ICreature conflictedObject) => true;

    public int GetDrawingPriority() => 10;

    public string GetImageFileName() => "Terrain.png";
}
public class Player : ICreature
{
    public CreatureCommand Act(int x, int y)
    {
        var command = new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        switch (Game.KeyPressed)
        {
            case Key.Up:
                if (y - 1 >= 0)
                    command.DeltaY--;
                break;
            case Key.Down:
                if (Game.MapHeight > y + 1)
                    command.DeltaY++;
                break;
            case Key.Left:
                if (x - 1 >= 0)
                    command.DeltaX--;
                break;
            case Key.Right:
                if (Game.MapWidth > x + 1)
                    command.DeltaX++;
                break;
        }
        return command;
    }

    public bool DeadInConflict(ICreature conflictedObject)
    {
        return conflictedObject.GetImageFileName() == "Monster.png";
    }

    public int GetDrawingPriority() => 1;

    public string GetImageFileName() => "Digger.png";
}