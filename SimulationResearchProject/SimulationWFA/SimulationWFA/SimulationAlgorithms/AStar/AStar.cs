using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SimulationWFA.SimulationAlgorithms.AStar
{
    public class AStar : ShortestPathAlgorithm
    {
        public override Vector3[] FindPath(Vector3 startPos, Vector3 targetPos, Grid grid)
        {
            Stopwatch sw = new Stopwatch();

            Vector3[] waypoints = new Vector3[0];
            bool pathSuccess = false;

            Node startNode = grid.NodeFromWorldPoint(startPos);
            Node targetNode = grid.NodeFromWorldPoint(targetPos);
            startNode.parent = startNode;


            if (startNode.walkable && targetNode.walkable)
            {
                Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
                HashSet<Node> closedSet = new HashSet<Node>();
                openSet.Add(startNode);

                while (openSet.Count > 0)
                {
                    Node currentNode = openSet.RemoveFirst();
                    closedSet.Add(currentNode);

                    if (currentNode == targetNode)
                    {
                        pathSuccess = true;
                        break;
                    }

                    foreach (Node neighbour in grid.GetNeighbours(currentNode))
                    {
                        if (!neighbour.walkable || closedSet.Contains(neighbour))
                        {
                            continue;
                        }

                        int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour) + neighbour.movementPenalty;
                        if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                        {
                            neighbour.gCost = newMovementCostToNeighbour;
                            neighbour.hCost = GetDistance(neighbour, targetNode);
                            neighbour.parent = currentNode;

                            if (!openSet.Contains(neighbour))
                                openSet.Add(neighbour);
                            else
                                openSet.UpdateItem(neighbour);
                        }
                    }
                }
            }

            if (pathSuccess)
            {
                waypoints = RetracePath(startNode, targetNode);
                return waypoints;
            }
            return null;
        }
    }
}
