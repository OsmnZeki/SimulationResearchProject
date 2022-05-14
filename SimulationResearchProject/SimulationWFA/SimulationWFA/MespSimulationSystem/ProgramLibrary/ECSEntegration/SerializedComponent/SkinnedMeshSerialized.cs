using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using RenderLibrary.Graphics;
using SimulationSystem.ECSComponents;

namespace TheSimulation.SerializedComponent
{
    public class SkinnedMeshSerialized : SimulationSystem.ECS.Entegration.SerializedComponent
    {
        public string modelPath;

        public override void AddComponent(Entity entity, World world)
        {
            var model = ModelLoader.LoadModel(modelPath);

            ref var skinnedMesh = ref entity.AddComponent<SkinnedMeshRendererComp>();
            skinnedMesh = new SkinnedMeshRendererComp {
                 rootModel = model,
             };

            skinnedMesh.SetMeshRenderers(world,ref ownerSimObj);

        }

        public override string GetName()
        {
            return "Skinned Mesh Serialized";
        }
    }
}
