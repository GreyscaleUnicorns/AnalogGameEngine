using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Linq;

namespace AnalogGameEngine.SimpleGUI
{
    class RenderableObject
    {
        private float[] vertices;
        private int[] config;
        private PrimitiveType drawType;
        private int vertexBufferObject, vertexArrayObject;

        private int VertexAmount
        {
            get => vertices.Length / config.Sum();
        }

        public RenderableObject(float[] vertices, int[] config, PrimitiveType type)
        {
            if (vertices.Length % config.Sum() != 0)
            {
                throw new ArgumentException("Config and vertices array do not match!");
            }

            this.vertices = vertices;
            this.config = config;
            this.drawType = type;

            // Create VBO
            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            // Create VAO
            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexArrayObject);

            int configSum = config.Sum();
            int tmpSum = 0;
            for (int i = 0; i < config.Length; i++)
            {
                GL.VertexAttribPointer(i, config[i], VertexAttribPointerType.Float, false, configSum * sizeof(float), tmpSum * sizeof(float));
                GL.EnableVertexAttribArray(i);
                tmpSum += config[i];
            }
        }

        public void Draw()
        {
            GL.BindVertexArray(vertexArrayObject);
            GL.DrawArrays(drawType, 0, this.VertexAmount);
        }

        public void Unload()
        {
            GL.DeleteBuffer(vertexBufferObject);
            GL.DeleteVertexArray(vertexArrayObject);
        }
    }
}
