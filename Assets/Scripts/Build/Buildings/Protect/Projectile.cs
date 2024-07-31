using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

        [SerializeField] private float speed = 10f;
       // public float explosionRadius = 0f;
        [SerializeField] private int damage = 20;
        private Transform target;

        public void Seek(Transform _target)
        {
            target = _target;
        }

        private void Update()
        {
            if (target == null)
            {
                Destroy(gameObject);
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
            // Можна додати ефекти вибуху тут
            Damage(target);
            Destroy(gameObject);
        }

        private void Damage(Transform enemy)
        {
            Enemy e = enemy.GetComponent<Enemy>();

            if (e != null)
            {
            e.GetComponent<Health>().TakeDamage(damage, e.gameObject);
            }
        }
    
}
