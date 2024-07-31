using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NinjaPrefabs
{
    public GameObject ninjaPrefab;
    public string ninjaType;
}
public class LevelManager : BattleManager
{
    [SerializeField] private List<LevelSetting> _levels;
    [SerializeField] private List<NinjaPrefabs> _ninjaPrefabs;
    public BoxCollider boxCollider; 
    public static int currentLevel;

    protected override void Start()
    {
        PlaceNinjasInRandomPosition();
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

    private void PlaceNinjasInRandomPosition()
    {
        NinjaControl control = FindObjectOfType<NinjaControl>();
        foreach (Ninjas ninja in GameController.Instance.gameData.ninjas)
        {
            NinjaPrefabs currentNinjaType = _ninjaPrefabs.Find(n => n.ninjaType == ninja.ninjaType);
            for (int i = 0; i < ninja.alliveNinja; i++)
            {
                Vector3 randomPoint = GetRandomPointInCollider(boxCollider);
                GameObject instantNinja = Instantiate(currentNinjaType.ninjaPrefab, randomPoint, Quaternion.identity);
                control.button.onClick.AddListener(instantNinja.GetComponent<Ninja>().StartMoveToTarget);
            }
        }
        
    }
    private Vector3 GetRandomPointInCollider(BoxCollider boxCollider)
    {
        Vector3 min = boxCollider.bounds.min;
        Vector3 max = boxCollider.bounds.max;

        float randomX = UnityEngine.Random.Range(min.x, max.x);
        float randomY = UnityEngine.Random.Range(min.y, max.y);
        float randomZ = UnityEngine.Random.Range(min.z, max.z);

        return new Vector3(randomX, randomY, randomZ);
    }
}
