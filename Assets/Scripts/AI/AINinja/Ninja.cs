using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Health))]
public class Ninja : BaseAi
{
    protected override void Start()
    {
        base.Start();
        StartCoroutine(StartFollowing(_player));
    }
    public void StartMoveToTarget()
    {
        StopAllCoroutines();
        StartCoroutine(StartAtacking());
    }
    protected override void AIMove(Transform target)
    {
        if (target == _player)
        {
            StopAllCoroutines();
            
            StartCoroutine(StartFollowing(target));
        }
        else
        {
            base.AIMove(target);
        }
            
    }
    private IEnumerator StartAtacking()
    {
        while (true)
        {
            MoveToClosestTarget(true);
            yield return new WaitForSeconds(0.5f);
        }
    }
    private IEnumerator StartFollowing(Transform target)
    {
        _animator.SetBool("isStopped", false);
        _gameObjectNavMesh.stoppingDistance = 3;
        while (true)
        {
            _gameObjectNavMesh.SetDestination(target.position);
            _gameObjectNavMesh.speed = 3;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
