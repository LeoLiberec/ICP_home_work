using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP_3_3D
{
    struct Quatrenion
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public Quatrenion(Vector3f Axis, float Angle)
        {
            float SinAngle = (float)Math.Sin(Math3D.ToRadian(Angle / 2));
            float CosAngle = (float)Math.Cos(Math3D.ToRadian(Angle / 2));

            x = Axis.X * SinAngle;
            y = Axis.Y * SinAngle;
            z = Axis.Z * SinAngle;
            w = CosAngle;
        }

        public Quatrenion Reverse()
        {
            Quatrenion Result = new Quatrenion();

            Result.x = -x;
            Result.y = -y;
            Result.z = -z;
            Result.w = w;

            return Result;
        }

        public static Vector3f Rotate(Vector3f Vec, float Angle, Vector3f Axis)
        {
            Quatrenion Q = new Quatrenion(Axis, Angle);
            Quatrenion QV = Q * Vec;
            Quatrenion QVQ = QV * Q.Reverse();

            return new Vector3f(QVQ.x, QVQ.y, QVQ.z);
        }


        public static Quatrenion operator *(Quatrenion qaut1, Quatrenion qaut2)
        {
            Quatrenion Result = new Quatrenion();

            Vector3f CrossVec = Math3D.Cross(
                new Vector3f(qaut1.x, qaut1.y, qaut1.z), 
                new Vector3f(qaut2.x, qaut2.y, qaut2.z));
             
            Result.x = (qaut1.x * qaut2.w) + (qaut1.w * qaut2.x) + CrossVec.X;
            Result.y = (qaut1.y * qaut2.w) + (qaut1.w * qaut2.y) + CrossVec.Y;
            Result.z = (qaut1.z * qaut2.w) + (qaut1.w * qaut2.z) + CrossVec.Z;
            Result.w = (qaut1.w * qaut2.w) - (qaut1.x * qaut2.x) - (qaut1.y * qaut2.y) - (qaut1.z * qaut2.z); 

            return Result;
        }
         
        public static Quatrenion operator *(Quatrenion qaut, Vector3f vec)
        {
            Quatrenion Result = new Quatrenion();

            Vector3f CrossVec = Math3D.Cross(
                new Vector3f(qaut.x, qaut.y, qaut.z), vec);

            Result.x = (qaut.w * vec.X) + CrossVec.X;
            Result.y = (qaut.w * vec.Y) + CrossVec.Y;
            Result.z = (qaut.w * vec.Z) + CrossVec.Z;
            Result.w = - (qaut.x * vec.X) - (qaut.y * vec.Y) - (qaut.z * vec.Z);

            return Result;
        }
    }
}
