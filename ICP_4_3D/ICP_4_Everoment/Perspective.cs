using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP_4_Everoment
{
    class Perspective
    {
        private Matrix4 Projection;
        private float Ar; // AspectRation
        private float ZNear;
        private float ZFar;
        private float Angle;

        public Perspective(float Width, float Height, float Angle, float ZNear, float ZFar)
        {
            Ar = Width / Height;
            this.ZNear = ZNear;
            this.ZFar = ZFar;
            this.Angle = Angle;

            Update();
        }

        public Matrix4 GetMatrix()
        {
            return Projection;
        }

        public void SetAngle(float value)
        {
            Angle = value;
            Update();
        }

        public void SetLenghtZ(float ZNear, float ZFar)
        {
            this.ZNear = ZNear;
            this.ZFar = ZFar;
            Update();
        }

        public void SetSizeWindow(float Width, float Height)
        {
            Ar = Width / Height;
            Update();
        }
        private void Update()
        {
            float FOV = (float)Math.Tan(Math3D.ToRadian(Angle / 2)); // uhel zorniho pole
            float Lenght = ZNear - ZFar; // dilka vektora

            Projection = new Matrix4();

            Projection.Column1 = new Vectror4(1 / (Ar * FOV), 0, 0, 0);
            Projection.Column2 = new Vectror4(0, 1 / (FOV), 0, 0);
            Projection.Column3 = new Vectror4(0, 0, (-ZNear - ZFar) / Lenght, (2 * ZFar * ZNear) / Lenght);
            Projection.Column4 = new Vectror4(0, 0, 1, 0);

        }
    }
}
