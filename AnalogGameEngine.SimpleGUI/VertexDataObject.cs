using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Linq;

using AnalogGameEngine.SimpleGUI.Helper;

namespace AnalogGameEngine.SimpleGUI {
    public class VertexDataObject {
        private PrimitiveType drawType;
        private Texture texture;
        private int vertexBufferObject, vertexArrayObject;
        private int? elementBufferObject;

        private int vertexAmount;

        /// <summary>
        /// Constructor to use for VertexDataObjectBuilder
        /// </summary>
        public VertexDataObject(PrimitiveType drawType, Texture texture, int vertexBufferObject, int vertexArrayObject, int? elementBufferObject, int vertexAmount) {
            this.drawType = drawType;
            this.texture = texture;
            this.vertexBufferObject = vertexBufferObject;
            this.vertexArrayObject = vertexArrayObject;
            this.elementBufferObject = elementBufferObject;
            this.vertexAmount = vertexAmount;
        }

        public void Draw(Texture texture = null) {
            if (!(texture is null)) { texture.Use(); }
            else if (!(this.texture is null)) { this.texture.Use(); }
            else { throw new InvalidOperationException("No texture given!"); }

            GL.BindVertexArray(vertexArrayObject);
            if (this.elementBufferObject.HasValue) {
                GL.DrawElements(this.drawType, this.vertexAmount, DrawElementsType.UnsignedInt, 0);
            }
            else {
                GL.DrawArrays(this.drawType, 0, this.vertexAmount);
            }
        }

        public void Unload() {
            GL.DeleteBuffer(vertexBufferObject);
            if (this.elementBufferObject.HasValue) {
                GL.DeleteBuffer(this.elementBufferObject.Value);
            }
            GL.DeleteVertexArray(vertexArrayObject);
        }
    }
}
