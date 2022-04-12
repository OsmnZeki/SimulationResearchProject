using Dalak.Ecs;
using PhysicLibrary;
using SimulationSystem.ECSComponents;

namespace TheSimulation.SerializedComponent
{
    public class BoxColliderSerialized : SimulationSystem.ECS.Entegration.SerializedComponent
    {
        public override void AddComponent(Entity entity, World world)
        {
            entity.AddComponent<ColliderComp>() = new ColliderComp {
                collider = new BoxCollider()
            };
        }

        public override string GetName()
        {
            return "BoxColliderSerialized";
        }
    }
}
