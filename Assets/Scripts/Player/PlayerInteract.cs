using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float overlapRadius = 3.0f;
    [SerializeField] private LayerMask interactZoneLayer;
    private bool isInteracting = false;

    [Header("Attack")]
    [SerializeField] private Animator _animator;
    [SerializeField]  private GameObject _sword;

    [Header("Cut Tree")]
    [SerializeField] private GameObject _treeCutButton;
    [SerializeField] private GameObject _cutEffect;
    [SerializeField] private float _cutRadius = 2.0f;
    [SerializeField] private float _animationTime = 2.0f;


    void Update()
    {
        CheckOverlap();
    }

    private void CheckOverlap()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, overlapRadius, interactZoneLayer);
        bool foundEnemyZone = false;
        bool foundTree =false;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out Enemy obj))
            {
                foundEnemyZone = true;
                break;
            }
            if (hitCollider.TryGetComponent(out CutTree tree))
            {
                foundTree = true;
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
        if(foundTree)
        {
            _treeCutButton.SetActive(true);
        }
        else if(!foundTree)
        {
            
            _treeCutButton.SetActive(false);
        }
    }
    public void StartCut()
    {
        StartCoroutine(CuttingTree());
       _treeCutButton.SetActive(false );
    }
    private IEnumerator CuttingTree()
    {
        _animator.SetTrigger("CutTree");
        yield return new WaitForSeconds(_animationTime);
        GameObject effect = Instantiate(_cutEffect,new Vector3(transform.position.x,transform.position.y+1, transform.position.z ), _cutEffect.transform.rotation);
        yield return new WaitForSeconds(1.5f);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _cutRadius, interactZoneLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out CutTree tree))
            {
                tree.Cut();
            }
        }
        Destroy(effect);
        StopAllCoroutines();
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
