using System;

namespace func_rocket;

public class ControlTask
{
    private static double Angle;

    public static Turn ControlRocket(Rocket rocket, Vector target)
    {
        var distanceVector = new Vector(target.X - rocket.Location.X, target.Y - rocket.Location.Y);

        if (Math.Abs(distanceVector.Angle - rocket.Direction) < 0.5
            || Math.Abs(distanceVector.Angle - rocket.Velocity.Angle) < 0.5)
        {
            Angle = (distanceVector.Angle - rocket.Direction + distanceVector.Angle - rocket.Velocity.Angle) / 2;
        }
        else
        {
            Angle = distanceVector.Angle - rocket.Direction;
        }

        if (Angle < 0)
            return Turn.Left;
        return Angle > 0 ? Turn.Right : Turn.None;
    }
}