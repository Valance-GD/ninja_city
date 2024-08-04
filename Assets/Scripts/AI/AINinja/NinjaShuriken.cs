using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Health))]
public class NinjaShuriken : Ninja
{
    [SerializeField] private GameObject _shurikenPrefadb;
    [SerializeField] private Transform _throwPoint;
    protected override void Attack(Transform target)
    {
        ThrowShuriken(target);
    }
    private void ThrowShuriken(Transform target)
    {
        GameObject shurikenObj = Instantiate(_shurikenPrefadb, _throwPoint.position, Quaternion.identity);
        Projectile shuriken = shurikenObj.GetComponent<Projectile>();
        Debug.Log("throw");
        if (shuriken != null)
        {
            shuriken.GetDamage((int)_damage) ;
            shuriken.Seek(target);
            _attackTime = defTime;
        }
    }
}
