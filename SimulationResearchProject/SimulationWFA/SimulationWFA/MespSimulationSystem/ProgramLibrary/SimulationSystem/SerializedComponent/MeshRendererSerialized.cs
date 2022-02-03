using Dalak.Ecs;
using RenderLibrary.Graphics.RenderData;
using RenderLibrary.Graphics.Rendering;
using SimulationSystem.Components;

namespace TheSimulation.SerializedComponent
{
    public class MeshRendererSerialized : SimulationSystem.ECS.Entegration.SerializedComponent
    {
        public Mesh mesh;
        public Material material;

        public override void AddComponent(Entity entity, World world)
        {
            entity.AddComponent<MeshRendererComp>() = new MeshRendererComp() {material = material, mesh = mesh,};
        }

        public override string GetName()
        {
            return "Mesh Serialized";
        }
    }
}

