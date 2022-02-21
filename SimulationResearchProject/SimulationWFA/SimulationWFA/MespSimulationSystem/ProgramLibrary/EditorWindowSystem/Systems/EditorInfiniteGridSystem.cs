/*using System.Numerics;
using Dalak.Ecs;
using MESPSimulationSystem.Math;
using RenderLibrary.Graphics;
using RenderLibrary.Graphics.PreparedModels;
using RenderLibrary.Graphics.RenderData;
using RenderLibrary.Graphics.Rendering;
using RenderLibrary.Transform;
using SimulationSystem.Components;
using SimulationSystem.SharedData;

namespace SimulationSystem.Systems
{
    class EditorInfiniteGridSystem : Dalak.Ecs.System
    { 
        private Filter<CameraComp, TransformComp> cameraFilter = null;

        ShaderDatas shaderReferences;
        UnlitMaterial infiniteMaterial;
        MeshRenderer meshRenderer;
        Transform transform;

        public override void Awake()
        {
            transform = new Transform() { position = Vector3.Zero, scale = Vector3.One, rotation = Vector3.Zero};
            infiniteMaterial = new UnlitMaterial();
            infiniteMaterial.SetShader(shaderReferences.infiniteGridShader);

            meshRenderer = new MeshRenderer();
            meshRenderer.SetMesh(PlaneMesh());
            meshRenderer.SetMaterial(infiniteMaterial);
            meshRenderer.Setup();
        }

        public override void Render()
        {
            ref var cameraComp = ref cameraFilter.Get1(0);
            ref var camTransformComp = ref cameraFilter.Get2(0);

            Mat4 view = cameraComp.GetViewMatrix(camTransformComp.transform);
            Mat4 projection = cameraComp.Perspective(800f / 600f);

            shaderReferences.infiniteGridShader.Activate();
            shaderReferences.infiniteGridShader.SetMat4("view", view);
            shaderReferences.infiniteGridShader.SetMat4("projection", projection);
            meshRenderer.Render(transform);
        }

        public Mesh PlaneMesh()
        {
            Mesh infiniteGridMesh = new Mesh();

            Vector3[] gridPlanePos = new Vector3[] {
                new Vector3(1f,1f,0f),
                new Vector3(-1f,-1f,0f),
                new Vector3(-1f,1f,0f),
                new Vector3(-1f,-1f,0f),
                new Vector3(1f,1f,0f),
                new Vector3(1f,-1f,0f),
            };

            var indices = new int[6];

            for (int i = 0; i < 6; i++)
            {
                indices[i] = i;
            }
            infiniteGridMesh.SetVerticesPos(gridPlanePos);
            infiniteGridMesh.SetIndices(indices);
            return infiniteGridMesh;
        }
    }
}*/
