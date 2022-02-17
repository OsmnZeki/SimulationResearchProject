using RenderLibrary.Graphics.Rendering;

namespace SimulationSystem.SharedData
{
    public class ShaderReferences
    {
        public Shader defaultLitShader = new Shader(
                "C:/Unity/SimulationResearchProject/SimulationResearchProject/SimulationWFA/SimulationWFA/Assets/Shaders/object.vs",
                "C:/Unity/SimulationResearchProject/SimulationResearchProject/SimulationWFA/SimulationWFA/Assets/Shaders/lit.fs");

        public Shader defaultUnlitShader = new Shader(
                "C:/Unity/SimulationResearchProject/SimulationResearchProject/SimulationWFA/SimulationWFA/Assets/Shaders/object.vs",
                "C:/Unity/SimulationResearchProject/SimulationResearchProject/SimulationWFA/SimulationWFA/Assets/Shaders/unlit.fs");
    }
}
