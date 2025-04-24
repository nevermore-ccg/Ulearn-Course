using Avalonia.Controls.Primitives;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LimitedSizeStack;

public class ListModel<TItem> //Client
{
    public List<TItem> Items { get; }
    public int UndoLimit;
    private Receiver<TItem> _receiver;
    private Invoker<TItem> _invoker;
    public ListModel(int undoLimit) : this(new List<TItem>(), undoLimit)
    {
    }

    public ListModel(List<TItem> items, int undoLimit)
    {
        Items = items;
        UndoLimit = undoLimit;
        _invoker = new Invoker<TItem>(undoLimit);
        _receiver = new Receiver<TItem>(items);
    }

    public void AddItem(TItem item)
    {
        var command = new AddCommand<TItem>(_receiver, item);
        _invoker.ExecuteCommand(command);
    }

    public void RemoveItem(int index)
    {
        var command = new RemoveCommand<TItem>(_receiver, index);
        _invoker.ExecuteCommand(command);
    }

    public bool CanUndo() => _invoker.CanUndo;

    public void Undo() => _invoker.UndoCommand();
}
public abstract class Command<TItem>
{
    public abstract void Execute();
    public abstract void Undo();
}
public class Receiver<TItem>
{
    private readonly List<TItem> _items;
    public int LastIndex => _items.Count - 1;

    public Receiver(List<TItem> items)
    {
        _items = items;
    }

    public void AddItem(TItem item) => _items.Add(item);
    public void RemoveItem(int index) => _items.RemoveAt(index);
    public void InsertItem(TItem item, int index) => _items.Insert(index, item);
    public TItem GetItem(int index) => _items.ElementAt(index);
}
public class AddCommand<TItem> : Command<TItem>
{
    private Receiver<TItem> _receiver;
    private readonly TItem _item;

    public AddCommand(Receiver<TItem> receiver, TItem item)
    {
        _receiver = receiver;
        _item = item;
    }

    public override void Execute()
    {
        _receiver.AddItem(_item);
    }

    public override void Undo()
    {
        _receiver.RemoveItem(_receiver.LastIndex);
    }
}
public class RemoveCommand<TItem> : Command<TItem>
{
    private Receiver<TItem> _receiver;
    private readonly int _index;
    private readonly TItem _item;

    public RemoveCommand(Receiver<TItem> receiver, int index)
    {
        _receiver = receiver;
        _index = index;
        _item = receiver.GetItem(_index);
    }

    public override void Execute()
    {
        _receiver.RemoveItem(_index);
    }

    public override void Undo()
    {
        _receiver.InsertItem(_item, _index);
    }
}
public class Invoker<TItem>
{
    private readonly LimitedSizeStack<Command<TItem>> _commandStack;

    public Invoker(int undoLimit)
    {
        _commandStack = new LimitedSizeStack<Command<TItem>>(undoLimit);
    }

    public void ExecuteCommand(Command<TItem> command)
    {
        command.Execute();
        _commandStack.Push(command);
    }

    public void UndoCommand()
    {
        var command = _commandStack.Pop();
        command.Undo();
    }

    public bool CanUndo => _commandStack.Count > 0;
}