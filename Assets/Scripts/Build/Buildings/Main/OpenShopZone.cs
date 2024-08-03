using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShopZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            GetComponentInParent<MainHouse>().OpenShop();
        }
    }
}
