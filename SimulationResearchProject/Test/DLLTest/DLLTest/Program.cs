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

            Shader shader = new Shader("D:/GitRepos/SimulationResearchProject/SimulationResearchProject/Test/DLLTest/DLLTest/Assets/Shaders/object.vs", "D:/GitRepos/SimulationResearchProject/SimulationResearchProject/Test/DLLTest/DLLTest/Assets/Shaders/object.fs");
            Model model = new Model();
            model.LoadModelWithAssimp("D:/GitRepos/SimulationResearchProject/SimulationResearchProject/Test/DLLTest/DLLTest/Assets/Models/Trol/scene.gltf");
            
            Lights.DirectionalLight dirLight = new Lights.DirectionalLight()
            {
                direction = new Vector3(-0.2f, -1.0f, -0.3f), 
                ambient = new Vector4(0.1f,0.1f,0.1f,1.0f), 
                diffuse = new Vector4(0.4f,0.4f,0.4f,1.0f), 
                specular = new Vector4(0.75f,0.75f,0.75f,1.0f)
            };
            
            Vector3[] pointLightPositions = {
                new (0.7f, 0.2f, 2.0f),
                new(2.3f, -3.3f, -4.0f),
                new(-4.0f, 2.0f, -12.0f),
                new(0.0f, 0.0f, -3.0f),
            };
            
            
            
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
                
                model.Render(shader);
                RenderProgramDLL.ScreenNewFrame(screen);
            }

            model.CleanUp();
            
            RenderProgramDLL.ScreenTerminate(screen);
        }
    }
}
