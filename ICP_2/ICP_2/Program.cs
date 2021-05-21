using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.IO;
using SFML.System;

namespace ICP_2
{
    class Program
    {
        static RenderWindow window;
        static int program;
        static void Main(string[] args)
        {

            var gWin = new GameWindow();
            window = new RenderWindow(new VideoMode(800, 600), "ICP_2_2D 2D Object & Shader, Barva");

            window.Closed += (obj, e) => {
                window.Close();
            };
            window.Resized += (obj, e) => {
                window.SetView(new View(new FloatRect(0, 0, e.Width, e.Height)));
                GL.Viewport(0, 0, (int)e.Width, (int)e.Height);
            };

            window.SetActive(true);


            GL.Viewport(0, 0, (int)window.Size.X, (int)window.Size.Y);

            program = LoadShaders();

            VertexBuffer vertex = new VertexBuffer();

            vertex.AddVertex(new Vector3f(0, 1, 0), new Vector3f(1/3, 1, 1), new Vector2f());
            vertex.AddVertex(new Vector3f(-1, 0, 0), new Vector3f(1, 1/3, 1), new Vector2f());
            vertex.AddVertex(new Vector3f(1, 0, 0), new Vector3f(1, 1, 1/3), new Vector2f());

            int VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, vertex.GetMemory(), vertex.GetData(), BufferUsageHint.StaticDraw);

            while (window.IsOpen)
            {
                window.DispatchEvents();

                GL.UseProgram(program);

                GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);

                GL.EnableVertexAttribArray(0);
                GL.EnableVertexAttribArray(1);
                GL.EnableVertexAttribArray(2);

                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vertex.GetMemory(), 0);
                GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, Vertex.GetMemory(), sizeof(float) * 3);
                GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, Vertex.GetMemory(), sizeof(float) * 6);


                // 000(Pos) 000(Col) 00(Tex)
                // 000(Pos) 000(Col) 00(Tex)
                // 000(Pos) 000(Col) 00(Tex)

                GL.DrawArrays(OpenTK.Graphics.OpenGL.PrimitiveType.Triangles, 0, 3);

                GL.DisableVertexAttribArray(0);
                GL.DisableVertexAttribArray(1);
                GL.DisableVertexAttribArray(2);

                GL.UseProgram(0); // zavzit program
                window.Display();
            }
        }
        static int LoadShaders()
        {
            int vert = GL.CreateShader(ShaderType.VertexShader);
            int frag = GL.CreateShader(ShaderType.FragmentShader);

            //Vertex
            string source = File.ReadAllText("Vertex.glsl");
            GL.ShaderSource(vert, source);
            GL.CompileShader(vert); /// compilace shaderu

            int status;
            GL.GetShader(vert, ShaderParameter.CompileStatus, out status);
            if(status == 0){
                string infoErr;
                GL.GetShaderInfoLog(vert, out infoErr); // informace o chybe
                Console.WriteLine("Vertex Shader Error: {0}", infoErr);
            }

            //Fragment
            source = File.ReadAllText("Fragment.glsl");
            GL.ShaderSource(frag, source);
            GL.CompileShader(frag); /// compilace shaderu

            GL.GetShader(frag, ShaderParameter.CompileStatus, out status);
            if (status == 0)
            {
                string infoErr;
                GL.GetShaderInfoLog(frag, out infoErr); // informace o chybe
                Console.WriteLine("Vertex Shader Error: {0}", infoErr);
            }

            //program
            int program = GL.CreateProgram();
            GL.AttachShader(program, vert); // pripoujeme shader
            GL.AttachShader(program, frag);

            GL.LinkProgram(program);

            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out status);
            
            if(status == 0)
            {
                string infoErr;
                GL.GetProgramInfoLog(program, out infoErr);
                Console.WriteLine("Program Error: {0}", infoErr);
            }

            GL.UseProgram(0);
            GL.DeleteShader(vert); // vyprazdnime pamet
            GL.DeleteShader(frag);

            return program;
        }
    }
}
