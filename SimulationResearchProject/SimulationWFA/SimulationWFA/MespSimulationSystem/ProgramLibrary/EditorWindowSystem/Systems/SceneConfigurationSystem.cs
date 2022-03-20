using System;
using System.Numerics;
using ProgramLibrary;
using RenderLibrary.Animations;
using RenderLibrary.Graphics;
using RenderLibrary.Graphics.PreparedModels;
using RenderLibrary.Graphics.RenderData;
using RenderLibrary.Graphics.Rendering;
using RenderLibrary.Shaders;
using RenderLibrary.Transform;
using SimulationSystem.Components;
using SimulationSystem.ECSComponents;
using SimulationSystem.SharedData;
using SimulationWFA.MespUtils;
using TheSimulation.SerializedComponent;

namespace SimulationSystem.Systems
{
    public class SceneConfigurationSystem : Dalak.Ecs.System
    {
        ModelPaths modelReferences = null;
        TextureReferences textureRef = null;

        public override void Awake()
        {
            CreateEditorCamera();

            CreateDirectionalLight();

            CreateFPSDisplayer();
            //CreateLambs();

            //CreateBristleback1();

            CreateGruGru();

            //CreateBristleback2();

           // CreateJunkrat();

            //sCreateDragon();

           // CreateTrol();

            //CreateWindow();

          //  CreatePlane();

           // CreateGrass();
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
            camSimObj.InjectAllSerializedComponents(world);
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
            dirLightSimObj.InjectAllSerializedComponents(world);
        }

        //Lamba entitileri
        public void CreateLambs()
        {
            Transform[] lambTransforms = new[]
            {
                new Transform(new Vector3(0.7f, 5.0f, 10.0f),new Vector3(.25f), Vector3.Zero),
                new Transform(new Vector3(2.3f, -3.3f, -4.0f),new Vector3(.25f), Vector3.Zero),
                new Transform(new Vector3(-4.0f, 2.0f, -12.0f),new Vector3(.25f), Vector3.Zero),
                new Transform(new Vector3(0.0f, 0.0f, -3.0f),new Vector3(.25f), Vector3.Zero),
            };

            SimObject[] lambSimObj = new SimObject[4];
            for (int i = 0; i < 4; i++)
            {
                lambSimObj[i] = SimObject.NewSimObject();
                lambSimObj[i].CreateEntity(world);
                lambSimObj[i].InjectAllSerializedComponents(world);
                lambSimObj[i].entity.GetComponent<TransformComp>().transform = lambTransforms[i];
                lambSimObj[i].entity.AddComponent<PointLightComp>() = new PointLightComp {
                    pointLight = new Lights.PointLight {
                        ambient = new Vector4(0.05f, 0.05f, 0.05f, 1.0f),
                        diffuse = new Vector4(0.8f, 0.8f, 0.8f, 1.0f),
                        specular = new Vector4(1.0f),
                    }
                };
                lambSimObj[i].entity.AddComponent<MeshRendererComp>() = new MeshRendererComp {
                    material = AssetUtils.LoadFromAsset<UnlitMaterial>("lambMaterial.mat"),
                    mesh = AssetUtils.LoadFromAsset<Mesh>("cube.mesh"),
                };
            }
        }

        //Human entity
        public void CreateJunkrat()
        {
            var humanModel = ModelLoader.LoadModel(modelReferences.JunkratPath);

            var rootSimObj = SimObject.NewSimObject();
            rootSimObj.CreateEntity(world);
            rootSimObj.InjectAllSerializedComponents(world);

            Animation animation = new Animation(modelReferences.JunkratPath, humanModel);
            rootSimObj.entity.AddComponent<AnimatorComp>() = new AnimatorComp() {
                animator = new Animator(animation),
            };

            rootSimObj.entity.AddComponent<SkinnedMeshRendererComp>() = new SkinnedMeshRendererComp {
                rootModel = humanModel,
            };

            ref var skinnedMeshComp = ref rootSimObj.entity.GetComponent<SkinnedMeshRendererComp>();
            skinnedMeshComp.SetMeshRenderers(world, ref rootSimObj);

            rootSimObj.GetTransform().scale = new Vector3(0.05f);
        }

