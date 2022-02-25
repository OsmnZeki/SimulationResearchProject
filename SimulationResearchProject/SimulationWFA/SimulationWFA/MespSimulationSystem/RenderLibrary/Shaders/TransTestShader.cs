using ProgramLibrary;
using RenderLibrary.Graphics.Rendering;

namespace RenderLibrary.Shaders
{
    public class TransTestShader : UnlitShader
    {
        public TransTestShader()
        {
            shader = new Shader(SimPath.GetAssetPath + "/Shaders/object.vs", SimPath.GetAssetPath + "/Shaders/TransTestShader/grass.fs");;
        }
    }
}
