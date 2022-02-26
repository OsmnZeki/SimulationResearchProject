﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MESPSimulationSystem.Math;
using ProgramLibrary;
using RenderLibrary.Graphics.Rendering;

namespace RenderLibrary.Shaders
{
    public static class ShaderPool
    {
        public enum ShaderType
        {
            LitShader, UnlitShader, TransTestShader, OutlineBorderShader
        }

        public static Shader litShader = new Shader(SimPath.GetAssetPath + "/Shaders/object.vs", SimPath.GetAssetPath + "/Shaders/lit.fs", ShaderType.LitShader);
        public static Shader unlitShader = new Shader(SimPath.GetAssetPath + "/Shaders/object.vs", SimPath.GetAssetPath + "/Shaders/unlit.fs", ShaderType.UnlitShader);
        public static Shader transTestShader = new Shader(SimPath.GetAssetPath + "/Shaders/object.vs", SimPath.GetAssetPath + "/Shaders/TransTestShader/transparent.fs", ShaderType.TransTestShader);
        public static Shader outlineBorderShader = new Shader(SimPath.GetAssetPath + "/Shaders/object.vs", SimPath.GetAssetPath + "/Shaders/OutlineShader/Outline.fs", ShaderType.OutlineBorderShader);

        public static Shader[] allShaders = { litShader, unlitShader, transTestShader, outlineBorderShader };
        public static Shader[] allLitShader = { litShader };

        public static Shader GetShaderByType(ShaderType shaderType)
        {
            switch (shaderType)
            {
                case ShaderType.LitShader: return litShader;
                case ShaderType.UnlitShader: return unlitShader;
                case ShaderType.TransTestShader: return transTestShader;
                case ShaderType.OutlineBorderShader: return outlineBorderShader;
            }

            return null;
        }

        public static void SetupDefaultShadersToRender(Mat4 view, Mat4 projection)
        {
            for (int i = 0; i < allShaders.Length; i++)
            {
                allShaders[i].Activate();
                allShaders[i].SetMat4("view", view);
                allShaders[i].SetMat4("projection", projection);
            }
        }


    }
}