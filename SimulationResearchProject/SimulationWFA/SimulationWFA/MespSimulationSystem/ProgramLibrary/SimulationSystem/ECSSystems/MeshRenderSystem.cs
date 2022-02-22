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

        private Filter<CameraComp,TransformComp> cameraFilter = null;

        ShaderDatas shaderDatas = null;

        public override void Awake()
        {

        }


        public override void Render()
        {
            ref var cameraComp = ref cameraFilter.Get1(0);
            ref var camTransformComp = ref cameraFilter.Get2(0);

            Mat4 view = cameraComp.GetViewMatrix(camTransformComp.transform);
            Mat4 projection = cameraComp.Perspective(800f / 600f);

            shaderDatas.SetupDefaultShadersToRender(view,projection);
            
            foreach (var m in meshRendererFilter)
            {
                ref var transformComp = ref meshRendererFilter.Get2(m);
                ref var meshRendererComp = ref meshRendererFilter.Get1(m);

                if (meshRendererComp.meshRenderer == null)
                {
                    meshRendererComp.SetMeshRenderer();
                }

                meshRendererComp.material.GetShader().Activate();
                meshRendererComp.meshRenderer.Render(transformComp.transform);
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