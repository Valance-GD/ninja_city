using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaHouse : MonoBehaviour
{
    [SerializeField] private float _secondToSpawn;
    [SerializeField] private int _alliveNinjaCount;
    [SerializeField] private GameObject _ninjaPrefab;
    [SerializeField] private Transform _respawnPoint;
    private NinjaControl _ninjaControl;
    private List<GameObject> _allaviNinjaList;
    private Coroutine respawnCoroutine;
    private void Start()
    {
        _allaviNinjaList = new List<GameObject>();

        NinjaDie();

    }
    public void NinjaDie(GameObject target = null)
    {
        if (target != null)
        {
            _allaviNinjaList.Remove(target);

        }
        if (_ninjaControl == null)
        {
            _ninjaControl = FindObjectOfType<NinjaControl>();
        }
        if (respawnCoroutine == null)
        {
            respawnCoroutine = StartCoroutine(RespawnNinja());
        }

    }
    private IEnumerator RespawnNinja()
    {
        while (_allaviNinjaList.Count < _alliveNinjaCount)
        {
            yield return new WaitForSeconds(_secondToSpawn);
            GameObject ninja = Instantiate(_ninjaPrefab, _respawnPoint.position, Quaternion.identity);
            ninja.GetComponent<Ninja>()._ninjaHouse = this;
            _allaviNinjaList.Add(ninja);
            _ninjaControl.button.onClick.AddListener(ninja.GetComponent<Ninja>().StartMoveToTarget);
        }
        respawnCoroutine = null;
        StopAllCoroutines();
    }
}
