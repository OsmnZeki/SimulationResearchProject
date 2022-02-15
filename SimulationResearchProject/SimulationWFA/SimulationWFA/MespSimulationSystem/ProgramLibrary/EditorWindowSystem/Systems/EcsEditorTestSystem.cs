using System;
using System.Diagnostics;
using Dalak.Ecs;
using RenderLibrary.Graphics;
using RenderLibrary.Graphics.Rendering;
using RenderLibrary.IO;
using SimulationSystem.Components;
using SimulationSystem.EditorEvents;
using TheSimulation.SerializedComponent;

namespace SimulationSystem.Systems
{
    public class EcsEditorTestSystem : Dalak.Ecs.System
    {
        readonly Filter<TransformComp> transformFilter = null;

        Shader shader;
        LitMaterial litMaterial;
        ModelLoading trolModel;

        public override void Awake()
        {
            var simObj = SimObject.NewSimObject();
            EditorEventListenSystem.eventManager.SendEvent(new OnEditorCreateSimObjEvent() { simObject = simObj });

            shader = new Shader(
                "C:/Unity/SimulationResearchProject/SimulationResearchProject/Test/DLLTest/DLLTest/Assets/Shaders/object.vs",
                "C:/Unity/SimulationResearchProject/SimulationResearchProject/Test/DLLTest/DLLTest/Assets/Shaders/lit.fs");

            litMaterial = new LitMaterial();
            litMaterial.SetShader(shader);

            trolModel = new ModelLoading();
            trolModel.LoadModel("C:/Unity/SimulationResearchProject/SimulationResearchProject/SimulationWFA/SimulationWFA/Assets/Models/Trol/scene.gltf");

            MeshRendererSerialized meshRendererSerialized = new MeshRendererSerialized {
                mesh = trolModel.GetMesh(0),
                material = trolModel.GetMaterial(0),
            };

        }


        public override void Update()
        {
            Console.WriteLine(transformFilter.NumberOfEntities);
            if (Input.GetKeyDown(KeyCode.D))
            {
                
            }
        }
    }
}