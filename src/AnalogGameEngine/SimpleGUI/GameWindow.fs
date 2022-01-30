namespace AnalogGameEngine.SimpleGUI

open OpenTK.Graphics
open OpenTK.Graphics.OpenGL4
open OpenTK.Mathematics
open OpenTK.Windowing
open OpenTK.Windowing.GraphicsLibraryFramework

open System

open AnalogGameEngine.SimpleGUI.Helper

type Textures = { cardBack: Texture; table: Texture }

type VertexData =
    {
        card: VertexDataObject
        cardBack: VertexDataObject
        stack: VertexDataObject
        table: VertexDataObject
    }

type GameWindow(gameWindowSettings, nativeWindowSettings) =
    inherit Desktop.GameWindow(gameWindowSettings, nativeWindowSettings)

    let mutable time = 0.
    let mutable keyNr1 = false

    let mutable shader: Shader option = None
    let mutable textures: Textures option = None
    let mutable vertexData: VertexData option = None

    let configureGL () =
        GL.ClearColor(0.2f, 0.2f, 0.2f, 1.f)
        GL.Enable(EnableCap.DepthTest)
        GL.Enable(EnableCap.CullFace)
        GL.Enable(EnableCap.Multisample)

    let initializeShader () =
        // TODO: workaround to make shaders work for now
        let shader =
            Shader.create "AnalogGameEngine.SimpleGUI/shader.vert" "AnalogGameEngine.SimpleGUI/shader.frag"

        Shader.useIt shader
        Shader.setInt shader "texture0" 0
        shader

    let initializeTextures (_: Shader) =
        {
            cardBack = Texture.create "assets/textures/Playingcards/CardBack_Red.jpg"
            table = Texture.create "assets/textures/Wood_006_COLOR.jpg"
        }

    let initializeVertexData (textures: Textures) =
        {
            table =
                // 3 position + 2 tex coords
                let topRight = [| 3.; 0.; -3.; 1.; 1. |]
                let topLeft = [| -3.; 0.; -3.; 0.; 1. |]
                let bottomLeft = [| -3.; 0.; 3.; 0.; 0. |]
                let bottomRight = [| 3.; 0.; 3.; 1.; 0. |]

                {
                    vertices =
                        [|
                            yield! topRight
                            yield! topLeft
                            yield! bottomLeft
                            yield! bottomRight
                        |]
                    config = [| 3; 2 |]
                    drawType = PrimitiveType.TriangleStrip
                    texture = textures.table
                    bufferObjectIndices = None
                }
                |> VertexDataObject.create
            cardBack =
                // 3 position + 2 tex coords
                let topRight = [| -0.315; 0.44; 0.; 1.; 1. |]
                let topLeft = [| 0.315; 0.44; 0.; 0.; 1. |]
                let bottomLeft = [| -0.315; -0.44; 0.; 1.; 0. |]
                let bottomRight = [| 0.315; -0.44; 0.; 0.; 0. |]

                {
                    vertices =
                        [|
                            yield! topRight
                            yield! topLeft
                            yield! bottomLeft
                            yield! bottomRight
                        |]
                    config = [| 3; 2 |]
                    drawType = PrimitiveType.TriangleStrip
                    texture = textures.cardBack
                    bufferObjectIndices = None
                }
                |> VertexDataObject.create
            stack =
                // 3 position + 2 tex coords
                let frontBottomRight = [| 0.315; 0.003; 0.44; 1.; 0. |]
                let frontBottomLeft = [| -0.315; 0.003; 0.44; 0.; 0. |]
                let frontTopRight = [| 0.315; 0.003; -0.44; 1.; 1. |]
                let frontTopLeft = [| -0.315; 0.003; -0.44; 0.; 1. |]
                let backBottomRight = [| 0.315; 0.; 0.44; 1.; 0. |]
                let backBottomLeft = [| -0.315; 0.; 0.44; 0.; 0. |]
                let backTopRight = [| 0.315; 0.; -0.44; 1.; 1. |]
                let backTopLeft = [| -0.315; 0.; -0.44; 0.; 1. |]

                {
                    vertices =
                        [|
                            yield! frontBottomRight
                            yield! frontBottomLeft
                            yield! frontTopRight
                            yield! frontTopLeft
                            yield! backBottomRight
                            yield! backBottomLeft
                            yield! backTopRight
                            yield! backTopLeft
                        |]
                    config = [| 3; 2 |]
                    drawType = PrimitiveType.TriangleStrip
                    texture = textures.cardBack
                    bufferObjectIndices =
                        Some [| 3u
                                1u
                                2u
                                0u
                                4u
                                1u
                                5u
                                3u
                                7u
                                2u
                                6u
                                4u |]
                }
                |> VertexDataObject.create
            card =
                // 3 position + 2 tex coords
                let topRight = [| 0.315; 0.44; 0.; 1.; 1. |]
                let topLeft = [| -0.315; 0.44; 0.; 0.; 1. |]
                let bottomRight = [| 0.315; -0.44; 0.; 1.; 0. |]
                let bottomLeft = [| 0.315; -0.44; 0.; 0.; 0. |]

                {
                    vertices =
                        [|
                            yield! topRight
                            yield! topLeft
                            yield! bottomLeft
                            yield! bottomRight
                        |]
                    config = [| 3; 2 |]
                    drawType = PrimitiveType.TriangleStrip
                    texture = textures.cardBack
                    bufferObjectIndices = None
                }
                |> VertexDataObject.create
        }

    let draw () =
        // Draw table
        let mutable model = Matrix4.Identity
        Shader.setMatrix4 shader.Value "model" model

        VertexDataObject.draw vertexData.Value.table None

    override _.OnLoad() =
        configureGL ()
        let newShader = initializeShader ()
        shader <- Some newShader
        let newTextures = initializeTextures newShader
        textures <- Some newTextures
        vertexData <- initializeVertexData newTextures |> Some

        base.OnLoad()

    override this.OnRenderFrame e =
        time <- time + (float e.Time)

        GL.Clear(
            ClearBufferMask.ColorBufferBit
            ||| ClearBufferMask.DepthBufferBit
        )

        Shader.useIt shader.Value

        // TODO: remove test code
        // TODO: move matrices to own camera class
        let radius = 7.0
        let camX = single (Math.Sin(time / 2.) * radius)
        let camZ = single (Math.Cos(time / 2.) * radius)

        let view =
            Matrix4.LookAt(Vector3(camX, 2.5f, camZ), Vector3(0.f, 0.f, 0.f), Vector3(0.f, 1.f, 0.f))

        Shader.setMatrix4 shader.Value "view" view

        let mutable projection = Matrix4.Identity

        projection <-
            projection
            * Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(45) |> single,
                this.ClientRectangle.Size.X
                / this.ClientRectangle.Size.Y
                |> single,
                0.1f,
                100.f
            )

        Shader.setMatrix4 shader.Value "projection" projection

        draw ()

        base.SwapBuffers()

        base.OnRenderFrame e

    override this.OnUpdateFrame e =
        let mouse = base.MouseState
        let input = base.KeyboardState

        if input.IsKeyDown(Keys.Escape) then
            this.Close()

        if not keyNr1 && input.IsKeyDown(Keys.D1) then
            if GL.IsEnabled EnableCap.CullFace then
                GL.Disable EnableCap.CullFace
            else
                GL.Enable EnableCap.CullFace

            keyNr1 <- true

        if keyNr1 && input.IsKeyReleased(Keys.D1) then
            keyNr1 <- false

        base.OnUpdateFrame e

    override _.OnResize e =
        GL.Viewport(0, 0, base.Size.X, base.Size.Y)
        base.OnResize e

    // Now, for cleanup. This isn't technically necessary since C# will clean up
    // all resources automatically when the program closes, but it's very
    // important to know how anyway
    override _.OnUnload() =
        // Unbind all the resources by binding the targets to 0/null.
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0)
        GL.BindVertexArray 0
        GL.UseProgram 0

        // Delete all the resources.
        VertexDataObject.unload vertexData.Value.card
        VertexDataObject.unload vertexData.Value.cardBack
        VertexDataObject.unload vertexData.Value.table

        (shader.Value :> IDisposable).Dispose()
        base.OnUnload()

module GameWindow =
    let create width height title =
        let gameWindowSettings = new Desktop.GameWindowSettings()
        let nativeWindowsSettings = new Desktop.NativeWindowSettings()
        nativeWindowsSettings.Size <- new Vector2i(width, height)
        nativeWindowsSettings.Title <- title
        new GameWindow(gameWindowSettings, nativeWindowsSettings)

// TODO: remove
module Program =
    let window = GameWindow.create 800 600 "Test"
    window.Run()
