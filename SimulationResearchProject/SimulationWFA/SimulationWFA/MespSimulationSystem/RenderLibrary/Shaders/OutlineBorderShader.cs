using ProgramLibrary;
using RenderLibrary.Graphics.Rendering;

namespace RenderLibrary.Shaders
{
    public class OutlineBorderShader
    {
        public Shader shader = new Shader(SimPath.GetAssetPath + "/Shaders/object.vs", SimPath.GetAssetPath + "/Shaders/OutlineShader/Outline.fs");
    }
}
