namespace AnalogGameEngine.SimpleGUI.Helper

open System
open System.IO
open System.Text

open OpenTK.Graphics.OpenGL4

/// A simple class meant to help create shaders
type Shader(handle: int) =
    member _.Handle = handle

    // This section is dedicated to cleaning up the shader after it's finished.
    // Doing this solely in a finalizer results in a crash because of the Object-Oriented Language Problem
    // ( https://www.khronos.org/opengl/wiki/Common_Mistakes#The_Object_Oriented_Language_Problem )
    interface IDisposable with
        member this.Dispose() =
            GL.DeleteProgram(handle)
            GC.SuppressFinalize(this);

    override _.Finalize() =
        GL.DeleteProgram(handle)

[<RequireQualifiedAccess>]
module Shader =
    let create (vertPath: string) (fragPath: string) =
        // Just loads the entire file into a string
        let loadSource (path: string) =
            use streamReader = new StreamReader(path, Encoding.UTF8)
            streamReader.ReadToEnd()

        let vertexShaderSource = loadSource vertPath
        let vertexShader = GL.CreateShader ShaderType.VertexShader
        GL.ShaderSource (vertexShader, vertexShaderSource)
        GL.CompileShader vertexShader

        // Check for compile errors
        let infoLogVert = GL.GetShaderInfoLog(vertexShader)
        if infoLogVert <> String.Empty then
            printfn "%s" infoLogVert

        let fragmentShaderSource = loadSource fragPath
        let fragmentShader = GL.CreateShader ShaderType.FragmentShader
        GL.ShaderSource (fragmentShader, fragmentShaderSource)
        GL.CompileShader fragmentShader

        // Check for compile errors
        let infoLogFrag = GL.GetShaderInfoLog fragmentShader
        if infoLogFrag <> String.Empty then
            printfn "%s" infoLogFrag

        let handle = GL.CreateProgram()

        // Attach both shaders
        GL.AttachShader (handle, vertexShader)
        GL.AttachShader (handle, fragmentShader)

        // Link both shaders together
        GL.LinkProgram handle

        // Check for linker errors
        let infoLogLink = GL.GetProgramInfoLog handle
        if infoLogFrag <> String.Empty then
            printfn "%s" infoLogLink


        // Now that we're done, clean up.
        // When the shader program is linked, it no longer needs the individual shaders attached to it; the compiled code is copied into the shader program.
        // Detach them, and then delete them.
        GL.DetachShader (handle, vertexShader)
        GL.DetachShader (handle, fragmentShader)
        GL.DeleteShader (fragmentShader)
        GL.DeleteShader (vertexShader)

        new Shader(handle)

    let useIt (shader: Shader) =
        GL.UseProgram shader.Handle

    let setValue uniform (shader: Shader) name value =
        let location = GL.GetUniformLocation (shader.Handle, name)
        match location with
        | -1 -> printfn "Warning: Uniform name '%s' not found!" name
        | location -> uniform location value

    let setInt shader =
        setValue (fun location (value: int) -> GL.Uniform1(location, value)) shader

    let setFloat shader =
        setValue (fun location (value: float) -> GL.Uniform1(location, value)) shader

    let setMatrix4 (shader: Shader) name matrix =
        let location = GL.GetUniformLocation(shader.Handle, name);
        match location with
        | -1 -> failwithf "Uniform name '%s' not found!" name
        | location -> GL.UniformMatrix4(location, false, ref matrix)

    // The shader sources provided with this project use hardcoded layout(location)-s. If you want to do it dynamically,
    // you can omit the layout(location=X) lines in the vertex shader, and use this in VertexAttribPointer instead of the hardcoded values.
    let getAttribLocation (shader: Shader) attribName =
        GL.GetAttribLocation (shader.Handle, attribName)
