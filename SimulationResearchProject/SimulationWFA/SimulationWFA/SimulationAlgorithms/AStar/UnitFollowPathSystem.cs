using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using MespSimulationSystem.Math;
using MESPSimulationSystem.Math;
using SimulationSystem.Components;
using SimulationSystem.Timer;

namespace SimulationWFA.SimulationAlgorithms.AStar
{
    public class UnitFollowPathSystem : Dalak.Ecs.System
    {
        readonly Filter<UnitFollowPathComp, UnitComp, TransformComp> unitFollowFilter = null;

        public override void Update()
        {
            foreach (var u in unitFollowFilter)
            {
                ref var unitFollowPathComp = ref unitFollowFilter.Get1(u);
                ref var unitComp = ref unitFollowFilter.Get2(u);
                ref var transformComp = ref unitFollowFilter.Get3(u);

                bool followingPath = true;

                Vector2 pos2D = new Vector2(transformComp.transform.position.X, transformComp.transform.position.Z);

                while (unitFollowPathComp.path.turnBoundaries[unitFollowPathComp.pathIndex].HasCrossedLine(pos2D))
                {
                    if (unitFollowPathComp.pathIndex == unitFollowPathComp.path.finishLineIndex)
                    {
                        followingPath = false;
                        var entity = unitFollowFilter.GetEntity(u);
                        entity.RemoveComponent<UnitFollowPathComp>();
                        return;
                    }
                    else
                    {
                        unitFollowPathComp.pathIndex++;
                    }
                }

                if (followingPath)
                {

                    if (unitFollowPathComp.pathIndex >= unitFollowPathComp.path.slowDownIndex && unitComp.stoppingDst > 0)
                    {
                        unitFollowPathComp.speedPercent = MathFunctions.Clamp(unitFollowPathComp.path.turnBoundaries[unitFollowPathComp.path.finishLineIndex].DistanceFromPoint(pos2D) / unitComp.stoppingDst, 0, 1);
                        if (unitFollowPathComp.speedPercent < 0.01f)
                        {
                            var entity = unitFollowFilter.GetEntity(u);
                            entity.RemoveComponent<UnitFollowPathComp>();
                            return;
                        }
                    }

                    var direction = unitFollowPathComp.path.lookPoints[unitFollowPathComp.pathIndex] - transformComp.transform.position;

                    var pos = transformComp.transform.position;
                    pos += direction.normalized() * Time.deltaTime * unitComp.speed * unitFollowPathComp.speedPercent;
                    pos.Y = transformComp.transform.position.Y;
                    
                    transformComp.transform.position = pos;
                }

            }
        }

        public override void PostRender()
        {
            foreach(var u in unitFollowFilter)
            {
                ref var unitFollowPathComp = ref unitFollowFilter.Get1(u);
                ref var unitComp = ref unitFollowFilter.Get2(u);
                ref var transformComp = ref unitFollowFilter.Get3(u);

                for(int i = 0; i < unitFollowPathComp.path.lookPoints.Length-1; i++)
                {
                    ProgramLibrary.MespDebug.DrawLine(unitFollowPathComp.path.lookPoints[i], unitFollowPathComp.path.lookPoints[i + 1], new Vector3(0, 0, 1));
                }

            }
        }

    }
}
