using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLLTest
{
    class Shader
    {
        IntPtr shaderAdress;

        public Shader(string vertexShaderPath, string fragmentShaderPath)
        {
            shaderAdress = RenderProgramDLL.NewShader(vertexShaderPath, fragmentShaderPath);
        }

    }
}
