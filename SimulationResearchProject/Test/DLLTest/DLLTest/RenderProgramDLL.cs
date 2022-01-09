using System;
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

        #endregion

        #region ModelFunctions

        [DllImport(RenderProgramDLLPath, EntryPoint = "NewModel")]
        public static extern IntPtr NewModel();
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "LoadModel")]
        public static extern void LoadModel(IntPtr model, string modelPath);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "Render")]
        public static extern void Render(IntPtr model, IntPtr shader);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "CleanUp")]
        public static extern void CleanUp(IntPtr model);

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
