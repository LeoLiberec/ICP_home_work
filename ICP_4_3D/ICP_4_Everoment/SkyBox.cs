using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using SFML.System;

namespace ICP_4_Everoment
{
    class SkyBox
    {
        private const string VertexPath = "SkyVertex.glsl";
        private const string FragmentPath = "SkyFragment.glsl";
        private const string MatrixLocation = "WorldMat";

        private VertexBuffer buffer;
        private int program;
        private int vbo; // tady bude ulozena kryhle
        private int location; // location WorldMat matice
        private Transform3D transform;
        private TextureCube cube;
        public SkyBox(TextureCube cube)
        {
            buffer = new VertexBuffer();

            buffer.AddVertex(new Vector3f(-1, 1, -1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(-1, -1, -1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(1, -1, -1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(1, -1, -1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(1, 1, -1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(-1, 1, -1), new Vector3f(), new Vector2f());

            buffer.AddVertex(new Vector3f(-1, -1, 1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(-1, -1, -1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(-1, 1, -1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(-1, 1, -1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(-1, 1, 1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(-1, -1, 1), new Vector3f(), new Vector2f());

            buffer.AddVertex(new Vector3f(1, -1, -1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(1, -1, 1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(1, 1, 1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(1, 1, 1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(1, 1, -1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(1, -1, -1), new Vector3f(), new Vector2f());

            buffer.AddVertex(new Vector3f(-1, -1, 1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(-1, 1, 1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(1, 1, 1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(1, 1, 1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(1, -1, 1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(-1, -1, 1), new Vector3f(), new Vector2f());

            buffer.AddVertex(new Vector3f(-1, 1, -1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(1, 1, -1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(1, 1, 1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(1, 1, 1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(-1, 1, 1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(-1, 1, -1), new Vector3f(), new Vector2f());

            buffer.AddVertex(new Vector3f(-1, -1, -1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(-1, -1, 1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(1, -1, -1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(1, -1, -1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(-1, -1, 1), new Vector3f(), new Vector2f());
            buffer.AddVertex(new Vector3f(1, -1, 1), new Vector3f(), new Vector2f());

            vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, buffer.GetMemory(), buffer.GetData(), 
                BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            program = Program.LoadShaders(VertexPath, FragmentPath);
            location = GL.GetUniformLocation(program, MatrixLocation);

            transform = new Transform3D();
            transform.scale = new Vector3f(20, 20, 20);

            this.cube = cube;
        }

        public void Render(Matrix4 space, Vector3f cameraPosition)
        {
            GL.DepthMask(false);

            transform.position = cameraPosition;
            Matrix4 global = space * transform.GetMatrix();

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);



            GL.UseProgram(program);
            GL.UniformMatrix4(location, 1, true, global.GetArray());
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vertex.GetMemory(), 0);
            GL.DrawArrays(PrimitiveType.Triangles, 0, buffer.Count);

            GL.DisableVertexAttribArray(0);
            TextureCube.Disable();
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.DepthMask(true);
        }
    }
}
