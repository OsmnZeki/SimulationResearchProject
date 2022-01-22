using System.Numerics;

namespace DLLTest
{
    public class UnlitMaterial : Material
    {
        public UnlitMaterial()
        {
            //TODO: yorum satırlarını yaz
            // materialAdress = RenderDLL.NewUnlitMaterial();
        }

        public void SetColor(Vector4 color)
        {
            //TODO: RenderDLL.SetMaterialColor(color)
        }

        public Vector4 GetColor()
        {
            //TODO: RenderDLL.GetColor
            //TODO: return color
        }
        
    }
}