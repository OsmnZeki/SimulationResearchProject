using System;
using System.Diagnostics;
using RenderLibrary.IO;

namespace SimulationSystem.Systems
{
    public class EcsEditorTestSystem : Dalak.Ecs.System
    {
        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                Console.WriteLine("dds");
            }
        }
    }
}