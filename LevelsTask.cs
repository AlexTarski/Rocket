using System;
using System.Collections.Generic;
using System.Drawing;

namespace func_rocket;

public class LevelsTask
{
	static readonly Physics standardPhysics = new();
	static readonly Rocket standartRocket = new (new Vector(200, 500), Vector.Zero, -0.5 * Math.PI);
	static readonly Vector standartTarget = new(600, 200);
    static Gravity blackHole = (size, v) =>
	{
		var anomaly = (standartTarget + standartRocket.Location) / 2;
		var d = (anomaly - v).Length;
		return (anomaly - v).Normalize() * (300 * d) / (d * d + 1);
	};
	static Gravity whiteHole = (size, v) =>
	{
        var d = (standartTarget - v).Length;
        return (standartTarget - v).Normalize() * (-140 * d) / (d * d + 1);
    };

	public static IEnumerable<Level> CreateLevels()
	{
		yield return new Level("Zero", 	standartRocket, standartTarget, (size, v) => Vector.Zero, standardPhysics);
		yield return new Level("Heavy", standartRocket, standartTarget, (size, v) => new Vector(0.0d, 0.9d), standardPhysics);
		yield return new Level("Up", standartRocket, new Vector(700, 500), (size, v) => new Vector(0.0d, -300.0d / (size.Y - v.Y + 300.0d)), standardPhysics);
		yield return new Level("WhiteHole", standartRocket, standartTarget, (size, v) => whiteHole(size, v), standardPhysics);
		yield return new Level("BlackHole", standartRocket,	standartTarget, (size, v) => blackHole(size, v), standardPhysics);
		yield return new Level("BlackAndWhite", standartRocket, standartTarget, (size, v) => (blackHole(size, v) + whiteHole(size, v)) / 2, standardPhysics);
	}
}