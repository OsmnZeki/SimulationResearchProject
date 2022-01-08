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
