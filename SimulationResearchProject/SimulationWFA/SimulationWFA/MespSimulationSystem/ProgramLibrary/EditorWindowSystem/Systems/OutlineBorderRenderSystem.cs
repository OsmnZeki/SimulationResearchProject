using Dalak.Ecs;
using SimulationSystem.Components;
using SimulationSystem.ECSComponents;
using RenderLibrary.OpenGLCustomFunctions;
using MESPSimulationSystem.Math;
using SimulationSystem.SharedData;
using System.Numerics;

namespace SimulationSystem
{
    public class OutlineBorderRenderSystem : Dalak.Ecs.System
    {
        readonly Filter<MeshRendererComp,TransformComp, OutlineBorderRenderComp> renderFilter = null;

        private Filter<CameraComp, TransformComp> cameraFilter = null;

        ShaderDatas shaderDatas = null;

        public override void Render()
        {
            OpenGLFunctions.GLStencilOp(OpenGLEnum.GL_KEEP, OpenGLEnum.GL_KEEP, OpenGLEnum.GL_REPLACE);
            OpenGLFunctions.GLStencilFunc(OpenGLEnum.GL_ALWAYS, 1, 1);
            OpenGLFunctions.GLStencilMask(1);

            ref var cameraComp = ref cameraFilter.Get1(0);
            ref var camTransformComp = ref cameraFilter.Get2(0);

            Mat4 view = cameraComp.GetViewMatrix(camTransformComp.transform);
            Mat4 projection = cameraComp.Perspective(800f / 600f);

            shaderDatas.SetupDefaultShadersToRender(view, projection);

            shaderDatas.outLineShader.shader.Activate();
            shaderDatas.outLineShader.shader.SetMat4("view", view);
            shaderDatas.outLineShader.shader.SetMat4("projection", projection);

            foreach (var m in renderFilter)
            {
                ref var transformComp = ref renderFilter.Get2(m);
                ref var meshRendererComp = ref renderFilter.Get1(m);

                if (meshRendererComp.meshRenderer == null)
                {
                    meshRendererComp.SetMeshRenderer();
                }

                meshRendererComp.material.GetShader().Activate();
                meshRendererComp.meshRenderer.Render(transformComp.transform);

                OpenGLFunctions.GLStencilFunc(OpenGLEnum.GL_NOTEQUAL, 1, 1);
                OpenGLFunctions.GLStencilMask(0);
                OpenGLFunctions.GLDisable(OpenGLEnum.GL_DEPTH_TEST);

                var tempTransfrom = transformComp.transform;
                tempTransfrom.scale += new Vector3(0.1f,.1f,.1f);

                shaderDatas.outLineShader.shader.Activate();
                meshRendererComp.meshRenderer.Render(transformComp.transform);

                tempTransfrom.scale -= new Vector3(0.1f, .1f, .1f);

                OpenGLFunctions.GLStencilMask(1);
                OpenGLFunctions.GLStencilFunc(OpenGLEnum.GL_ALWAYS, 1, 1);
                OpenGLFunctions.GLEnable(OpenGLEnum.GL_DEPTH_TEST);
            }

            



        }

    }
}
