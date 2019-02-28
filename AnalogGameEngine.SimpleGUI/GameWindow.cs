using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

using AnalogGameEngine.Entities;

using AnalogGameEngine.SimpleGUI.Helper;
using AnalogGameEngine.SimpleGUI.Entities;

namespace AnalogGameEngine.SimpleGUI {
    public class GameWindow<T, U> : OpenTK.GameWindow where T : SimpleGuiCard<U> where U : SimpleGuiCardType {
        private readonly GameBase<T> game;

        /* 1 = 1dm in real world */
        private VertexDataObject card, cardBack, stack, table;

        private Shader shader;
        private Texture cardbackTexture, tableTexture;

        private float time = 0.0f;
        private float deltaTime = 0.0f;
        private float lastFrame = 0.0f;

        private bool keyNr1 = false;

        public GameWindow(GameBase<T> game, int width, int height, string title)
            : base(
                width,
                height,
                GraphicsMode.Default,
                title,
                GameWindowFlags.Default,
                DisplayDevice.Default,
                4, 5,
                GraphicsContextFlags.Default
            ) {
            this.game = game;
        }

        protected override void OnLoad(EventArgs e) {
            ConfigureGL();
            shader = InitializeShader();
            this.InitializeTextures(shader);
            this.InitializeVertexData();

            base.OnLoad(e);
        }

        protected static void ConfigureGL() {
            GL.ClearColor(0.2f, 0.2f, 0.2f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.Multisample);
        }

        protected static Shader InitializeShader() {
            // TODO: workaround to make shaders work for now
            var shader = new Shader("AnalogGameEngine.SimpleGUI/shader.vert", "AnalogGameEngine.SimpleGUI/shader.frag");
            shader.Use();
            shader.SetInt("texture0", 0);
            return shader;
        }

        // TODO: make more functional and add static
        protected void InitializeTextures(Shader shader) {
            // TODO: workaround to make textures work for now
            cardbackTexture = new Texture("Assets/Textures/Playingcards/CardBack_Red.jpg");
            tableTexture = new Texture("Assets/Textures/Wood_006_COLOR.jpg");
        }

        // TODO: make more functional and add static
        protected void InitializeVertexData() {
            table =
                new VertexDataObjectBuilder(
                    new[] {
                        // positions   // tex coords
                         3f, 0f, -3f,  1f, 1f, // top right
                        -3f, 0f, -3f,  0f, 1f, // top left
                        -3f, 0f,  3f,  0f, 0f, // bottom left
                         3f, 0f,  3f,  1f, 0f, // bottom right
                    },
                    new[] { 3, 2 },
                    PrimitiveType.TriangleFan
                )
                .WithTexture(tableTexture)
                .Build();

            cardBack =
                new VertexDataObjectBuilder(
                    new[] {
                        // positions          // tex coords
                        -0.315f,  0.44f, 0f,  1f, 1f, // top right
                         0.315f,  0.44f, 0f,  0f, 1f, // top left
                        -0.315f, -0.44f, 0f,  1f, 0f, // bottom right
                         0.315f, -0.44f, 0f,  0f, 0f, // bottom left
                    },
                    new[] { 3, 2 },
                    PrimitiveType.TriangleStrip
                )
                .WithTexture(cardbackTexture)
                .Build();

            stack =
                new VertexDataObjectBuilder(
                    new[] {
                        // positions              // tex coords
                         0.315f, 0.003f,  0.44f,  1f, 0f, // front bottom right
                        -0.315f, 0.003f,  0.44f,  0f, 0f, // front bottom left
                         0.315f, 0.003f, -0.44f,  1f, 1f, // front top right
                        -0.315f, 0.003f, -0.44f,  0f, 1f, // front top left
                         0.315f, 0f,      0.44f,  1f, 0f, // back bottom right
                        -0.315f, 0f,      0.44f,  0f, 0f, // back bottom left
                         0.315f, 0f,     -0.44f,  1f, 1f, // back top right
                        -0.315f, 0f,     -0.44f,  0f, 1f, // back top left
                    },
                    new[] { 3, 2 },
                    PrimitiveType.TriangleStrip
                )
                .WithTexture(cardbackTexture)
                .WithElementBufferObject(
                    new uint[] { 3, 1, 2, 0, 4, 1, 5, 3, 7, 2, 6, 4 }
                )
                .Build();

            card =
                new VertexDataObjectBuilder(
                    new[] {
                        // positions          // tex coords
                         0.315f,  0.44f, 0f,  1f, 1f, // top right
                        -0.315f,  0.44f, 0f,  0f, 1f, // top left
                         0.315f, -0.44f, 0f,  1f, 0f, // bottom right
                        -0.315f, -0.44f, 0f,  0f, 0f, // bottom left
                    },
                    new[] { 3, 2 },
                    PrimitiveType.TriangleStrip
                )
                .WithTexture(cardbackTexture)
                .Build();
        }

