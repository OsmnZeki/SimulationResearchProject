using System;
using Dalak.Ecs;
using RenderLibrary.Graphics;
using RenderLibrary.Graphics.RenderData;
using RenderLibrary.Graphics.Rendering;
using SimulationSystem.Components;
using SimulationWFA.MespUtils;

namespace TheSimulation.SerializedComponent
{
    [Serializable]
    public class MeshRendererSerialized : SimulationSystem.ECS.Entegration.SerializedComponent
    {
        public string meshID;
        public string materialID;

        public override void AddComponent(Entity entity, World world)
        {
            var materialPath = AssetOrganizer.GetMaterialPathByFileID(materialID);
            var meshPath = AssetOrganizer.GetMeshPathByFileID(materialID);

            var material = AssetUtils.LoadFromAsset<Material>(materialPath);
            var mesh = AssetUtils.LoadFromAsset<Mesh>(meshPath);

            entity.AddComponent<MeshRendererComp>() = new MeshRendererComp() {material = material, mesh = mesh,};
        }

        public override string GetName()
        {
            return "Mesh Serialized";
        }
    }
}

