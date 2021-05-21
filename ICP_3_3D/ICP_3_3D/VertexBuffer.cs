using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace ICP_3_3D
{
    class VertexBuffer
    {
        List<Vertex> vertices;

        public VertexBuffer()
        {
            vertices = new List<Vertex>();
        }

        public void AddVertex(Vertex vertex)
        {
            vertices.Add(vertex);
        }

        public void AddVertex(Vector3f Pos, Vector3f Col, Vector2f Tex)
        {
            vertices.Add(new Vertex(Pos, Col, Tex));
        }

        public int GetMemory()
        {
            return Vertex.GetMemory() * vertices.Count;
        }

        public float[] GetData()
        {
            List<float> data = new List<float>();

            for (int i = 0; i < vertices.Count; i++)
            {
                data.AddRange(vertices[i].GetData());
            }
            return data.ToArray();
        }

    }
    class Vertex
    {
        public Vector3f Pos;
        public Vector3f Col;
        public Vector2f Tex;

        public Vertex(Vector3f Pos, Vector3f Col, Vector2f Tex)
        {
            this.Pos = Pos;
            this.Col = Col;
            this.Tex = Tex;
        }

        public float[] GetData()
        {
            float[] data = new float[8]
            {
                Pos.X, Pos.Y, Pos.Z,
                Col.X, Col.Y, Col.Z,
                Tex.X, Tex.Y
            };
            return data;
        }
        public static int GetMemory()
        {
            return sizeof(float) * 8; //byte * 8 // Vector3f,3f,2f - (3+3+2)
        }
    }
}
