using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP_3_3D
{
    static class Math3D
    {
        public static float ToRadian(float degrees)
        {
            return (float)(degrees * Math.PI / 180); // prevod do radianu
        }
        public static Vector3f Cross(Vector3f vec1, Vector3f vec2)
        {
            Vector3f Result = new Vector3f();
            Result.X = vec1.Y * vec2.Z - vec1.Z * vec2.Y;
            Result.Y = vec1.Z * vec2.X - vec1.X * vec2.Z;
            Result.Z = vec1.X * vec2.Y - vec1.Y * vec2.X;

            return Result;
        }
        public static Vector3f GetNormalize(this Vector3f vec)
        {
            float lenght = vec.GetLenght();
            return new Vector3f(vec.X, vec.Y, vec.Z) / lenght;
        }

        public static float GetLenght(this Vector3f vec)
        {
            float length = (float)Math.Sqrt(Math.Pow(vec.X, 2) + Math.Pow(vec.Y, 2) + Math.Pow(vec.Z, 2));
            return length;
        }
    }
}
