using UnityEngine;

public class IdleWithTimerState : IState
{
    private float _duration;

    private float _startTime;

    private Entity _entity;

    public bool TimesUp
    {
        get => _startTime + _duration < Time.time;
    }

    public IdleWithTimerState(Entity entity, float idleDuration)
    {
        _entity = entity;
        _duration = idleDuration;
    }

    public void OnEnter()
    {
        _startTime = Time.time;
        _entity.AnimController.SetAnimation("Idle");
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        
    }
}
