using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using SimulationSystem.Components;

namespace TheSimulation.SerializedComponent
{
    class CameraSerializedComponent : SimulationSystem.ECS.Entegration.SerializedComponent
    {
        public override void AddComponent(Entity entity, World world)
        {
            entity.AddComponent<CameraComp>() = new CameraComp() {
                camera = new RenderLibrary.IO.Camera(),
            };
        }

        public override string GetName()
        {
            return "Camera Serialized";
        }
    }
}
