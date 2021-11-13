using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToBombState : IState
{
    private Farmer _farmer;

    private Pathfinder _pathfinder;

    private Bomb _bombToDefuse;

    public Bomb BombToDefuse
    {
        get => _bombToDefuse;
    }

    public GoToBombState(Farmer farmer, Pathfinder pathfinder)
    {
        _farmer = farmer;
        _pathfinder = pathfinder;
    }

    public void OnEnter()
    {
        _bombToDefuse = _farmer.BombsToDefuse[0];
        _pathfinder.EndMove();
        _pathfinder.SetGoalNode(_bombToDefuse.Node);
        _pathfinder.MoveTo();
        _farmer.AnimController.SetAnimation("Angry");
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        
    }
}
