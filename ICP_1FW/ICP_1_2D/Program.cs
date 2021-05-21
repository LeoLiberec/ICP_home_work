using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace ICP_1
{
    class Program
    {
        static RenderWindow window;
        static void Main(string[] args)
        {

            var gWin = new GameWindow();
            window = new RenderWindow(new VideoMode(800, 600), "ICP_1_2D 2D Object");

            window.Closed += (obj, e) => {
                window.Close();
            };
            window.Resized += (obj, e) => {
                window.SetView(new View(new FloatRect(0, 0, e.Width, e.Height)));
                GL.Viewport(0, 0, (int)e.Width, (int)e.Height);
            };

            window.SetActive(true);


            GL.Viewport(0, 0, (int)window.Size.X, (int)window.Size.Y);

            float[] vertices =
            {
                0,-1,0,
                1,0,0,
                -1,0,0,
                0,1,0,
                1,0,0,
                -1,0,0
            };

            int VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * vertices.Length, vertices, BufferUsageHint.StaticDraw);

            while (window.IsOpen)
            {
                window.DispatchEvents();

                GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
                GL.DrawArrays(OpenTK.Graphics.OpenGL.PrimitiveType.Triangles, 0, 6);
                GL.DisableVertexAttribArray(0);

                window.Display();
            }
        }
    }
}
