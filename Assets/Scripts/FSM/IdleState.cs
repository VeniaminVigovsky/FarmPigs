using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private Entity _entity; 

    public IdleState(Entity entity)
    {
        _entity = entity;
    }

    public void OnEnter()
    {
        _entity.AnimController.SetAnimation("Idle");
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        
    }
}
