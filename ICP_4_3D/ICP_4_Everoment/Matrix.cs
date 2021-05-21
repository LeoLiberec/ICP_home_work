using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP_4_Everoment
{
    struct Matrix4
    {
        public Vectror4 Column1;
        public Vectror4 Column2;
        public Vectror4 Column3;
        public Vectror4 Column4;

        public Matrix4(Vectror4 column1, Vectror4 column2, Vectror4 column3, Vectror4 column4)
        {
            Column1 = column1;
            Column2 = column2;
            Column3 = column3;
            Column4 = column4;
        }

        public void Translate(Vector3f Pos)
        {
            Column1.X = 1;
            Column2.Y = 1;
            Column3.Z = 1;
            Column4.V = 1;

            Column1.V = Pos.X;
            Column2.V = Pos.Y;
            Column3.V = Pos.Z;
        }

        public void Scale(Vector3f Scale)
        {
            Column1.X = Scale.X;
            Column2.Y = Scale.Y;
            Column3.Z = Scale.Z;
            Column4.V = 1;
        }

        public void Rotation(Vector3f Rotat)
        {
            Matrix4 matX = new Matrix4();
            Matrix4 matY = new Matrix4();
            Matrix4 matZ = new Matrix4();

            //MatX
            matX.Column1.X = 1;
            matX.Column4.V = 1;

            matX.Column2.Y = (float)Math.Cos(Rotat.X);
            matX.Column2.Z = -(float)Math.Sin(Rotat.X);

            matX.Column3.Y = (float)Math.Sin(Rotat.X);
            matX.Column3.Z = (float)Math.Cos(Rotat.X);

            //MatY
            matY.Column2.X = 1;
            matY.Column4.V = 1;

            matY.Column1.X = (float)Math.Cos(Rotat.Y);
            matY.Column1.Z = -(float)Math.Sin(Rotat.Y);

            matY.Column3.Y = (float)Math.Sin(Rotat.Y);
            matY.Column3.Z = (float)Math.Cos(Rotat.Y);

            //MatZ
            matZ.Column3.Z = 1;
            matZ.Column4.V = 1;

            matZ.Column1.X = (float)Math.Cos(Rotat.Z);
            matZ.Column1.Z = -(float)Math.Sin(Rotat.Z);

            matZ.Column2.Y = (float)Math.Sin(Rotat.Z);
            matZ.Column2.Z = (float)Math.Cos(Rotat.Z);

            //Result
            Matrix4 resultMat = matX * matY * matZ;

            Column1 = resultMat.Column1;
            Column2 = resultMat.Column2;
            Column3 = resultMat.Column3;
            Column4 = resultMat.Column4;
        }

        public float[] GetArray()
        {
            float[] array =
            {
                Column1.X, Column1.Y, Column1.Z, Column1.V,
                 Column2.X, Column2.Y, Column2.Z, Column2.V,
                  Column3.X, Column3.Y, Column3.Z, Column3.V,
                   Column4.X, Column4.Y, Column4.Z, Column4.V,
            };
            return array;
        }

        public static Matrix4 operator *(Matrix4 mat1, Matrix4 mat2) // Matica operator *
        {
            Matrix4 result = new Matrix4();
            result.Column1.X = (mat1.Column1.X * mat2.Column1.X) + (mat1.Column1.Y * mat2.Column2.X)
                + (mat1.Column1.Z * mat2.Column3.X) + (mat1.Column1.V * mat2.Column4.X);
            result.Column1.Y = (mat1.Column1.X * mat2.Column1.Y) + (mat1.Column1.Y * mat2.Column2.Y)
                + (mat1.Column1.Z * mat2.Column3.Y) + (mat1.Column1.V * mat2.Column4.Y);
            result.Column1.Z = (mat1.Column1.X * mat2.Column1.Z) + (mat1.Column1.Y * mat2.Column2.Z)
                + (mat1.Column1.Z * mat2.Column3.Z) + (mat1.Column1.V * mat2.Column4.Z);
            result.Column1.V = (mat1.Column1.X * mat2.Column1.V) + (mat1.Column1.Y * mat2.Column2.V)
                + (mat1.Column1.Z * mat2.Column3.V) + (mat1.Column1.V * mat2.Column4.V);

            result.Column2.X = (mat1.Column2.X * mat2.Column1.X) + (mat1.Column2.Y * mat2.Column2.X)
                + (mat1.Column2.Z * mat2.Column3.X) + (mat1.Column2.V * mat2.Column4.X);
            result.Column2.Y = (mat1.Column2.X * mat2.Column1.Y) + (mat1.Column2.Y * mat2.Column2.Y)
                + (mat1.Column2.Z * mat2.Column3.Y) + (mat1.Column2.V * mat2.Column4.Y);
            result.Column2.Z = (mat1.Column2.X * mat2.Column1.Z) + (mat1.Column2.Y * mat2.Column2.Z)
                + (mat1.Column2.Z * mat2.Column3.Z) + (mat1.Column2.V * mat2.Column4.Z);
            result.Column2.V = (mat1.Column2.X * mat2.Column1.V) + (mat1.Column2.Y * mat2.Column2.V)
                + (mat1.Column2.Z * mat2.Column3.V) + (mat1.Column2.V * mat2.Column4.V);

            result.Column3.X = (mat1.Column3.X * mat2.Column1.X) + (mat1.Column3.Y * mat2.Column2.X)
                + (mat1.Column3.Z * mat2.Column3.X) + (mat1.Column3.V * mat2.Column4.X);
            result.Column3.Y = (mat1.Column3.X * mat2.Column1.Y) + (mat1.Column3.Y * mat2.Column2.Y)
                + (mat1.Column3.Z * mat2.Column3.Y) + (mat1.Column3.V * mat2.Column4.Y);
            result.Column3.Z = (mat1.Column3.X * mat2.Column1.Z) + (mat1.Column3.Y * mat2.Column2.Z)
                + (mat1.Column3.Z * mat2.Column3.Z) + (mat1.Column3.V * mat2.Column4.Z);
            result.Column3.V = (mat1.Column3.X * mat2.Column1.V) + (mat1.Column3.Y * mat2.Column2.V)
                + (mat1.Column3.Z * mat2.Column3.V) + (mat1.Column3.V * mat2.Column4.V);

            result.Column4.X = (mat1.Column4.X * mat2.Column1.X) + (mat1.Column4.Y * mat2.Column2.X)
                + (mat1.Column4.Z * mat2.Column3.X) + (mat1.Column4.V * mat2.Column4.X);
            result.Column4.Y = (mat1.Column4.X * mat2.Column1.Y) + (mat1.Column4.Y * mat2.Column2.Y)
                + (mat1.Column4.Z * mat2.Column3.Y) + (mat1.Column4.V * mat2.Column4.Y);
            result.Column4.Z = (mat1.Column4.X * mat2.Column1.Z) + (mat1.Column4.Y * mat2.Column2.Z)
                + (mat1.Column4.Z * mat2.Column3.Z) + (mat1.Column4.V * mat2.Column4.Z);
            result.Column4.V = (mat1.Column4.X * mat2.Column1.V) + (mat1.Column4.Y * mat2.Column2.V)
                + (mat1.Column4.Z * mat2.Column3.V) + (mat1.Column4.V * mat2.Column4.V);

            return result;
        }

        public override string ToString()
        {
            return $"{Column1} {Column2} {Column3} {Column4}";
        }
    }

    struct Vectror4
    {
        public float X;
        public float Y;
        public float Z;
        public float V; // vector
        public Vectror4(float x, float y, float z, float v) : this()
        {
            X = x;
            Y = y;
            Z = z;
            V = v;
        }

        public override string ToString()
        {
            return "{" + $"{X}, {Y}, {Z}, {V}" + "}";
        }
    }
}
