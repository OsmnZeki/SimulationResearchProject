using System;
using System.Numerics;
using ProgramLibrary;
using RenderLibrary.Graphics;
using RenderLibrary.Graphics.PreparedModels;
using RenderLibrary.Graphics.Rendering;
using RenderLibrary.Shaders;
using RenderLibrary.Transform;
using SimulationSystem.Components;
using SimulationSystem.ECSComponents;
using SimulationSystem.SharedData;
using TheSimulation.SerializedComponent;

namespace SimulationSystem.Systems
{
    public class SceneConfigurationSystem : Dalak.Ecs.System
    {
        SceneShaderManager sceneShaderManager = null;
        ModelPaths modelReferences = null;
        TextureReferences textureRef = null;

        public override void Awake()
        {
            textureRef.grassTexture.Load(true);

            CreateEditorCamera();

            CreateDirectionalLight();

            CreateLambs();

            CreateTrol();

            CreatePlane();

            CreateGrass();
        }

        //Camera entity
        public void CreateEditorCamera()
        {
            var camSimObj = SimObject.NewSimObject();
            camSimObj.CreateEntity(world);
            CameraSerialized camSerialized = new CameraSerialized() {
                speed = 2.5f,
                zoom = 45f,
                near = 0.1f,
                far = 100f,
            };
            camSimObj.AddNewSerializedComponent(world, camSerialized);
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
        }

        //Directional light entity
        public void CreateDirectionalLight()
        {
            var dirLightSimObj = SimObject.NewSimObject();
            dirLightSimObj.CreateEntity(world);
            DirectionalLightSerialized dirLightSerialized = new DirectionalLightSerialized();
            dirLightSerialized.ambient = new Vector4(0.1f, 0.1f, 0.1f, 1.0f);
            dirLightSerialized.diffuse = new Vector4(0.4f, 0.4f, 0.4f, 1.0f);
            dirLightSerialized.specular = new Vector4(0.75f, 0.75f, 0.75f, 1.0f);
            dirLightSimObj.AddNewSerializedComponent(world, dirLightSerialized);
            dirLightSimObj.AddAllComponents(world);
        }

        //Lamba entitileri
        public void CreateLambs()
        {
            UnlitShader lambShader = new UnlitShader();
            sceneShaderManager.unlitShaders.Add(lambShader);

            UnlitMaterial unlitMaterial = new UnlitMaterial();
            unlitMaterial.SetColor(Vector4.One);
            unlitMaterial.SetShader(lambShader.shader);


            Transform[] lambTransforms = new[]
            {
                new Transform(new Vector3(0.7f, 5.0f, 10.0f),new Vector3(.25f), Vector3.Zero),
                new Transform(new Vector3(2.3f, -3.3f, -4.0f),new Vector3(.25f), Vector3.Zero),
                new Transform(new Vector3(-4.0f, 2.0f, -12.0f),new Vector3(.25f), Vector3.Zero),
                new Transform(new Vector3(0.0f, 0.0f, -3.0f),new Vector3(.25f), Vector3.Zero),
            };

            CubeMesh cubeModel = new CubeMesh();
            SimObject[] lambSimObj = new SimObject[4];
            for (int i = 0; i < 4; i++)
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
        }

        //Trol entity
        public void CreateTrol()
        {
            var simObj = SimObject.NewSimObject();
            simObj.CreateEntity(world);
            simObj.AddAllComponents(world);
            simObj.entity.GetComponent<TransformComp>().transform.scale = new System.Numerics.Vector3(.05f);
            simObj.entity.GetComponent<TransformComp>().transform.position = new System.Numerics.Vector3(0, 0, -10);
            simObj.entity.GetComponent<TransformComp>().transform.rotation = new System.Numerics.Vector3(0, 0, 0);

            LitShader trolShader = new LitShader();
            sceneShaderManager.litShaders.Add(trolShader);

            var trolModel = new ModelLoading();
            trolModel.LoadModel(modelReferences.TrolModelPath);
            var trolMat = trolModel.GetMaterial(0);
            trolMat.SetShader(trolShader.shader);

            simObj.entity.AddComponent<MeshRendererComp>() = new MeshRendererComp {
                material = trolMat,
                mesh = trolModel.GetMesh(0),
            };
        }

        public void CreatePlane()
        {
            var planeSimObj = SimObject.NewSimObject();
            planeSimObj.CreateEntity(world);
            planeSimObj.AddAllComponents(world);

            var groundShader = new LitShader();
            sceneShaderManager.litShaders.Add(groundShader);

            CubeMesh cubeModel = new CubeMesh();
            LitMaterial groundMaterial = LitMaterial.gold;
            groundMaterial.SetShader(groundShader.shader);

            ref var transform = ref planeSimObj.entity.GetComponent<TransformComp>().transform;
            transform.position = new Vector3(0, 0f, 10);
            transform.scale = new Vector3(10, .1f, 10); ;

            planeSimObj.entity.AddComponent<MeshRendererComp>() = new MeshRendererComp {
                material = groundMaterial,
                mesh = cubeModel,
            };

            //planeSimObj.entity.AddComponent<OutlineBorderRenderComp>();
        }

        public void CreateGrass()
        {
            var grassObj = SimObject.NewSimObject();
            grassObj.CreateEntity(world);
            grassObj.AddAllComponents(world);

            var grassShader = new TransTestShader();
            sceneShaderManager.unlitShaders.Add(grassShader);

            QuadPlane quaModel = new QuadPlane();
            UnlitMaterial grassMaterial = new UnlitMaterial();
            grassMaterial.AddTexture(textureRef.grassTexture);
            grassMaterial.SetShader(grassShader.shader);

            ref var transform = ref grassObj.entity.GetComponent<TransformComp>().transform;
            transform.position = new Vector3(0, .55f, 10);
            transform.scale = new Vector3(1, 1f, 1); ;

            grassObj.entity.AddComponent<MeshRendererComp>() = new MeshRendererComp {
                material = grassMaterial,
                mesh = quaModel,
            };

        }
    }
}