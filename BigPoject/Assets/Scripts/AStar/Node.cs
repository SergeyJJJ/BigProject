
using UnityEngine;

public class Node
{
    private bool _isPassable;                      // Determine if current node is walkable.
    private Vector2 _positionInWorld;              // Determine where is node placed in world space.

    #region Properties

    public bool IsWalkable => _isPassable;

    public Vector2 PositionInWorld
    {
        get => _positionInWorld;
        set => _positionInWorld = value;
    }

    #endregion Properties

    public Node(bool isPassable, Vector2 positionInWorld)
    {
        _isPassable = isPassable;
        _positionInWorld = positionInWorld;
    }
}
