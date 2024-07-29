using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }
    protected List<GameObject> _enemysOnLevel;
    [SerializeField] protected int _winMoney;
    [SerializeField] protected GameObject _endBattleUI;    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _enemysOnLevel = new List<GameObject>();
    }
    

    protected virtual void Start()
    {
        
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
            if (_enemysOnLevel.Count == 0)
            {
                _endBattleUI.SetActive(true);
                ResurcesManager.Instance.AddResource("Money", _winMoney);
            }
        }
    }
}
