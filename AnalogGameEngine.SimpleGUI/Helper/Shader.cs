using System;
using System.IO;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace AnalogGameEngine.SimpleGUI.Helper {
    //A simple class meant to help create shaders.
    public class Shader : System.IDisposable {
        public int Handle {
            get; private set;
        }

        public Shader(string vertPath, string fragPath) {
            int VertexShader;
            int FragmentShader;

            string VertexShaderSource = LoadSource(vertPath);
            VertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, VertexShaderSource);
            GL.CompileShader(VertexShader);

            //Check for compile errors
            string infoLogVert = GL.GetShaderInfoLog(VertexShader);
            if (infoLogVert != System.String.Empty)
                System.Console.WriteLine(infoLogVert);

            string FragmentShaderSource = LoadSource(fragPath);
            FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, FragmentShaderSource);
            GL.CompileShader(FragmentShader);

            //Check for compile errors
            string infoLogFrag = GL.GetShaderInfoLog(VertexShader);
            if (infoLogFrag != System.String.Empty)
                System.Console.WriteLine(infoLogFrag);


            Handle = GL.CreateProgram();

            //Attach both shaders...
            GL.AttachShader(Handle, VertexShader);
            GL.AttachShader(Handle, FragmentShader);

            //And then link them together.
            GL.LinkProgram(Handle);

            //Check for linker errors
            string infoLogLink = GL.GetProgramInfoLog(Handle);
            if (infoLogLink != System.String.Empty)
                System.Console.WriteLine(infoLogLink);


            //Now that it's done, clean up.
            //When the shader program is linked, it no longer needs the individual shaders attached to it; the compiled code is copied into the shader program.
            //Detach them, and then delete them.
            GL.DetachShader(Handle, VertexShader);
            GL.DetachShader(Handle, FragmentShader);
            GL.DeleteShader(FragmentShader);
            GL.DeleteShader(VertexShader);
        }

        public void Use() {
            GL.UseProgram(Handle);
        }

        public void SetInt(string name, int value) {
            int location = GL.GetUniformLocation(Handle, name);

            if (location == -1) {
                Console.WriteLine("Warning: Uniform name \"" + name + "\" not found!");
            }
            else {
                GL.Uniform1(location, value);
            }
        }

        public void SetFloat(string name, float value) {
            int location = GL.GetUniformLocation(Handle, name);

            if (location == -1) {
                throw new ArgumentException("uniform name not found");
            }

            GL.Uniform1(location, value);
        }

        public void SetMatrix4(string name, Matrix4 matrix) {
            int location = GL.GetUniformLocation(Handle, name);

            if (location == -1) {
                throw new ArgumentException("uniform name not found");
            }

            GL.UniformMatrix4(location, false, ref matrix);
        }

        //The shader sources provided with this project use hardcoded layout(location)-s. If you want to do it dynamically,
        //you can omit the layaout(location=X) lines in the vertex shader, and use this in VertexAttribPointer instead of the hardcoded values.
        public int GetAttribLocation(string attribName) {
            return GL.GetAttribLocation(Handle, attribName);
        }

        //Just loads the entire file into a string.
        private string LoadSource(string path) {
            string readContents;

            using (StreamReader streamReader = new StreamReader(path, Encoding.UTF8)) {
                readContents = streamReader.ReadToEnd();
            }

            return readContents;
        }

        //This section is dedicated to cleaning up the shader after it's finished.
        //Doing this solely in a finalizer results in a crash because of the Object-Oriented Language Problem
        //( https://www.khronos.org/opengl/wiki/Common_Mistakes#The_Object_Oriented_Language_Problem )
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

        ~Shader() {
            GL.DeleteProgram(Handle);
        }


        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
