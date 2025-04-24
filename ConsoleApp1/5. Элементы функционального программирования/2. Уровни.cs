using System;
using System.Collections.Generic;

namespace func_rocket;

public class LevelsTask
{
    private static readonly Physics _standardPhysics = new();

    public static IEnumerable<Level> CreateLevels()
    {
        var rocket = new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI);
        var target = new Vector(600, 200);
        Gravity zero = (size, v) => Vector.Zero;
        Gravity heavy = (size, v) => new Vector(0, 0.9);
        Gravity up = (size, v) => new Vector(0, -300 / (size.Y - v.Y + 300));
        Gravity whiteHole = (size, v) =>
        {
            var vector = target - v;
            return vector.Normalize() * -140 * vector.Length / (vector.Length * vector.Length + 1);
        };
        Gravity blackHole = (size, v) =>
        {
            var anomaly = (target + rocket.Location) / 2 - v;
            return anomaly.Normalize() * 300 * anomaly.Length / (anomaly.Length * anomaly.Length + 1);
        };
        Gravity blackAndWhite = (size, v) => (whiteHole(size, v) + blackHole(size, v)) / 2;
        yield return new Level("Zero", rocket, target, zero, _standardPhysics);
        yield return new Level("Heavy", rocket, target, heavy, _standardPhysics);
        yield return new Level("Up", rocket, new Vector(700, 500), up, _standardPhysics);
        yield return new Level("WhiteHole", rocket, target, whiteHole, _standardPhysics);
        yield return new Level("BlackHole", rocket, target, blackHole, _standardPhysics);
        yield return new Level("BlackAndWhite", rocket, target, blackAndWhite, _standardPhysics);
    }
}