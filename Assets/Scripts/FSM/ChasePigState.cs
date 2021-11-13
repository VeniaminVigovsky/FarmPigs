using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePigState : IState
{
    private Entity _entity;

    private float _startTime;

    private float _pathSetTime;

    private float _timeBetweenSet;

    private PigDetector _pigDetector;

    private Pathfinder _pathfinder;

    private Pathfinder _pigPathfinder;

    private float _chaseSpeed;

    private float _stateDuration;

    private float _stateFinished;

    public bool TimesUp
    {
        get => _startTime + _stateDuration < Time.time;
    }

    public bool TimePassed
    {
        get => _stateFinished + _stateDuration / 2 < Time.time;
    }

    public ChasePigState(Entity entity, PigDetector pigDetector, Pathfinder pathfinder, EntityData entityData, float stateDuration)
    {
        _pigDetector = pigDetector;
        _pathfinder = pathfinder;
        _chaseSpeed = entityData.MovementSpeed * 3;
        _entity = entity;
        _stateDuration = stateDuration;
        _stateFinished = -500f;
        _timeBetweenSet = 7f;
    }

    public float StartTime
    {
        get => _startTime;
    }

    public void OnEnter()
    {        
        _startTime = Time.time;
        _pigPathfinder = _pigDetector.Pig.Pathfinder;
        SetPath();
        _pathfinder.SetSpeed(_chaseSpeed);
        _entity.AnimController.SetAnimation("Angry");
    }

    public void OnExit()
    {        
        _pathfinder.EndMove();
        _pathfinder.SetDefaultSpeed();
        _stateFinished = Time.time;
    }

    public void Tick()
    {
        if (_pathSetTime + _timeBetweenSet < Time.time)
        {
            SetPath();
        }
    }

    private void SetPath()
    {
        if (!_pathfinder.PathSet)
        {
            _pathfinder.SetGoalNode(_pigPathfinder.CurrentNode);
            _pathfinder.MoveTo();
        }
        else
        {
            _pathfinder.SetGoalNode(_pigPathfinder.CurrentNode);
        }

        _pathSetTime = Time.time;
        
    }
}
