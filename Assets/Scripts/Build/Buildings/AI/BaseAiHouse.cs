using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAiHouse : MonoBehaviour
{

    [SerializeField] protected float _secondToSpawn;
    [SerializeField] protected int _alliveAICountMax;
    [SerializeField] protected GameObject _AIPrefab;
    [SerializeField] protected Transform _respawnPoint;
    [SerializeField] protected int _foodForRespawn;
    protected List<GameObject> _allaviAIList;
    public int _alliveAICount;
    protected Coroutine respawnCoroutine;
    private void Start()
    {
        _allaviAIList = new List<GameObject>();
        RespawnInStart();
        AIDie();
    }

    public virtual void AIDie(GameObject target = null)
    {
        if (target != null)
        {
            _allaviAIList.Remove(target);

        }
        if (respawnCoroutine == null)
        {
            respawnCoroutine = StartCoroutine(RespawnHuman());
        }

    }
    private IEnumerator RespawnHuman()
    {
        while (_allaviAIList.Count < _alliveAICountMax && ResurcesManager.Instance.SpendResource("Food", _foodForRespawn))
        {
                yield return new WaitForSeconds(_secondToSpawn);
                RespawnProcess();
        }
        respawnCoroutine = null;
        NinjasManager.Instance.SaveNinjasHouse();
        StopAllCoroutines();
    }
    protected virtual void RespawnProcess()
    {

    }
    private void RespawnInStart()
    {
        for (int i = 0; i < _alliveAICount; i++)
        {
            RespawnProcess();
        }
        NinjasManager.Instance.SaveNinjasHouse();
    }
}
