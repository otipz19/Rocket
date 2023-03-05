using System;

namespace func_rocket
{
	public class ControlTask
	{
        public static Turn ControlRocket(Rocket rocket, Vector target)
        {
            double angleOfRocket = (rocket.Direction * 0.33) + (rocket.Velocity.Angle * 0.66);
            Vector fromRocketToTarget = target - rocket.Location;
            double angleToTarget = Math.Atan2(fromRocketToTarget.Y, fromRocketToTarget.X);
            double offsetAngle = angleOfRocket - angleToTarget;
            if (offsetAngle < 0)
                return Turn.Right;
            else if (offsetAngle > 0)
                return Turn.Left;
            return Turn.None;
        }
    }
}