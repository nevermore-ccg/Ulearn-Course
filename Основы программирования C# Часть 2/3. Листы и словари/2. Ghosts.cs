using System;
using System.Text;

namespace hashes;

public class GhostsTask :
    IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>,
    IMagic
{
    private readonly Document _document;
    private readonly Vector _vector;
    private readonly Segment _segment;
    private readonly Cat _cat;
    private readonly Robot _robot;
    private byte[] _documentContent;

    public GhostsTask()
    {
        _documentContent = new byte[] { 2, 3, 4, 5 };
        _document = new Document("Title", Encoding.UTF8, _documentContent);
        _vector = new Vector(1, 2);
        _segment = new Segment(_vector, new Vector(2, 1));
        _cat = new Cat("Kitty", "English", DateTime.Now);
        _robot = new Robot("1");
    }

    public void DoMagic()
    {
        _documentContent[3] = 123;
        _vector.Add(_vector);
        _segment.Start.Add(_vector);
        _cat.Rename("Cat");
        Robot.BatteryCapacity += 11111;
    }

    Vector IFactory<Vector>.Create() => _vector;

    Segment IFactory<Segment>.Create() => _segment;

    Document IFactory<Document>.Create() => _document;

    Cat IFactory<Cat>.Create() => _cat;

    Robot IFactory<Robot>.Create() => _robot;
}