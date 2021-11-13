using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyAboutBombState : IState
{
    private float _duration;

    private float _startTime;

    private Dog _dog;

    public bool Notified
    {
        get => _duration + _startTime < Time.time;
    }

    public NotifyAboutBombState(Dog dog, float duration)
    {
        _dog = dog;
        _duration = duration;
    }

    public void OnEnter()
    {
        _dog.ToggleNewBombNoticed(false);
        _startTime = Time.time;
        _dog.AnimController.SetAnimation("Notify");
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        
    }
}
