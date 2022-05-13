using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;

namespace SimulationWFA.SimulationAlgorithms.AStar
{
    public class ObstacleSerialized : SimulationSystem.ECS.Entegration.SerializedComponent
    {
        public override void AddComponent(Entity entity, World world)
        {
            entity.AddComponent<ObstacleComp>();
        }

        public override string GetName()
        {
            return "Obstacle Serialized";
        }
    }

    public struct ObstacleComp
    {
    }
}
