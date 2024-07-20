using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourseInfo : MonoBehaviour
{
    private int _resAmount;
    [SerializeField]private string _typeRes;
    private PlayerResurses _playerRes;

    public void TakeInfo(int amount)
    {
        _resAmount = amount;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent (out PlayerController player)) 
        {
            player.GetComponent<PlayerResurses>().AddResource(_typeRes, _resAmount);
            Destroy(gameObject);
        }
    }
}