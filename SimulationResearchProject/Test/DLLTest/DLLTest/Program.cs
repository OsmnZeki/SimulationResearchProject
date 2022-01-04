using System;


namespace DLLTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IntPtr screen = ScreenDLL.CreateScreen();
            if (screen == IntPtr.Zero) return;

            while (!ScreenDLL.ScreenShouldClose(screen))
            {
                ScreenDLL.ScreenProcessInput(screen);
                // render
                // ------
                ScreenDLL.ScreenUpdate(screen);

                ScreenDLL.ScreenNewFrame(screen);
            }

            ScreenDLL.ScreenTerminate(screen);
        }
    }
}
