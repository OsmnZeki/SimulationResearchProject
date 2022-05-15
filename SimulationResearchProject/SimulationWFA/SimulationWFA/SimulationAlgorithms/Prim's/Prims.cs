using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SimulationWFA.SimulationAlgorithms.AStar;

namespace SimulationWFA.SimulationAlgorithms
{
   /* public class Prims
    {
        public class PrimsNode
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

            PrimsNode[,] distanceGrid = new PrimsNode[grid.gridSizeX, grid.gridSizeY];
            for (int x = 0; x < grid.gridSizeX; x++)
            {
                for (int y = 0; y < grid.gridSizeY; y++)
                {
                    distanceGrid[x, y] = new PrimsNode();
                    distanceGrid[x, y].distance = int.MaxValue;
                }
            }
            distanceGrid[startNode.gridX, startNode.gridY].distance = 0;

            List<Node> exploringGrid = new List<Node>();
            exploringGrid.Add(startNode);
            float minDistance = float.MaxValue;
            Node tempNode = null;
            Node currentNode = null;
            while (exploringGrid.Count > 0 && exploringGrid.Count < grid.gridSizeX * grid.gridSizeY)
            {
                for (int i = 0; i < exploringGrid.Count; i++)
                {
                    currentNode = exploringGrid[i];
                    foreach (Node neighbour in grid.GetNeighbours(currentNode))
                    {
                        if (exploringGrid.Contains(neighbour) || exploringGrid.Contains(neighbour.parent))
                        {
                            continue;
                        }
                        else
                        {
                            float dist = Pathfinding.GetDistance(neighbour, currentNode);
                            if (neighbour.walkable && dist + neighbour.movementPenalty < minDistance)
                            {
                                minDistance = dist;
                                tempNode = neighbour;
                            }
                        }
                    }

                }
                tempNode.parent = currentNode;
                exploringGrid.Add(tempNode);
                minDistance = float.MaxValue;
                if (tempNode == targetNode)
                {
                    break;
                }
            }

            sw.Stop();
            Console.WriteLine("Path found: " + sw.ElapsedMilliseconds + " ms");
            waypoints = Pathfinding.RetracePath(startNode, targetNode);
            return waypoints;

        }
    }*/
}
