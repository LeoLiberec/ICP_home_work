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

namespace ICP_4_Everoment
{
    class Program
    {
        static RenderWindow window;
        static int program;
        private static Perspective perspective;
        private static Camera camera;
        private static float speedMove = 0.005f;
        private static float speedRotate = 0.05f;
        //private static Vector2i lastMausePos;
        private static readonly ContextSettings settings = new ContextSettings()
        {
            DepthBits = 24,
            StencilBits = 8,
            MajorVersion = 3,
            MinorVersion = 3,
            AntialiasingLevel = 0
        };


        static void Main(string[] args)
        {
            var gWin = new GameWindow();
            window = new RenderWindow(new VideoMode(800, 600), "ICP_4_3D_Everoment Kamera, Shaders, Textures, Object", Styles.Default, settings);

            window.Closed += (obj, e) => {
                window.Close();
            };
            window.Resized += (obj, e) => {
                window.SetView(new View(new FloatRect(0, 0, e.Width, e.Height)));
                GL.Viewport(0, 0, (int)e.Width, (int)e.Height);
            };

            window.SetActive(true);
            //Mouse.SetPosition((Vector2i)window.Size / 2, window);
            GL.Viewport(0, 0, (int)window.Size.X, (int)window.Size.Y);

            GL.Enable(EnableCap.DepthTest); // test hlubky

            program = LoadShaders("Vertex.glsl", "Fragment.glsl");
            //LoadShaders("SkyVertex.glsl", "SkyFragment.glsl");
            VertexBuffer vertex = new VertexBuffer();

            //vertex.AddVertex(new Vector3f(0, 1, 0), new Vector3f(1 , 0, 0), new Vector2f());
            //vertex.AddVertex(new Vector3f(-1, 0, 0), new Vector3f(1, 0, 0), new Vector2f());
            //vertex.AddVertex(new Vector3f(1, 0, 0), new Vector3f(0, 0, 1), new Vector2f());

            vertex.AddVertex(new Vector3f(0, 1, 0), new Vector3f(1, 0, 0), new Vector2f(0.5f, 0));
            vertex.AddVertex(new Vector3f(-1, 0, 0), new Vector3f(1, 0, 0), new Vector2f(0, 1));
            vertex.AddVertex(new Vector3f(1, 0, 0), new Vector3f(0, 0, 1), new Vector2f(1, 1));


            //Matrix4 mat1 = new Matrix4(
            //    new Vectror4(2, 4, 2, 1),
            //        new Vectror4(3, 7, 1, 1),
            //            new Vectror4(3, 8, 1, 0),
            //                new Vectror4(2, 7, 0, 6));
            //Matrix4 mat2 = new Matrix4(
            //   new Vectror4(1, 2, 5, 1),
            //       new Vectror4(0, 5, 7, 1),
            //           new Vectror4(0, 3, 0, 3),
            //               new Vectror4(1, 0, 0, 0));
            //Console.WriteLine(mat1 * mat2);

            perspective = new Perspective(window.Size.X, window.Size.Y, 120, 1, 1000); // Angle - 60 stupnu

            camera = new Camera(new Vector3f(0, 0, -3), new Vector3f(0, 0, 1), new Vector3f(0, 1, 0));

            Transform3D transform3D = new Transform3D();
            transform3D.position = new Vector3f(0f, 0f, 10f);
            transform3D.rotation = new Vector3f(1, 1, 1); // nesmime porit 0 -> 0 * neco = 0
            transform3D.scale = new Vector3f(1f, 1f, 1f); // nesmime porit 0 -> 0 * neco = 0


            int VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, vertex.GetMemory(), vertex.GetData(), BufferUsageHint.StaticDraw);

            int index = GL.GetUniformLocation(program, "WorldMat"); // nazev matice v Vertex

            Texture2D texture = new Texture2D("D://ICP//ICP_CV//ICP_4_3D//ICP_4_Everoment//bin//Debug//img.jpg");

            TextureCube cube = new TextureCube();
            cube.LoadFromFile("D://ICP//ICP_CV//ICP_4_3D//ICP_4_Everoment//bin//Debug//Sky");
            SkyBox skyBox = new SkyBox(cube);


