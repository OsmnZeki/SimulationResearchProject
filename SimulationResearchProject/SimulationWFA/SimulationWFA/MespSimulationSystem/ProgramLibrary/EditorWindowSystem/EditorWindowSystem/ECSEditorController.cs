using SimulationSystem.SharedData;
using SimulationWFA.MespSimulationSystem.ProgramLibrary.EditorWindowSystem.Systems;

namespace SimulationSystem.Systems
{
    public class ECSEditorController : EasyECSController
    {
        public const int GenericSystemGroup = 0; // Always active systems
        
        public override void OnInject()
        {
            systemManager.Inject(new ShaderReferences());
        }

        public override void AddSystems() // Ecs Sistemleri
        {
            
            systemManager.AddSystem(new TransformSystem(), GenericSystemGroup);
            //Custom Systems
            systemManager.AddSystem(new EcsEditorTestSystem(), GenericSystemGroup);



            //

            systemManager.AddSystem(new EditorCameraSystem(), GenericSystemGroup);

            //RenderSystems
            systemManager.AddSystem(new LightSystem(), GenericSystemGroup);
            systemManager.AddSystem(new MeshRenderSystem(), GenericSystemGroup);

            //EventSystems
            systemManager.AddSystem(new EditorEventListenSystem(), GenericSystemGroup);
            //systemManager.AddSystem(new TestSystem(), GenericSystemGroup);
        }
    }
}