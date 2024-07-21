using System.Collections.Generic;
using UnityEngine;

public class ResourseSpawn : MonoBehaviour
{

    [SerializeField] private GameObject resourcePrefab;
    [SerializeField] private float resourceHeight = 0.5f;
    [SerializeField] private float resourceRotation = 0;

    private BaseHouse _house;
    private List<GameObject> _resoursesToCollect = new List<GameObject>();
    private Vector3 spawnOffset;
    private float rotationOffset;
    private void Start()
    {
        _house = GetComponentInParent<BaseHouse>();
    }
    public void SpawnRes()
    {
        Vector3 spawnPosition = transform.position + spawnOffset;
        Quaternion rotation = Quaternion.Euler(0,rotationOffset , 0);
        
        GameObject res = Instantiate(resourcePrefab, spawnPosition, rotation);
        _resoursesToCollect.Add(res);
        spawnOffset += Vector3.up * resourceHeight;
        rotationOffset += resourceRotation;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        { 
            spawnOffset = Vector3.zero;
            foreach (GameObject res in _resoursesToCollect)
            {
                player.GetComponent<PlayerResurces>().AddResource(_house.ResourseType, _house.ResAmount);
                Destroy(res);
            }
            _resoursesToCollect.Clear();
        }
    }
}
