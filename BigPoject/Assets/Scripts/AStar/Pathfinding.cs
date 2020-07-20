using System;
using System.Collections.Generic;
using UnityEngine;

namespace AStar
{
    public class Pathfinding : MonoBehaviour
    {
        public Transform seeker, target;                            // For development process.
        
        private Grid _grid;                                         // Contains grid component.

        private void Awake()
        {
            _grid = GetComponent<Grid>();
        }

        // For development process
        private void Update()
        {
             FindPath(seeker.position, target.position);
        }

        private void FindPath(Vector2 startPosition, Vector2 targetPosition)
        {
            Node startNode = _grid.GetNodeFromWorldPoint(startPosition);
            Node targetNode = _grid.GetNodeFromWorldPoint(targetPosition);
            
            List<Node> openSet = new List<Node>();                  // Contains the set of nodes to be evaluated.
            HashSet<Node> closedSet = new HashSet<Node>();          // Contains the set of nodes that are already evaluated.
            
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet[0];
                
                // We start from second element because first element is now already set as a currentNode.  
                for (int nodeIndex = 1; nodeIndex < openSet.Count; nodeIndex++)
                {
                    if (openSet[nodeIndex].FCost < currentNode.FCost)
                    {
                        currentNode = openSet[nodeIndex];
                    }
                    else if (openSet[nodeIndex].FCost == currentNode.FCost)
                    {
                        if (openSet[nodeIndex].HCost < currentNode.HCost)
                        {
                            currentNode = openSet[nodeIndex];
                        }
                    }
                }
                
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    RetracePath(startNode,targetNode);
                    return;
                }

                foreach (Node neighbour in _grid.GetNeighbours(currentNode))
                {
                    if (!neighbour.IsWalkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour =
                        currentNode.GCost + GetDistanceBetweenNodes(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour))
                    {
                        neighbour.GCost = newMovementCostToNeighbour;
                        neighbour.HCost = GetDistanceBetweenNodes(neighbour, targetNode);
                        neighbour.Parent = currentNode;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }
        }


        private void RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }
            
            path.Reverse();
            _grid.path = path; 
        }
        

        private int GetDistanceBetweenNodes(Node firstNode, Node secondNode)
        {
            int straightTransitionCost = 10;
            int diagonalTransitionCost = 14;
            int distanceX = Mathf.Abs(secondNode.GridX - firstNode.GridX);
            int distanceY = Mathf.Abs(secondNode.GridY - firstNode.GridY);

            int distance = 0;
            if (distanceX > distanceY)
            {
                distance = diagonalTransitionCost * distanceY - straightTransitionCost * (distanceX - distanceY);
            }
            else
            {
                distance = diagonalTransitionCost * distanceX - straightTransitionCost * (distanceY - distanceX);
            }

            return distance;
        }
    }
}
