using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoor : MonoBehaviour
{
    [SerializeField] private GameObject _levelChouseUI;
    private bool isEnemysEnd = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            if (isEnemysEnd)
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
    public void EnemyEnd()
    {
        isEnemysEnd=true;
    }
}
