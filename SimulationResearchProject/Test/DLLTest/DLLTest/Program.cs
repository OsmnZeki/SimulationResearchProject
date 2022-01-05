using System;


namespace DLLTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IntPtr screen = RenderProgramDLL.CreateScreen();
            if (screen == IntPtr.Zero) return;

            Shader shader = new Shader("D:/GitRepos/SimulationResearchProject/SimulationResearchProject/Test/DLLTest/DLLTest/Assets/Shaders/object.vs", "D:/GitRepos/SimulationResearchProject/SimulationResearchProject/Test/DLLTest/DLLTest/Assets/Shaders/object.fs");

            while (!RenderProgramDLL.ScreenShouldClose(screen))
            {
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

            RenderProgramDLL.ScreenTerminate(screen);
        }
    }
}
