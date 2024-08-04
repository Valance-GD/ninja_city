using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseAi : MonoBehaviour
{
    protected NavMeshAgent _gameObjectNavMesh;
    protected Transform _player;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _attackTime;
    [SerializeField] protected int _addResurseAmount;
    [SerializeField] protected string _targetTag;
    protected float defTime;
    public int AddResourseAmount => _addResurseAmount;
    protected virtual void Start()
    {
        _gameObjectNavMesh = GetComponent<NavMeshAgent>();
        _player = FindObjectOfType<PlayerController>().transform;
        defTime = _attackTime;
    }
    protected virtual void MoveToClosestTarget(float attackTime, bool isNinja = false)
    {
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        GameObject[] enemys = GameObject.FindGameObjectsWithTag(_targetTag);
        List<Transform> targets = new List<Transform>();
        foreach (GameObject enemy in enemys)
        {
            targets.Add(enemy.transform);
        }
        if (isNinja && enemys.Length == 0)
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
        AIMove(closestTarget, attackTime, isNinja);

    }
    protected virtual void Attack( Transform target)
    {
        if (target != null)
        {
            target.GetComponent<Health>().TakeDamage(_damage, target.gameObject);
            _attackTime = defTime;
        }
    }
    protected virtual void AIMove(Transform target, float attackTime, bool isNinja = false)
    {
        if (target == null)
        {
            return;
        }
        _gameObjectNavMesh.SetDestination(target.position);

        if (_gameObjectNavMesh.velocity.sqrMagnitude < 0.1f && _gameObjectNavMesh.remainingDistance <= _gameObjectNavMesh.stoppingDistance && _gameObjectNavMesh.remainingDistance != 0)
        {

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {

                if (isNinja)
                {
                    _animator.SetBool("isAttack", false);
                }
                _animator.SetBool("isStopped", true);

                if (hit.collider.gameObject == target.gameObject)
                {
                    if(_attackTime <=0)
                    {
                        Attack(target);
                    }
                    else
                    {
                        _attackTime -=Time.deltaTime;
                    }
                }
                else
                {
                    _gameObjectNavMesh.transform.LookAt(target.transform);

                }
            }
            else
            {
                _gameObjectNavMesh.transform.LookAt(target.transform);
            }
        }
        else
        {
            if (isNinja)
            {
                _animator.SetBool("isAttack", true);
            }
            _animator.SetBool("isStopped", false);

        }
    }
}
