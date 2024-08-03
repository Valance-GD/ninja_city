using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private GameObject _attackButton;
    [SerializeField] private GameObject _followButton;
    [SerializeField] private GameObject _stayHomeButton;
    public Transform enemySpawnPoint;
    
    public static int currentRade = 0;

    protected override void Start()
    {
        if (currentRade > _radeWaves.Count - 1)
        {
            _isRadeEnds = true;
            CheckIsEndMap();
        }
        base.Start();
        _followButton.GetComponent<Button>().onClick.AddListener(DefUI);
    }
    public void LoadRade()
    {
        if (currentRade > _radeWaves.Count - 1)
        {
            return;
        }
        _stayHomeButton.SetActive(false);
        _followButton.SetActive(true);
        _followButton.GetComponent<Button>().onClick.RemoveListener(DefUI);
        _followButton.GetComponent<Button>().onClick.AddListener(AttackUI);
        
        foreach (RadeWaveEnemys enemy in _radeWaves[currentRade]._enemysInWave)
        {
            for (int i = 0; i < enemy._countEnemy; i++)
            {
                GameObject spawnEnemy = Instantiate(enemy._enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
                AddEnemy(spawnEnemy);
            }
        }
    }
    private void AttackUI()
    {
        _followButton.SetActive(false);
        _attackButton.SetActive(true);
    }
    private void DefUI()
    {
        _followButton.SetActive(false);
        _stayHomeButton.SetActive(true);
    }
    
    public void SwitchToNextRade()
    {
        _attackButton.SetActive(false);
        _followButton.SetActive(true);
        _followButton.GetComponent<Button>().onClick.RemoveListener(AttackUI);
        _followButton.GetComponent<Button>().onClick.AddListener(DefUI);
        currentRade++;
        if (currentRade > _radeWaves.Count - 1)
        {
            _isRadeEnds = true;
            CheckIsEndMap();
        }
        GameController.Instance.Save();
    }
}
