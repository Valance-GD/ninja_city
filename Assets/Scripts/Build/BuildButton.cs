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
            if (player.GetComponent<PlayerResurses>().SpendResource("Wood", 20))
            {
                _building.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
