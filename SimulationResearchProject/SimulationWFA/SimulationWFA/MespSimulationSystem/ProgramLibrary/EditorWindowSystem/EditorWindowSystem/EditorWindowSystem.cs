using System;
using System.Numerics;
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
            screen.SetClearColor(screen.clearColor);

            windowEcsManager = new WindowEcsManager(new ECSEditorController(screen));
            Time.StartTimer();

            windowEcsManager.Awake();
            windowEcsManager.Start();

            while (!screen.ShouldClose())
            {
                Time.UpdateTimer();
                screen.ProcessWindowInput();

                windowEcsManager.Update();
                windowEcsManager.LateUpdate();

                //Render
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