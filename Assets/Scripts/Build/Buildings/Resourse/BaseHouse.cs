using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHouse : MonoBehaviour
{
    [SerializeField] protected float _secondToCollect;
    [SerializeField] protected int _resAmount;
    [SerializeField] protected string _resourseType;
    [SerializeField] protected ResourseSpawn _spawnResPoint;
    public int ResAmount => _resAmount; 
    public string ResourseType => _resourseType;

    private float _defoultSecondToCollect;
    private void Start()
    {
        _defoultSecondToCollect = _secondToCollect;

    }
    protected virtual void Update()
    {
        _secondToCollect -= Time.deltaTime;
        if (_secondToCollect <= 0)
        {
            _secondToCollect = _defoultSecondToCollect;
            _spawnResPoint.SpawnRes();
        }
    }


}
