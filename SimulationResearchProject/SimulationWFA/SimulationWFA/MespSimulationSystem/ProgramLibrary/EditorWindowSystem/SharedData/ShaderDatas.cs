using System.Collections.Generic;
using MESPSimulationSystem.Math;
using RenderLibrary.Graphics.Rendering;
using RenderLibrary.Shaders;

namespace SimulationSystem.SharedData
{
    public class ShaderDatas
    {
        public List<LitShader> litShaders;
        public List<UnlitShader> unlitShaders;

        public OutlineBorderShader outLineShader;
        public ShaderDatas()
        {
            litShaders = new List<LitShader>();
            unlitShaders = new List<UnlitShader>();

            outLineShader = new OutlineBorderShader();
        }


        public void SetupDefaultShadersToRender(Mat4 view,Mat4 projection)
        {
            for (int i = 0; i < litShaders.Count; i++)
            {
                litShaders[i].shader.Activate();
                litShaders[i].shader.SetMat4("view", view);
                litShaders[i].shader.SetMat4("projection", projection);
            }

            for (int i = 0; i < unlitShaders.Count; i++)
            {
                unlitShaders[i].shader.Activate();
                unlitShaders[i].shader.SetMat4("view", view);
                unlitShaders[i].shader.SetMat4("projection", projection);
            }
        }
    }
}
