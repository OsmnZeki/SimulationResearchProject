using System;
using RenderLibrary.IO;
using SimulationSystem.Systems;
using SimulationSystem.Timer;
using RenderLibrary.OpenGLCustomFunctions;

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
            screen.SetParameters();
            screen.SetClearColor(screen.clearColor);

            OpenGLFunctions.GLEnable(OpenGLEnum.GL_DEPTH_TEST);
            OpenGLFunctions.GLEnable(OpenGLEnum.GL_STENCIL_TEST);
            OpenGLFunctions.GLStencilMask(0);
            
            

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