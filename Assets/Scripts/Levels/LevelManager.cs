using System.Collections.Generic;
using UnityEngine;

public class LevelManager : BattleManager
{
    [SerializeField] private GameObject _ninjaPrefab;
    [SerializeField] private List<LevelSetting> _levels;
    public BoxCollider boxCollider;
    public static int currentLevel;

    protected override void Start()
    {
        PlaceObjectInRandomPosition();
        LoadCurrentLevel();
    }
    public void SwitchToNextLevel()
    {
        currentLevel++;
        GameController.Instance.Save();
    }
    private void LoadCurrentLevel()
    {
        _levels[currentLevel].gameObject.SetActive(true);
    }

    private void PlaceObjectInRandomPosition()
    {
        for (int i = 0; i < GameController.Instance.gameData.alliveNinja; i++)
        {

            Vector3 randomPoint = GetRandomPointInCollider(boxCollider);
            GameObject ninja = Instantiate(_ninjaPrefab, randomPoint, Quaternion.identity);
            FindObjectOfType<NinjaControl>().button.onClick.AddListener(ninja.GetComponent<Ninja>().StartMoveToTarget);
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
}
