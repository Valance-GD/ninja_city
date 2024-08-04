using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Health))]
public class NinjaShuriken : Ninja
{
    [SerializeField] private GameObject _shurikenPrefadb;
    [SerializeField] private Transform _throwPoint;
    [SerializeField] private int _safetyZone = 4;
    [SerializeField] private int _safeCount = 3;
    [SerializeField] private float _timeToRecoverSafeTP = 10;
    private int defSafeCount;
    protected override void Start()
    {
        base.Start();
        defSafeCount = _safeCount;
    }

    private IEnumerator RecoverSafeCount()
    {
        if(_safeCount < defSafeCount)
        {
            yield return new WaitForSeconds(_timeToRecoverSafeTP);
            _safeCount++;
        }
    }
    protected override void Attack(Transform target)
    {
        _gameObjectNavMesh.speed = 2;
        ThrowShuriken(target);
    }

    private void FleeFromEnemy(Transform target)
    {
        Vector3 directionAwayFromEnemy = transform.position - target.position;
        directionAwayFromEnemy.Normalize();
        Vector3 newTargetPosition = transform.position + directionAwayFromEnemy * _gameObjectNavMesh.stoppingDistance;
        transform.position = newTargetPosition;
    }

    private void ThrowShuriken(Transform target)
    {
        GameObject shurikenObj = Instantiate(_shurikenPrefadb, _throwPoint.position, Quaternion.identity);
        Projectile shuriken = shurikenObj.GetComponent<Projectile>();
        if (shuriken != null)
        {
            shuriken.GetDamage((int)_damage);
            shuriken.Seek(target);
            _attackTime = defTime;
        }
    }

    protected override void AIMove(Transform target, float attackTime, bool isNinja = false)
    {
        if (target == null)
        {
            return;
        }
        if (_gameObjectNavMesh.remainingDistance <= _safetyZone && _safeCount!=0 && _gameObjectNavMesh.remainingDistance != 0)
        {
            _gameObjectNavMesh.speed = 5;
            FleeFromEnemy(target);
            _safeCount--;
            StartCoroutine(RecoverSafeCount());
            return;
        }
        base.AIMove(target, attackTime, isNinja);
    }
}
