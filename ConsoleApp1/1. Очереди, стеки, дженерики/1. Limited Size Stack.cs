using System;
using System.Collections.Generic;

namespace LimitedSizeStack;

public class LimitedSizeStack<T>
{
    private readonly LinkedList<T> _list;
    private readonly int _limit;
    public LimitedSizeStack(int undoLimit)
    {
        _list = new LinkedList<T>();
        _limit = undoLimit;
    }

    public void Push(T item)
    {
        _list.AddLast(item);
        if (_list.Count > _limit) _list.RemoveFirst();
    }

    public T Pop()
    {
        if (Count == 0) throw new InvalidOperationException();
        var result = _list.Last.Value;
        _list.RemoveLast();
        return result;
    }

    public int Count => _list.Count;
}