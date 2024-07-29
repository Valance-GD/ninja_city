using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
       if(LevelManager.currentLevel> _levelsInMap.Count-1)// перенести на останню місію 
        {
            GameController.Instance.gameData.currentMap = 2; 
            GameController.Instance.gameData = GameDataManager.ResetGameData(GameController.Instance.gameData);           
            GameController.Instance.Save();
            SceneManager.LoadScene(GameController.Instance.gameData.currentMap);
        }
        else
        {
            _levelsInMap[LevelManager.currentLevel].enabled = true;
            _levelsInMap[LevelManager.currentLevel].GetComponent<Image>().sprite = _openLevelIcon;
        }
        
    }
}
