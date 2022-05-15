using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        //Prims prims = new Prims();
        bool isProcessingPath;

        public Vector3[] StartAlgorithms(Vector3 pathStart, Vector3 pathEnd, Grid grid)
        {
            var algorithms = GetAllShortesPathAlgorithm();

            ShortestPathAlgorithm shortestPathAlgorithm;
            long shortestTimePassed = long.MaxValue;
            Type shortestPathType = null;
            Vector3[] waypoints = null;

            foreach(var algorithm in algorithms)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                waypoints = algorithm.FindPath(pathStart, pathEnd, grid);


                sw.Stop();
                Console.WriteLine(algorithm.GetType().Name + " found: " + sw.ElapsedMilliseconds + " ms");
                if(sw.ElapsedMilliseconds < shortestTimePassed)
                {
                    shortestPathAlgorithm = algorithm;
                    shortestPathType = algorithm.GetType();
                }

            }

            Console.WriteLine("Chosen algoritm: " + shortestPathType.Name);
            return waypoints;

        }


        IEnumerable<ShortestPathAlgorithm> GetAllShortesPathAlgorithm()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(ShortestPathAlgorithm)))
                .Select(type => Activator.CreateInstance(type) as ShortestPathAlgorithm);
        }

    }
}
