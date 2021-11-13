using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCoveredInMudState : IState
{
    private AnimationController _animationController;

    private Entity _entity;

    private float _recoverTime;

    private float _startTime;

    public bool TimesUp
    {
        get => _startTime + _recoverTime < Time.time;
    }

    public GetCoveredInMudState(Entity entity, float recoverTime)
    {
        _entity = entity;
        _recoverTime = recoverTime;
    }

    public void OnEnter()
    {
        _entity.NearDetonatedBomb = false;
        _startTime = Time.time;
        _entity.AnimController.SetAnimation("Dirty");
    }

    public void OnExit()
    {
    }

    public void Tick()
    {

    }
}