        protected override void OnRenderFrame(FrameEventArgs e) {
            time += (float)e.Time;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            shader.Use();

            // TODO: remove test code
            // TODO: move matrices to own camera class
            float radius = 7.0f;
            float camX = (float)Math.Sin(time / 2) * radius;
            float camZ = (float)Math.Cos(time / 2) * radius;

            Matrix4 view = Matrix4.LookAt(new Vector3(camX, 2.5f, camZ), new Vector3(0f, 0f, 0f), new Vector3(0f, 1f, 0f));
            shader.SetMatrix4("view", view);

            Matrix4 projection = Matrix4.Identity;
            projection *= Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), this.ClientRectangle.Width / this.ClientRectangle.Height, 0.1f, 100f);
            shader.SetMatrix4("projection", projection);

            this.Draw();

            Context.SwapBuffers();

            base.OnRenderFrame(e);
        }

        protected void Draw() {
            Matrix4 model;

            // Draw table
            model = Matrix4.Identity;
            shader.SetMatrix4("model", model);

            this.table.Draw();

            // Draw game stacks
            int stackAmount = game.Stacks.Count;
            for (int i = 0; i < stackAmount; i++) {
                var stack = game.Stacks.ElementAt(i).Value;
                if (stack.Count > 0) {
                    model = Matrix4.Identity;
                    model *= Matrix4.CreateScale(1f, stack.Count, 1f);
                    model *= Matrix4.CreateTranslation(0f, 0f, 0.8f);
                    model *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(i * (360f / stackAmount)));
                    shader.SetMatrix4("model", model);

                    this.stack.Draw();
                }
            }

            // Draw handcards
            float spacingX = 0.1f;
            float spacingY = 0.02f;
            float spacingZ = 0.001f;

            Matrix4 handBase = Matrix4.Identity;
            handBase *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-20));
            handBase *= Matrix4.CreateTranslation(0f, 1.5f, 3f);

            int playerAmount = game.Players.Count;
            for (int i = 0; i < playerAmount; i++) {
                Matrix4 hand = handBase;
                hand *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(i * (360 / playerAmount)));

                // TODO: Add support for more private sets
                Set<T> handSet = game.Players[i].Sets.First().Value;
                int cardAmount = handSet.Cards.Count;
                for (int j = 0; j < cardAmount; j++) {
                    T card = handSet.Cards.ElementAt(j);
                    float spacingIndex = (j - cardAmount / 2f);

                    model = Matrix4.Identity;
                    model *= Matrix4.CreateTranslation(spacingIndex * spacingX, spacingIndex * spacingY, spacingIndex * spacingZ);
                    model *= hand;
                    shader.SetMatrix4("model", model);

                    this.card.Draw(card.Type.Texture);
                    this.cardBack.Draw();
                }
            }
        }

        protected override void OnUpdateFrame(FrameEventArgs e) {
            MouseState mouse = Mouse.GetState();
            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape)) {
                Exit();
            }
            if (!keyNr1 && input.IsKeyDown(Key.Number1)) {
                if (GL.IsEnabled(EnableCap.CullFace)) {
                    GL.Disable(EnableCap.CullFace);
                }
                else {
                    GL.Enable(EnableCap.CullFace);
                }
                keyNr1 = true;
            }
            if (keyNr1 && input.IsKeyUp(Key.Number1)) { keyNr1 = false; }

            base.OnUpdateFrame(e);
        }

        protected override void OnResize(EventArgs e) {
            GL.Viewport(0, 0, Width, Height);
            base.OnResize(e);
        }

        // Now, for cleanup. This isn't technically necessary since C# will clean up all resources automatically when the program closes, but it's very
        // important to know how anyway.
        protected override void OnUnload(EventArgs e) {
            // Unbind all the resources by binding the targets to 0/null.
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            // Delete all the resources.
            card.Unload();
            cardBack.Unload();
            table.Unload();

            shader.Dispose();
            base.OnUnload(e);
        }
    }
}
