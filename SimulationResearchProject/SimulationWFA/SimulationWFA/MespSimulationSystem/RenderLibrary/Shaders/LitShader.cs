using ProgramLibrary;
using RenderLibrary.Graphics.Rendering;

namespace RenderLibrary.Shaders
{
    public class LitShader
    {
        public Shader shader;

        public LitShader()
        {
            shader = new Shader(SimPath.GetAssetPath + "/Shaders/object.vs", SimPath.GetAssetPath + "/Shaders/lit.fs");
        }

    }

}
