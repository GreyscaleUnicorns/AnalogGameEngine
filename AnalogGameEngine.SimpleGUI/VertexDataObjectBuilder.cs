using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Linq;

using AnalogGameEngine.SimpleGUI.Helper;

namespace AnalogGameEngine.SimpleGUI {
    public class VertexDataObjectBuilder {
        private PrimitiveType drawType;
        private Texture texture;
        private int vertexBufferObject, vertexArrayObject;
        private int? elementBufferObject;

        private int vertexAmount;
        private int[] config;
        private int configSum;

        public VertexDataObjectBuilder(float[] vertices, int[] config, PrimitiveType type) {
            this.configSum = config.Sum();
            if (vertices.Length % configSum != 0) {
                throw new ArgumentException("Config and vertices array do not match!");
            }

            this.config = config;
            this.drawType = type;
            this.vertexAmount = vertices.Length / configSum;

            // Create VBO
            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
        }

        public VertexDataObjectBuilder WithElementBufferObject(uint[] indices) {
            this.vertexAmount = indices.Length;

            int ebo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
            this.elementBufferObject = ebo;

            return this;
        }

        public VertexDataObjectBuilder WithTexture(Texture texture) {
            this.texture = texture;

            return this;
        }

        public VertexDataObject Build() {
            // Create VAO
            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, this.vertexBufferObject);
            if (this.elementBufferObject.HasValue) {
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.elementBufferObject.Value);
            }

            int tmpSum = 0;
            for (int i = 0; i < this.config.Length; i++) {
                GL.VertexAttribPointer(i, this.config[i], VertexAttribPointerType.Float, false, this.configSum * sizeof(float), tmpSum * sizeof(float));
                GL.EnableVertexAttribArray(i);
                tmpSum += this.config[i];
            }
            GL.BindVertexArray(0);

            return new VertexDataObject(this.drawType, this.texture, this.vertexBufferObject, this.vertexArrayObject, this.elementBufferObject, this.vertexAmount);
        }
    }
}
