using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected StateMachine _stateMachine;

    protected Pathfinder _pathfinder;

    protected AnimationController _animController;

    protected bool _nearDetonatedBomb;

    [SerializeField]
    protected EntityData _entityData;

    public StateMachine StateMachine
    {
        get => _stateMachine;
    }

    public bool NearDetonatedBomb
    {
        get => _nearDetonatedBomb;
        set => _nearDetonatedBomb = value;
    }

    public Pathfinder Pathfinder
    {
        get => _pathfinder;
    }

    public AnimationController AnimController
    {
        get => _animController;
    }

    public virtual void Initialize()
    {
        _pathfinder = GetComponent<Pathfinder>();
        _stateMachine = new StateMachine();
        _animController = GetComponent<AnimationController>();
    }

    public void Update()
    {
        _stateMachine?.Tick();
    }

    public virtual void OnBombDetonated()
    {
        _nearDetonatedBomb = true;
    }


}
