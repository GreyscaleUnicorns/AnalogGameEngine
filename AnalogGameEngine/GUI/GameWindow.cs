using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using System;
using System.Diagnostics;
using System.Linq;

namespace AnalogGameEngine.GUI
{
    public class GameWindow : OpenTK.GameWindow
    {
        float[] vertices = {
            /// positions        //colors            // texture coords
             0.5f,  0.5f,  0.5f,  1.0f, 0.0f, 0.0f,  1.0f, 1.0f, // top right front
             0.5f, -0.5f,  0.5f,  0.0f, 1.0f, 0.0f,  1.0f, 0.0f, // bottom right front
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f, 1.0f,  0.0f, 0.0f, // bottom left front
            -0.5f,  0.5f,  0.5f,  1.0f, 1.0f, 0.0f,  0.0f, 1.0f, // top left front
             0.5f,  0.5f, -0.5f,  1.0f, 0.0f, 0.0f,  0.0f, 0.0f, // top right back
             0.5f, -0.5f, -0.5f,  0.0f, 1.0f, 0.0f,  0.0f, 1.0f, // bottom right back
            -0.5f, -0.5f, -0.5f,  0.0f, 0.0f, 1.0f,  1.0f, 1.0f, // bottom left back
            -0.5f,  0.5f, -0.5f,  1.0f, 1.0f, 0.0f,  1.0f, 0.0f  // top left back
        };
        uint[] indices = {  // note that we start from 0!
            0, 1, 3, // front 1
            1, 2, 3, // front 2
            4, 5, 7, // back 1
            5, 6, 7, // back 2
            0, 3, 4, // top 1
            3, 4, 7, // top 2
            2, 3, 6, // left 1
            3, 6, 7, // left 2
            1, 2, 5, // bottom 1
            2, 5, 6, // bottom 2
            0, 1, 4, // right 1
            1, 4, 5, // right 2
        };

        Vector3[] cubePositions = {
            new Vector3( 0.0f,  0.0f,   0.0f),
            new Vector3( 2.0f,  5.0f, -15.0f),
            new Vector3(-1.5f, -2.2f,  -2.5f),
            new Vector3(-3.8f, -2.0f, -12.3f),
            new Vector3( 2.4f, -0.4f,  -3.5f),
            new Vector3(-1.7f,  3.0f,  -7.5f),
            new Vector3( 1.3f, -2.0f,  -2.5f),
            new Vector3( 1.5f,  2.0f,  -2.5f),
            new Vector3( 1.5f,  0.2f,  -1.5f),
            new Vector3(-1.3f,  1.0f,  -1.5f)
        };

        int VertexBufferObject;
        int VertexArrayObject;
        int ElementBufferObject;
        Shader shader;
        Texture texture0, texture1;
        float time = 0.0f;
        float deltaTime = 0.0f;
        float lastFrame = 0.0f;
        int lastX, lastY;

        float yaw = -90, pitch = 0;

        Vector3 cameraPos = new Vector3(0f, 0f, 3f);
        Vector3 cameraFront = new Vector3(0f, 0f, -1f);
        Vector3 cameraUp = new Vector3(0f, 1f, 0f);

