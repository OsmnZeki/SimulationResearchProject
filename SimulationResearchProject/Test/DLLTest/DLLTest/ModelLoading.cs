using System;
using DLLTest;


namespace MESPSimulation.Graphics.Model
{
    public class ModelLoading
    {
        private IntPtr modelAdress;

        public Mesh[] meshes;
        public Material[] materials;
        private int totalMesh;

        public ModelLoading()
        {
        }

        public void LoadModel(string path)
        {
            //TODO: yorum satırlarını yaz
            //RenderDLL.LoadModel(path)
            //meshes = RenderDLL.GetMeshesFromModel(modelAdress);
            //materials = RenderDLL.GetMaterialsFromModel(modelAdress);
        }

        public Mesh GetMesh(int idx)
        {
            return meshes[idx];
        }

        public Material GetMaterial(int idx)
        {
            return materials[idx];
        }
    }
}