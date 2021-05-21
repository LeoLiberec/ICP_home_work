using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using ICP_6_3D.Common;

namespace ICP_6_3D
{
    public class Win3D : GameWindow // Zdedime tridu 
    {
        private readonly float[] vertices =            // Vytvoreme cube - krychle ma 6 stran 
        {
            // Pos                Normals              Texture pos
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,

            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,

            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
            -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,

             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,

            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f
        };

        private readonly Vector3[] cubePositions =  // vytvorime nekolik krychle
        {
            new Vector3(0.0f, 0.5f, 0.0f),          // pozice krychle x,y,z
            new Vector3(2.0f, 2.0f, 1.5f),
            new Vector3(-2.0f, -1.5f, 2.0f),
            //new Vector3(0.0f, 0.0f, 0.0f),
            //new Vector3(0.0f, 0.0f, 0.0f),
        };

       
        private readonly Vector3[] pointLightPos =  // Pozice spot svetla, kolik mame tady tolik musime ukazat v lighting.frag
        {
            new Vector3(0.7f, 0.2f, 2.0f),
            new Vector3(2.3f, -1.3f, -2.0f),
            //new Vector3(0.0f, 0.0f, 0.0f)
        };

        private int vertexBuffObj;

        private int vaoModel;

        private int vaoLamp;

        private Shader lampShader;

        private Shader lightingShader;

        private Texture diffuseMap;

        private Texture specularMap;

        private Cam3D cam;

        private bool firstMove = true;

        private Vector2 lastPos;

        public Win3D(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            GL.ClearColor(0.25f, 0.35f, 0.65f, 1.0f); // BG color rgbA

            GL.Enable(EnableCap.DepthTest);           // Spravne zobrazit normaly 

            vertexBuffObj = GL.GenBuffer();           
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffObj);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            lightingShader = new Shader("Sha/shader.vert", "Sha/lighting.frag");
            lampShader = new Shader("Sha/shader.vert", "Sha/shader.frag");

            {
                vaoModel = GL.GenVertexArray();
                GL.BindVertexArray(vaoModel);

                var positionLocation = lightingShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionLocation);
                GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

                var normalLocation = lightingShader.GetAttribLocation("aNormal");
                GL.EnableVertexAttribArray(normalLocation);
                GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

                var texCoordLocation = lightingShader.GetAttribLocation("aTexCoords");
                GL.EnableVertexAttribArray(texCoordLocation);
                GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
            }

