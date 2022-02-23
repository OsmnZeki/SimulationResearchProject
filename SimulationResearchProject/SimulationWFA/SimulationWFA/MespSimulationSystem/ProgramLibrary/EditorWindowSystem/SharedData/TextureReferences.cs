using ProgramLibrary;
using RenderLibrary.Graphics.Rendering;

namespace SimulationSystem.SharedData
{
    public class TextureReferences
    {
        public Texture grassTexture = new Texture(SimPath.GetAssetPath + "/Images", "grass.png", Texture.TextureType.Diffuse);
    }
}
