﻿using System;
using System.Diagnostics;
using System.Numerics;
using Dalak.Ecs;
using MESPSimulationSystem.Math;
using RenderLibrary.Graphics;
using RenderLibrary.Graphics.PreparedModels;
using RenderLibrary.Graphics.Rendering;
using RenderLibrary.IO;
using RenderLibrary.Transform;
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
            //Camera entity
            var camSimObj = SimObject.NewSimObject();
            camSimObj.CreateEntity(world);
            CameraSerialized camSerialized = new CameraSerialized() {
                speed = 2.5f,
                zoom = 45f,
            };
            camSimObj.AddNewSerializedComponent(world,camSerialized);
            camSimObj.AddAllComponents(world);
            camSimObj.entity.AddComponent<SpotLightComp>() = new SpotLightComp() {
                spotLight = new Lights.SpotLight() {
                    cutOff = 5f,
                    outerCutOff = 10f,
                    ambient = new Vector4(0.0f, 0.0f, 0.0f, 1.0f),
                    diffuse = Vector4.One,
                    specular = Vector4.One,
                },
            };
            
            //Directional light entity
            var dirLightSimObj = SimObject.NewSimObject();
            dirLightSimObj.CreateEntity(world);
            DirectionalLightSerialized dirLightSerialized = new DirectionalLightSerialized();
            dirLightSerialized.ambient = new Vector4(0.1f, 0.1f, 0.1f, 1.0f);
            dirLightSerialized.diffuse = new Vector4(0.4f, 0.4f, 0.4f, 1.0f);
            dirLightSerialized.specular = new Vector4(0.75f, 0.75f, 0.75f, 1.0f);
            dirLightSimObj.AddNewSerializedComponent(world, dirLightSerialized);
            dirLightSimObj.AddAllComponents(world);

            //Lamba entitileri
            UnlitMaterial unlitMaterial = new UnlitMaterial();
            unlitMaterial.SetColor(Vector4.One);
            unlitMaterial.SetShader(shaderReferences.defaultUnlitShader);
            Transform[] lambTransforms = new[]
            {
                new Transform(new Vector3(0.7f, 0.2f, 2.0f),new Vector3(.25f), Vector3.Zero),
                new Transform(new Vector3(2.3f, -3.3f, -4.0f),new Vector3(.25f), Vector3.Zero),
                new Transform(new Vector3(-4.0f, 2.0f, -12.0f),new Vector3(.25f), Vector3.Zero),
                new Transform(new Vector3(0.0f, 0.0f, -3.0f),new Vector3(.25f), Vector3.Zero),
            };

            CubeMesh cubeModel = new CubeMesh();
            SimObject[] lambSimObj = new SimObject[4];
            for(int i = 0; i < 4; i++)
            {
                lambSimObj[i] = SimObject.NewSimObject();
                lambSimObj[i].CreateEntity(world);
                lambSimObj[i].AddAllComponents(world);
                lambSimObj[i].entity.GetComponent<TransformComp>().transform = lambTransforms[i];
                lambSimObj[i].entity.AddComponent<PointLightComp>() = new PointLightComp {
                    pointLight = new Lights.PointLight {
                        ambient = new Vector4(0.05f, 0.05f, 0.05f, 1.0f),
                        diffuse = new Vector4(0.8f, 0.8f, 0.8f, 1.0f),
                        specular = new Vector4(1.0f),
                    }
                };
                lambSimObj[i].entity.AddComponent<MeshRendererComp>() = new MeshRendererComp {
                    material = unlitMaterial,
                    mesh = cubeModel,
                };
            }

            //Trol entity
            simObj = SimObject.NewSimObject();
            simObj.CreateEntity(world);
            simObj.AddAllComponents(world);
            simObj.entity.GetComponent<TransformComp>().transform.scale = new System.Numerics.Vector3(.05f);
            simObj.entity.GetComponent<TransformComp>().transform.position = new System.Numerics.Vector3(0, 0, -10);
            simObj.entity.GetComponent<TransformComp>().transform.rotation = new System.Numerics.Vector3(0, 0, 0);
            
            trolModel = new ModelLoading();
            trolModel.LoadModel("C:/Unity/SimulationResearchProject/SimulationResearchProject/SimulationWFA/SimulationWFA/Assets/Models/Trol/scene.gltf");
            trolMat = trolModel.GetMaterial(0);
            trolMat.SetShader(shaderReferences.defaultLitShader);

            simObj.entity.AddComponent<MeshRendererComp>() = new MeshRendererComp {
                material = trolMat,
                mesh = trolModel.GetMesh(0),
            };
        }


        public override void Update()
        {
            
        }
    }
}