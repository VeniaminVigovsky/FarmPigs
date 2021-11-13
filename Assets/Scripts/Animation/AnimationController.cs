using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    private LookingDirection _currentDirection;

    [SerializeField]
    private LookingDirection _defaultLookingDirection;

    private string _currentAnimName;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _currentDirection = _defaultLookingDirection;
    }

    public void ChangeDirection(LookingDirection direction)
    {
        if (direction == _currentDirection) return;

        _currentDirection = direction;
        SetAnimation(_currentAnimName);
    }

    public void SetAnimation(string animName)
    {
        _currentAnimName = animName;
        string name = animName + "_" + _currentDirection.ToString();
        _animator.Play(name);        
    }
}
