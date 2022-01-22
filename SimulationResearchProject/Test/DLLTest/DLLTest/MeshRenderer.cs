using System;

namespace DLLTest
{//TODO: test amaçlı sonrasında ecs için entegre et
    public class MeshRenderer
    {
        private IntPtr meshRendererAdress;

        public MeshRenderer()
        {
            //TODO: meshRendererAdress = RenderDLL.NewMeshRenderer();
        }

        public void SetMesh()
        {
            //TODO: RenderDLL.SetMeshToRenderer();
        }

        public void SetMaterial()
        {
            //TODO: RenderDLL.SetMaterialToRenderer();
        }
        
        public void GetMesh()
        {
            //TODO: RenderDLL.GetMeshFromRenderer();
        }

        public void GetMaterial()
        {
            //TODO: RenderDLL.GetMaterialFromRenderer();
        }

        public void Setup()
        {
            //TODO: RenderDLL.RendererSetup();
        }

        public void Render(Transform transform)
        {
            //TODO: RenderDLL.MeshRender(transform);
        }

        public void CleanUp()
        {
            //TODO: RenderDLL.CleanUp();
        }
        
    }
}