using System;
using System.Runtime.InteropServices;

namespace DLLTest
{
    static class ScreenDLL
    {
        [DllImport("RenderProgramDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CreateScreen();
        [DllImport("RenderProgramDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ScreenShouldClose(IntPtr screen);
        [DllImport("RenderProgramDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ScreenUpdate(IntPtr screen);
        [DllImport("RenderProgramDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ScreenNewFrame(IntPtr screen);
        [DllImport("RenderProgramDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ScreenTerminate(IntPtr screen);
        [DllImport("RenderProgramDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ScreenReturnWidth(IntPtr screen);
        [DllImport("RenderProgramDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ScreenProcessInput(IntPtr screen);
    }
}