            //lastMausePos = Mouse.GetPosition(window);

            while (window.IsOpen)
            {
                window.DispatchEvents();

                MoveCamera();

                RotateCamera();

                camera.Update();

                GL.UseProgram(program);

                Matrix4 space = perspective.GetMatrix() * camera.GetMatrix();
                Matrix4 matrix = space * transform3D.GetMatrix();
                
                //Matrix4 matrix = perspective.GetMatrix() * camera.GetMatrix() * transform3D.GetMatrix(); // Canera 
                //Matrix4 matrix =  transform3D.GetMatrix(); // *** Jenom trasformace bez camery
                GL.UniformMatrix4(index, 1, true, matrix.GetArray()); //1 - jedna matice, true - data jdou jako retezec  

                GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);

                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                GL.EnableVertexAttribArray(0);
                GL.EnableVertexAttribArray(1);
                GL.EnableVertexAttribArray(2);

                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vertex.GetMemory(), 0);
                GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, Vertex.GetMemory(), sizeof(float) * 3);
                GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, Vertex.GetMemory(), sizeof(float) * 6);


                // 000(Pos) 000(Col) 00(Tex)
                // 000(Pos) 000(Col) 00(Tex)
                // 000(Pos) 000(Col) 00(Tex)

                texture.BindTexture();

                GL.DrawArrays(OpenTK.Graphics.OpenGL.PrimitiveType.Triangles, 0, 3);

                GL.DisableVertexAttribArray(0);
                GL.DisableVertexAttribArray(1);
                GL.DisableVertexAttribArray(2);

                GL.UseProgram(0); // zavzit program
                Texture2D.Unbind();

                skyBox.Render(space, camera.position);

                window.Display();
            }
        }
        public static int LoadShaders(string vertexPath, string fragmentPath)
        {
            int vert = GL.CreateShader(ShaderType.VertexShader);
            int frag = GL.CreateShader(ShaderType.FragmentShader);

            //Vertex
            string source = File.ReadAllText(vertexPath);
            GL.ShaderSource(vert, source);
            GL.CompileShader(vert); /// compilace shaderu

            int status;
            GL.GetShader(vert, ShaderParameter.CompileStatus, out status);
            if (status == 0)
            {
                string infoErr;
                GL.GetShaderInfoLog(vert, out infoErr); // informace o chybe
                Console.WriteLine("Vertex Shader Error: {0}", infoErr);
            }

            //Fragment
            source = File.ReadAllText(fragmentPath);
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

            if (status == 0)
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

        private static void MoveCamera()
        {
            bool IsLeft = Keyboard.IsKeyPressed(Keyboard.Key.Left);
            bool IsRight = Keyboard.IsKeyPressed(Keyboard.Key.Right);
            bool IsUp = Keyboard.IsKeyPressed(Keyboard.Key.Up);
            bool IsDown = Keyboard.IsKeyPressed(Keyboard.Key.Down);

            Vector3f Direction = new Vector3f();

            if (IsLeft)
            {
                Direction += Math3D.Cross(camera.target, camera.up);
            }
            if (IsRight)
            {
                Direction += Math3D.Cross(camera.up, camera.target);
            }
            if (IsUp)
            {
                Direction += camera.target;
            }
            if (IsDown)
            {
                Direction += -camera.target;
            }

            camera.position += Direction * speedMove;
        }

        private static void RotateCamera()
        {
            Vector2i MousePos = Mouse.GetPosition(window);

            //float DeltaX = MousePos.X - lastMausePos.X;
            //float DeltaY = MousePos.Y - lastMausePos.Y;
            //camera.angleH += DeltaY * speedRotate;
            //camera.angleV += DeltaX * speedRotate;

            Vector2f delta = (Vector2f)MousePos - (Vector2f)(window.Size / 2);

            camera.angleH += delta.Y * speedRotate;
            camera.angleV += delta.X * speedRotate;

            //lastMausePos = MousePos;

            if (window.HasFocus())
                Mouse.SetPosition((Vector2i)window.Size / 2, window);
        }
    }
}
