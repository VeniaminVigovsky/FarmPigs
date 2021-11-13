using UnityEngine;
using System.Collections.Generic;
public class NodeGrid : MonoBehaviour
{
    private Node[,] _grid;

    [SerializeField]
    private int _gridSizeX, _gridSizeY;

    [SerializeField]
    private Transform _nodes;

    [SerializeField]
    private LayerMask _obstacleLayer;

    private List<Node> _walkableNodes = new List<Node>();

    private void Awake()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        _grid = new Node[_gridSizeX, _gridSizeY];

        Node[] childNodes = _nodes.GetComponentsInChildren<Node>();

        int nodeCount = childNodes.Length;

        int x = 0, y = 0;

        for (int i = 0; i < nodeCount; i++)
        {
            if (x == _gridSizeX)
            {
                y++;
                x = 0;                
            }

            if (x >= _gridSizeX || y >= _gridSizeY)
            {
                break;
            }

            Node n = childNodes[i];

            n.TryGetComponent<BoxCollider2D>(out var nodeCollider);
            
            var nodeSize = nodeCollider != null ? nodeCollider.size : Vector2.zero;

            bool isWalkable = !Physics2D.BoxCast(n.transform.position, nodeSize, 0.0f, Vector2.zero, 0.0f, _obstacleLayer);

            if (isWalkable) _walkableNodes.Add(n);

            n.SetNode(isWalkable, x, y);

            _grid[x, y] = n;               

            x++;

        }  
    }


    public Node GetNodeByIndeces(int x, int y)
    {
        if (x < 0 || y < 0 || x >= _gridSizeX || y >= _gridSizeY)
        {
            return null;
        }
        else
        {
            return _grid[x, y];
        }
    }

    public Node[] GetNodeNeighbours(Node node)
    {  
        List<Node> n = new List<Node>();       

        int i = 0;

        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                int nX = node.X + x;
                int nY = node.Y + y;
                
                if (nX < 0 || nX >= _gridSizeX ||
                    nY < 0 || nY >= _gridSizeY ||
                    (x == 0 && y == 0) || (x !=0 && y !=0)
                    )
                {
                    continue;
                }
                else
                {                    
                    n.Add( _grid[node.X + x, node.Y + y]);
                    i++;
                }

            }
        }

        int length = n.Count;

        Node[] neighbours = new Node[length];

        for (int j = 0; j < length; j++)
        {
            neighbours[j] = n[j];
        }

        return neighbours;
    }


    public int GridSizeX
    {
        get => _gridSizeX;
    }

    public int GridSizeY
    {
        get => _gridSizeY;
    }

    public Node[,] Grid
    {
        get => _grid;
    }

    public List<Node> WalkableNodes
    {
        get => _walkableNodes;
    }
}
