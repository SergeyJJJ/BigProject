using System;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsUnpassable = Physics2D.AllLayers;        // Determine through which nodes we cant lead the way.
    [SerializeField] private Vector2 _gridSizeInWorld = Vector2.zero;                  // Size of grid in world space.
    [SerializeField] private float _nodeRadius = 0f;                                   // Radius of a node.
    [SerializeField] private Transform _player;
    private Node[,] _grid = null;                                                      // Storage for all nodes.

    private float _nodeDiameter = 0f;                                                  // Diameter of a node. 
    private int _nodesAmountInGridByX = 0;                                             // Amount of nodes in grid by x Axis.
    private int _nodesAmountInGridByY = 0;                                             // Amount of nodes in grid by y Axis.
    
    #region Properties

    public Vector2 GridSizeInWorld => _gridSizeInWorld;

    public float NodeRadius => _nodeRadius;

    #endregion Properties

    public Node GetNodeWorldPoint(Vector2 nodePositionInWorld)
    {
        float nodeXCoordinatePercentage = (nodePositionInWorld.x + _gridSizeInWorld.x / 2) / _gridSizeInWorld.x;
        float nodeYCoordinatePercentage = (nodePositionInWorld.y + _gridSizeInWorld.y / 2) / _gridSizeInWorld.y;
        nodeXCoordinatePercentage = Mathf.Clamp01(nodeXCoordinatePercentage);
        nodeYCoordinatePercentage = Mathf.Clamp01(nodeYCoordinatePercentage);

        int xCoordinate = Mathf.RoundToInt(nodeXCoordinatePercentage * (_nodesAmountInGridByX - 1));
        int yCoordinate = Mathf.RoundToInt(nodeYCoordinatePercentage * (_nodesAmountInGridByY - 1));
        
        return _grid[xCoordinate, yCoordinate];
    }
    

    private void Start()
    {
        _nodeDiameter = _nodeRadius * 2f;
        _nodesAmountInGridByX = Mathf.RoundToInt(_gridSizeInWorld.x / _nodeDiameter);
        _nodesAmountInGridByY = Mathf.RoundToInt(_gridSizeInWorld.y / _nodeDiameter);
        CreateGrid();
    }


    private void CreateGrid()
    {
        Vector2 worldGridBottomLeft = transform.position - 
                                      (Vector3.right * _nodesAmountInGridByX / 2) -
                                      (Vector3.up * _nodesAmountInGridByY / 2);
        
        _grid = new Node[_nodesAmountInGridByX, _nodesAmountInGridByY];

        for (var x = 0; x < _nodesAmountInGridByX; x++)
        {
            for (var y = 0; y < _nodesAmountInGridByY; y++)
            {
                Vector2 positionInWorld = worldGridBottomLeft + 
                                          Vector2.right * (x * _nodeDiameter + _nodeRadius) +
                                          Vector2.up * (y * _nodeDiameter + _nodeRadius);
                Collider2D overlapInfo = Physics2D.OverlapCircle(positionInWorld, _nodeRadius, _whatIsUnpassable);
                bool isPassable = overlapInfo == null ? true : false;
                
                _grid[x, y] = new Node(isPassable, positionInWorld);
            }
        }
    }
    
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, _gridSizeInWorld);

        if (_grid != null)
        {
            Node playerNode = GetNodeWorldPoint(_player.position);

            foreach (Node node in _grid)
            {
                if (playerNode == node)
                {
                    Debug.Log("Node" + playerNode.PositionInWorld);
                    Gizmos.color = Color.black;
                }
                Gizmos.color = node.IsWalkable ? Color.white : Color.red;
                Gizmos.DrawCube(node.PositionInWorld, Vector2.one * (_nodeDiameter - 0.05f));
            }
        }
    }
}
