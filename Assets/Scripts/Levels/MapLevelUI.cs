using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLevelUI : MonoBehaviour
{
    [SerializeField] private List<Button> _levelsInMap;
    [SerializeField] private Sprite _openLevelIcon;

    private void Start()
    {
        OpenCurrentLevel();
    }
    private void OpenCurrentLevel()
    {
        _levelsInMap[LevelManager.currentLevel].enabled = true;
        _levelsInMap[LevelManager.currentLevel].GetComponent<Image>().sprite = _openLevelIcon;
    }
}
