using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void LoadScene(int scene)
    {
        GameController.Instance.Save();
        SceneManager.LoadScene(scene);
    }
}
