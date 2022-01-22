using System;
using System.Diagnostics;
using System.Numerics;
using MESPLibrary.MESPMath;
using MESPSimulation.Graphics.Model;
using MESPSimulation.Graphics.Objects;
using MESPSimulation.Graphics.Rendering;
using MESPSimulation.IO;
using MESPSimulation.Window;


namespace DLLTest
{
    class Program
    {
        static float deltaTime = 0.0f;
        static float lastFrame = 0.0f;

        static bool spotLightOn;

        static void Main(string[] args)
        {
            Screen screen = new Screen();
            screen.Create(800, 600);
            if (screen.screenAdress == IntPtr.Zero) return;

            Shader shader = new Shader(
                "D:/GitRepos/SimulationResearchProject/SimulationResearchProject/Test/DLLTest/DLLTest/Assets/Shaders/object.vs",
                "D:/GitRepos/SimulationResearchProject/SimulationResearchProject/Test/DLLTest/DLLTest/Assets/Shaders/object.fs");

            Shader lampShader = new Shader(
                "D:/GitRepos/SimulationResearchProject/SimulationResearchProject/Test/DLLTest/DLLTest/Assets/Shaders/object.vs",
                "D:/GitRepos/SimulationResearchProject/SimulationResearchProject/Test/DLLTest/DLLTest/Assets/Shaders/lamp.fs");

            ModelLoading trolModel = new ModelLoading();
            trolModel.LoadModel("D:/GitRepos/SimulationResearchProject/SimulationResearchProject/Test/DLLTest/DLLTest/Assets/Models/Trol/scene.gltf");

            Camera camera = new Camera(new Vector3(0.0f, 0.0f, 3.0f));

            Lights.DirectionalLight dirLight = new Lights.DirectionalLight()
            {
                direction = new Vector3(-0.2f, -1.0f, -0.3f),
                ambient = new Vector4(0.1f, 0.1f, 0.1f, 1.0f),
                diffuse = new Vector4(0.4f, 0.4f, 0.4f, 1.0f),
                specular = new Vector4(0.75f, 0.75f, 0.75f, 1.0f)
            };

            Vector3[] pointLightPositions =
            {
                new Vector3(0.7f, 0.2f, 2.0f),
                new Vector3(2.3f, -3.3f, -4.0f),
                new Vector3(-4.0f, 2.0f, -12.0f),
                new Vector3(0.0f, 0.0f, -3.0f),
            };

            Lamp[] lamps = new Lamp[4];
            for (int i = 0; i < 4; i++)
            {
                lamps[i] = new Lamp(new Vector3(1.0f), new Lights.PointLight()
                {
                    ambient = new Vector4(0.05f, 0.05f, 0.05f, 1.0f),
                    diffuse = new Vector4(0.8f, 0.8f, 0.8f, 1.0f),
                    specular = new Vector4(1.0f),
                    position = pointLightPositions[i]
                });
                lamps[i].SetPosAndSize(pointLightPositions[i], new Vector3(.25f));
                lamps[i].Initialize();
            }

            spotLightOn = true;
            Lights.SpotLight spotLight = new Lights.SpotLight()
            {
                position = camera.cameraPos,
                direction = camera.cameraFront,
                cutOff = MathF.Cos((float)MathFunctions.ConvertToRadians(5f)),
                outerCutOff = MathF.Cos((float)MathFunctions.ConvertToRadians(10f)),
                ambient = new Vector4(0.0f, 0.0f, 0.0f, 1.0f),
                diffuse = Vector4.One,
                specular = Vector4.One,
            };

            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (!screen.ShouldClose())
            {
                float currentTime = sw.ElapsedMilliseconds / 1000f;
                deltaTime = currentTime - lastFrame;
                lastFrame = currentTime;

                screen.ProcessWindowInput();
                ProcessInput(deltaTime, camera);
                if (Input.GetKeyDown(KeyCode.M))
                {
                    Console.WriteLine("Pressed D");
                }

                // render
                // ------
                screen.Update();

                shader.Activate();
                shader.Set3Float("viewPos", camera.cameraPos);

                dirLight.direction = MathFunctions.Rotate(new Mat4(1f), 0.5f, new Vector3(1.0f, 0, 0),
                    dirLight.direction);
                dirLight.Render(shader);

                if (spotLightOn)
                {
                    spotLight.position = camera.cameraPos;
                    spotLight.direction = camera.cameraFront;
                    spotLight.Render(shader, 0);
                    shader.SetInt("numbSpotLights", 1);
                }
                else
                {
                    shader.SetInt("numbSpotLights", 0);
                }

                for (int i = 0; i < 4; i++)
                {
                    lamps[i].pointLight.Render(shader, i);
                }
                shader.SetInt("numbPointLights", 4);

                Mat4 view = camera.GetViewMatrix();
                Mat4 projection = camera.Perspective(800f / 600f);
                shader.SetMat4("view", view);
                shader.SetMat4("projection", projection);

                //cube.Render(shader);
                trolModel.Render(shader);

                lampShader.Activate();
                lampShader.SetMat4("view", view);
                lampShader.SetMat4("projection", projection);
                for (int i = 0; i < 4; i++)
                {
                    lamps[i].Render(lampShader);
                }

                screen.NewFrame();
            }

            sw.Stop();

            trolModel.CleanUp();
            //cube.CleanUp();
            for (int i = 0; i < 4; i++)
            {
                lamps[i].CleanUp();
            }

            screen.Terminate();
        }


        public static void ProcessInput(double dt, Camera camera)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                spotLightOn = !spotLightOn;
            }

            /*if (KeyboardInput::KeyWentDown(GLFW_KEY_TAB))
            {
                activeCamera += (activeCamera == 0) ? 1 : -1;
            }*/

            if (Input.GetKey(KeyCode.Space))
            {
                camera.UpdateCameraPos(CameraDirection.UP, dt);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                camera.UpdateCameraPos(CameraDirection.DOWN, dt);
            }

            if (Input.GetKey(KeyCode.D))
            {
                camera.UpdateCameraPos(CameraDirection.RIGHT, dt);
            }

            if (Input.GetKey(KeyCode.A))
            {
                camera.UpdateCameraPos(CameraDirection.LEFT, dt);
            }

            if (Input.GetKey(KeyCode.W))
            {
                camera.UpdateCameraPos(CameraDirection.FORWARD, dt);
            }

            if (Input.GetKey(KeyCode.S))
            {
                camera.UpdateCameraPos(CameraDirection.BACKWARD, dt);
            }

            double dx = Input.GetMouseDx();
            double dy = Input.GetMouseDy();

            if (dx != 0 || dy != 0)
            {
                camera.UpdataCameraDirection(dx, dy);
            }

            double scrollDy = Input.GetMouseScrolDy();

            if (scrollDy != 0)
            {
                camera.UpdateCameraZoom(scrollDy);
            }
        }
    }
}