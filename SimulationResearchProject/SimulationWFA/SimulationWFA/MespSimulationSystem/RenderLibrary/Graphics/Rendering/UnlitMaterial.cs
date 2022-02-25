using System.Numerics;
using RenderLibrary.DLL;

namespace RenderLibrary.Graphics.Rendering
{
    public class UnlitMaterial : Material
    {
        public UnlitMaterial()
        {
            materialAdress = RenderProgramDLL.NewUnlitMaterial();
        }

        public void SetColor(Vector4 color)
        {
            float[] colorF = new[] {color.X, color.Y, color.Z, color.W};
            RenderProgramDLL.SetColorToMaterial(materialAdress,colorF);
        }

        public void AddTexture(Texture texture)
        {
            RenderProgramDLL.AddTextureToUnlitMaterial(materialAdress, texture.textureAdress);
        }

        //TODO: get fonksiyonunu yaz
        /*public Vector4 GetColor()
        {
            //TODO: RenderDLL.GetColor
            //TODO: return color
        }*/

    }
}