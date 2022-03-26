using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using RenderLibrary.Graphics;

namespace ProgramLibrary
{
    public static class MespDebug
    {
        public const int LineRendererCount = 255;

        public static BitSet renderBitSet;
        public static LineRenderer[] lineRendererArray = new LineRenderer[LineRendererCount];


        public static void Initialize()
        {
            for(int i =0;i< LineRendererCount; i++)
            {
                lineRendererArray[i] = new LineRenderer();
                lineRendererArray[i].Setup();
            }
        }

        public static void DrawLine(Vector3 from, Vector3 to)
        {
            for(int i =0;i< LineRendererCount; i++)
            {
                if(renderBitSet)
            }
        }

    }
}
