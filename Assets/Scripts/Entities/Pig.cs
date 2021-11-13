using System;
using UnityEngine;

public class Pig : Entity
{
    public static event Action PigCaptured;
    public override void Initialize()
    {
        base.Initialize();

        var moveState = new MoveByGridState(this, _pathfinder);
        
        var idleState = new IdleState(this);

        var plantBombState = new PlantBombState(this);

        _stateMachine.AddTransition(idleState, moveState, () => _pathfinder.PathSet);

        _stateMachine.AddTransition(moveState, idleState, () => _pathfinder.Finished);


        _stateMachine.AddTransition(idleState, plantBombState, () => plantBombState.HasBombs && plantBombState.PlantBomb);
        _stateMachine.AddTransition(plantBombState, idleState, () => plantBombState.BombIsPlanted);

        _stateMachine.SetState(idleState);
    }

    private void OnEnable()
    {
        Node.NodeSelected += ForwardNodeToPathfinder;
    }

    private void OnDisable()
    {
        Node.NodeSelected -= ForwardNodeToPathfinder;
    }

    private void ForwardNodeToPathfinder(Node node)
    {        
        _pathfinder?.SetGoalNode(node);        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Farmer>() != null || collision.GetComponent<Dog>() != null)
        {
            PigCaptured?.Invoke();
        }
    }


}