        public GameWindow(int width, int height, string title)
            : base(
                width, height, GraphicsMode.Default, title, GameWindowFlags.Default,
                DisplayDevice.Default, 4, 5, GraphicsContextFlags.Default
            )
        {
            lastX = (int)Math.Round(width / 2d);
            lastY = (int)Math.Round(height / 2d);
            this.CursorVisible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.2f, 0.2f, 0.2f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            // TODO: Dirty workaround to make shaders work for now
            shader = new Shader("AnalogGameEngine/GUI/shader.vert", "AnalogGameEngine/GUI/shader.frag");
            shader.Use();

            // TODO: Dirty workaround to make textures work for now
            texture0 = new Texture("AnalogGameEngine/GUI/container.jpg");
            texture0.Use(TextureUnit.Texture0);

            // TODO: Dirty workaround to make textures work for now
            /*texture1 = new Texture("AnalogGameEngine/GUI/awesomeface.png");
            texture1.Use(TextureUnit.Texture1);*/

            shader.SetInt("texture0", 0);
            //shader.SetInt("texture1", 1);

            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
            GL.EnableVertexAttribArray(2);

            base.OnLoad(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            time += (float)e.Time;
            deltaTime = time - lastFrame;
            lastFrame = time;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            texture0.Use(TextureUnit.Texture0);
            //texture1.Use(TextureUnit.Texture1);
            shader.Use();

            Matrix4 view = Matrix4.LookAt(cameraPos, cameraPos + cameraFront, cameraUp);
            shader.SetMatrix4("view", view);

            Matrix4 projection = Matrix4.Identity;
            projection *= Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), this.ClientRectangle.Width / this.ClientRectangle.Height, 0.1f, 100f);
            shader.SetMatrix4("projection", projection);

            foreach (var (translation, index) in cubePositions.Select((v, i) => (v, i)))
            {
                Matrix4 model = Matrix4.Identity;
                var angle = 20f * index;
                if (index % 3 == 0)
                {
                    angle += time * 6 % 360f;
                }
                model *= Matrix4.CreateFromAxisAngle(new Vector3(1f, 0.3f, 0.5f), MathHelper.DegreesToRadians(angle));
                model *= Matrix4.CreateTranslation(translation);
                shader.SetMatrix4("model", model);

                GL.BindVertexArray(VertexArrayObject);
                GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            }

            Context.SwapBuffers();

            base.OnRenderFrame(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            MouseState mouse = Mouse.GetState();
            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }

            // Manipulate mouse only if window is focused
            if (this.Focused)
            {
                float offsetX = mouse.X - lastX;
                float offsetY = mouse.Y - lastY;

                lastX = mouse.X;
                lastY = mouse.Y;

                float sensitivity = 0.05f;
                offsetX *= sensitivity;
                offsetY *= sensitivity;

                yaw += offsetX;
                pitch += offsetY;

                if (pitch > 89) pitch = 89f;
                if (pitch < -89) pitch = -89f;
                yaw %= 360;
                if (yaw < 0) yaw += 360;

                Vector3 front = new Vector3();
                front.X = (float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Cos(MathHelper.DegreesToRadians(yaw));
                front.Y = (float)Math.Sin(MathHelper.DegreesToRadians(pitch));
                front.Z = (float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Sin(MathHelper.DegreesToRadians(yaw));
                front.Normalize();
                cameraFront = front;
            }

            float cameraSpeed = 2.5f * deltaTime;
            if (input.IsKeyDown(Key.W))
            {
                cameraPos += cameraSpeed * cameraFront;
            }
            if (input.IsKeyDown(Key.S))
            {
                cameraPos -= cameraSpeed * cameraFront;
            }
            if (input.IsKeyDown(Key.A))
            {
                cameraPos -= Vector3.Normalize(Vector3.Cross(cameraFront, cameraUp)) * cameraSpeed;
            }
            if (input.IsKeyDown(Key.D))
            {
                cameraPos += Vector3.Normalize(Vector3.Cross(cameraFront, cameraUp)) * cameraSpeed;
            }

            base.OnUpdateFrame(e);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            base.OnResize(e);
        }

        //Now, for cleanup. This isn't technically necessary since C# will clean up all resources automatically when the program closes, but it's very
        //important to know how anyway.
        protected override void OnUnload(EventArgs e)
        {
            // Unbind all the resources by binding the targets to 0/null.
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            // Delete all the resources.
            GL.DeleteBuffer(VertexBufferObject);
            GL.DeleteVertexArray(VertexArrayObject);

            shader.Dispose();
            base.OnUnload(e);
        }
    }
}
