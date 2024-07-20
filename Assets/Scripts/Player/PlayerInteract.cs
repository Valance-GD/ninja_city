using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField]  private GameObject _sword;

    [SerializeField] private float _raycastDistance = 5f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out MineZone obj))
        {
            StartInteract();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out MineZone obj))
        {
            EndInteract();
        }
    }
    public void StartInteract()
    {
        _animator.SetBool("IsAttack", true);
        _sword.SetActive(true);
    }
    public void EndInteract()
    {
        _sword.SetActive(false);
        _animator.SetBool("IsAttack", false);   
    }
}
