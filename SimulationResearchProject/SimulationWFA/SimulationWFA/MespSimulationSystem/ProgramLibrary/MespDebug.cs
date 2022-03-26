using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using PhysicLibrary;
using RenderLibrary.Graphics;
using SimulationSystem;

namespace ProgramLibrary
{
    public static class MespDebug
    {

        public static void DrawLine(Vector3 from, Vector3 to, Vector3 color)
        {
            MespEditorDebugSystem.eventManager.SendEvent(new DrawLineEvent() {

                from = from,
                to = to,
                color = color,
            });
        }

        public static void DrawBox(BoxBounds bound, Vector3 color)
        {
            Vector3[] points = new Vector3[] {

                new Vector3(bound.xPoints.X, bound.yPoints.X, bound.zPoints.X),
                new Vector3(bound.xPoints.X, bound.yPoints.Y, bound.zPoints.X),
                new Vector3(bound.xPoints.Y, bound.yPoints.Y, bound.zPoints.X),
                new Vector3(bound.xPoints.Y, bound.yPoints.X, bound.zPoints.X),
                new Vector3(bound.xPoints.X, bound.yPoints.X, bound.zPoints.Y),
                new Vector3(bound.xPoints.X, bound.yPoints.Y, bound.zPoints.Y),
                new Vector3(bound.xPoints.Y, bound.yPoints.Y, bound.zPoints.Y),
                new Vector3(bound.xPoints.Y, bound.yPoints.X, bound.zPoints.Y),

            };

            int[] indices = new int[] {
                0,1,1,2,2,3,3,0,4,5,5,6,6,7,7,4,0,4,1,5,2,6,3,7
            };

            for(int i = 0; i < 24;)
            {
                MespEditorDebugSystem.eventManager.SendEvent(new DrawLineEvent() {

                    from = points[indices[i++]],
                    to = points[indices[i++]],
                    color = color,
                });
            }

            
        }

    }
}
