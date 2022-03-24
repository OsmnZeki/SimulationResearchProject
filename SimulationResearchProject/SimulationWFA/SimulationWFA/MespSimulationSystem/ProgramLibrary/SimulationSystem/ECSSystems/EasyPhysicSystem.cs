using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using PhysicLibrary;
using SimulationSystem.Components;
using SimulationSystem.ECSComponents;
using SimulationSystem.Timer;

namespace SimulationSystem.ECSSystems
{
    public class EasyPhysicSystem : Dalak.Ecs.System
    {
        readonly Filter<RigidbodyComp,TransformComp> rigidFilter = null;

        public override void Awake()
        {
            
        }

        public override void FixedUpdate()
        {
            foreach(var r in rigidFilter)
            {
                ref RigidbodyComp rigidComp = ref rigidFilter.Get1(r);
                ref TransformComp transformComp = ref rigidFilter.Get2(r);
                Physics.Simulate(ref rigidComp.rigidbody, Time.fixedDeltaTime);

                transformComp.transform.position = rigidComp.rigidbody.position;
            }
        }

    }
}
