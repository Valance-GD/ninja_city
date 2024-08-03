using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent), typeof(Health))]
public class Enemy : BaseAi
{

    protected override void Start()
    {
        base.Start();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Instrument sword))
        {
            _gameObjectNavMesh.GetComponent<Health>().TakeDamage
                (sword.Damage+ _player.GetComponent<PlayerInteract>().Damage, gameObject, _addResurseAmount);
        }
    }
    private void Update()
    {
        MoveToClosestTarget(_attackTime,false);       
    }
}
