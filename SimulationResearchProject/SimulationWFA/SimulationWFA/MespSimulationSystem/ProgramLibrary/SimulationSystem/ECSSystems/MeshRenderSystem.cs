using System;
using Dalak.Ecs;
using MESPSimulationSystem.Math;
using SimulationSystem.Components;
using SimulationSystem.SharedData;

namespace SimulationSystem.Systems
{
    public class MeshRenderSystem : Dalak.Ecs.System
    {
        private Filter<MeshRendererComp, TransformComp> meshRendererFilter = null;
        private Filter<CameraComp> cameraFilter = null;

        ShaderReferences shaderReferences = null;

        public override void Awake()
        {

        }

        public override void Update()
        {
            foreach(var m in meshRendererFilter)
            {
               
            }
        }

        public override void Render()
        {
            ref var cameraComp = ref cameraFilter.Get1(0);

            Mat4 view = cameraComp.camera.GetViewMatrix();
            Mat4 projection = cameraComp.camera.Perspective(800f / 600f);

            shaderReferences.defaultLitShader.Activate();
            shaderReferences.defaultLitShader.SetMat4("view", view);
            shaderReferences.defaultLitShader.SetMat4("projection", projection);

            foreach (var m in meshRendererFilter)
            {
                var transformComp = meshRendererFilter.Get2(m);
                var meshRendererComp = meshRendererFilter.Get1(m);

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