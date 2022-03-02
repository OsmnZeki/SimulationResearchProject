using ProgramLibrary;
using RenderLibrary.Graphics.Rendering;
using SimulationWFA.MespUtils;
using static RenderLibrary.Graphics.Rendering.Texture;

namespace SimulationSystem.SharedData
{
    public class TextureReferences
    {
        public Texture grassTexture;
        public Texture windowTexture;

        public TextureReferences()
        {
            grassTexture = new Texture(SimPath.ImagesPath, "grass.png", Texture.TextureMapType.Diffuse);
            grassTexture.SetWrapParameters(TextureWrapType.GL_CLAMP_TO_EDGE, TextureWrapType.GL_CLAMP_TO_EDGE);

            AssetUtils.CreateAsset(grassTexture, "grassTexture.texture");

            windowTexture = new Texture(SimPath.ImagesPath, "blending_transparent_window.png", Texture.TextureMapType.Diffuse);

            AssetUtils.CreateAsset(windowTexture, "windowTexture.texture");
        }

    }
}
