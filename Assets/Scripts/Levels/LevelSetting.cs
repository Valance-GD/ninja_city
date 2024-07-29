using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class WaveEnemys
{
    public GameObject _enemyPrefab;
    public int _countEnemy;
}
[Serializable]
public class EnemysInWave
{
    public List<WaveEnemys> _enemysInWave;
    public LevelWaveTrigger _triggerZone;
    public bool isTriggered = false;
}
public class LevelSetting : MonoBehaviour
{
    [SerializeField] private List<EnemysInWave> _levelWaves;
    

    public void Start()
    {
        foreach (EnemysInWave wave in _levelWaves)
        {
            wave._triggerZone._waveSettings = this;
        }
    }

    public void LoadWave(LevelWaveTrigger triggerZone)
    {
        EnemysInWave wave = _levelWaves.FirstOrDefault(wave => wave._triggerZone == triggerZone);
       foreach (WaveEnemys enemy in wave._enemysInWave)
       {
            for (int i = 0; i < enemy._countEnemy; i++)
            {
                GameObject spawnEnemy = Instantiate(enemy._enemyPrefab, triggerZone._spawnEnemyPoint.position, Quaternion.identity);
                wave.isTriggered = true;
            }
       }
       if (_levelWaves.All(waves => waves.isTriggered))
       {
            List<Enemy>enemys = FindObjectsOfType<Enemy>().ToList();
            foreach (Enemy enemy in enemys)
            {
                LevelManager.Instance.AddEnemy(enemy.gameObject);
            }
       }
    }

}
