using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByGridState : IState
{
    private Pathfinder _pathfinder;

    private Entity _entity;


    public MoveByGridState(Entity entity, Pathfinder pathfinder)
    {
        _entity = entity;
        _pathfinder = pathfinder;
    }

    public void OnEnter()
    {        
        _pathfinder.MoveTo();
        _entity.AnimController.SetAnimation("Move");        
    }

    public void OnExit()
    {
        _pathfinder.EndMove();       
    }

    public void Tick()
    {
        
    }
}
