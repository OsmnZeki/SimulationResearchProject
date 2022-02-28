using System.Numerics;
using RenderLibrary.DLL;
using RenderLibrary.Shaders;
using SimulationWFA.MespUtils;

namespace RenderLibrary.Graphics.Rendering
{
    public class UnlitMaterial : Material, IAssetSerialization
    {
        public UnlitMaterial()
        {
            materialAdress = RenderProgramDLL.NewUnlitMaterial();
            materialType = MaterialType.UnlitMaterial;
            RenderProgramDLL.SetColorToMaterial(materialAdress, new float[]{1,1,1,1});
        }

        public void SetColor(Vector4 color)
        {
            float[] colorF = new[] {color.X, color.Y, color.Z, color.W};
            RenderProgramDLL.SetColorToMaterial(materialAdress,colorF);
        }

        public void AddTexture(Texture texture)
        {
            this.texture = texture;
            RenderProgramDLL.AddTextureToUnlitMaterial(materialAdress, texture.GetTextureAdress());
        }

        public Texture GetTexture()
        {
            return texture;
        }

        public object Serialization()
        {
            UnlitMaterialSerializationData serializationData = new UnlitMaterialSerializationData();
            serializationData.materialType = materialType;
            serializationData.shaderType = shaderType;
            serializationData.color = GetColor();
            serializationData.transparent = transparent;
            if (texture == null) { serializationData.textureData = null; }
            else { serializationData.textureData = texture.Serialization(); }
           

            return serializationData;
        }


        public Vector4 GetColor()
        {
            float[] colorF = new float[4];
            RenderProgramDLL.GetColorFromMaterial(materialAdress, colorF);
            return new Vector4(colorF[0], colorF[1], colorF[2], colorF[3]);
        }

        public object Deserialization(object serializationObj)
        {
            UnlitMaterialSerializationData serializationData = new UnlitMaterialSerializationData();
            serializationData = (UnlitMaterialSerializationData)serializationObj;

            UnlitMaterial unlitMaterial = new UnlitMaterial();
            unlitMaterial.SetColor(serializationData.color);
            unlitMaterial.SetShader(ShaderPool.GetShaderByType(serializationData.shaderType));
            unlitMaterial.SetTransparent(serializationData.transparent);
            if(serializationData.textureData != null)
            {

            }

            return unlitMaterial;

        }

    }
}