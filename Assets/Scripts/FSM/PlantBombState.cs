using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBombState : IState
{
    private Entity _entity;

    private BombPool _pool;

    private int _bombCount;

    private bool _plantBomb;
    private bool _bombIsPlanted;

    public bool PlantBomb
    {
        get => _plantBomb;        
    }

    public bool HasBombs
    {
        get => _bombCount > 0;
    }

    public bool BombIsPlanted
    {
        get => _bombIsPlanted;
    }

    public PlantBombState(Entity entity)
    {
        _entity = entity;
        _pool = GameObject.FindObjectOfType<BombPool>();
        _bombCount = _pool.Size;
        InputController.BombInputReceived += TogglePlantBomb;

    }


    public void OnEnter()
    {        
        GameObject b = _pool.GetBomb();
        b.TryGetComponent<Bomb>(out var bomb);
        bomb?.InitializeBomb(_entity.Pathfinder.CurrentNode);
        _plantBomb = false;
        _bombIsPlanted = true;
        _entity.AnimController.SetAnimation("Idle");
        _bombCount--;
    }

    public void OnExit()
    {
        _bombIsPlanted = false;
    }

    public void Tick()
    {
        
    }

    private void TogglePlantBomb()
    {
        if (_entity.StateMachine.CurrentState is IdleState)
            _plantBomb = true;
    }
}
