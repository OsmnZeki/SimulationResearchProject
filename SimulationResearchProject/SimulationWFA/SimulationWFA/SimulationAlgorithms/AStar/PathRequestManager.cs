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
        public delegate void OnAlgorithmDoneDelegate();

        public static event OnAlgorithmDoneDelegate OnAlgoDoneEvent;


        IEnumerable<ShortestPathAlgorithm> algorithms;
        bool isProcessingPath;

        public static Dictionary<string, bool> algorithmDictionary = new Dictionary<string, bool>() {
            { "AStar",  false },
            { "Dijkstra",  false },
            { "Prims",  false },
            { "DFS",  false },
            { "BFS",  false },
            { "Custom",  false },
        };

        public static Dictionary<string, string> algorithmMSDictionary = new Dictionary<string, string>()
        {
             { "AStar",  "NONE" },
            { "Dijkstra",  "NONE" },
            { "Prims",  "NONE" },
            { "DFS",  "NONE" },
            { "BFS",  "NONE" },
            { "Custom",  "NONE" },
        };

        public static Dictionary<string, string> algorithmDistanceDictionary = new Dictionary<string, string>()
        {
             { "AStar",  "NONE" },
            { "Dijkstra",  "NONE" },
            { "Prims",  "NONE" },
            { "DFS",  "NONE" },
            { "BFS",  "NONE" },
            { "Custom",  "NONE" },
        };


        public Vector3[] StartAlgorithms(Vector3 pathStart, Vector3 pathEnd, Grid grid, out ShortestPathAlgorithm foundedAlgorithm)
        {
            algorithmMSDictionary = new Dictionary<string, string>()
         {
             { "AStar",  "NONE" },
            { "Dijkstra",  "NONE" },
            { "Prims",  "NONE" },
            { "DFS",  "NONE" },
            { "BFS",  "NONE" },
            { "Custom",  "NONE" },
        };

            algorithmDistanceDictionary = new Dictionary<string, string>()
            {
             { "AStar",  "NONE" },
            { "Dijkstra",  "NONE" },
            { "Prims",  "NONE" },
            { "DFS",  "NONE" },
            { "BFS",  "NONE" },
            { "Custom",  "NONE" },
        };



            algorithms = GetAllShortesPathAlgorithm();

            List<ShortestPathAlgorithm> activeAlgorithms = new List<ShortestPathAlgorithm>();

            foreach (var algorithm in algorithms)
            {
                if (algorithmDictionary[algorithm.GetType().Name])
                {
                    activeAlgorithms.Add(algorithm);
                }
            }



            foundedAlgorithm = null;
            long shortestTimePassed = long.MaxValue;
            Type shortestPathType = null;
            Vector3[] waypoints = null;

            foreach (var algorithm in activeAlgorithms)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                waypoints = algorithm.FindPath(pathStart, pathEnd, grid);

                sw.Stop();
                Console.WriteLine(algorithm.GetType().Name + " found: " + sw.ElapsedMilliseconds + " ms");

                algorithmMSDictionary[algorithm.GetType().Name] = sw.ElapsedMilliseconds.ToString();
                algorithmDistanceDictionary[algorithm.GetType().Name] = GetDistanceMagnitude(waypoints).ToString();


                if (sw.ElapsedMilliseconds < shortestTimePassed)
                {
                    foundedAlgorithm = algorithm;
                    shortestPathType = algorithm.GetType();
                }
            }

            if (OnAlgoDoneEvent != null) OnAlgoDoneEvent();

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

        public float GetDistanceMagnitude(Vector3[] path)
        {
            float totalDistance = 0;
            for (int i = 0; i < path.Length - 1; i++)
            {
                totalDistance += (path[i + 1] - path[i]).Length();
            }

            return totalDistance;
        }

    }
}
