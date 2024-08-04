using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static NinjaControl;

[RequireComponent(typeof(NavMeshAgent), typeof(Health))]
public class Ninja : BaseAi
{
    public NinjaHouse _ninjaHouse;
    private bool isAttack;
    [SerializeField] private string _type;
    private float _stopingDistanse;
    public NinjaControl _ninjaControl;
    public Transform _homePoint;
    public string Type => _type;
    protected override void Start()
    {
        base.Start();
        _stopingDistanse = _gameObjectNavMesh.stoppingDistance;
        if (_ninjaControl == null)
        {
            _ninjaControl = FindObjectOfType<NinjaControl>();
        }
        SetState(_ninjaControl.State);
    }
    private void SetState(NinjaState newState)
    {
        switch (newState)
        {
            case NinjaState.Attacking:
                StartAttack();
                break;
            case NinjaState.Following:
                StartFollow();
                break;
            case NinjaState.StayingHome:
                StartGoToHome();
                break;
        }
    }
    public void StartAttack()
    {
        StopAllCoroutines();
        _gameObjectNavMesh.stoppingDistance = _stopingDistanse;
        _gameObjectNavMesh.speed = 5f;
        isAttack = true;
    }
    public void StartFollow()
    {
        StopAllCoroutines();
        isAttack = false;
        _animator.SetBool("isStopped", false);
        _animator.SetBool("isAttack", false);
        _gameObjectNavMesh.stoppingDistance = _stopingDistanse;
        _gameObjectNavMesh.speed = 3;
        StartCoroutine(StartFollowing(_player));
    }
    public void StartGoToHome()
    {
        StopAllCoroutines();
        isAttack = false;
        _animator.SetBool("isStopped", false);
        _animator.SetBool("isAttack", false);
        _gameObjectNavMesh.stoppingDistance = 0;
        _gameObjectNavMesh.speed = 3;
        if(_homePoint != null)
        {
            StartCoroutine(GoingHome(_homePoint));
        }
        else
        {
            _animator.SetBool("isStoppedPlayer", true);
        }
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
            StartFollow();
        }
        else
        {
            base.AIMove(target, attackTime, true);
        }
            
    }
    private IEnumerator StartFollowing(Transform target)
    { 
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
            if(_ninjaControl.State == NinjaState.Attacking)
            {
                if(GameObject.FindGameObjectsWithTag(_targetTag) != null)
                {
                    StartAttack();
                }  
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
    private IEnumerator GoingHome(Transform target)
    { 
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
