using System;
using Dalak.Ecs;
using RenderLibrary.IO;
using SimulationSystem.Systems;
using SimulationSystem.Timer;

namespace SimulationSystem
{
    public class EditorWindowSystem
    {
        WindowEcsManager windowEcsManager;
        public void CreateEditorWindow()
        {
            Screen screen = new Screen();
            screen.Create(800, 600);
            if (screen.screenAdress == IntPtr.Zero) return;

            windowEcsManager = new WindowEcsManager(new ECSEditorController());
            Time.StartTimer();

            windowEcsManager.Awake();
            windowEcsManager.Start();

            while (!screen.ShouldClose())
            {
                Time.UpdateTimer();
                screen.ProcessWindowInput();
                windowEcsManager.Update();
                windowEcsManager.LateUpdate();

                screen.Update();
                windowEcsManager.Render();
                screen.NewFrame();
            }

            windowEcsManager.OnSimulationQuit();

            Time.StopTimer();
            screen.Terminate();
        }


    }
}