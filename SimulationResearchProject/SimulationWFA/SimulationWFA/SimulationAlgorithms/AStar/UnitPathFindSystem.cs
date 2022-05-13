using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using RenderLibrary.IO;
using SimulationSystem.Components;

namespace SimulationWFA.SimulationAlgorithms.AStar
{
    public class UnitPathFindSystem : Dalak.Ecs.System
    {
        readonly Filter<UnitComp,TransformComp> unitFilter = null;
        readonly Filter<TargetComp, TransformComp> targetFilter = null;
        readonly Filter<GridComp> gridFilter = null;

        PathRequestManager pathRequestManager = null;

        public const float minPathUpdateTime = .2f;
        public const float pathUpdateMoveThreshold = .5f;

        public override void Update()
        {

            foreach(var u in unitFilter)
            {
                ref var unitComp = ref unitFilter.Get1(u);
                ref var transformComp = ref unitFilter.Get2(u);

                if (Input.GetKeyDown(KeyCode.M))
                {
                    Vector3 targetPosition = new Vector3();
                    foreach (var t in targetFilter)
                    {
                        ref var targetComp = ref targetFilter.Get1(t);
                        ref var targetTransformComp = ref targetFilter.Get2(t);
                        targetPosition = targetTransformComp.transform.position;
                    }

                    ref var gridComp = ref gridFilter.Get1(0);

                    var waypoints = pathRequestManager.GetAStarPath(transformComp.transform.position, targetPosition, gridComp.grid);
                    if(waypoints != null)
                    {
                        float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
                        Vector3 targetPosOld = targetPosition;

                        var path = new Path(waypoints, transformComp.transform.position, unitComp.turnDst, unitComp.stoppingDst);

                        var unitEntity = unitFilter.GetEntity(u);
                        unitEntity.AddComponent<UnitFollowPathComp>() = new UnitFollowPathComp() {
                            path = path,
                            pathIndex = 0,
                            speedPercent = 1,
                        };
                    }

                    


                }

            }
        }

       

    }
}
