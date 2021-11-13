using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPool : MonoBehaviour
{
    [SerializeField]
    private GameObject _bombPrefab;

    [SerializeField]
    private int _poolSize;

    private Stack<GameObject> _pool = new Stack<GameObject>();

    public int Size
    {
        get => _poolSize;
    }

    private void Awake()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject b = Instantiate(_bombPrefab, transform);
            _pool.Push(b);            
        }
    }

    public GameObject GetBomb()
    {
        if (_pool.Count > 0)
        {
            GameObject b = _pool.Pop();
            b.transform.parent = null;

            return b;
        }
        else return null;
    }
}
