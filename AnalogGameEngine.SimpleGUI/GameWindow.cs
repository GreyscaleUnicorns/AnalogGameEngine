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
        VertexDataObject card, cardBack, table;

        Shader shader;
        Texture texture0, cardbackTexture, tableTexture;

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
            ConfigureGL();
            shader = InitializeShader();
            this.InitializeTextures(shader);
            this.InitializeVertexData();

            base.OnLoad(e);
        }

        protected static void ConfigureGL()
        {
            GL.ClearColor(0.2f, 0.2f, 0.2f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
        }

        protected static Shader InitializeShader()
        {
            // TODO: Dirty workaround to make shaders work for now
            var shader = new Shader("AnalogGameEngine.SimpleGUI/shader.vert", "AnalogGameEngine.SimpleGUI/shader.frag");
            shader.Use();
            shader.SetInt("texture0", 0);
            return shader;
        }

        // TODO: make more functional and add static
        protected void InitializeTextures(Shader shader)
        {
            // TODO: Dirty workaround to make textures work for now
            texture0 = new Texture("AnalogGameEngine.SimpleGUI/Assets/Textures/awesomeface.png");
            cardbackTexture = new Texture("AnalogGameEngine.SimpleGUI/Assets/Textures/cardback.png");
            tableTexture = new Texture("AnalogGameEngine.SimpleGUI/Assets/Textures/Wood_006_COLOR.jpg");
        }

        // TODO: make more functional and add static
        protected void InitializeVertexData()
        {
            card = new VertexDataObject(new[] {
                // positions          // tex coords
                 0.315f,  0.44f, 0f,  1f, 1f, // top right
                -0.315f,  0.44f, 0f,  0f, 1f, // top left
                 0.315f, -0.44f, 0f,  1f, 0f, // bottom right
                -0.315f, -0.44f, 0f,  0f, 0f, // bottom left
            }, new[] { 3, 2 }, PrimitiveType.TriangleStrip, texture0);

            cardBack = new VertexDataObject(new[] {
                // positions          // tex coords
                -0.315f,  0.44f, 0f,  1f, 1f, // top right
                 0.315f,  0.44f, 0f,  0f, 1f, // top left
                -0.315f, -0.44f, 0f,  1f, 0f, // bottom right
                 0.315f, -0.44f, 0f,  0f, 0f, // bottom left
            }, new[] { 3, 2 }, PrimitiveType.TriangleStrip, cardbackTexture);

            table = new VertexDataObject(new[] {
                // positions           // tex coords
                 3f, 0f, -3f,  1f, 1f, // top right
                -3f, 0f, -3f,  0f, 1f, // top left
                -3f, 0f,  3f,  0f, 0f, // bottom left
                 3f, 0f,  3f,  1f, 0f, // bottom right
            }, new[] { 3, 2 }, PrimitiveType.TriangleFan, tableTexture);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            time += (float)e.Time;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            shader.Use();

            // TODO: remove test code
            float radius = 7.0f;
            float camX = (float)Math.Sin(time / 2) * radius;
            float camZ = (float)Math.Cos(time / 2) * radius;

            Matrix4 view = Matrix4.LookAt(new Vector3(camX, 2.5f, camZ), new Vector3(0f, 0f, 0f), new Vector3(0f, 1f, 0f));
            shader.SetMatrix4("view", view);

            Matrix4 projection = Matrix4.Identity;
            projection *= Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), this.ClientRectangle.Width / this.ClientRectangle.Height, 0.1f, 100f);
            shader.SetMatrix4("projection", projection);

            /* Draw table */
            Matrix4 model = Matrix4.Identity;
            shader.SetMatrix4("model", model);

            table.Draw();

            /* Draw handcards */
            // Work for now only with one private Set
            float spacingX = 0.1f;
            float spacingY = 0.02f;
            float spacingZ = 0.001f;

            Matrix4 handBase = Matrix4.Identity;
            handBase *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-20));
            handBase *= Matrix4.CreateTranslation(0f, 1.5f, 3f);

            int playerAmount = game.Players.Count;
            for (int i = 0; i < playerAmount; i++)
            {
                Matrix4 hand = handBase;
                hand *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(i * (360 / playerAmount)));

                int cardAmount = game.Players[i].Sets.First().Value.Cards.Count;
                for (int j = 0; j < cardAmount; j++)
                {
                    float spacingIndex = (j - cardAmount / 2f);

                    model = Matrix4.Identity;
                    model *= Matrix4.CreateTranslation(spacingIndex * spacingX, spacingIndex * spacingY, spacingIndex * spacingZ);
                    model *= hand;
                    shader.SetMatrix4("model", model);

                    card.Draw();
                    cardBack.Draw();
                }
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
            card.Unload();
            table.Unload();

            shader.Dispose();
            base.OnUnload(e);
        }
    }
}
