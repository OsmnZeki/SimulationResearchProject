using RenderLibrary.Graphics.Rendering;

namespace RenderLibrary.Shaders
{
    public class UnlitShader
    {
        public Shader shader = new Shader(
                "D:/GitRepos/SimulationResearchProject/SimulationResearchProject/SimulationWFA/SimulationWFA/Assets/Shaders/object.vs",
                "D:/GitRepos/SimulationResearchProject/SimulationResearchProject/SimulationWFA/SimulationWFA/Assets/Shaders/unlit.fs");


        //TODO: grid sistemi hazır olduğu zaman aç
        /*        public Shader infiniteGridShader = new Shader(
                "D:/GitRepos/SimulationResearchProject/SimulationResearchProject/SimulationWFA/SimulationWFA/Assets/InfiniteGridShader/infiniteGrid.vs",
                "D:/GitRepos/SimulationResearchProject/SimulationResearchProject/SimulationWFA/SimulationWFA/Assets/Shaders/InfiniteGridShader/infiniteGrid.fs");*/

    }
}
