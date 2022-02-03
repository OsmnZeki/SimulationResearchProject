using Dalak.Ecs;

namespace SimulationSystem.Systems
{
    public class SceneLoadingSystem : Dalak.Ecs.System
    {
        public override void Awake()
        {
            var simObjectCount = SimObject.GetSimObjectCountInScene();
            Entity[] entities = new Entity[simObjectCount];
            
            /*for (int i = 0; i < gameObjectEntities.Length; i++)
            {
                entities[i] = world.NewEntity();
                gameObjectEntities[i].entity = entities[i];
            }

            for (int i = 0; i < gameObjectEntities.Length; i++)
            {
                gameObjectEntities[i].CreateEntity(world,entities[i]);
            }*/
        }
    }
}