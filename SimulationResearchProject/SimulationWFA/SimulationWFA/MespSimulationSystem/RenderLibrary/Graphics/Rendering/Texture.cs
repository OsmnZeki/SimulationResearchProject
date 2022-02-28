using System;
using System.IO;
using RenderLibrary.DLL;
using SimulationWFA.MespUtils;

namespace RenderLibrary.Graphics.Rendering
{
    public class Texture : IAssetSerialization
    {
        protected IntPtr textureAdress;
        public string path;
        public TextureWrapType wrapSParameter;
        public TextureWrapType wrapTParameter;
        public TextureMapType textureMappingType;

        public Texture(IntPtr textureAdress)
        {
            this.textureAdress = textureAdress;
        }

        public Texture(string directory, string name, TextureMapType textureType)
        {
            path = Path.Combine("Images", name);
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

        public string GetPath()
        {
            return path;
        }

        public object Serialization()
        {
            TextureSerializationData textureSerializedData = new TextureSerializationData();

            textureSerializedData.textureMappingType = textureMappingType;
            textureSerializedData.wrapSParameter = wrapSParameter;
            textureSerializedData.wrapTParameter = wrapTParameter;
            textureSerializedData.path = path;



            return textureSerializedData;
        }

        public T Deserialization<T>(object data)
        {
            throw new NotImplementedException();
        }

        public object Deserialization(object data)
        {
            throw new NotImplementedException();
        }
    }
}