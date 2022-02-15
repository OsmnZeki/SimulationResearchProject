using System;
using Dalak.Ecs;
using MESPSimulationSystem.Math;
using RenderLibrary.Graphics;
using RenderLibrary.Transform;
using SimulationSystem.Components;

namespace SimulationSystem.Systems
{
    public class MeshRenderSystem : Dalak.Ecs.System
    {
        private Filter<MeshRendererComp, TransformComp> meshRendererFilter = null;
        private Filter<CameraComp> cameraFilter = null; //TODO: tek kamera var arttırılabilir

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

            foreach (var m in meshRendererFilter)
            {
                var transformComp = meshRendererFilter.Get2(m);
                var meshRendererComp = meshRendererFilter.Get1(m);

                var shader = meshRendererComp.material.GetShader();
                shader.Activate();
                shader.Set3Float("viewPos", cameraComp.camera.cameraPos);

                shader.SetMat4("view", view);
                shader.SetMat4("projection", projection);

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