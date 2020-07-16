
using UnityEngine;

public class Node
{
    private bool _isWalkable;                      // Determine if current node is walkable.
    private Vector2 _positionInWorld;              // Determine where is node placed in world space.

    #region Properties

    public bool IsWalkable => _isWalkable;

    public Vector2 PositionInWorld
    {
        get => _positionInWorld;
        set => _positionInWorld = value;
    }

    #endregion Properties

    public Node(bool isWalkable, Vector2 positionInWorld)
    {
        _isWalkable = isWalkable;
        _positionInWorld = positionInWorld;
    }
}
