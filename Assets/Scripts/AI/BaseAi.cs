using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseAi : MonoBehaviour
{
    protected NavMeshAgent _gameObjectNavMesh;
    protected Transform _player;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected float _damage;
    [SerializeField] protected int _addResurseAmount;
    [SerializeField] protected string _targetTag;
    public int AddResourseAmount => _addResurseAmount;
    protected virtual void Start()
    {
        _gameObjectNavMesh = GetComponent<NavMeshAgent>();
        _player = FindObjectOfType<PlayerController>().transform;
    }
    protected virtual void MoveToClosestTarget(bool isNinja)
    {
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        GameObject[] enemys = GameObject.FindGameObjectsWithTag(_targetTag);
        List<Transform> targets = new List<Transform>();
        foreach (GameObject enemy in enemys)
        {
            targets.Add(enemy.transform);
        }
        if(isNinja&& enemys.Length ==0)
        {
            targets.Add(_player);
        }
        else if (!isNinja)
        {
            targets.Add(_player);
        }
        foreach (Transform target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target;
            }
        }
        AIMove(closestTarget);
        
    }
    protected virtual void AIMove(Transform target)
    {
        if (target == null)
        {
            return;
        }
        _gameObjectNavMesh.SetDestination(target.position);

        if (_gameObjectNavMesh.velocity.sqrMagnitude < 0.1f && _gameObjectNavMesh.remainingDistance <= _gameObjectNavMesh.stoppingDistance && _gameObjectNavMesh.remainingDistance!=0)
        {
            Debug.Log(_gameObjectNavMesh.remainingDistance);
            _animator.SetBool("isStopped", true);
            if(target.TryGetComponent(out BaseAi ai))
            {
                target.GetComponent<Health>().TakeDamage(_damage, target.gameObject, ai.AddResourseAmount);
            }
            else// змінити
            {
                target.GetComponent<Health>().TakeDamage(_damage,target.gameObject);
            }
            
        }
        else
        {
            _animator.SetBool("isStopped", false);
        }
    }
    
}
