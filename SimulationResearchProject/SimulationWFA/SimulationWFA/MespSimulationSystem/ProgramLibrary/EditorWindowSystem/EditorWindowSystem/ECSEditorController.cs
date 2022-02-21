using RenderLibrary.IO;
using SimulationSystem.SharedData;
using SimulationWFA.MespSimulationSystem.ProgramLibrary.EditorWindowSystem.Systems;

namespace SimulationSystem.Systems
{
    public class ECSEditorController : EasyECSController
    {
        public const int GenericSystemGroup = 0; // Always active systems
        public Screen screen;

        public ECSEditorController(Screen screen)
        {
            this.screen = screen;
        }

        public override void OnInject()
        {
            systemManager.Inject(screen);
            systemManager.Inject(new ShaderDatas());
            systemManager.Inject(new ModelPaths());
        }

        public override void AddSystems() // Ecs Sistemleri
        {
            systemManager.AddSystem(new TransformSystem(), GenericSystemGroup);

            //Custom Systems
            systemManager.AddSystem(new SceneConfigurationSystem(), GenericSystemGroup);
            //systemManager.AddSystem(new RotationTestSystem(), GenericSystemGroup);
            //systemManager.AddSystem(new ClearColorTestSystem(), GenericSystemGroup);

            //

            systemManager.AddSystem(new EditorCameraSystem(), GenericSystemGroup);
            systemManager.AddSystem(new InputSystem(), GenericSystemGroup);

            //RenderSystems
            systemManager.AddSystem(new LightSystem(), GenericSystemGroup);
            systemManager.AddSystem(new MeshRenderSystem(), GenericSystemGroup);
            //systemManager.AddSystem(new EditorInfiniteGridSystem(), GenericSystemGroup);

            //EventSystems
            systemManager.AddSystem(new EditorEventListenSystem(), GenericSystemGroup);
            //systemManager.AddSystem(new TestSystem(), GenericSystemGroup);
        }
    }
}