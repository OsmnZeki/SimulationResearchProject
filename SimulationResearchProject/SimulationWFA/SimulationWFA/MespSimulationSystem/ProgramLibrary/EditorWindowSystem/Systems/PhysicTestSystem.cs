using System.Numerics;
using Dalak.Ecs;
using RenderLibrary.IO;
using SimulationSystem.Components;
using SimulationSystem.ECSComponents;

namespace SimulationSystem.ECSSystems
{
    public class PhysicTestSystem : Dalak.Ecs.System
    {
        readonly Filter<RigidbodyComp, TransformComp> rigidFilter = null;

        public override void Update()
        {
            foreach (var r in rigidFilter)
            {
                ref RigidbodyComp rigidComp = ref rigidFilter.Get1(r);
                ref TransformComp transformComp = ref rigidFilter.Get2(r);

                if (Input.GetKeyDown(KeyCode.M)) rigidComp.rigidbody.AddTorque(Vector3.UnitY * 100);
                if (Input.GetKeyDown(KeyCode.N)) rigidComp.rigidbody.AddForce(Vector3.UnitX * 5);
            }

        }
    }
}