        public void CreateDragon()
        {
            var humanModel = ModelLoader.LoadModel(modelReferences.DragonPath);

            var rootSimObj = SimObject.NewSimObject();
            rootSimObj.CreateEntity(world);
            rootSimObj.InjectAllSerializedComponents(world);

            Animation animation = new Animation(modelReferences.DragonPath, humanModel);
            rootSimObj.entity.AddComponent<AnimatorComp>() = new AnimatorComp() {
                animator = new Animator(animation),
            };

            SetupModel(humanModel, ref rootSimObj);


        }

        public void CreateGruGru()
        {
            var humanModel = ModelLoader.LoadModel(modelReferences.GruGruPath);

            var rootSimObj = SimObject.NewSimObject();
            rootSimObj.CreateEntity(world);
            rootSimObj.InjectAllSerializedComponents(world);

            Animation animation = new Animation(modelReferences.GruGruPath, humanModel);
            rootSimObj.entity.AddComponent<AnimatorComp>() = new AnimatorComp() {
                animator = new Animator(animation),
            };

            rootSimObj.entity.AddComponent<SkinnedMeshRendererComp>() = new SkinnedMeshRendererComp {
                rootModel = humanModel,
            };

            ref var skinnedMeshComp = ref rootSimObj.entity.GetComponent<SkinnedMeshRendererComp>();
            skinnedMeshComp.SetMeshRenderers(world, ref rootSimObj);

            rootSimObj.GetTransform().scale = new Vector3(0.05f);
        }



        public void CreateBristleback1()
        {
            var humanModel = ModelLoader.LoadModel(modelReferences.BristlebackPath);

            var rootSimObj = SimObject.NewSimObject();
            rootSimObj.CreateEntity(world);
            rootSimObj.InjectAllSerializedComponents(world);

            Animation animation = new Animation(modelReferences.BristlebackPath, humanModel);
            rootSimObj.entity.AddComponent<AnimatorComp>() = new AnimatorComp() {
                animator = new Animator(animation),
            };

            rootSimObj.entity.AddComponent<SkinnedMeshRendererComp>() = new SkinnedMeshRendererComp {
                rootModel = humanModel,
            };

            ref var skinnedMeshComp = ref rootSimObj.entity.GetComponent<SkinnedMeshRendererComp>();
            skinnedMeshComp.SetMeshRenderers(world, ref rootSimObj);

            rootSimObj.GetTransform().scale = new Vector3(0.05f);
        }

        public void CreateBristleback2()
        {
            var humanModel = ModelLoader.LoadModel(modelReferences.BristlebackPath);

            var rootSimObj = SimObject.NewSimObject();
            rootSimObj.CreateEntity(world);
            rootSimObj.InjectAllSerializedComponents(world);

            Animation animation = new Animation(modelReferences.BristlebackPath, humanModel);
            rootSimObj.entity.AddComponent<AnimatorComp>() = new AnimatorComp() {
                animator = new Animator(animation),
            };

            rootSimObj.entity.AddComponent<SkinnedMeshRendererComp>() = new SkinnedMeshRendererComp {
                rootModel = humanModel,
            };

            ref var skinnedMeshComp = ref rootSimObj.entity.GetComponent<SkinnedMeshRendererComp>();
            skinnedMeshComp.SetMeshRenderers(world, ref rootSimObj);

            rootSimObj.GetTransform().scale = new Vector3(0.05f);
            rootSimObj.GetTransform().position = new Vector3(0, 0, -10f);
        }

        //Trol entity
        public void CreateTrol()
        {

            var trolModel = ModelLoader.LoadModel(modelReferences.TrolModelPath);

            var rootSimObj = SimObject.NewSimObject();
            rootSimObj.CreateEntity(world);
            rootSimObj.InjectAllSerializedComponents(world);

            SetupModel(trolModel, ref rootSimObj);

        }

