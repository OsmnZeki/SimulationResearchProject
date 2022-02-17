﻿using Dalak.Ecs;
using SimulationSystem.Components;
using SimulationSystem.SharedData;

namespace SimulationSystem.Systems
{
    public class LightSystem : Dalak.Ecs.System
    {
        readonly Filter<DirectionalLightComp, TransformComp> directionalFilter = null;
        readonly Filter<PointLightComp, TransformComp> pointLightFilter = null;
        readonly Filter<SpotLightComp, TransformComp> spotLightFilter = null;

        private Filter<CameraComp,TransformComp> cameraFilter = null;

        ShaderReferences shaderReferences = null;

        public override void Render()
        {
            ref var cameraComp = ref cameraFilter.Get1(0);
            ref var camTransformComp = ref cameraFilter.Get2(0);
            shaderReferences.defaultLitShader.Activate();
            shaderReferences.defaultLitShader.Set3Float("viewPos", camTransformComp.transform.position);


            foreach(var d in directionalFilter)
            {
                ref var directioanLightComp = ref directionalFilter.Get1(d);
                ref var transformComp = ref directionalFilter.Get2(d);

                directioanLightComp.directionalLight.direction = transformComp.transform.forward;
                directioanLightComp.directionalLight.Render(shaderReferences.defaultLitShader);
            }

            shaderReferences.defaultLitShader.SetInt("numbPointLights", pointLightFilter.NumberOfEntities);
            foreach(var p in pointLightFilter)
            {
                ref var pointLightComp = ref pointLightFilter.Get1(p);
                ref var transformComp = ref pointLightFilter.Get2(p);
                pointLightComp.pointLight.position = transformComp.transform.position;
                pointLightComp.pointLight.Render(shaderReferences.defaultLitShader, p);
            }

            shaderReferences.defaultLitShader.SetInt("numbSpotLights", spotLightFilter.NumberOfEntities);
            foreach (var s in spotLightFilter)
            {
                ref var spotLightComp = ref spotLightFilter.Get1(s);
                ref var transformComp = ref spotLightFilter.Get2(s);
                spotLightComp.spotLight.position = transformComp.transform.position;
                spotLightComp.spotLight.direction = transformComp.transform.forward;
                spotLightComp.spotLight.Render(shaderReferences.defaultLitShader, s);
            }


        }

    }
}
