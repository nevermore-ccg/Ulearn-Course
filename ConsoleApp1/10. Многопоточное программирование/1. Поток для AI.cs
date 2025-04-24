using System.Collections.Generic;

namespace rocket_bot;

public class Channel<T> where T : class
{
    private readonly List<T> _list = new();
    private readonly object _locker = new();

    public T this[int index]
    {
        get
        {
            lock (_locker)
            {
                return Count > index ? _list[index] : null;
            }
        }
        set
        {
            lock (_locker)
            {
                if (Count > index)
                {
                    _list[index] = value;
                    _list.RemoveRange(index + 1, Count - index - 1);
                }
                else if (Count == index) _list.Add(value);
            }
        }
    }

    public T LastItem()
    {
        lock (_locker)
        {
            return Count > 0 ? _list[Count - 1] : null;
        }
    }

    public void AppendIfLastItemIsUnchanged(T item, T knownLastItem)
    {
        lock (_locker)
        {
            if (LastItem() == knownLastItem)
                _list.Add(item);
        }
    }

    public int Count
    {
        get
        {
            lock (_locker)
            {
                return _list.Count;
            }
        }
    }
}