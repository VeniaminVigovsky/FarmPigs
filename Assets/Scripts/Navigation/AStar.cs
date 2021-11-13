using System.Collections.Generic;
using UnityEngine;
public class AStar
{
    private List<Node> _openSet;
    private HashSet<Node> _closedSet;

    private Dictionary<Node, int> _gCost;
    private Dictionary<Node, int> _fCost;
    private Dictionary<Node, int> _hCost;

    private Dictionary<Node, Node> _nodeParents;

    private Node _startingNode;
    private Node _goalNode;
    private Node _currentNode;

    private NodeGrid _grid;
    
    public AStar(NodeGrid grid)
    {
        _grid = grid;
    }


    public List<Node> GetNodePath(Node startingPosition, Node goalPosition)
    {        
        Node goal = GetGoalNode(startingPosition, goalPosition);

        if (_nodeParents.Count == 0) return null;

        List<Node> path = new List<Node>();

        Node child = goal;

        while (child != _startingNode)
        {
            path.Add(child);

            child = _nodeParents[child];
        }

        path.Add(child);
        path.Reverse();

        return path;
    }

    private Node GetGoalNode(Node startingPosition, Node goalPosition)
    {
        _openSet = new List<Node>();
        _closedSet = new HashSet<Node>();

        _gCost = new Dictionary<Node, int>();
        _fCost = new Dictionary<Node, int>();
        _hCost = new Dictionary<Node, int>();


        _nodeParents = new Dictionary<Node, Node>();
        

        _startingNode = startingPosition;
        _goalNode = goalPosition;

        _openSet.Add(_startingNode);

        _gCost[_startingNode] = 0;        
        _fCost[_startingNode] = 0;
        _hCost[_startingNode] = GetDistance(_startingNode, _goalNode);
        

        while (_openSet.Count > 0)
        {

            _currentNode = _openSet[0];

            for (int i = 0; i < _openSet.Count; i++)
            {
                if (_fCost[_openSet[i]] < _fCost[_currentNode] || _fCost[_openSet[i]] == _fCost[_currentNode] && _hCost[_openSet[i]] < _hCost[_currentNode])
                {
                    _currentNode = _openSet[i];
                }
            }

            _openSet.Remove(_currentNode);
            _closedSet.Add(_currentNode);

            if (_currentNode == _goalNode)
            {
                return _currentNode;
            }

            Node[] neighbours = _grid.GetNodeNeighbours(_currentNode);

            foreach (var n in neighbours)
            {
                if (!n.IsWalkable || _closedSet.Contains(n))
                    continue;

                int newDistanceToNeighbour = _gCost[_currentNode] + GetDistance(_currentNode, n);

                if (!_openSet.Contains(n) || newDistanceToNeighbour < _gCost[n])
                {
                    _gCost[n] = newDistanceToNeighbour;
                    _hCost[n] = GetDistance(n, _goalNode);
                    _fCost[n] = _gCost[n] + _hCost[n];

                    if (!_openSet.Contains(n))
                        _openSet.Add(n);
                    _nodeParents[n] = _currentNode;
                }
            }
        }

        return _startingNode;
    }
    private int GetDistance(Node nodeA, Node nodeB)
    {
        return Mathf.Abs(nodeA.X - nodeB.X) + Mathf.Abs(nodeA.Y - nodeB.Y);
    }
}
