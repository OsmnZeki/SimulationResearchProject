using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        private Filter<CameraComp, TransformComp> cameraFilter = null;

        SceneShaderManager sceneShaderManager = null;

        Dictionary<int, float> transparentObjectDist = new Dictionary<int, float>();

        public override void Awake()
        {

        }


        public override void Render()
        {
            ref var cameraComp = ref cameraFilter.Get1(0);
            ref var transformCameraComp = ref cameraFilter.Get2(0);

            sceneShaderManager.SetupDefaultShadersToRender(cameraComp.view, cameraComp.projection);

            transparentObjectDist.Clear();

            //opaque render
            foreach (var m in meshRendererFilter)
            {
                ref var transformComp = ref meshRendererFilter.Get2(m);
                ref var meshRendererComp = ref meshRendererFilter.Get1(m);


                if (meshRendererComp.SetMeshRenderer() == false) continue;
                //Console.WriteLine("Meshhh");
                if (meshRendererComp.material.transparent)
                {
                    float sqrDist = Vector3.DistanceSquared(transformCameraComp.transform.position, transformComp.transform.position);
                    transparentObjectDist.Add(m, sqrDist);
                    continue;
                }

                meshRendererComp.meshRenderer.Render(transformComp.transform, meshRendererComp.material);
            }

            //transparent render
            foreach (var m in transparentObjectDist.OrderByDescending(pair => pair.Value))
            {
                ref var transformComp = ref meshRendererFilter.Get2(m.Key);
                ref var meshRendererComp = ref meshRendererFilter.Get1(m.Key);
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