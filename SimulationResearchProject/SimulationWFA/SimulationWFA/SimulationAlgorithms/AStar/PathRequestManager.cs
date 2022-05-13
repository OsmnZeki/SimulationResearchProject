using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SimulationWFA.SimulationAlgorithms.AStar
{
    public class PathRequestManager
    {
        Pathfinding pathfinding = new Pathfinding();

        bool isProcessingPath;

        public Vector3[] GetAStarPath(Vector3 pathStart, Vector3 pathEnd,Grid grid)
        {
            var waypoint = pathfinding.FindPath(pathStart, pathEnd, grid);
            return waypoint;
        }




    }
}
