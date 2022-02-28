using System;
using RenderLibrary.DLL;
using SimulationWFA.MespUtils;

namespace RenderLibrary.Graphics.Rendering
{
    public class Texture : IAssetSerialization<TextureSerializationData>
    {
        protected IntPtr textureAdress;
        public TextureWrapType wrapSParameter;
        public TextureWrapType wrapTParameter;
        public TextureMapType textureMappingType;

        public Texture(IntPtr textureAdress)
        {
            this.textureAdress = textureAdress;
        }

        public Texture(string directory, string name, TextureMapType textureType)
        {
            textureMappingType = textureType;
            textureAdress = RenderProgramDLL.NewTexture(directory, name, (int) textureType);
            Load(true);
        }

        private void Load(bool flip)
        {
            RenderProgramDLL.TextureLoad(textureAdress, flip);
        }

        public void SetWrapParameters(TextureWrapType wrapSParameter, TextureWrapType wrapTParameter)
        {
            this.wrapSParameter = wrapSParameter;
            this.wrapTParameter = wrapTParameter;
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

        public IntPtr GetTextureAdress()
        {
            return textureAdress;
        }

        public TextureSerializationData Serialization()
        {
            TextureSerializationData textureSerializedData = new TextureSerializationData();

            textureSerializedData.textureMappingType = textureMappingType;
            textureSerializedData.wrapSParameter = wrapSParameter;
            textureSerializedData.wrapTParameter = wrapTParameter;

            return textureSerializedData;
        }
    }
}