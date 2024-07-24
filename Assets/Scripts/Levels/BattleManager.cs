using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }
    private List<GameObject> _enemysOnLevel;
    [SerializeField] private int _winMoney;
    [SerializeField] private GameObject _endBattleUI;

    [Header("Level")]
    [SerializeField] private GameObject _ninjaPrefab;
    [SerializeField] private BoxCollider _ninjaSpawnPoint;
    [SerializeField] private bool _isOnLevel;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _enemysOnLevel = new List<GameObject>();
    }
    public BoxCollider boxCollider;

    void Start()
    {
        if (_isOnLevel)
        {
            PlaceObjectInRandomPosition();
        }
    }

    private void PlaceObjectInRandomPosition()
    {
        for (int i = 0; i < GameController.Instance.gameData.alliveNinja; i++)
        {


            if (boxCollider != null)
            {
                Vector3 randomPoint = GetRandomPointInCollider(boxCollider);
                GameObject ninja = Instantiate(_ninjaPrefab, randomPoint, Quaternion.identity);
                FindObjectOfType<NinjaControl>().button.onClick.AddListener(ninja.GetComponent<Ninja>().StartMoveToTarget);

            }
            else
            {
                Debug.LogError("BoxCollider is not assigned.");
            }

        }
    }

    private Vector3 GetRandomPointInCollider(BoxCollider boxCollider)
    {
        Vector3 min = boxCollider.bounds.min;
        Vector3 max = boxCollider.bounds.max;

        float randomX = Random.Range(min.x, max.x);
        float randomY = Random.Range(min.y, max.y);
        float randomZ = Random.Range(min.z, max.z);

        return new Vector3(randomX, randomY, randomZ);
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
