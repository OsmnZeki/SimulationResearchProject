using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SimulationWFA.SimulationAlgorithms.AStar;
using SimulationWFA.SimulationAlgorithms.DijkstraAlgorithm;

namespace SimulationWFA.SimulationAlgorithms
{
    public class PathRequestManager
    {
        Pathfinding pathfinding = new Pathfinding();
        Dijkstra dijkstra = new Dijkstra();
        Prims prims = new Prims();
        bool isProcessingPath;

        public Vector3[] GetAStarPath(Vector3 pathStart, Vector3 pathEnd,Grid grid)
        {
            var waypoint = pathfinding.FindPath(pathStart, pathEnd, grid);
            return waypoint;
        }

        public Vector3[] GetDijkstraPath(Vector3 pathStart, Vector3 pathEnd, Grid grid)
        {
            var waypoint = dijkstra.FindPath(pathStart, pathEnd, grid);
            return waypoint;
        }

        public Vector3[] GetPrimsPath(Vector3 pathStart, Vector3 pathEnd, Grid grid)
        {
            var waypoint = prims.FindPath(pathStart, pathEnd, grid);
            return waypoint;
        }


    }
}
