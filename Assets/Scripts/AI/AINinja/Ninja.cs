using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Health))]
public class Ninja : BaseAi
{
    public NinjaHouse _ninjaHouse;
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
        _gameObjectNavMesh.stoppingDistance = 2.5f;
        _gameObjectNavMesh.speed = 2.5f;
        while (true)
        {
            MoveToClosestTarget(true);
            yield return new WaitForSeconds(0.5f);
        }
    }
    private IEnumerator StartFollowing(Transform target)
    {
        _animator.SetBool("isStopped", false);
        _gameObjectNavMesh.stoppingDistance = 4;
        _gameObjectNavMesh.speed = 4;
        while (true)
        {
            _gameObjectNavMesh.SetDestination(target.position);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
