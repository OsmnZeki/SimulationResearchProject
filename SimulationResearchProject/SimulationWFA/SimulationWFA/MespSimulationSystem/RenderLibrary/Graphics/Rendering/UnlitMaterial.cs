using System.Numerics;
using RenderLibrary.DLL;
using SimulationWFA.MespUtils;

namespace RenderLibrary.Graphics.Rendering
{
    public class UnlitMaterial : Material, IAssetSerialization<UnlitMaterialSerializationData>
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
            RenderProgramDLL.AddTextureToUnlitMaterial(materialAdress, texture.GetTextureAdress());
        }

        public Texture GetTexture()
        {
            return new Texture(RenderProgramDLL.GetTextureFromUnlitMaterial(materialAdress));
        }

        public UnlitMaterialSerializationData Serialization()
        {
            UnlitMaterialSerializationData serializationData = new UnlitMaterialSerializationData();
            serializationData.materialType = materialType;
            serializationData.shaderType = shaderType;
            serializationData.color = GetColor();
            serializationData.transparent = transparent;
            serializationData.texture = GetTexture();

            return serializationData;
        }


        public Vector4 GetColor()
        {
            float[] colorF = new float[4];
            RenderProgramDLL.GetColorFromMaterial(materialAdress, colorF);
            return new Vector4(colorF[0], colorF[1], colorF[2], colorF[3]);
        }

    }
}