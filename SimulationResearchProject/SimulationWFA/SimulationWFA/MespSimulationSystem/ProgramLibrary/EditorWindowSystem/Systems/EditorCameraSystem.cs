using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using RenderLibrary.IO;
using SimulationSystem.Components;
using SimulationSystem.Timer;

namespace SimulationSystem.Systems
{
    class EditorCameraSystem : Dalak.Ecs.System
    {
        readonly Filter<CameraComp> cameraFilter = null;


        public override void Update()
        {
            foreach(var c in cameraFilter)
            {
                CameraInput(cameraFilter.Get1(c).camera);
            }
        }

        public override void LateUpdate()
        {
            foreach(var c in cameraFilter)
            {
                ref var cameraComp = ref cameraFilter.Get1(c);
            }
        }

        public void CameraInput(Camera camera)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                camera.UpdateCameraPos(CameraDirection.UP, Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                camera.UpdateCameraPos(CameraDirection.DOWN, Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                camera.UpdateCameraPos(CameraDirection.RIGHT, Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                camera.UpdateCameraPos(CameraDirection.LEFT, Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.W))
            {
                camera.UpdateCameraPos(CameraDirection.FORWARD, Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S))
            {
                camera.UpdateCameraPos(CameraDirection.BACKWARD, Time.deltaTime);
            }

            double dx = Input.GetMouseDx();
            double dy = Input.GetMouseDy();

            if (dx != 0 || dy != 0)
            {
                camera.UpdataCameraDirection(dx, dy);
            }

            double scrollDy = Input.GetMouseScrolDy();

            if (scrollDy != 0)
            {
                camera.UpdateCameraZoom(scrollDy);
            }
        }
    }
}
