using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationSystem.Components;

namespace SimulationWFA.MespSimulationSystem.ProgramLibrary.EditorWindowSystem.Systems
{
    public class TestSystem : Dalak.Ecs.System
    {
        readonly Dalak.Ecs.Filter<TransformComp> transformFilter = null;

        public override void Update()
        {
            foreach (var t in transformFilter)
            {
                ref var transformComp = ref transformFilter.Get1(t);
                Console.WriteLine(transformComp.transform.position);
            }
        }
    }
}
