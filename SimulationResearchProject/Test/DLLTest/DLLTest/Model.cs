using System;
using System.Collections.Generic;
using System.Numerics;
using MESPSimulation.DLL;
using MESPSimulation.Graphics.Rendering;

namespace MESPSimulation.Graphics.Model
{
    public class Model
    {
        public Vector3 position;
        public Vector3 size;

        public IntPtr modelAdress;

        public List<Mesh> meshes;
        private int totalMesh;

        public Model()
        {
            totalMesh = 0;
            meshes = new List<Mesh>();
            position = Vector3.Zero;
            this.size = Vector3.One;
            modelAdress = RenderProgramDLL.NewModel();
        }
        
        public Model(Vector3 pos , Vector3 size)
        {
            totalMesh = 0;
            meshes = new List<Mesh>();
            position = pos;
            this.size = size;
            modelAdress = RenderProgramDLL.NewModel();
        }

        public void SetPosAndSize(Vector3 pos , Vector3 size)
        {
            position = pos;
            this.size = size;
            
            float[] posF = {position.X, position.Y, position.Z};
            float[] sizeF = {size.X, size.Y, size.Z};
            
            RenderProgramDLL.SetPosAndSize(modelAdress,posF,sizeF);
        }

        public void AddMesh(Mesh mesh)
        {
            meshes.Add(mesh);
            RenderProgramDLL.AddMeshToModel(modelAdress, mesh.meshAdress);
            meshes[totalMesh] = (GetMesh(totalMesh++));
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

        public Mesh GetMesh(int meshIdx)
        {
            meshes[meshIdx].meshAdress = RenderProgramDLL.GetMesh(modelAdress, meshIdx);

            return meshes[meshIdx];
        }


    }
}