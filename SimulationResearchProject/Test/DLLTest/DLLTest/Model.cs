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
            position = Vector3.Zero;
            this.size = Vector3.One;
            modelAdress = RenderProgramDLL.NewModel();
        }
        
        public Model(Vector3 pos , Vector3 size)
        {
            position = pos;
            this.size = size;
            modelAdress = RenderProgramDLL.NewModel();
        }
        
        public void LoadModelWithAssimp(string path)
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