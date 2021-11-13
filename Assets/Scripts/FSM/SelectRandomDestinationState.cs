using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRandomDestinationState : IState
{
    private NodeGrid _grid;

    private Node _destinationNode;

    private Pathfinder _pathfinder;

    private Entity _entity;

    public Node DestinationNode
    {
        get => _destinationNode;
    }
    public SelectRandomDestinationState(Entity entity, Pathfinder pathfinder)
    {
        _grid = GameObject.FindObjectOfType<NodeGrid>();

        _pathfinder = pathfinder;

        _entity = entity;
    }
    public void OnEnter()
    {       

        _destinationNode = _grid.WalkableNodes[Random.Range(0, _grid.WalkableNodes.Count)];
        _pathfinder.SetGoalNode(_destinationNode);
        _entity.AnimController.SetAnimation("Idle");
    }

    public void OnExit()
    {
        _destinationNode = null;
    }

    public void Tick()
    {
        
    }
}
