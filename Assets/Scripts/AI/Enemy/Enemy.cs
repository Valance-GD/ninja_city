using System.Collections;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent), typeof(Health))]
public class Enemy : BaseAi
{

    protected override void Start()
    {
        base.Start();
        StartCoroutine(StartAtacking());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Instrument sword))
        {
            _gameObjectNavMesh.GetComponent<Health>().TakeDamage(sword.Damage, gameObject, _addResurseAmount);
        }
    }
    private IEnumerator StartAtacking()
    {     
        while (true)
        {
            MoveToClosestTarget(false);
            yield return new WaitForSeconds(0.5f);
        }
        
    }
}
