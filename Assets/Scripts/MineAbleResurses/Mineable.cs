using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;

[Serializable]
public class Faze
{
    public GameObject _faze;
    public int _fazeCount;
}
public class Mineable : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _basicFaxe;
    [SerializeField] private List <Faze> _fazes;
    [SerializeField] private int _health;
    [SerializeField] private int _addResurseAmount;
    [SerializeField] private BoxCollider _zone;
    [SerializeField] private bool _isTree;
    
    private int _maxHealth;
    private void Start()
    {
        _maxHealth = _health;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Instrument axe))
        {
            if (_health == _maxHealth)
            {
                _basicFaxe.SetActive(false);
                foreach (var item in _fazes)
                {
                    item._faze.SetActive(true);
                }
            }
            _health -= axe.Damage;
            for (int i = 0; i < _fazes.Count; i++)
            {
                if (_health <= _fazes[i]._fazeCount)
                {
                    
                    
                    _fazes[i]._faze.SetActive(false);
                    if (i == _fazes.Count - 1)
                    {
                        StartCoroutine(Recover());
                    }
                }
                else { break; }
            }
            
        }
    }
    private IEnumerator Recover()
    {
        if (_isTree)
        {
            _player.GetComponent<PlayerResurses>().AddResource("Wood", _addResurseAmount);
        }
        _zone.gameObject.SetActive(false);
        _player.GetComponent<PlayerInteract>().EndInteract();
        yield return new WaitForSeconds(5f);
        _basicFaxe.SetActive(true);
        _health = _maxHealth;
        _zone.gameObject.SetActive(true);
        foreach (var item in _fazes)
        {
            item._faze.SetActive(false);
        }  
    }
}
