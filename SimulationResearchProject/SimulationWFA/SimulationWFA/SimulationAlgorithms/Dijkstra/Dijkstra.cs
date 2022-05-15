using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SimulationWFA.SimulationAlgorithms.AStar;

namespace SimulationWFA.SimulationAlgorithms.DijkstraAlgorithm
{
    public class Dijkstra
    {
        public class DijkstraNode
        {
            public float distance = float.MaxValue;


        }

        public Vector3[] FindPath(Vector3 startPos, Vector3 targetPos, Grid grid)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            Vector3[] waypoints = new Vector3[0];
            bool pathSuccess = false;

            Node startNode = grid.NodeFromWorldPoint(startPos);
            Node targetNode = grid.NodeFromWorldPoint(targetPos);

            DijkstraNode[,] distanceGrid = new DijkstraNode[grid.gridSizeX, grid.gridSizeY];
            for (int x = 0; x < grid.gridSizeX; x++)
            {
                for (int y = 0; y < grid.gridSizeY; y++)
                {
                    distanceGrid[x, y] = new DijkstraNode();
                    distanceGrid[x, y].distance = int.MaxValue;
                }
            }
            distanceGrid[startNode.gridX, startNode.gridY].distance = 0;

            Queue<Node> exploringGrid = new Queue<Node>();
            exploringGrid.Enqueue(startNode);

            while (exploringGrid.Count > 0)
            {
                Node currentNode = exploringGrid.Dequeue();

                foreach (Node neighbour in grid.GetNeighbours(currentNode))
                {
                    var tempNeighborDist = Pathfinding.GetDistance(neighbour, currentNode);

                    if (neighbour.walkable && distanceGrid[neighbour.gridX, neighbour.gridY].distance > distanceGrid[currentNode.gridX, currentNode.gridY].distance + tempNeighborDist+ neighbour.movementPenalty)
                    {
                        distanceGrid[neighbour.gridX, neighbour.gridY].distance = distanceGrid[currentNode.gridX, currentNode.gridY].distance + tempNeighborDist + neighbour.movementPenalty;
                        neighbour.parent = currentNode;

                        exploringGrid.Enqueue(neighbour);
                    }
                }



            }

            sw.Stop();
            Console.WriteLine("Path found: " + sw.ElapsedMilliseconds + " ms");
            waypoints = Pathfinding.RetracePath(startNode, targetNode);
            return waypoints;

        }

    }
}
