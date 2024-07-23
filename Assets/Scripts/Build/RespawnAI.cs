using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnAI : MonoBehaviour
{
    [SerializeField] private BaseAiHouse _house;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerController player))
        {
            _house.AIDie();
        }
    }
}
