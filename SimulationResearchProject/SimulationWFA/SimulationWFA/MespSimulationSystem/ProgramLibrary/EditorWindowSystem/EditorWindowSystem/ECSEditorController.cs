namespace SimulationSystem.Systems
{
    public class ECSEditorController : EasyECSController
    {
        public const int GenericSystemGroup = 0; // Always active systems
        
        public override void OnInject()
        {
            
        }

        public override void AddSystems() // Ecs Sistemleri
        {
            //systemManager.AddSystem(new SceneLoadingSystem(), GenericSystemGroup);
            systemManager.AddSystem(new EcsEditorTestSystem(), GenericSystemGroup);
            systemManager.AddSystem(new MeshRenderSystem(), GenericSystemGroup);
        }
    }
}