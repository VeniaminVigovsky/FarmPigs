using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    private Vector3 _worldPosition;
    private bool _isWalkable;    
    private int _gridX, _gridY;

    public static Action<Node> NodeSelected;

    public void SetNode(bool isWalkable, int gridX, int gridY)
    {
        _worldPosition = transform.position;
        _isWalkable = isWalkable;
        _gridX = gridX;
        _gridY = gridY;
    }

    public bool IsWalkable
    {
        get => _isWalkable;
    }

    public Vector3 WorldPosition
    {
        get => _worldPosition;
    }

    public int X
    {
        get => _gridX;
    }

    public int Y
    {
        get => _gridY;
    }

    private void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        NodeSelected?.Invoke(this);        
    }

}