        public void SetupModel(Model rootModel, ref SimObject rootSimObj)
        {
            int meshCount = rootModel.ModelMeshCount();

            SimObject[] meshSimObj = new SimObject[meshCount];

            for (int i = 0; i < meshCount; i++)
            {
                meshSimObj[i] = SimObject.NewSimObject();
                meshSimObj[i].CreateEntity(world);
                meshSimObj[i].InjectAllSerializedComponents(world);
                meshSimObj[i].SetParent(rootSimObj);

                meshSimObj[i].GetTransform().scale = new Vector3(0.05f);

                var mesh = rootModel.GetMesh(i);
                var material = rootModel.GetMaterial(i);
                material.SetShader(ShaderPool.GetShaderByType(ShaderPool.ShaderType.LitShader));

                meshSimObj[i].entity.AddComponent<MeshRendererComp>() = new MeshRendererComp {
                    material = material,
                    mesh = mesh,
                };

            }

            int childCount = rootModel.ModelChildCount();
            for (int i = 0; i < childCount; i++)
            {
                if (meshCount > 0) SetupModel(rootModel.GetChildModel(i), ref meshSimObj[0]);
                else SetupModel(rootModel.GetChildModel(i), ref rootSimObj);

            }
        }

        public void CreatePlane()
        {
            var planeSimObj = SimObject.NewSimObject();
            planeSimObj.CreateEntity(world);
            planeSimObj.InjectAllSerializedComponents(world); ;

            ref var transform = ref planeSimObj.entity.GetComponent<TransformComp>().transform;
            transform.position = new Vector3(0, 0f, 10);
            transform.scale = new Vector3(10, .1f, 10); ;

            planeSimObj.entity.AddComponent<MeshRendererComp>() = new MeshRendererComp {
                material = AssetUtils.LoadFromAsset<LitMaterial>("groundMaterial.mat"),
                mesh = AssetUtils.LoadFromAsset<Mesh>("cube.mesh"),
            };

            //planeSimObj.entity.AddComponent<OutlineBorderRenderComp>();
        }

        public void CreateGrass()
        {
            var grassObj = SimObject.NewSimObject();
            grassObj.CreateEntity(world);
            grassObj.InjectAllSerializedComponents(world);

            ref var transform = ref grassObj.entity.GetComponent<TransformComp>().transform;
            transform.position = new Vector3(0, .55f, 10);
            transform.scale = new Vector3(1, 1f, 1); ;

            grassObj.entity.AddComponent<MeshRendererComp>() = new MeshRendererComp {
                material = AssetUtils.LoadFromAsset<UnlitMaterial>("grassMaterial.mat"),
                mesh = AssetUtils.LoadFromAsset<Mesh>("quad.mesh"),
            };

        }

        public void CreateWindow()
        {
            var windowObj = SimObject.NewSimObject();
            windowObj.CreateEntity(world);
            windowObj.InjectAllSerializedComponents(world);

            ref var transform = ref windowObj.entity.GetComponent<TransformComp>().transform;
            transform.position = new Vector3(0, .55f, 8);
            transform.scale = new Vector3(1, 1f, 1); ;

            windowObj.entity.AddComponent<MeshRendererComp>() = new MeshRendererComp {
                material = AssetUtils.LoadFromAsset<UnlitMaterial>("WindowMaterial.mat"),
                mesh = AssetUtils.LoadFromAsset<Mesh>("quad.mesh"),
            };
        }

        public void CreateFPSDisplayer()
        {
            var simObj = SimObject.NewSimObject();
            simObj.CreateEntity(world);
            simObj.InjectAllSerializedComponents(world);

            simObj.entity.AddComponent<TextRendererComp>() = new TextRendererComp {
                color = new Vector3(1, 1, 1),
                UIPosition = new Vector2(0, 500),
                scale = 30,
                text = "FPS: ",
            };

            simObj.entity.AddComponent<FPSDisplayerComp>();
        }
    }
}