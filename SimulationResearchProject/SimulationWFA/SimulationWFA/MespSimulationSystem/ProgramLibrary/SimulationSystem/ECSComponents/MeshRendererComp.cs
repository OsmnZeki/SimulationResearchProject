using Dalak.Ecs;
using RenderLibrary.Graphics;
using RenderLibrary.Graphics.RenderData;
using RenderLibrary.Graphics.Rendering;

namespace SimulationSystem.Components
{
    public struct MeshRendererComp
    {
        public Mesh mesh;
        public Material material;
        public MeshRenderer meshRenderer;



        public void SetMeshRenderer()
        {
            if(meshRenderer == null)
            {
                meshRenderer = new MeshRenderer();
                meshRenderer.SetMesh(mesh);
                meshRenderer.SetMaterial(material);
                meshRenderer.Setup();
            }
        }

    }
}