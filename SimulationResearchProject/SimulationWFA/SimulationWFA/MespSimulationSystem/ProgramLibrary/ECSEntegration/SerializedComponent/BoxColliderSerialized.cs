﻿using System.Numerics;
using Dalak.Ecs;
using PhysicLibrary;
using SimulationSystem.ECSComponents;

namespace TheSimulation.SerializedComponent
{
    public class BoxColliderSerialized : SimulationSystem.ECS.Entegration.SerializedComponent
    {
        public Vector3 size;
        public override void AddComponent(Entity entity, World world)
        {
            BoxCollider boxCollider = new BoxCollider();
            (boxCollider.bound as BoxBounds).Size = size;

            entity.AddComponent<ColliderComp>() = new ColliderComp {
                collider = boxCollider,
            };
        }

        public override string GetName()
        {
            return "Box Collider Serialized";
        }
    }
}
