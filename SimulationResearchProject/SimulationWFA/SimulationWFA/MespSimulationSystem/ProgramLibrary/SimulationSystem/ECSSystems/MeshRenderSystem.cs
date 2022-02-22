using System;
using Dalak.Ecs;
using MESPSimulationSystem.Math;
using SimulationSystem.Components;
using SimulationSystem.ECSComponents;
using SimulationSystem.SharedData;

namespace SimulationSystem.Systems
{
    public class MeshRenderSystem : Dalak.Ecs.System
    {
        private Filter<MeshRendererComp, TransformComp>.Exclude<OutlineBorderRenderComp> meshRendererFilter = null;

        private Filter<CameraComp> cameraFilter = null;

        ShaderDatas shaderDatas = null;

        public override void Awake()
        {

        }


        public override void Render()
        {
            ref var cameraComp = ref cameraFilter.Get1(0);

            shaderDatas.SetupDefaultShadersToRender(cameraComp.view, cameraComp.projection);
            
            foreach (var m in meshRendererFilter)
            {
                ref var transformComp = ref meshRendererFilter.Get2(m);
                ref var meshRendererComp = ref meshRendererFilter.Get1(m);

                if (meshRendererComp.meshRenderer == null)
                {
                    meshRendererComp.SetMeshRenderer();
                }

                meshRendererComp.meshRenderer.Render(transformComp.transform, meshRendererComp.material);
            }
        }

        public override void OnApplicationQuit()
        {
            foreach (var m in meshRendererFilter)
            {
                var meshRendererComp = meshRendererFilter.Get1(m);
                meshRendererComp.meshRenderer.CleanUp();
            }
        }

    }
}