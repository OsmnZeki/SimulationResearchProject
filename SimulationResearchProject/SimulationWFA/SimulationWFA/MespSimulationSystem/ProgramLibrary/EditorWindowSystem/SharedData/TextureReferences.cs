using ProgramLibrary;
using RenderLibrary.Graphics.Rendering;
using static RenderLibrary.Graphics.Rendering.Texture;

namespace SimulationSystem.SharedData
{
    public class TextureReferences
    {
        public Texture grassTexture;

        public TextureReferences()
        {
            grassTexture = new Texture(SimPath.GetAssetPath + "/Images", "grass.png", Texture.TextureMapType.Diffuse);
            grassTexture.Load(true);
            grassTexture.SetWrapParameters(TextureWrapType.GL_CLAMP_TO_EDGE, TextureWrapType.GL_CLAMP_TO_EDGE);
        }

    }
}
