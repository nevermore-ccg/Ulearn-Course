using System;

namespace func_rocket;

public class ControlTask
{
    public static Turn ControlRocket(Rocket rocket, Vector target)
    {
        var vector = target - rocket.Location;
        var angle1 = vector.Angle - rocket.Direction;
        var angle2 = vector.Angle - rocket.Velocity.Angle;
        if (Math.Abs(angle1) < 0.5 || Math.Abs(angle2) < 0.5)
            angle1 += angle2;
        if (angle1 == 0)
            return Turn.None;
        return angle1 < 0 ? Turn.Left : Turn.Right;
    }
}