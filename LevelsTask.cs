using System;
using System.Collections.Generic;
using System.Drawing;

namespace func_rocket
{
	public class LevelsTask
	{
		static readonly Physics standardPhysics = new Physics();
        private static readonly Vector standardTargetLocation = new Vector(600, 200);
		private static readonly Vector standardRocketLocation = new Vector(200, 500);

		private static Vector WhiteHoleGravity(Size size, Vector location)
		{
			Vector fromTargetToRocket = location - standardTargetLocation;
			return fromTargetToRocket.Normalize() * (140 * fromTargetToRocket.Length) / (Math.Pow(fromTargetToRocket.Length, 2) + 1);
		}

		private static Vector BlackHoleGravity(Size size, Vector location)
		{
            Vector anomalyPos = new Vector(400, 350);
            Vector fromRocketToAnomaly = anomalyPos - location;
            return fromRocketToAnomaly.Normalize() * (300 * fromRocketToAnomaly.Length / (Math.Pow(fromRocketToAnomaly.Length, 2) + 1));
        }

        public static IEnumerable<Level> CreateLevels()
		{
			yield return CreateLevel("Zero", (size, location) => Vector.Zero);
			yield return CreateLevel("Heavy", (size, location) => new Vector(0, 0.9));
			Level up = CreateLevel("Up", (size, location) => new Vector(0, -1) * 300 / (size.Height - location.Y + 300));
			up.Target = new Vector(700, 500);
            yield return up;
			yield return CreateLevel("WhiteHole", WhiteHoleGravity);
			yield return CreateLevel("BlackHole", BlackHoleGravity);
			yield return CreateLevel("BlackAndWhite", (size, location) => 
				(WhiteHoleGravity(size, location) + BlackHoleGravity(size, location)) / 2);
        }

		private static Level CreateLevel(string name, Gravity gravity)
		{
			return new Level(name,
                new Rocket(standardRocketLocation, Vector.Zero, -0.5 * Math.PI),
                standardTargetLocation,
                gravity,
                standardPhysics);
        }
	}
}