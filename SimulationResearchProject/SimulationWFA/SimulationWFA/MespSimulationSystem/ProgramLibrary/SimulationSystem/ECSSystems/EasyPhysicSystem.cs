using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using PhysicLibrary;
using RenderLibrary.IO;
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
            foreach (var r in rigidFilter)
            {
                ref RigidbodyComp rigidComp = ref rigidFilter.Get1(r);
                ref TransformComp transformComp = ref rigidFilter.Get2(r);

                rigidComp.rigidbody.position = transformComp.transform.position;
                rigidComp.rigidbody.rotation =transformComp.transform.rotation;
            }
        }


        public override void FixedUpdate()
        {
            foreach(var r in rigidFilter)
            {
                ref RigidbodyComp rigidComp = ref rigidFilter.Get1(r);
                ref TransformComp transformComp = ref rigidFilter.Get2(r);
                Physics.Simulate(rigidComp.rigidbody, Time.fixedDeltaTime);

                transformComp.transform.position = rigidComp.rigidbody.position;
                transformComp.transform.rotation = rigidComp.rigidbody.rotation;
            }
        }

    }
}
