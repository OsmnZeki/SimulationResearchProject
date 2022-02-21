using System.Collections.Generic;
using RenderLibrary.Graphics.Rendering;
using RenderLibrary.Shaders;

namespace SimulationSystem.SharedData
{
    public class ShaderDatas
    {
        public List<LitShader> litShaders;
        public List<UnlitShader> unlitShaders;

        public ShaderDatas()
        {
            litShaders = new List<LitShader>();
            unlitShaders = new List<UnlitShader>();
        }
    }
}
