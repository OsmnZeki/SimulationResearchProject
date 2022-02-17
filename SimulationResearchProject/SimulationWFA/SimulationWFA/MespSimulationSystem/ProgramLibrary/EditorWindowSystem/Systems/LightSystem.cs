using Dalak.Ecs;
using SimulationSystem.Components;
using SimulationSystem.SharedData;

namespace SimulationWFA.MespSimulationSystem.ProgramLibrary.EditorWindowSystem.Systems
{
    public class LightSystem : Dalak.Ecs.System
    {
        readonly Filter<DirectionalLightComp, TransformComp> directionalFilter = null;
        private Filter<CameraComp> cameraFilter = null;

        ShaderReferences shaderReferences = null;

        public override void Update()
        {
            ref var cameraComp = ref cameraFilter.Get1(0);
            shaderReferences.defaultLitShader.Activate();
            shaderReferences.defaultLitShader.Set3Float("viewPos", cameraComp.camera.cameraPos);


            foreach(var d in directionalFilter)
            {
                ref var directioanLightComp = ref directionalFilter.Get1(d);
                ref var transformComp = ref directionalFilter.Get2(d);

                directioanLightComp.directionalLight.direction = transformComp.transform.rotation;
                directioanLightComp.directionalLight.Render(shaderReferences.defaultLitShader);
            }
        }

    }
}
