using Dalak.Ecs;
using TheSimulation.SerializedComponent;

namespace SimulationSystem.Systems
{
    public class SceneLoadingSystem : Dalak.Ecs.System
    {
        public override void Awake()
        {
            var simObjects = SimObject.FindObjectsOfType<TransformSerialized>();
            Entity[] entities = new Entity[simObjects.Length];
            
            for (int i = 0; i < simObjects.Length; i++)
            {
                entities[i] = world.NewEntity();
                simObjects[i].entity = entities[i];
            }

        }
    }
}