using System;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Entity
{
    private bool _newBombNoticed;

    public static event Action<Bomb> NewBombNoticed;

    private List<Bomb> _noticedBombs = new List<Bomb>();

    private PigDetector _pigDetector;

    public override void Initialize()
    {
        base.Initialize();

        _pigDetector = GetComponent<PigDetector>();

        _pigDetector.Init();

        var moveState = new MoveByGridState(this,_pathfinder);

        var idleState = new IdleWithTimerState(this, 2);

        var selectRandomDestinationState = new SelectRandomDestinationState(this, _pathfinder);

        var getCoveredInMudState = new GetCoveredInMudState(this, 2);

        var notifyAboutBombState = new NotifyAboutBombState(this, 2);

        var chasePigState = new ChasePigState(this, _pigDetector, _pathfinder, _entityData, 7f);

        _stateMachine.AddTransition(selectRandomDestinationState, moveState, () => _pathfinder.PathSet);

        _stateMachine.AddTransition(moveState, idleState, () => _pathfinder.Finished);

        _stateMachine.AddTransition(idleState, selectRandomDestinationState, () => idleState.TimesUp);

        _stateMachine.AddAnyTransition(getCoveredInMudState, () => _nearDetonatedBomb);

        _stateMachine.AddAnyTransition(notifyAboutBombState, () => _newBombNoticed);

        _stateMachine.AddTransition(getCoveredInMudState, selectRandomDestinationState, () => getCoveredInMudState.TimesUp);

        _stateMachine.AddTransition(notifyAboutBombState, selectRandomDestinationState, () => notifyAboutBombState.Notified);

        _stateMachine.AddTransition(moveState, chasePigState, () => !_pigDetector.PigLost && chasePigState.TimePassed);

        _stateMachine.AddTransition(idleState, chasePigState, () => !_pigDetector.PigLost && chasePigState.TimePassed);

        _stateMachine.AddTransition(chasePigState, idleState, () => _pigDetector.PigLost || chasePigState.TimesUp);

        _stateMachine.SetState(idleState);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Detonator>(out var detonator))
        {
            Bomb bomb = detonator.Bomb;

            if (!_noticedBombs.Contains(bomb))
            {
                _newBombNoticed = true;
                NewBombNoticed?.Invoke(bomb);
            }
        }
    }

    public void ToggleNewBombNoticed(bool value)
    {
        _newBombNoticed = value;
    }
}
