using System;
using System.Numerics;
using RenderLibrary.DLL;


namespace RenderLibrary.IO
{
    public class Screen
    {
        public IntPtr screenAdress;

        public Vector4 clearColor = new Vector4(.2f, .2f, .5f, 1.0f);

        public void Create(int widht, int height)
        {
            screenAdress = RenderProgramDLL.CreateScreen(widht,height);
        }

        public void SetClearColor(Vector4 clearColor)
        {
            this.clearColor = clearColor;
            RenderProgramDLL.SetClearColor(screenAdress, new float[]{ clearColor.X,clearColor.Y,clearColor.Z,clearColor.W});
        }

        public bool ShouldClose()
        {
            return RenderProgramDLL.ScreenShouldClose(screenAdress);
        }

        public void ProcessWindowInput()
        {
            RenderProgramDLL.ScreenProcessInput(screenAdress);
        }

        public void Update()
        {
            RenderProgramDLL.ScreenUpdate(screenAdress);
        }

        public void NewFrame()
        {
            RenderProgramDLL.ScreenNewFrame(screenAdress);
        }

        public void Terminate()
        {
            RenderProgramDLL.ScreenTerminate(screenAdress);
        }

    }
}
