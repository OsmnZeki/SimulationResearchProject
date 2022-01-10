﻿using System;
using System.Runtime.InteropServices;

namespace DLLTest
{
    class RenderProgramDLL
    {
        protected const string RenderProgramDLLPath = "RenderProgramDLL.dll";

        #region ScreenFunctions

        [DllImport(RenderProgramDLLPath, EntryPoint = "CreateScreen")]
        public static extern IntPtr CreateScreen();

        [DllImport(RenderProgramDLLPath, EntryPoint = "ScreenShouldClose")]
        public static extern bool ScreenShouldClose(IntPtr screen);

        [DllImport(RenderProgramDLLPath, EntryPoint = "ScreenUpdate")]
        public static extern void ScreenUpdate(IntPtr screen);

        [DllImport(RenderProgramDLLPath, EntryPoint = "ScreenNewFrame")]
        public static extern void ScreenNewFrame(IntPtr screen);

        [DllImport(RenderProgramDLLPath, EntryPoint = "ScreenTerminate")]
        public static extern void ScreenTerminate(IntPtr screen);

        [DllImport(RenderProgramDLLPath, EntryPoint = "ScreenProcessInput")]
        public static extern void ScreenProcessInput(IntPtr screen);

        #endregion

        #region ShaderFunction

        [DllImport(RenderProgramDLLPath, EntryPoint = "NewShader")]
        public static extern IntPtr NewShader(string vertexShaderPath, string fragmentShaderPath);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "ShaderSetInt")]
        public static extern void ShaderSetInt(IntPtr shaderAdress, string name, int value);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "ShaderSetFloat")]
        public static extern void ShaderSetFloat(IntPtr shaderAdress, string name, float value);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "ShaderSet3Float")]
        public static extern void ShaderSet3Float(IntPtr shaderAdress, string name, float value, float value1, float value2);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "ShaderSet4Float")]
        public static extern void ShaderSet4Float(IntPtr shaderAdress, string name, float value, float value1, float value2, float value3);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "ShaderSetBool")]
        public static extern void ShaderSetBool(IntPtr shaderAdress, string name, bool value);
        
        #endregion

        #region TextureFunctions

        [DllImport(RenderProgramDLLPath, EntryPoint = "NewTexture")]
        public static extern IntPtr NewTexture(string directory, string path, int aiType);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "TextureLoad")]
        public static extern IntPtr TextureLoad(IntPtr texture, bool flip);

        #endregion

        #region MeshFunctions

        [DllImport(RenderProgramDLLPath, EntryPoint = "CreateMesh")]
        public static extern IntPtr CreateMesh();
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "MeshSetVerticesPos")]
        public static extern void MeshSetVerticesPos(IntPtr mesh, float[] pos, int sizeOfVertices);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "MeshSetIndices")]
        public static extern void MeshSetIndices(IntPtr mesh, int[] indices);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "MeshSetVerticesNormal")]
        public static extern void MeshSetVerticesNormal(IntPtr mesh, float[] normal);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "MeshSetVerticesTexCoord")]
        public static extern void MeshSetVerticesTexCoord(IntPtr mesh, float[] texCoord);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "AddTextureToMesh")]
        public static extern void AddTextureToMesh(IntPtr mesh, IntPtr texture);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "MeshCleanUp")]
        public static extern void MeshCleanUp(IntPtr mesh);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "MeshSetup")]
        public static extern void MeshSetup(IntPtr mesh,int setupConfig);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "MeshSetDiffuse")]
        public static extern void MeshSetDiffuse(IntPtr mesh,float[] diffuse);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "MeshSetSpecular")]
        public static extern void MeshSetSpecular(IntPtr mesh,float[] specular);

        #endregion

        #region ModelFunctions

        [DllImport(RenderProgramDLLPath, EntryPoint = "NewModel")]
        public static extern IntPtr NewModel();
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "LoadModel")]
        public static extern void LoadModel(IntPtr model, string modelPath);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "ModelRender")]
        public static extern void ModelRender(IntPtr model, IntPtr shader);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "ModelCleanUp")]
        public static extern void ModelCleanUp(IntPtr model);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "AddMeshToModel")]
        public static extern void AddMeshToModel(IntPtr model, IntPtr mesh);

        #endregion

        #region InputFunctions

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetKeyDown")]
        public static extern bool GetKeyDown(int keyCode);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetKeyUp")]
        public static extern bool GetKeyUp(int keyCode);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetKey")]
        public static extern bool GetKey(int keyCode);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetMouseKeyDown")]
        public static extern bool GetMouseKeyDown(int keyCode);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetMouseKeyUp")]
        public static extern bool GetMouseKeyUp(int keyCode);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetMouseKey")]
        public static extern bool GetMouseKey(int keyCode);

        #endregion
    }
}