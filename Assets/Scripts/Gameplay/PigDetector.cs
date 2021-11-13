using System;
using UnityEngine;

public class PigDetector : MonoBehaviour
{
    private Transform _pigTransform;

    private Pig _pig;

    private float _distance = 4f;

    private float _lostDistance = 8f;

    private bool _pigLost;

    public Pig Pig
    {
        get => _pig;
    }

    public bool PigLost
    {
        get => _pigLost;
    }

    public void Init()
    {        
        _pig = FindObjectOfType<Pig>();
        _pigTransform = _pig.transform;
        _pigLost = true;
    }

    private void Update()
    {
        if (Vector2.Distance(_pigTransform.position, transform.position) < _distance && _pigLost)
        {
            _pigLost = false;
        }
        else if (Vector2.Distance(_pigTransform.position, transform.position) > _lostDistance && !_pigLost)
        {
            _pigLost = true;
        }

    }
}
