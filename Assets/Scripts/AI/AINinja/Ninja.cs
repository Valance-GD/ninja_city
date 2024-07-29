using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Health))]
public class Ninja : BaseAi
{
    public NinjaHouse _ninjaHouse;
    private bool isAttack;
    protected override void Start()
    {
        base.Start();
        StartCoroutine(StartFollowing(_player));
        
    }
    public void StartMoveToTarget()
    {
        StopAllCoroutines();
        StartAtacking();
    }
    private void Update()
    {
        if(isAttack)
        {
            MoveToClosestTarget(_attackTime,true);
        }
    }
    protected override void AIMove(Transform target, float attackTime, bool isNinja = false)
    {
        if (target == _player)
        {
            StopAllCoroutines();
            isAttack = false;
            StartCoroutine(StartFollowing(target));
        }
        else
        {
            base.AIMove(target, attackTime, true);
        }
            
    }
    private void StartAtacking()
    {
        _gameObjectNavMesh.stoppingDistance = 1.5f;
        _gameObjectNavMesh.speed = 5f;
        isAttack = true;
    }
    private IEnumerator StartFollowing(Transform target)
    {
        _animator.SetBool("isStopped", false);      
        _animator.SetBool("isAttack", false);
        _gameObjectNavMesh.stoppingDistance = 2;
        _gameObjectNavMesh.speed = 3;
        
        while (true)
        {
            _gameObjectNavMesh.SetDestination(target.position);
            if (_gameObjectNavMesh.velocity.sqrMagnitude < 0.1f && _gameObjectNavMesh.remainingDistance <= _gameObjectNavMesh.stoppingDistance)
            {
                _animator.SetBool("isStoppedPlayer", true);  
            }
            else
            {    
                _animator.SetBool("isStoppedPlayer", false);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
