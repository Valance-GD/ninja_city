using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NinjaControl : MonoBehaviour
{
    public Button button;


    private void Start()
    {
        GameObject[] ninjas = FindObjectsOfType<Ninja>().Select(n => n.gameObject).ToArray();
        foreach (GameObject n in ninjas)
        {
            button.onClick.AddListener(n.GetComponent<Ninja>().StartMoveToTarget);
        }
    }
   
}
