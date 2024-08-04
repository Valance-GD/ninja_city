using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float speed = 10f;
    // public float explosionRadius = 0f;
    private int _damage;
    private Transform target;

    public void GetDamage(int damage)
    {
        _damage = damage;
    }
    public void Seek(Transform _target)
    {
        target = _target.GetChild(2);
    }
    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);// ����� ������� ��� �� ������ ������ 
            return;
        }
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    private void HitTarget()
    {
        // ����� ������ ������ ������ ���
        Damage(target);
        Destroy(gameObject);
    }

    private void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponentInParent<Enemy>();

        if (e != null)
        {
            e.GetComponent<Health>().TakeDamage(_damage, e.gameObject);
        }
    }

}
