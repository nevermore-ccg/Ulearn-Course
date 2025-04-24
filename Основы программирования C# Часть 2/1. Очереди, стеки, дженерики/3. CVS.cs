using System;
using System.Collections.Generic;

namespace Clones;

public class CloneVersionSystem : ICloneVersionSystem
{
    private readonly Dictionary<int, Clone> _clones;

    public CloneVersionSystem()
    {
        _clones = new Dictionary<int, Clone>
        {
            { 1, new Clone() }
        };
    }

    public string Execute(string query)
    {
        var queryArguments = query.Split(' ');
        var command = queryArguments[0];
        var cloneId = Convert.ToInt32(queryArguments[1]);
        switch (command)
        {
            case "learn":
                var program = queryArguments[2];
                _clones[cloneId].Learn(program);
                return null;
            case "rollback":
                _clones[cloneId].Rollback();
                return null;
            case "relearn":
                _clones[cloneId].Relearn();
                return null;
            case "clone":
                _clones.Add(_clones.Count + 1, new Clone(_clones[cloneId]));
                return null;
            case "check":
                return _clones[cloneId].Check();
            default:
                throw new ArgumentException();
        }
    }
}
public class Clone
{
    private StackProgram<string> _stackAdopted;
    private StackProgram<string> _stackCancelled;

    public Clone()
    {
    }

    public Clone(Clone cloneParent)
    {
        _stackAdopted = cloneParent._stackAdopted;
        _stackCancelled = cloneParent._stackCancelled;
    }

    public void Learn(string program)
    {
        _stackCancelled = null;
        _stackAdopted = new StackProgram<string>(program, _stackAdopted);
    }

    public void Rollback()
    {
        _stackCancelled = new StackProgram<string>(_stackAdopted.Program, _stackCancelled);
        _stackAdopted = _stackAdopted.Next;
    }

    public void Relearn()
    {
        _stackAdopted = new StackProgram<string>(_stackCancelled.Program, _stackAdopted);
        _stackCancelled = _stackCancelled.Next;
    }

    public string Check()
    {
        if (_stackAdopted == null)
            return "basic";
        else
            return _stackAdopted.Program;
    }
}
public class StackProgram<T>
{
    public T Program { get; }
    public StackProgram<T> Next { get; }

    public StackProgram(T program, StackProgram<T> next)
    {
        Program = program;
        Next = next;
    }
}