using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Node _node;

    private float _timer;

    private float _countDownTime;

    private bool _init;

    private List<Entity> _entitesNearby = new List<Entity>();

    public Node Node
    {
        get => _node;
    }

    public void InitializeBomb(Node node)
    {
        _node = node;
        transform.position = node.WorldPosition;
        

    }

    private void Update()
    {
        if (_init)
        {
            _timer += Time.deltaTime;
        }

        if (_timer > _countDownTime)
        {
            Detonate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Entity>(out var entity))
        {
            _entitesNearby.Add(entity);
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Entity>(out var entity))
        {
            if (_entitesNearby.Contains(entity))
                _entitesNearby.Remove(entity);
        }
    }

    public void Detonate()
    {
        if (_entitesNearby.Count <= 0) return;

        foreach (var entity in _entitesNearby)
        {
            entity.OnBombDetonated();
        }

        gameObject.SetActive(false);
    }

}
