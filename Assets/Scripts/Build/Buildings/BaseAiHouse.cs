using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAiHouse : MonoBehaviour
{

    [SerializeField] protected float _secondToSpawn;
    [SerializeField] protected int _alliveAICount;
    [SerializeField] protected GameObject _AIPrefab;
    [SerializeField] protected Transform _respawnPoint;
    [SerializeField] protected int _foodForRespawn;
    protected List<GameObject> _allaviAIList;
    protected Coroutine respawnCoroutine;
    private void Start()
    {
        _allaviAIList = new List<GameObject>();
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
        if (ResurcesManager.Instance.SpendResource("Food", _foodForRespawn))
        {
            while (_allaviAIList.Count < _alliveAICount)
            {

                yield return new WaitForSeconds(_secondToSpawn);
                RespawnProcess();
            }
        }
        respawnCoroutine = null;
        StopAllCoroutines();
    }
    protected virtual void RespawnProcess()
    {

    }
}
