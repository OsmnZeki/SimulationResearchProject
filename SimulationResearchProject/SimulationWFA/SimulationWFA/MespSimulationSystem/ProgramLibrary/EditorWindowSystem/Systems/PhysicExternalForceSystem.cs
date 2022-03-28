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
    public class PhysicExternalForceSystem : Dalak.Ecs.System
    {
        readonly Filter<RigidbodyComp> rigidFilter = null;

        public override void FixedUpdate()
        {
            foreach (var r in rigidFilter)
            {
                ref RigidbodyComp rigidComp = ref rigidFilter.Get1(r);

                if (rigidComp.rigidbody.useGravity)
                {
                    Vector3 gravityForce = Physics.ComputeGravityForce(rigidComp.rigidbody);
                    rigidComp.rigidbody.totalForce += gravityForce;
                }
            }
        }
    }
}
