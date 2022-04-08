using System.Numerics;
using Dalak.Ecs;
using RenderLibrary.IO;
using SimulationSystem.Components;
using SimulationSystem.ECSComponents;

namespace SimulationSystem.ECSSystems
{
    public class PhysicTestSystem : Dalak.Ecs.System
    {
        readonly Filter<ParticleComp, TransformComp> rigidFilter = null;

        readonly Filter<CanMoveTestTag,ParticleComp> canMoveableFilter = null;

        public override void Update()
        {
            foreach (var r in rigidFilter)
            {
                ref ParticleComp rigidComp = ref rigidFilter.Get1(r);
                ref TransformComp transformComp = ref rigidFilter.Get2(r);

                if (Input.GetKeyDown(KeyCode.M)) rigidComp.particle.velocity = -(Vector3.UnitY * 10);
                if (Input.GetKeyDown(KeyCode.N)) rigidComp.particle.AddForce(Vector3.UnitX * 5);
            }

            foreach(var c in canMoveableFilter)
            {
                ref var rigidComp = ref canMoveableFilter.Get2(c);

                if (Input.GetKeyDown(KeyCode.K))
                {
                    rigidComp.particle.velocity = new Vector3(1, 0, 0);
                }
                else if (Input.GetKeyDown(KeyCode.L))
                {
                    rigidComp.particle.velocity = new Vector3(-1, 0, 0);
                }
            }

        }
    }
}
