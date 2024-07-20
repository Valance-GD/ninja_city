using UnityEngine;

public class ResourseSpawn : MonoBehaviour
{

    [SerializeField] private GameObject resourcePrefab;
    [SerializeField] private float resourceHeight = 0.5f;

    private BaseHouse _house;
    private PlayerResurses _playerRes;
    private Vector3 spawnOffset;
    private void Start()
    {
        _house = GetComponentInParent<BaseHouse>();
        _playerRes = FindObjectOfType<PlayerResurses>();
    }
    public void SpawnRes()
    {
        Vector3 spawnPosition = transform.position + spawnOffset;
        GameObject res = Instantiate(resourcePrefab, spawnPosition, Quaternion.identity);
        spawnOffset += Vector3.up * resourceHeight;
        res.GetComponent<ResourseInfo>().TakeInfo(_house.MoneyAmount);
    }
}
