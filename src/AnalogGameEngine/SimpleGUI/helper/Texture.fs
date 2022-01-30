namespace AnalogGameEngine.SimpleGUI.Helper

open System
open System.Runtime.InteropServices
open OpenTK.Graphics.OpenGL4

open SixLabors.ImageSharp
open SixLabors.ImageSharp.PixelFormats
open SixLabors.ImageSharp.Processing

// A helper class, much like Shader, meant to simplify loading textures.
type Texture(handle: int) =
    member _.Handle = handle

    // This section is dedicated to cleaning up the shader after it's finished.
    // Doing this solely in a finalizer results in a crash because of the Object-Oriented Language Problem
    // ( https://www.khronos.org/opengl/wiki/Common_Mistakes#The_Object_Oriented_Language_Problem )
    interface IDisposable with
        member this.Dispose() =
            GL.DeleteProgram(handle)
            GC.SuppressFinalize(this)

    override _.Finalize() = GL.DeleteProgram(handle)

[<RequireQualifiedAccess>]
module Texture =
    // Create texture from path
    let create (path: string) =
        // Generate handle
        let handle = GL.GenTexture()

        // Bind the handle
        GL.BindTexture(TextureTarget.Texture2D, handle)

        // Load the image
        let image = Image.Load<Rgba32> path

        // ImageSharp loads from the top-left pixel, whereas OpenGL loads from the bottom-left, causing the texture to be flipped vertically.
        // This will correct that, making the texture display properly.
        image.Mutate(fun x -> x.Flip(FlipMode.Vertical) |> ignore)

        // Convert ImageSharp's format into a byte array, so we can use it with OpenGL.
        let pixels =
            let mutable pixelSpan = Span<Rgba32>.Empty

            if image.TryGetSinglePixelSpan(&pixelSpan) then
                MemoryMarshal.AsBytes(pixelSpan).ToArray()
            else
                [||]

        GL.TexParameter(
            TextureTarget.Texture2D,
            TextureParameterName.TextureMinFilter,
            (int) TextureMinFilter.LinearMipmapNearest
        )

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Linear)
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) TextureWrapMode.ClampToEdge)
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) TextureWrapMode.ClampToEdge)

        GL.TexImage2D(
            TextureTarget.Texture2D,
            0,
            PixelInternalFormat.Rgba,
            image.Width,
            image.Height,
            0,
            PixelFormat.Rgba,
            PixelType.UnsignedByte,
            pixels
        )

        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D)
        new Texture(handle)

    let useIt (image: Texture) textureUnit =
        let textureUnit = Option.defaultValue TextureUnit.Texture0 textureUnit
        GL.ActiveTexture textureUnit
        GL.BindTexture(TextureTarget.Texture2D, image.Handle)
