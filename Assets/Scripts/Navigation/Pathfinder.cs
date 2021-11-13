using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Pathfinder : MonoBehaviour
{
    [SerializeField]
    private UnityEvent<LookingDirection> OnDirectionChanged;


    [SerializeField]
    private EntityData _entityData;      

    private AStar _pathfinding;

    private NodeGrid _grid;

    private float _speed;

    private float _defaultSpeed;

    private WaitForSeconds _timeBetweenMoves;

    private WaitForSeconds _defaultTimeBetweenMoves;

    private Node _goalNode;

    private Node _currentNode;

    private LookingDirection _lookingDirection = LookingDirection.Right;

    private bool _pathSet = false;

    public NodeGrid Grid
    {
        get => _grid;
    }



    public bool PathSet
    {
        get => _pathSet;
    }


    private bool _finished;

    public bool Finished
    {
        get => _finished;
    }

    public Node GoalNode
    {
        get
        {
            if (_goalNode == null)
            {
                return _currentNode;
            }
            else
            {
                return _goalNode;
            }
        }
    }

    public Node CurrentNode
    {
        get => _currentNode;
    }


    private void Start()
    {
        _grid = FindObjectOfType<NodeGrid>();

        _pathfinding = new AStar(_grid);
        _speed = 10 / _entityData.MovementSpeed;
        _timeBetweenMoves = new WaitForSeconds(_speed);
        _defaultSpeed = _speed;
        _defaultTimeBetweenMoves = _timeBetweenMoves;
    }

    public void MoveTo()
    {
        if (_currentNode == _goalNode) return;

        List<Node> nodes = _pathfinding.GetNodePath(_currentNode, _goalNode);

        if (nodes == null) return;

        _finished = false;

        StartCoroutine(MoveByNodes(nodes));
    }

    public void EndMove()
    {
        StopAllCoroutines();
        _finished = false;
        _pathSet = false;
    }


    private IEnumerator MoveByNodes(List<Node> nodes)
    {
        int count = nodes.Count;

        for (int i = 1; i < count; i++)
        {            

            CheckForDirection(nodes[i].WorldPosition, transform.position);

            Vector2 endPos = nodes[i].WorldPosition;

            transform.DOMove(endPos, _speed);

            yield return _timeBetweenMoves;

            _currentNode = nodes[i];
        }

        _pathSet = false;
        _finished = true;        
        
    }

    private void CheckForDirection(Vector3 nextPos, Vector3 currPos)
    {
        float dist = 0.5f;

        if (nextPos.x - currPos.x > dist && _lookingDirection != LookingDirection.Right)
        {
            ChangeDirection(LookingDirection.Right);
        }
        else if (nextPos.x - currPos.x < -dist && _lookingDirection != LookingDirection.Left)
        {
            ChangeDirection(LookingDirection.Left);
        }
        else if (nextPos.y - currPos.y > dist && _lookingDirection != LookingDirection.Up)
        {
            ChangeDirection(LookingDirection.Up);
        }
        else if (nextPos.y - currPos.y < -dist && _lookingDirection != LookingDirection.Down)
        {
            ChangeDirection(LookingDirection.Down);
        }
    }

    private void ChangeDirection(LookingDirection newDirection)
    {
        _lookingDirection = newDirection;
        OnDirectionChanged?.Invoke(_lookingDirection);
    }


    public void SetGoalNode(Node node)
    {        
        if (!_pathSet)
        {
            _goalNode = node;
            _pathSet = true;
        }
        else
        {
            EndMove();
            _goalNode = node;
            MoveTo();
        }

    }

    public void SetStartingNode(Node node)
    {
        _currentNode = node;
        transform.position = node.WorldPosition;
    }

    public void SetSpeed(float speed)
    {
        _speed = 10 / speed;
        _timeBetweenMoves = new WaitForSeconds(_speed);
    }

    public void SetDefaultSpeed()
    {
        _speed = _defaultSpeed;
        _timeBetweenMoves = _defaultTimeBetweenMoves;
    }



}
