using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using RenderLibrary.Graphics;
using RenderLibrary.Shaders;
using SimulationSystem.ECSComponents;

namespace SimulationSystem
{
    public class ColliderBoundVisualizeSystem : Dalak.Ecs.System
    {
        Filter<BoxColliderComp> animatorFilter = null;

        LineRenderer lineRenderer = new LineRenderer();

        public override void Awake()
        {
            lineRenderer.from = new System.Numerics.Vector3(0, 0, 0);
            lineRenderer.to = new System.Numerics.Vector3(0, 10, 0);
            lineRenderer.color = new System.Numerics.Vector3(0, 1, 0);
            lineRenderer.Setup();
        }

        public override void Render()
        {
            lineRenderer.LineRender(ShaderPool.lineRenderShader,3);
        }

    }
}
