using UnityEngine;

namespace AStar
{
    public class Node
    {
        private bool _isPassable;                      // Determine if current node is walkable.
        private Vector2 _positionInWorld;              // Determine where is node placed in world space.
        private int _gridX;                            // Determine node position by X-Axis in grid.
        private int _gridY;                            // Determine node position by Y-Axis in grid.
    
        private int _gCost;                            // Distance from start position to target position. 
        private int _hCost;                            // Distance from target position to start position (heuristic).
        private int _fCost;                            // Sum of gCost and hCost.
        private Node _parent;                          // Parent node of this node. 
    
        #region Properties

        public bool IsWalkable => _isPassable;

        public Vector2 PositionInWorld
        {
            get => _positionInWorld;
            set => _positionInWorld = value;
        }

        public int GridX => _gridX;

        public int GridY => _gridY;

        public int GCost
        {
            get => _gCost;
            set => _gCost = value;
        }

        public int HCost
        {
            get => _hCost;
            set => _hCost = value;
        }

        public int FCost
        {
            get => _gCost + _hCost;
            set => _fCost = value;
        }

        public Node Parent
        {
            get => _parent;
            set => _parent = value;
        }

        #endregion Properties

        public Node(bool isPassable, Vector2 positionInWorld, int gridX, int gridY)
        {
            _isPassable = isPassable;
            _positionInWorld = positionInWorld;
            _gridX = gridX;
            _gridY = gridY;
        }
    }
}
