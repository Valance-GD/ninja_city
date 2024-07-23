using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetting : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private int _countEnemy;
    [SerializeField] private Transform _enemySpawn;
    [SerializeField] private bool _isFinalBatttle;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerController player))
        {
            for (int i = 0; i < _countEnemy; i++)
            {
                GameObject enemy = Instantiate(_enemy, _enemySpawn.position, Quaternion.identity);
                if (_isFinalBatttle)
                {
                    BattleManager.Instance.AddEnemy(enemy);
                }
            }
            Destroy(gameObject);
        }  
    }
}
