using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rade : MonoBehaviour
{
    [SerializeField] private int _enemyCount;
    [SerializeField] private GameObject _enemy;
    public void StartRade()
    {
        for (int i = 0; i < _enemyCount; i++)
        {
            Instantiate(_enemy);
        }
    }
 }
