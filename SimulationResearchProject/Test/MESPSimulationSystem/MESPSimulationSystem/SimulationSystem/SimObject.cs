using System.Collections.Generic;
using Dalak.Ecs;
using SimulationSystem.ECS.Entegration;

namespace SimulationSystem
{
    public class SimObject
    {
        public static SimObject Hiearchy = new SimObject();
        public SimObject parent;
        public List<SimObject> child;
        public List<SerializedComponent> componentList;
    }
    
}