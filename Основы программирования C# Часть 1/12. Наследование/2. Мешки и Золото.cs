using Avalonia.Input;
using Digger.Architecture;
using System;
using System.ComponentModel;

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
        if (Game.Map[x + command.DeltaX, y + command.DeltaY] is Sack)
        {
            command.DeltaX = 0;
            command.DeltaY = 0;
        }
        if (command.DeltaX != 0 || command.DeltaY != 0)
            Game.Map[x, y] = null;
        return command;
    }

    public bool DeadInConflict(ICreature conflictedObject)
    {
        if (conflictedObject is Monster || conflictedObject is Sack)
            return true;
        if (conflictedObject is Gold)
            Game.Scores += 10;
        return false;
    }

    public int GetDrawingPriority() => 2;

    public string GetImageFileName() => "Digger.png";
}
public class Sack : ICreature
{
    public int Count = 0;
    public CreatureCommand Act(int x, int y)
    {
        if (y + 1 < Game.MapHeight
            && ((Count > 0 && (Game.Map[x, y + 1] is Player
            || Game.Map[x, y + 1] is Monster))
            || Game.Map[x, y + 1] == null))
        {
            Count++;
            return new CreatureCommand { DeltaX = 0, DeltaY = 1 };
        }
        if (Count > 1 || y == Game.MapHeight)
            return new CreatureCommand { TransformTo = new Gold() };
        else
            Count = 0;
        return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
    }

    public bool DeadInConflict(ICreature conflictedObject) => false;

    public int GetDrawingPriority() => 0;

    public string GetImageFileName() => "Sack.png";
}
public class Gold : ICreature
{
    public CreatureCommand Act(int x, int y)
    {
        return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
    }

    public bool DeadInConflict(ICreature conflictedObject)
    {
        return conflictedObject is Player;
    }

    public int GetDrawingPriority() => 1;

    public string GetImageFileName() => "Gold.png";
}
public class Monster : ICreature
{
    public CreatureCommand Act(int x, int y)
    {
        throw new NotImplementedException();
    }

    public bool DeadInConflict(ICreature conflictedObject)
    {
        throw new NotImplementedException();
    }

    public int GetDrawingPriority() => 3;

    public string GetImageFileName() => "Monster.png";
}