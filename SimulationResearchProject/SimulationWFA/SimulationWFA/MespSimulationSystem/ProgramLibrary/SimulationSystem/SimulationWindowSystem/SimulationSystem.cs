using System;
using RenderLibrary.IO;

namespace SimulationSystem
{
    public class SimulationSystem
    {

        public void CreateSimulationSystem()
        {
            Screen screen = new Screen();
            screen.Create(800, 600);
            if (screen.screenAdress == IntPtr.Zero) return;

            SimulationLifecyleMethods simulationEvents = new SimulationLifecyleMethods(new ECSSimulationController());

            simulationEvents.Awake();
            simulationEvents.Start();
            
            while (!screen.ShouldClose())
            {
                screen.ProcessWindowInput();
                simulationEvents.Update();
                simulationEvents.LateUpdate();
                
                screen.Update();
                simulationEvents.Render();
                screen.NewFrame();
            }
            
            simulationEvents.OnSimulationQuit();
            screen.Terminate();
        }
        
    }
}