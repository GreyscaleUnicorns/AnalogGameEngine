using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using System;
using System.Diagnostics;
using System.Linq;

using AnalogGameEngine.Entities;

using AnalogGameEngine.SimpleGUI.Helper;

namespace AnalogGameEngine.SimpleGUI
{
    public class GameWindow : OpenTK.GameWindow
    {
        private readonly Game game;

        /* 1 = 1dm in real world */
        float[] cardVertices = {
            // positions           // tex coords
             0.315f,  0.44f, 0f,  1f, 1f, // top right
             0.315f, -0.44f, 0f,  1f, 0f, // bottom right
            -0.315f,  0.44f, 0f,  0f, 1f, // top left
            -0.315f, -0.44f, 0f,  0f, 0f, // bottom left
        };

        float[] tableVertices = {
            // positions           // tex coords
             3f, 0, -3f,  1f, 1f, // top right
             3f, 0,  3f,  1f, 0f, // bottom right
            -3f, 0, -3f,  0f, 1f, // top left
            -3f, 0,  3f,  0f, 0f, // bottom left
        };

        int vboCard, vboTable;
        int vaoCard, vaoTable;
        Shader shader;
        Texture texture0;

        float time = 0.0f;
        float deltaTime = 0.0f;
        float lastFrame = 0.0f;

        public GameWindow(Game game, int width, int height, string title)
            : base(
                width, height, GraphicsMode.Default, title, GameWindowFlags.Default,
                DisplayDevice.Default, 4, 5, GraphicsContextFlags.Default
            )
        {
            this.game = game;
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.2f, 0.2f, 0.2f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            vboCard = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboCard);
            GL.BufferData(BufferTarget.ArrayBuffer, cardVertices.Length * sizeof(float), cardVertices, BufferUsageHint.StaticDraw);

            vboTable = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboTable);
            GL.BufferData(BufferTarget.ArrayBuffer, tableVertices.Length * sizeof(float), tableVertices, BufferUsageHint.StaticDraw);

            // TODO: Dirty workaround to make shaders work for now
            shader = new Shader("AnalogGameEngine.SimpleGUI/shader.vert", "AnalogGameEngine.SimpleGUI/shader.frag");
            shader.Use();

            // TODO: Dirty workaround to make textures work for now
            texture0 = new Texture("AnalogGameEngine.SimpleGUI/awesomeface.png");
            texture0.Use(TextureUnit.Texture0);

            shader.SetInt("texture0", 0);

            // Create VAO for card
            vaoCard = GL.GenVertexArray();
            GL.BindVertexArray(vaoCard);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vboCard);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            // Create VAO for table
            vaoTable = GL.GenVertexArray();
            GL.BindVertexArray(vaoTable);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vboTable);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            base.OnLoad(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            time += (float)e.Time;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            texture0.Use(TextureUnit.Texture0);
            shader.Use();

            // TODO: remove test code
            float radius = 7.0f;
            float camX = (float)Math.Sin(time) * radius;
            float camZ = (float)Math.Cos(time) * radius;

            Matrix4 view = Matrix4.LookAt(new Vector3(camX, 2.5f, camZ), new Vector3(0f, 0f, 0f), new Vector3(0f, 1f, 0f));
            shader.SetMatrix4("view", view);

            Matrix4 projection = Matrix4.Identity;
            projection *= Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), this.ClientRectangle.Width / this.ClientRectangle.Height, 0.1f, 100f);
            shader.SetMatrix4("projection", projection);

            /* Draw table */
            Matrix4 model = Matrix4.Identity;
            shader.SetMatrix4("model", model);

            GL.BindVertexArray(vaoTable);
            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, tableVertices.Length);

            /* Draw handcards */
            // Work for now only with one private Set
            var (id, handSet) = game.ActivePlayer.Sets.First();

            float spacingX = 0.1f;
            float spacingY = 0.02f;
            float spacingZ = 0.001f;
            Matrix4 hand = Matrix4.Identity;
            hand *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-20));
            hand *= Matrix4.CreateTranslation(0f, 1.5f, 3f);

            int cardAmount = handSet.Cards.Count;
            for (int i = 0; i < cardAmount; i++)
            {
                float spacingIndex = (i - cardAmount / 2f);

                model = Matrix4.Identity;
                model *= Matrix4.CreateTranslation(spacingIndex * spacingX, spacingIndex * spacingY, spacingIndex * spacingZ);
                model *= hand;
                shader.SetMatrix4("model", model);

                GL.BindVertexArray(vaoCard);
                GL.DrawArrays(PrimitiveType.TriangleStrip, 0, cardVertices.Length);
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
            GL.DeleteBuffer(vboCard);
            GL.DeleteBuffer(vboTable);
            GL.DeleteVertexArray(vaoCard);
            GL.DeleteVertexArray(vaoTable);

            shader.Dispose();
            base.OnUnload(e);
        }
    }
}
