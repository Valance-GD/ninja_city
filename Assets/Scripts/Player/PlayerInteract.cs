using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField]  private GameObject _sword;

    [SerializeField] private float _raycastDistance = 5f;


    [SerializeField] private  float overlapRadius = 3.0f;
    [SerializeField] private LayerMask enemyZoneLayer; 

    private bool isInteracting = false;

    void Update()
    {
        CheckOverlap();
    }

    private void CheckOverlap()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, overlapRadius, enemyZoneLayer);
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
