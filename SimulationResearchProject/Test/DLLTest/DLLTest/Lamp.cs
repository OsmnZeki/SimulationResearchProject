using System.Numerics;

namespace DLLTest
{
    public class Lamp : Cube
    {
        public Vector3 lightColor;
        public Lights.PointLight pointLight;
        
        public Lamp(){}

        public Lamp(Vector3 lightColor,Lights.PointLight pointLight) : base()
        {
            this.lightColor = lightColor;
            this.pointLight = pointLight;
        }

        public void Render(Shader shader)
        {
            //set light color

            shader.Set3Float("lightColor", lightColor);

            base.Render(shader);
        }

    }
}