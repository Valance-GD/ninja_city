using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapLevelUI : MonoBehaviour
{
    [SerializeField] private List<Button> _levelsInMap;
    [SerializeField] private Sprite _openLevelIcon;
    [SerializeField] private LevelDoor _door;

    private void Start()
    {
        if (LevelManager.currentLevel > _levelsInMap.Count - 1)
        {
            BattleManager.Instance.LevelEnd();
            BattleManager.Instance.CheckIsEndMap();
        }
        OpenCurrentLevel();
        
    }
    private void OpenCurrentLevel()
    {
        if (LevelManager.currentLevel > _levelsInMap.Count - 1)
        {
            return;
        }
        _levelsInMap[LevelManager.currentLevel].enabled = true;
        _levelsInMap[LevelManager.currentLevel].GetComponent<Image>().sprite = _openLevelIcon;
    }
}
