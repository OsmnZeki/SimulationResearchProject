using RenderLibrary.IO;
using SimulationSystem.ECSSystems;
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
            systemManager.Inject(new ModelPaths());
            systemManager.Inject(new TextureReferences());
        }

        public override void AddSystems() // Ecs Sistemleri
        {
            systemManager.AddSystem(new FPSCalculatorSystem(), GenericSystemGroup);
            systemManager.AddSystem(new TransformSystem(), GenericSystemGroup);
            

            //Custom Systems
            systemManager.AddSystem(new SceneConfigurationSystem(), GenericSystemGroup);
            systemManager.AddSystem(new TestSystem(), GenericSystemGroup);
            systemManager.AddSystem(new PhysicTestSystem(), GenericSystemGroup);

            //

            systemManager.AddSystem(new PhysicExternalForceSystem(), GenericSystemGroup);
            systemManager.AddSystem(new EasyPhysicSystem(), GenericSystemGroup);
            systemManager.AddSystem(new ColliderBoundsUpdateSystem(), GenericSystemGroup);
            systemManager.AddSystem(new CollisionDetectionSystem(), GenericSystemGroup);
            systemManager.AddSystem(new ResolveCollisionSystem(), GenericSystemGroup);
            

            systemManager.AddSystem(new EditorCameraSystem(), GenericSystemGroup);
            systemManager.AddSystem(new InputSystem(), GenericSystemGroup);


            //RenderSystems 
            systemManager.AddSystem(new AnimationSystem(), GenericSystemGroup);
            systemManager.AddSystem(new LightSystem(), GenericSystemGroup);
            
            systemManager.AddSystem(new MeshRenderSystem(), GenericSystemGroup);
            systemManager.AddSystem(new OutlineBorderRenderSystem(), GenericSystemGroup);

            systemManager.AddSystem(new MespEditorDebugSystem(), GenericSystemGroup);
            systemManager.AddSystem(new EditorInfiniteGridSystem(), GenericSystemGroup);
            systemManager.AddSystem(new TextRendererSystem(), GenericSystemGroup);

            //EventSystems
            systemManager.AddSystem(new EditorEventListenSystem(), GenericSystemGroup);
        }
    }
}