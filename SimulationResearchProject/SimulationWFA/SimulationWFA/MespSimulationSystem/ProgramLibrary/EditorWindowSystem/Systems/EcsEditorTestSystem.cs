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
        Material trolMat;
        ModelLoading trolModel;

        SimObject simObj;

        public override void Awake()
        {
            var camSimObj = SimObject.NewSimObject();
            camSimObj.CreateEntity(world);
            CameraSerializedComponent camSerialized = new CameraSerializedComponent();
            camSimObj.AddNewSerializedComponent(world,camSerialized);
            camSimObj.AddAllComponents(world);

            simObj = SimObject.NewSimObject();
            simObj.CreateEntity(world);
            simObj.AddAllComponents(world);
            simObj.entity.GetComponent<TransformComp>().transform.scale = new System.Numerics.Vector3(.05f);
            simObj.entity.GetComponent<TransformComp>().transform.position = new System.Numerics.Vector3(0, 0, -10);
            

            shader = new Shader(
                "C:/Unity/SimulationResearchProject/SimulationResearchProject/Test/DLLTest/DLLTest/Assets/Shaders/object.vs",
                "C:/Unity/SimulationResearchProject/SimulationResearchProject/Test/DLLTest/DLLTest/Assets/Shaders/lit.fs");
     

            trolModel = new ModelLoading();
            trolModel.LoadModel("C:/Unity/SimulationResearchProject/SimulationResearchProject/SimulationWFA/SimulationWFA/Assets/Models/Trol/scene.gltf");
            trolMat = trolModel.GetMaterial(0);
            trolMat.SetShader(shader);

        }


        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                simObj.entity.AddComponent < MeshRendererComp >() = new MeshRendererComp {
                    material = trolMat,
                    mesh = trolModel.GetMesh(0),
                };

                simObj.entity.GetComponent<MeshRendererComp>().SetMeshRenderer();
            }
        }
    }
}