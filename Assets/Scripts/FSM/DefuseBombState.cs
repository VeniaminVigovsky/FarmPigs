using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefuseBombState : IState
{
    private bool _defused;

    private GoToBombState _goToBombState;

    private Farmer _farmer;

    public bool Defused
    {
        get => _defused;
    }

    public DefuseBombState(Farmer farmer, GoToBombState goToBombState)
    {
        _farmer = farmer;
        _goToBombState = goToBombState;
    }

    public void OnEnter()
    {
        _farmer.AnimController.SetAnimation("Defuse");

        Bomb bomb = _goToBombState.BombToDefuse;

        bomb.gameObject.SetActive(false);

        _farmer.RemoveDefusedBomb(bomb);

        _defused = true;
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        
    }
}
