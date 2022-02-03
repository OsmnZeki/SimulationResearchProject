using System;
using RenderLibrary.IO;
using SimulationSystem.Systems;

namespace SimulationSystem
{
    public class EditorWindowSystem
    {
        
        
        public void CreateEditorWindow()
        {
            Screen screen = new Screen();
            screen.Create(800, 600);
            if (screen.screenAdress == IntPtr.Zero) return;

            SimulationLifecyleMethods editorWindowLifecycle = new SimulationLifecyleMethods(new ECSEditorController());

            editorWindowLifecycle.Awake();
            editorWindowLifecycle.Start();
            
            while (!screen.ShouldClose())
            {
                screen.ProcessWindowInput();
                editorWindowLifecycle.Update();
                editorWindowLifecycle.LateUpdate();
                
                screen.Update();
                editorWindowLifecycle.Render();
                screen.NewFrame();
            }
            
            editorWindowLifecycle.OnSimulationQuit();
            screen.Terminate();
        }
    }
}