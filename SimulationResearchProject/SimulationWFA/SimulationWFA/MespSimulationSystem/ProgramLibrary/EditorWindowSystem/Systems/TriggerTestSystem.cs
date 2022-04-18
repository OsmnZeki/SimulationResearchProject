using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using SimulationSystem.ECSComponents;

namespace SimulationSystem
{
    public class TriggerTestSystem : Dalak.Ecs.System
    {
        readonly Filter<OnTriggerEnterComp> triggerEnterFilter = null;

        public override void Update()
        {
            foreach(var t in triggerEnterFilter)
            {
                ref var onTriggerEnterComp = ref triggerEnterFilter.Get1(t);
                foreach(var collider in onTriggerEnterComp.collidedEntityList)
                {
                    Console.WriteLine("Triggered");
                }

            }
        }
    }
}
