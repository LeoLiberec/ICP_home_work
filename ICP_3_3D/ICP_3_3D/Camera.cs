using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP_3_3D
{
    class Camera
    {
        public Vector3f position;
        public Vector3f target;
        public Vector3f up;

        public float angleH;
        public float angleV;
        public Camera(Vector3f position, Vector3f target, Vector3f up)
        {
            this.position = position;
            this.target = target;
            this.up = up;

            angleH = 0;
            angleV = 0;
        }

        public void Update()
        {
            Vector3f VAxis = new Vector3f(0, 1, 0);
            Vector3f View = new Vector3f(1, 0, 0);

            View = Quatrenion.Rotate(View, angleV, VAxis);
            View = View.GetNormalize(); // normalizujeme vector 

            Vector3f HAxis = Math3D.Cross(VAxis, View);
            View = Quatrenion.Rotate(View, angleH, HAxis); // otocime vectror View kolem osi

            target = View.GetNormalize();
            up = Math3D.Cross(target, HAxis).GetNormalize();
        }

        public Matrix4 GetMatrix()
        {
            Matrix4 Result = new Matrix4();
            
            Vector3f N = target.GetNormalize();
            Vector3f U = up.GetNormalize();

            U = Math3D.Cross(U, N);
            Vector3f V = Math3D.Cross(N, U);

            Matrix4 Rotate = new Matrix4();

            Rotate.Column1 = new Vectror4(U.X, U.Y, U.Z, 0);
            Rotate.Column2 = new Vectror4(V.X, V.Y, V.Z, 0);
            Rotate.Column3 = new Vectror4(N.X, N.Y, N.Z, 0);
            Rotate.Column4 = new Vectror4(0, 0, 0, 1);

            Matrix4 Position = new Matrix4();

            Position.Translate(-position);
             
            return Rotate * Position;
        }
    }
}
