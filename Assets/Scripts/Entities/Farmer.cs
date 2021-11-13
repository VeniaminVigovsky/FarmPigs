using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : Entity
{
    private List<Bomb> _bombsToDefuse = new List<Bomb>();

    public List<Bomb> BombsToDefuse
    {
        get => _bombsToDefuse;
    }

    public override void Initialize()
    {
        base.Initialize();

        Dog.NewBombNoticed += OnNewBombNoticed;

        var moveState = new MoveByGridState(this, _pathfinder);

        var idleState = new IdleWithTimerState(this, 3);

        var selectRandomDestinationState = new SelectRandomDestinationState(this, _pathfinder);

        var getCoveredInMudState = new GetCoveredInMudState(this, 2);

        var goToBombState = new GoToBombState(this, _pathfinder);

        var defuseBomb = new DefuseBombState(this, goToBombState);

        _stateMachine.AddTransition(selectRandomDestinationState, moveState, () => _pathfinder.PathSet);

        _stateMachine.AddTransition(moveState, idleState, () => _pathfinder.Finished);

        _stateMachine.AddTransition(idleState, selectRandomDestinationState, () => idleState.TimesUp && _bombsToDefuse.Count <= 0);

        _stateMachine.AddTransition(idleState, goToBombState, () => idleState.TimesUp && _bombsToDefuse.Count > 0);

        _stateMachine.AddTransition(moveState, goToBombState, () => _bombsToDefuse.Count > 0);

        _stateMachine.AddTransition(goToBombState, defuseBomb , () => _pathfinder.Finished);

        _stateMachine.AddTransition(defuseBomb, goToBombState, () => defuseBomb.Defused && _bombsToDefuse.Count > 0);

        _stateMachine.AddTransition(defuseBomb, selectRandomDestinationState, () => defuseBomb.Defused && _bombsToDefuse.Count <= 0);

        _stateMachine.AddTransition(getCoveredInMudState, selectRandomDestinationState, () => getCoveredInMudState.TimesUp && _bombsToDefuse.Count <= 0);

        _stateMachine.AddTransition(getCoveredInMudState, goToBombState, () => getCoveredInMudState.TimesUp && _bombsToDefuse.Count > 0);



        _stateMachine.AddAnyTransition(getCoveredInMudState, () => _nearDetonatedBomb);

        _stateMachine.SetState(idleState);        
    }

    private void OnNewBombNoticed(Bomb bomb)
    {
        if (!_bombsToDefuse.Contains(bomb))
        {
            _bombsToDefuse.Add(bomb);
        }
    }

    public void RemoveDefusedBomb(Bomb bomb)
    {
        _bombsToDefuse.Remove(bomb);
    }

    private void OnDisable()
    {
        Dog.NewBombNoticed -= OnNewBombNoticed;
    }
}
