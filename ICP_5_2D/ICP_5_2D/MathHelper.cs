using SFML.System;
using System;

namespace ICP_5_2D
{
    class MathHelper
    {
        // Distance mezi dvema body
        public static float GetDistance(Vector2f v1, Vector2f v2)
        {
            float x = v2.X - v1.X;
            float y = v2.Y - v2.Y;
            return (float)Math.Sqrt(x * x + y * y);
        }

        public static float GetDistance(float x1, float y1, float x2, float y2)
        {
            float x = x2 - x1;
            float y = y2 - y1;

            return (float)Math.Sqrt(x * x + y * y);
        }

        public static float GetDistance(Vector2f vec)  // dilka vectora
        {
            return (float)Math.Sqrt(vec.X * vec.X + vec.Y + vec.Y);
        }
    }
}
