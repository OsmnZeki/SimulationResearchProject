using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using SimulationSystem.ECSComponents;

namespace SimulationWFA.SimulationAlgorithms.AStar
{
    class ObstacleInitializeSystem : Dalak.Ecs.System
    {
        readonly Filter<ObstacleComp, ColliderComp> obstacleFilter = null;

        public override void Awake()
        {
            foreach(var o in obstacleFilter)
            {
                ref var colliderComp = ref obstacleFilter.Get2(o);
                colliderComp.collider.physicsLayer = PhysicLibrary.PhysicsLayer.unwalkableLayer;
            }
        }

    }
}