            {
                vaoLamp = GL.GenVertexArray();
                GL.BindVertexArray(vaoLamp);

                var positionLocation = lampShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionLocation);
                GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            }

            diffuseMap = Texture.LoadFromFile("Res/box.png");
            specularMap = Texture.LoadFromFile("Res/box_spec.png");

            cam = new Cam3D(Vector3.UnitZ * 3, Size.X / (float)Size.Y);

            CursorGrabbed = true;

            base.OnLoad();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.BindVertexArray(vaoModel);

            diffuseMap.Use(TextureUnit.Texture0);
            specularMap.Use(TextureUnit.Texture1);
            lightingShader.Use();

            lightingShader.SetMatrix4("view", cam.GetViewMatrix());
            lightingShader.SetMatrix4("projection", cam.GetProjectionMatrix());

            lightingShader.SetVector3("viewPos", cam.Position);

            lightingShader.SetInt("material.diffuse", 0);
            lightingShader.SetInt("material.specular", 1);
            lightingShader.SetVector3("material.specular", new Vector3(0.3f, 0.3f, 0.3f));
            lightingShader.SetFloat("material.shininess", 32.0f);

            // Smerove svetlo
            lightingShader.SetVector3("dirLight.direction", new Vector3(0.5f, 1.0f, 0.5f));
            lightingShader.SetVector3("dirLight.ambient", new Vector3(0.1f, 0.1f, 0.1f));
            lightingShader.SetVector3("dirLight.diffuse", new Vector3(0.75f, 0.75f, 0.75f));
            lightingShader.SetVector3("dirLight.specular", new Vector3(0.3f, 0.3f, 0.3f));

            // bodove svetlo
            for (int i = 0; i < pointLightPos.Length; i++)
            {
                lightingShader.SetVector3($"pointLights[{i}].position", pointLightPos[i]);
                lightingShader.SetVector3($"pointLights[{i}].ambient", new Vector3(0.05f, 0.05f, 0.05f));  // "Barva" ambient 
                lightingShader.SetVector3($"pointLights[{i}].diffuse", new Vector3(0.7f, 0.8f, 0.9f));     // "Barva" diffuse 
                lightingShader.SetVector3($"pointLights[{i}].specular", new Vector3(1.0f, 1.0f, 1.0f));    // "Barva" specular
                lightingShader.SetFloat($"pointLights[{i}].constant", 0.85f);                               // Sila svetla 0 max
                lightingShader.SetFloat($"pointLights[{i}].linear", 0.009f);                                // dist utlumu
                lightingShader.SetFloat($"pointLights[{i}].quadratic", 0.032f);
            }
            // Lampa
            lightingShader.SetVector3("spotLight.position", cam.Position);
            lightingShader.SetVector3("spotLight.direction", cam.Front);
            lightingShader.SetVector3("spotLight.ambient", new Vector3(0.1f, 0.1f, 0.0f));    // "Barva" ambient  
            lightingShader.SetVector3("spotLight.diffuse", new Vector3(1.0f, 1.0f, 0.5f));    // "Barva" diffuse
            lightingShader.SetVector3("spotLight.specular", new Vector3(1.0f, 1.0f, 1.0f));   // "Barva" specular
            lightingShader.SetFloat("spotLight.constant", 0.1f);                              // Sila svetla
            lightingShader.SetFloat("spotLight.linear", 0.02f);                               // dist utlumu
            lightingShader.SetFloat("spotLight.quadratic", 0.064f);                           // Cvadraticky utlum
            lightingShader.SetFloat("spotLight.cutOff", MathF.Cos(MathHelper.DegreesToRadians(7.0f)));        // Radius lampy
            lightingShader.SetFloat("spotLight.outerCutOff", MathF.Cos(MathHelper.DegreesToRadians(10.0f)));  // Utlum lampy

            for (int i = 0; i < cubePositions.Length; i++)
            {
                Matrix4 model = Matrix4.CreateTranslation(cubePositions[i]);
                float angle = 20.0f * i;
                model = model * Matrix4.CreateFromAxisAngle(new Vector3(1.0f, 0.3f, 0.5f), angle);
                lightingShader.SetMatrix4("model", model);

                GL.DrawArrays(PrimitiveType.Triangles, 0, 36);  // 36 - 6 na kazdu stranu cube
            }

            GL.BindVertexArray(vaoModel);

            lampShader.Use();

            lampShader.SetMatrix4("view", cam.GetViewMatrix());
            lampShader.SetMatrix4("projection", cam.GetProjectionMatrix());

            for (int i = 0; i < pointLightPos.Length; i++)
            {
                Matrix4 lampMatrix = Matrix4.CreateScale(0.2f);
                lampMatrix = lampMatrix * Matrix4.CreateTranslation(pointLightPos[i]);

                lampShader.SetMatrix4("model", lampMatrix);

                GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
            }

            SwapBuffers();

            base.OnRenderFrame(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (!IsFocused)
            {
                return;
            }

            var input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            const float cameraSpeed = 2.0f;
            const float sensitivity = 0.2f;

            if (input.IsKeyDown(Keys.W))
            {
                cam.Position += cam.Front * cameraSpeed * (float)e.Time; 
            }
            if (input.IsKeyDown(Keys.S))
            {
                cam.Position -= cam.Front * cameraSpeed * (float)e.Time; 
            }
            if (input.IsKeyDown(Keys.A))
            {
                cam.Position -= cam.Right * cameraSpeed * (float)e.Time; 
            }
            if (input.IsKeyDown(Keys.D))
            {
                cam.Position += cam.Right * cameraSpeed * (float)e.Time; 
            }
            if (input.IsKeyDown(Keys.Space))
            {
                cam.Position += cam.Up * cameraSpeed * (float)e.Time; 
            }
            if (input.IsKeyDown(Keys.LeftShift))
            {
                cam.Position -= cam.Up * cameraSpeed * (float)e.Time; 
            }

            var mouse = MouseState;

            if (firstMove)
            {
                lastPos = new Vector2(mouse.X, mouse.Y);
                firstMove = false;
            }
            else
            {
                var deltaX = mouse.X - lastPos.X;
                var deltaY = mouse.Y - lastPos.Y;
                lastPos = new Vector2(mouse.X, mouse.Y);

                cam.Yaw += deltaX * sensitivity;
                cam.Pitch -= deltaY * sensitivity;
            }

            base.OnUpdateFrame(e);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            cam.Fov -= e.OffsetY;
            base.OnMouseWheel(e);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, Size.X, Size.Y);
            cam.AspectRatio = Size.X / (float)Size.Y;
            base.OnResize(e);
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            GL.DeleteBuffer(vertexBuffObj);
            GL.DeleteVertexArray(vaoModel);
            GL.DeleteVertexArray(vaoLamp);

            GL.DeleteProgram(lampShader.Handle);
            GL.DeleteProgram(lightingShader.Handle);

            base.OnUnload();
        }
    }
}