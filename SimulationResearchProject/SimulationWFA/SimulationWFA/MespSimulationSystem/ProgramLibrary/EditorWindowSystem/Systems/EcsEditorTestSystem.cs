using System;
using System.Diagnostics;
using System.Numerics;
using Dalak.Ecs;
using RenderLibrary.Graphics;
using RenderLibrary.Graphics.Rendering;
using RenderLibrary.IO;
using SimulationSystem.Components;
using SimulationSystem.EditorEvents;
using SimulationSystem.SharedData;
using TheSimulation.SerializedComponent;

namespace SimulationSystem.Systems
{
    public class EcsEditorTestSystem : Dalak.Ecs.System
    {
        ShaderReferences shaderReferences = null;
        Material trolMat;
        ModelLoading trolModel;

        SimObject simObj;

        public override void Awake()
        {
            var camSimObj = SimObject.NewSimObject();
            camSimObj.CreateEntity(world);
            CameraSerialized camSerialized = new CameraSerialized();
            camSimObj.AddNewSerializedComponent(world,camSerialized);
            camSimObj.AddAllComponents(world);

            var dirLightSimObj = SimObject.NewSimObject();
            dirLightSimObj.CreateEntity(world);
            DirectionalLightSerialized dirLightSerialized = new DirectionalLightSerialized();
            dirLightSerialized.ambient = new Vector4(0.1f, 0.1f, 0.1f, 1.0f);
            dirLightSerialized.diffuse = new Vector4(0.4f, 0.4f, 0.4f, 1.0f);
            dirLightSerialized.specular = new Vector4(0.75f, 0.75f, 0.75f, 1.0f);
            dirLightSimObj.AddNewSerializedComponent(world, dirLightSerialized);
            dirLightSimObj.AddAllComponents(world);


            simObj = SimObject.NewSimObject();
            simObj.CreateEntity(world);
            simObj.AddAllComponents(world);
            simObj.entity.GetComponent<TransformComp>().transform.scale = new System.Numerics.Vector3(.05f);
            simObj.entity.GetComponent<TransformComp>().transform.position = new System.Numerics.Vector3(0, 0, -10);
               

            trolModel = new ModelLoading();
            trolModel.LoadModel("C:/Unity/SimulationResearchProject/SimulationResearchProject/SimulationWFA/SimulationWFA/Assets/Models/Trol/scene.gltf");
            trolMat = trolModel.GetMaterial(0);
            trolMat.SetShader(shaderReferences.defaultLitShader);

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