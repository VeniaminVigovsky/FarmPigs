using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detonator : MonoBehaviour
{
    private Bomb _bomb;

    public Bomb Bomb
    {
        get => _bomb;
    }

    private void Awake()
    {
        _bomb = GetComponentInParent<Bomb>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Farmer>(out var farmer))
        {
            if (!farmer.BombsToDefuse.Contains(_bomb))
                _bomb.Detonate();
        }
    }
}
