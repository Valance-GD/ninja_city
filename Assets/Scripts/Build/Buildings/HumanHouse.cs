using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanHouse : MonoBehaviour
{
    [SerializeField] private float _secondToSpawn;
    [SerializeField] private int _alliveHumanCount;
    [SerializeField] private GameObject _humanPrefab;
    [SerializeField] private Transform _respawnPoint;
    private List<GameObject> _allaviHumanList;
    private Coroutine respawnCoroutine;
    private void Start()
    {
        _allaviHumanList = new List<GameObject>();
        HumanDie();
    }
    public void HumanDie(GameObject target = null)
    {
        if (target != null)
        {
            _allaviHumanList.Remove(target);

        }
        if (respawnCoroutine == null)
        {
            respawnCoroutine = StartCoroutine(RespawnHuman());
        }

    }
    private IEnumerator RespawnHuman()
    {
        while (_allaviHumanList.Count < _alliveHumanCount)
        {
            yield return new WaitForSeconds(_secondToSpawn);
            GameObject human = Instantiate(_humanPrefab, _respawnPoint.position, Quaternion.identity);
            human.GetComponent<Human>()._humanHouse = this;
            _allaviHumanList.Add(human);      
        }
        respawnCoroutine = null;
        StopAllCoroutines();
    }
}
