using System;
using MESPSimulation.Graphics.Rendering;

namespace DLLTest
{
    public abstract class Material
    {
        protected IntPtr materialAdress;
        protected Shader shader;
    }
}