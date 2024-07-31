using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RadeWaveEnemys
{
    public GameObject _enemyPrefab;
    public int _countEnemy;
}
[Serializable]
public class EnemysInRade
{
    public List<RadeWaveEnemys> _enemysInWave;
}
public class RadeManager : BattleManager
{
    [SerializeField] private List<EnemysInRade> _radeWaves;
    public Transform enemySpawnPoint;
    public static int currentRade = 0;


    public void LoadRade()
    {
        if (currentRade > _radeWaves.Count - 1)
        {
            currentRade = _radeWaves.Count - 1;
        }
        foreach (RadeWaveEnemys enemy in _radeWaves[currentRade]._enemysInWave)
        {
            for (int i = 0; i < enemy._countEnemy; i++)
            {
                GameObject spawnEnemy = Instantiate(enemy._enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
                AddEnemy(spawnEnemy);
            }
        }
    }
    public void SwitchToNextRade()
    {
        currentRade++;
        GameController.Instance.Save();
    }
}
