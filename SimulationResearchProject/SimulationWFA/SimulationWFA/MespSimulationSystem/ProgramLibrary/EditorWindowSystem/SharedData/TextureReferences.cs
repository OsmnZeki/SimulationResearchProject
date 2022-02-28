using ProgramLibrary;
using RenderLibrary.Graphics.Rendering;
using static RenderLibrary.Graphics.Rendering.Texture;

namespace SimulationSystem.SharedData
{
    public class TextureReferences
    {
        public Texture grassTexture;
        public Texture windowTexture;

        public TextureReferences()
        {
            grassTexture = new Texture(SimPath.GetAssetPath + "/Images", "grass.png", Texture.TextureMapType.Diffuse);
            grassTexture.SetWrapParameters(TextureWrapType.GL_CLAMP_TO_EDGE, TextureWrapType.GL_CLAMP_TO_EDGE);

            windowTexture = new Texture(SimPath.GetAssetPath + "/Images", "blending_transparent_window.png", Texture.TextureMapType.Diffuse);
        }

    }
}
