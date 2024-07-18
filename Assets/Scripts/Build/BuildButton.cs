using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButton : MonoBehaviour
{
    [SerializeField] private GameObject _building;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            _building.SetActive(true);
            Destroy(gameObject);
        }
    }
}
