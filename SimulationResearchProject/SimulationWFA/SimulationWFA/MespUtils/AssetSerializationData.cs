using System;
using System.Numerics;
using RenderLibrary.Graphics.Rendering;
using static RenderLibrary.Graphics.Rendering.Texture;
using static RenderLibrary.Shaders.ShaderPool;

namespace SimulationWFA.MespUtils
{
    public interface IAssetSerialization
    {
        object Serialization();
        object Deserialization(object data);
    }

    public class TextureSerializationData
    {
        public string path;
        public TextureWrapType wrapSParameter;
        public TextureWrapType wrapTParameter;
        public TextureMapType textureMappingType;
    }


    public class UnlitMaterialSerializationData
    {
        public ShaderType shaderType;
        public MaterialType materialType;
        public bool transparent;
        public Vector4 color;
        public object textureData;
    }

    public class LitMaterialSerializationData
    {
        public ShaderType shaderType;
        public MaterialType materialType;
        public bool transparent;
        public Vector4 ambient;
        public Vector4 dffuse;
        public Vector4 specular;
        public float shininess;
        public object textureData;

    }

    public class MeshSerializationData
    {
        public Vector3[] verticesPos;
        public Vector3[] normalPos;
        public Vector2[] texCoord;
    }


}
