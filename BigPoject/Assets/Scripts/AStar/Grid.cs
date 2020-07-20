using System;
using System.Collections.Generic;
using AStar;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsUnpassable = Physics2D.AllLayers;        // Determine through which nodes we cant lead the way.
    [SerializeField] private Vector2 _gridSizeInWorld = Vector2.zero;                  // Size of grid in world space.
    [SerializeField] private float _nodeRadius = 0f;                                   // Radius of a no
    private Node[,] _grid = null;                                                      // Storage for all nodes.

    private float _nodeDiameter = 0f;                                                  // Diameter of a node. 
    private int _gridSizeX = 0;                                                        // Amount of nodes in grid by x Axis.
    private int _gridSizeY = 0;                                                        // Amount of nodes in grid by y Axis.
    
    #region Properties

    public Vector2 GridSizeInWorld => _gridSizeInWorld;

    public float NodeRadius => _nodeRadius;

    #endregion Properties

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
    
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                bool isNodeInCenter = ((x == 0) && (y == 0));
                if (isNodeInCenter)
                {
                    continue;
                }

                int checkX = node.GridX + x;
                int checkY = node.GridY + y;

                bool isNodeInsideOfTheGrid = (((checkX >= 0) && (checkX < _gridSizeX)) &&
                                             ((checkY >= 0) && (checkY < _gridSizeY)));
                if (isNodeInsideOfTheGrid)
                {
                    neighbours.Add(_grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }
    
    
    public Node GetNodeFromWorldPoint(Vector2 worldPosition)
    {
        float nodeXCoordinatePercentage = (worldPosition.x / _gridSizeInWorld.x) + 0.5f;
        float nodeYCoordinatePercentage = (worldPosition.y / _gridSizeInWorld.y) + 0.5f;
        nodeXCoordinatePercentage = Mathf.Clamp01(nodeXCoordinatePercentage);
        nodeYCoordinatePercentage = Mathf.Clamp01(nodeYCoordinatePercentage);

        Mathf.FloorToInt(Mathf.Min(_gridSizeX * nodeXCoordinatePercentage, _gridSizeX - 1));
        
        int xCoordinate = Mathf.FloorToInt(Mathf.Min(_gridSizeX * nodeXCoordinatePercentage, _gridSizeX - 1));
        int yCoordinate = Mathf.FloorToInt(Mathf.Min(_gridSizeY * nodeYCoordinatePercentage, _gridSizeY - 1));
        
        return _grid[xCoordinate, yCoordinate];
    }
    

    private void Start()
    {
        _nodeDiameter = _nodeRadius * 2f;
        _gridSizeX = Mathf.RoundToInt(_gridSizeInWorld.x / _nodeDiameter);
        _gridSizeY = Mathf.RoundToInt(_gridSizeInWorld.y / _nodeDiameter);
        CreateGrid();
    }


    private void CreateGrid()
    {
        Vector2 worldGridBottomLeft = transform.position - 
                                      (Vector3.right * _gridSizeX / 2) -
                                      (Vector3.up * _gridSizeY / 2);
        
        _grid = new Node[_gridSizeX, _gridSizeY];

        for (var x = 0; x < _gridSizeX; x++)
        {
            for (var y = 0; y < _gridSizeY; y++)
            {
                Vector2 positionInWorld = worldGridBottomLeft + 
                                          Vector2.right * (x * _nodeDiameter + _nodeRadius) +
                                          Vector2.up * (y * _nodeDiameter + _nodeRadius);
                Collider2D overlapInfo = Physics2D.OverlapCircle(positionInWorld, _nodeRadius, _whatIsUnpassable);
                bool isPassable = overlapInfo == null ? true : false;
                
                _grid[x, y] = new Node(isPassable, positionInWorld, x, y);
            }
        }
    }
    
    
    public List<Node> path;                       // For Debug.
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, _gridSizeInWorld);

        if (_grid != null)
        {
            foreach (Node node in _grid)
            {
                Gizmos.color = node.IsWalkable ? Color.white : Color.red;
                if (path != null)
                {
                    if (path.Contains(node))
                    {
                        Gizmos.color = Color.black;
                    }
                }
                Gizmos.DrawCube(node.PositionInWorld, Vector2.one * (_nodeDiameter - 0.05f));
            }
        }
    }
}
