using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace ICP_3_3D
{
    class Transform3D
    {
        public Vector3f position;
        public Vector3f scale;
        public Vector3f rotation;
        public Transform3D(Vector3f position, Vector3f scale, Vector3f rotation)
        {
            this.position = position;
            this.scale = scale;
            this.rotation = rotation;
        }

        public Transform3D()
        {
            position = new Vector3f(0, 0, 0);
            scale = new Vector3f(1, 1, 1); // nejaky rozmer pro object 
            rotation = new Vector3f(1, 1, 1);  
        }
        public Matrix4 GetMatrix()
        {
            Matrix4 Pos = new Matrix4(); // position
            Matrix4 Scl = new Matrix4(); // scaling
            Matrix4 Rot = new Matrix4(); // rotation

            Pos.Translate(position);
            Scl.Scale(scale);
            Rot.Rotation(rotation);

            return Pos * Rot * Scl; 
        }
    }
}
