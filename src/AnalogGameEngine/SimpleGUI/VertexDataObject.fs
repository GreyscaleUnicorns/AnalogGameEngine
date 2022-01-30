namespace AnalogGameEngine.SimpleGUI

open OpenTK.Graphics
open OpenTK.Graphics.OpenGL4

open AnalogGameEngine.SimpleGUI.Helper

type VertexDataObject =
    {
        drawType: PrimitiveType
        texture: Texture
        vertexBufferObject: int
        vertexArrayObject: int
        elementBufferObject: int option
        vertexAmount: int
    }

type VertexDataObjectParams =
    {
        vertices: float []
        config: int []
        drawType: PrimitiveType
        bufferObjectIndices: uint [] option
        texture: Texture
    }

module VertexDataObject =
    let create parameters =
        let configSum = Array.sum parameters.config

        if parameters.vertices.Length % configSum <> 0 then
            failwith "Config and vertices array do not match!"

        // Create VBO
        let vertexBufferObject = GL.GenBuffer()
        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject)

        GL.BufferData(
            BufferTarget.ArrayBuffer,
            parameters.vertices.Length * sizeof<float>,
            parameters.vertices,
            BufferUsageHint.StaticDraw
        )

        let mutable elementBufferObject = None

        match parameters.bufferObjectIndices with
        | Some bufferObjectIndices ->
            let ebo = GL.GenBuffer()
            elementBufferObject <- ebo |> Some
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo)

            GL.BufferData(
                BufferTarget.ElementArrayBuffer,
                bufferObjectIndices.Length * sizeof<uint>,
                bufferObjectIndices,
                BufferUsageHint.StaticDraw
            )
        | None -> ()

        // Create VAO
        let vertexArrayObject = GL.GenVertexArray()
        GL.BindVertexArray vertexArrayObject

        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject)

        match elementBufferObject with
        | Some elementBufferObject -> GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject)
        | None -> ()

        let mutable tmpSum = 0

        for i in 0 .. (parameters.config.Length - 1) do
            let configEl = parameters.config[i]

            GL.VertexAttribPointer(
                i,
                configEl,
                VertexAttribPointerType.Float,
                false,
                configSum * sizeof<float>,
                tmpSum * sizeof<float>
            )

            GL.EnableVertexAttribArray i
            tmpSum <- tmpSum + configEl

        GL.BindVertexArray 0

        {
            drawType = parameters.drawType
            texture = parameters.texture
            vertexBufferObject = vertexBufferObject
            vertexArrayObject = vertexArrayObject
            elementBufferObject = elementBufferObject
            vertexAmount = parameters.vertices.Length / configSum
        }

    let draw (vertexData: VertexDataObject) texture =
        let texture =
            match texture, vertexData.texture with
            | Some texture, _ -> texture
            | None, texture -> texture

        Texture.useIt texture None

        GL.BindVertexArray vertexData.vertexArrayObject

        match vertexData.elementBufferObject with
        | Some _ -> GL.DrawElements(vertexData.drawType, vertexData.vertexAmount, DrawElementsType.UnsignedInt, 0)
        | None -> GL.DrawArrays(vertexData.drawType, 0, vertexData.vertexAmount)

    let unload (vertexData: VertexDataObject) =
        GL.DeleteBuffer(vertexData.vertexBufferObject)

        match vertexData.elementBufferObject with
        | Some buffer -> GL.DeleteBuffer buffer
        | None -> ()

        GL.DeleteVertexArray vertexData.vertexArrayObject
