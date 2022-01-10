using System;
using System.Numerics;


namespace DLLTest
{
    class Program
    {
        
       
        
        static void Main(string[] args)
        {
            IntPtr screen = RenderProgramDLL.CreateScreen();
            if (screen == IntPtr.Zero) return;

            Shader shader = new Shader("C:/Unity/SimulationResearchProject/SimulationResearchProject/Test/DLLTest/DLLTest/Assets/Shaders/object.vs", "C:/Unity/SimulationResearchProject/SimulationResearchProject/Test/DLLTest/DLLTest/Assets/Shaders/object.fs");
            Shader lampShader = new Shader("C:/Unity/SimulationResearchProject/SimulationResearchProject/Test/DLLTest/DLLTest/Assets/Shaders/object.vs" , "C:/Unity/SimulationResearchProject/SimulationResearchProject/Test/DLLTest/DLLTest/Assets/Shaders/lamp.fs");
            
            Cube cube = new Cube();
            
            double deltaTime = 0.0f;
            double lastFrame = 0.0f;
            while (!RenderProgramDLL.ScreenShouldClose(screen))
            {
                double currentTime = DateTime.Now.Millisecond;
                deltaTime = currentTime - lastFrame;
                lastFrame = currentTime;
                
                RenderProgramDLL.ScreenProcessInput(screen);
                
                if (Input.GetMouseKeyDown(2))
                {
                    Console.WriteLine("Pressed D");
                }
                
                // render
                // ------
                RenderProgramDLL.ScreenUpdate(screen);
                
                
                
                RenderProgramDLL.ScreenNewFrame(screen);
            }
            
            cube.CleanUp();
            
            RenderProgramDLL.ScreenTerminate(screen);
        }
    }
}
