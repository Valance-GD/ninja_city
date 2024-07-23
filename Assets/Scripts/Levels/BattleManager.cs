using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }
    private List<GameObject> _enemysOnLevel;
    [SerializeField] private GameObject _endBattleUI;
    [SerializeField] private int _winMoney;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _enemysOnLevel = new List<GameObject>();
    }

    public void AddEnemy(GameObject enemy)
    {
        _enemysOnLevel.Add(enemy);
    }
    public void EnemyDie(GameObject enemy)
    {
        if (_enemysOnLevel.Contains(enemy))
        {
            _enemysOnLevel.Remove(enemy);
            if(_enemysOnLevel.Count ==0)
            {
                _endBattleUI.SetActive(true);
                ResurcesManager.Instance.AddResource("Money", _winMoney);
            }
        }     
    }
}
