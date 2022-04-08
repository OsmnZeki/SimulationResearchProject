using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using PhysicLibrary;
using SimulationSystem.Components;
using SimulationSystem.ECSComponents;

namespace SimulationSystem.ECSSystems
{
    public class UpdateForceSystem : Dalak.Ecs.System
    {
        readonly Filter<ParticleComp> rigidFilter = null;

        public override void FixedUpdate()
        {
            foreach (var r in rigidFilter)
            {
                ref ParticleComp rigidComp = ref rigidFilter.Get1(r);

                if (rigidComp.particle.useGravity)
                {
                    /*Vector3 gravityForce = Physics.ComputeGravityForce(rigidComp.particle);
                    rigidComp.particle.totalForce += gravityForce;*/
                }
            }
        }
    }
}
