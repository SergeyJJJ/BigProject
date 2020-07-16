using System;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsPassable = Physics2D.AllLayers;
    [SerializeField] private Vector2 _gridSizeInWorld = Vector2.zero;
    [SerializeField] private float nodeRadius;
    private Node[,] _grid = null;

    #region Properties

    public Vector2 GridSizeInWorld => _gridSizeInWorld;

    public float NodeRadius => nodeRadius;

    #endregion Properties

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, _gridSizeInWorld);
    }
}
