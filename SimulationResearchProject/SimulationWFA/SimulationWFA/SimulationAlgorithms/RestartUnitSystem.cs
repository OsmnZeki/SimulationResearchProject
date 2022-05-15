using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using RenderLibrary.IO;
using SimulationSystem.Components;
using SimulationWFA.SimulationAlgorithms.AStar;

namespace SimulationWFA.SimulationAlgorithms
{
    public class RestartUnitSystem : Dalak.Ecs.System
    {
        readonly Filter<VisualizeShortestPathComp> visualizeFilter = null;
        readonly Filter<UnitComp, TransformComp> unitFollowFilter = null;


        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                foreach(var v in visualizeFilter)
                {
                    var visualizeEntity = visualizeFilter.GetEntity(v);
                    visualizeEntity.RemoveComponent<VisualizeShortestPathComp>();
                    visualizeEntity.Destroy();
                }

                foreach(var u in unitFollowFilter)
                {
                    ref var unitComp = ref unitFollowFilter.Get1(u);
                    ref var transformCop = ref unitFollowFilter.Get2(u);
                    var unitEntity = unitFollowFilter.GetEntity(u);

                    transformCop.transform.position = unitComp.startPos;

                    unitEntity.RemoveComponent<StartPathFollowComp>();
                    unitEntity.RemoveComponent<UnitFollowPathComp>();

                }
            }
        }

    }
}
