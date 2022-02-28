using System;
using RenderLibrary.DLL;
using static RenderLibrary.Shaders.ShaderPool;

namespace RenderLibrary.Graphics.Rendering
{
    public enum MaterialType
    {
        LitMaterial,UnlitMaterial
    }


    public class Material
    {
        protected IntPtr materialAdress;
        public ShaderType shaderType;
        public MaterialType materialType;
        public Texture texture;
        public bool transparent = false;

        public void SetShader(Shader shader)
        {
            shaderType = shader.shaderType;
            RenderProgramDLL.SetShaderToMaterial(materialAdress,shader.GetShaderAdress());
        }

        public Shader GetShader()
        {
            return new Shader(RenderProgramDLL.GetShaderFromMaterial(materialAdress));
        }
        
        public void SetAdress(IntPtr materialAdress)
        {
            this.materialAdress = materialAdress;
        }

        public IntPtr GetAdress()
        {
            return materialAdress;
        }

        public void SetTransparent(bool isTransparent)
        {
            transparent = isTransparent;
            RenderProgramDLL.SetTransparent(materialAdress, isTransparent);
        }
    }
}