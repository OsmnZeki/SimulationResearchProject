using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using SimulationSystem.Components;

namespace SimulationSystem.Systems
{
    class MainCameraSystem : Dalak.Ecs.System
    {
        readonly Filter<CameraComp, TransformComp> cameraFilter = null;


        public override void LateUpdate()
        {
            foreach(var c in cameraFilter)
            {
                ref var cameraComp = ref cameraFilter.Get1(c);
                ref var tranformComp = ref cameraFilter.Get2(c);

                cameraComp.camera.cameraPos = tranformComp.transform.position;
            }
        }
    }
}
