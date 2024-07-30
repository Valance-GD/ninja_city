using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoor : MonoBehaviour
{
    [SerializeField] private GameObject _levelChouseUI;
    private bool isLevelsEnd =false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerController player))
        {
            if (isLevelsEnd)
            {
                GameController.Instance.gameData.currentMap = 2;
                GameDataManager.ResetGameData(GameController.Instance.gameData);
                SceneManager.LoadScene(GameController.Instance.gameData.currentMap);
            }
            else
            {
                _levelChouseUI.SetActive(true);
            }
        }
    }
    public void LevelEnds()
    {
        isLevelsEnd = true;
    }
}
