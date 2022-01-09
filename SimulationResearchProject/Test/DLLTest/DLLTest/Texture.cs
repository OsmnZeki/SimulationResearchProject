using System;

namespace DLLTest
{
    public class Texture
    {
        public IntPtr textureAdress;

        public Texture(string directory, string name, TextureType textureType)
        {
            RenderProgramDLL.NewTexture(directory, name, (int) textureType);
        }

        public enum TextureType
        {
            Ambient=0,
            Diffuse=1,
            Specular=2
        }
    }
}