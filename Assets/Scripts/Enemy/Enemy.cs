using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent), typeof(Health))]
public class Enemy : MonoBehaviour
{
    private NavMeshAgent _enemy;
    private Transform _player;
    [SerializeField]private Animator _animator;
    [SerializeField]private float _damage;
    [SerializeField] private int _addResurseAmount;
    private void Start()
    {
        _enemy = GetComponent<NavMeshAgent>();
        _player = FindObjectOfType<PlayerController>().transform;
    }
    private void Update()
    {
        EnemyMove(_player);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Instrument sword))
        {
            if (_enemy.GetComponent<Health>().TakeDamage(sword.Damage) <=0)
            {
                _player.GetComponent<PlayerResurses>().AddResource("Money", _addResurseAmount);
                Destroy(gameObject);
            }
            
        }
    }
    private void EnemyMove(Transform target)
    {
        _enemy.SetDestination(target.position);
        if (_enemy.velocity.sqrMagnitude < 0.1f && _enemy.remainingDistance <= _enemy.stoppingDistance)
        {
            _animator.SetBool("isStopped", true);
            if (target.GetComponent<Health>().TakeDamage(_damage)<= 0)
            {
                Destroy(target.gameObject);
            }
            
        }
        else
        {
            _animator.SetBool("isStopped", false);
        }
    }
}
