using System;
using RenderLibrary.IO;
using SimulationSystem.Systems;
using SimulationSystem.Timer;
using RenderLibrary.OpenGLCustomFunctions;
using RenderLibrary.DLL;
using PhysicLibrary;

namespace SimulationSystem
{
    public class EditorWindowSystem
    {
        WindowEcsManager windowEcsManager;
        Physics physics = new Physics();

        public void CreateEditorWindow()
        {
            Screen screen = new Screen();
            screen.Create(800, 600);
            if (screen.screenAdress == IntPtr.Zero) return;
            screen.SetParameters();
            screen.SetClearColor(screen.clearColor);

            OpenGLFunctions.GLEnable(OpenGLEnum.GL_DEPTH_TEST);
            OpenGLFunctions.GLEnable(OpenGLEnum.GL_STENCIL_TEST);
            OpenGLFunctions.GLEnable(OpenGLEnum.GL_BLEND);
            OpenGLFunctions.GLBlendFunc(OpenGLEnum.GL_SRC_ALPHA, OpenGLEnum.GL_ONE_MINUS_SRC_ALPHA);


            windowEcsManager = new WindowEcsManager(new ECSEditorController(screen));
            Time.StartTimer();

            windowEcsManager.Awake();
            windowEcsManager.Start();

            while (!screen.ShouldClose())
            {
                Time.UpdateTimer();
                screen.ProcessWindowInput();

                //physic
                int physicLoopCount = physics.CalculatePhyicsLoopCount(Time.deltaTime, Time.fixedDeltaTime);
                for(int i = 0;i<physicLoopCount;i++) windowEcsManager.FixedUpdate();

                //
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