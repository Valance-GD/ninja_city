using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float overlapRadius = 2.0f;
    [SerializeField] private LayerMask interactZoneLayer;
    private bool isInteracting = false;

    [Header("Attack")]
    [SerializeField] private Animator _animator;
    [SerializeField]  private GameObject _sword;


    private void Update()
    {
        CheckOverlap();
    }

    private void CheckOverlap()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, overlapRadius, interactZoneLayer);
        bool foundEnemyZone = false;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out Enemy obj))
            {
                foundEnemyZone = true;
                break;
            }
        }
        if (foundEnemyZone && !isInteracting)
        {
            isInteracting = true;
            StartInteract();
        }
        else if (!foundEnemyZone && isInteracting)
        {
            isInteracting = false;
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
