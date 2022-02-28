using System.Numerics;
using RenderLibrary.Graphics.Rendering;
using static RenderLibrary.Graphics.Rendering.Texture;
using static RenderLibrary.Shaders.ShaderPool;

namespace SimulationWFA.MespUtils
{
    public interface IAssetSerialization<T>
    {
        T Serialization();
    }

    public struct TextureSerializationData
    {
        public TextureWrapType wrapSParameter;
        public TextureWrapType wrapTParameter;
        public TextureMapType textureMappingType;
    }

    public struct UnlitMaterialSerializationData
    {
        public ShaderType shaderType;
        public MaterialType materialType;
        public bool transparent;
        public Vector4 color;
        public Texture texture;

    }


}
