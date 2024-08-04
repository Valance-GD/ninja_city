using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instrument : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int index;

    public int Damage => _damage;
    public int Index => index;

}
