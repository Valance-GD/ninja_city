using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NinjaControl : MonoBehaviour
{
    [SerializeField] private Button button;


    private void Start()
    {
        GameObject[] ninjas = GameObject.FindGameObjectsWithTag("Ninja");
        foreach (GameObject n in ninjas)
        {
            button.onClick.AddListener(n.GetComponent<Ninja>().StartMoveToTarget);
        }
    }
    
}
