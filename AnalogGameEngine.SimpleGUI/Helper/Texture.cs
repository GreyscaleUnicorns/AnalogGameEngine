using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace AnalogGameEngine.SimpleGUI.Helper {
    // A helper class, much like Shader, meant to simplify loading textures.
    public class Texture : IDisposable {
        int Handle;

        // Create texture from path.
        public Texture(string path) {
            // Generate handle
            Handle = GL.GenTexture();

            // Bind the handle
            GL.BindTexture(TextureTarget.Texture2D, Handle);

            // Load the image
            var image = Image.Load<Rgba32>(path);

            // ImageSharp loads from the top-left pixel, whereas OpenGL loads from the bottom-left, causing the texture to be flipped vertically.
            // This will correct that, making the texture display properly.
            image.Mutate(x => x.Flip(FlipMode.Vertical));

            // Convert ImageSharp's format into a byte array, so we can use it with OpenGL.
            byte[] pixels = new byte[0];

            // Get an array of the pixels, in ImageSharp's internal format.
            if (image.TryGetSinglePixelSpan(out var pixelSpan)) {
                pixels = MemoryMarshal.AsBytes(pixelSpan).ToArray();
            }

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapNearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        public void Use(TextureUnit unit = TextureUnit.Texture0) {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: dispose managed state (managed objects).
                }

                GL.DeleteProgram(Handle);

                disposedValue = true;
            }
        }

        ~Texture() {
            GL.DeleteProgram(Handle);
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
