﻿using System;
using RenderLibrary.DLL;

namespace RenderLibrary.Graphics.Rendering
{
    public class Material
    {
        protected IntPtr materialAdress;

        public void SetShader(Shader shader)
        {
            RenderProgramDLL.SetShaderToMaterial(materialAdress,shader.GetShaderAdress());
        }

        public Shader GetShader()
        {
            var shader = new Shader(RenderProgramDLL.GetShaderFromMaterial(materialAdress));
            return shader;
        }
        
        public void SetAdress(IntPtr materialAdress)
        {
            this.materialAdress = materialAdress;
        }

        public IntPtr GetAdress()
        {
            return materialAdress;
        }
    }
}