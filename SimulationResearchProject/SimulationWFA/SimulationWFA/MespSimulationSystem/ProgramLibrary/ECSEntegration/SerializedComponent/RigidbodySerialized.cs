using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using PhysicLibrary;
using SimulationSystem.ECSComponents;

namespace ECSEntegration.SerializedComponent
{
    public class RigidbodySerialized : SimulationSystem.ECS.Entegration.SerializedComponent
    {
        public Vector3 position;
        public Vector3 velocity;
        public float mass;

        public override void AddComponent(Entity entity, World world)
        {
            Rigidbody rb = new Rigidbody();
            rb.position = position;
            rb.velocity = velocity;
            rb.mass = mass;

            entity.AddComponent<RigidbodyComp>() = new RigidbodyComp {
                rigidbody = rb,
            };
        }

        public override string GetName()
        {
            return "Rigidbody Serialized";
        }
    }
}
