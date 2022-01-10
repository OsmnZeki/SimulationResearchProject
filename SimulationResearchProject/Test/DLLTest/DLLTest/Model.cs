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

        private List<Mesh> meshes;

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

        public void AddMesh(Mesh mesh)
        {
            RenderProgramDLL.AddMeshToModel(modelAdress, mesh.meshAdress);
        }
        
        public void LoadModelWithAssimp(string path)
        {
            RenderProgramDLL.LoadModel(modelAdress, path);
        }

        public void Render(Shader shader)
        {
            RenderProgramDLL.ModelRender(modelAdress, shader.shaderAdress);
        }

        public void CleanUp()
        {
            RenderProgramDLL.ModelCleanUp(modelAdress);
        }


    }
}