using System;
using RenderLibrary.DLL;

namespace RenderLibrary.Graphics.Rendering
{
    public class Texture
    {
        public IntPtr textureAdress;

        public Texture(string directory, string name, TextureMapType textureType)
        {
            textureAdress = RenderProgramDLL.NewTexture(directory, name, (int) textureType);
        }

        public void Load(bool flip)
        {
            RenderProgramDLL.TextureLoad(textureAdress, flip);
        }

        public void SetWrapParameters(TextureWrapType wrapSParameter, TextureWrapType wrapTParameter)
        {
            RenderProgramDLL.TextureSetWrapParameters(textureAdress,(int)wrapSParameter,  (int)wrapTParameter);
        }

        public enum TextureMapType
        {
            Ambient=0,
            Diffuse=1,
            Specular=2
        }

        public enum TextureWrapType
        {
            GL_REPEAT = 0x2901,
            GL_CLAMP_TO_EDGE = 0x812F,
        }

    }
}