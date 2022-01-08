using System;
using System.Collections.Generic;
using System.Numerics;

namespace DLLTest
{
    public class Model
    {
        public Vector3 position;
        public Vector3 size;

        public IntPtr modelAdress;

        public Model()
        {
            modelAdress = RenderProgramDLL.NewModel();
        }

        public void LoadModel(string path)
        {
            RenderProgramDLL.LoadModel(modelAdress, path);
        }

        public void Render(Shader shader)
        {
            RenderProgramDLL.Render(modelAdress, shader.shaderAdress);
        }

        public void CleanUp()
        {
            RenderProgramDLL.CleanUp(modelAdress);
        }


    }
}