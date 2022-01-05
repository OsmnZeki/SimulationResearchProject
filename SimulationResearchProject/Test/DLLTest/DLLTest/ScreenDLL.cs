using System;
using System.Runtime.InteropServices;

namespace DLLTest
{
    class ScreenDLL
    {
        protected const string RenderProgramDLL = "RenderProgramDLL.dll";

        [DllImport(RenderProgramDLL, EntryPoint = "CreateScreen")]
        public static extern IntPtr CreateScreen();

        [DllImport(RenderProgramDLL, EntryPoint = "ScreenShouldClose")]
        public static extern bool ScreenShouldClose(IntPtr screen);

        [DllImport(RenderProgramDLL, EntryPoint = "ScreenUpdate")]
        public static extern void ScreenUpdate(IntPtr screen);

        [DllImport(RenderProgramDLL, EntryPoint = "ScreenNewFrame")]
        public static extern void ScreenNewFrame(IntPtr screen);

        [DllImport(RenderProgramDLL, EntryPoint = "ScreenTerminate")]
        public static extern void ScreenTerminate(IntPtr screen);

        [DllImport(RenderProgramDLL, EntryPoint = "ScreenProcessInput")]
        public static extern void ScreenProcessInput(IntPtr screen);
    }
}
