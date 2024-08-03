using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSuper : MonoBehaviour
{
    [SerializeField] private GameObject _superEffect;
    [SerializeField] private float _superRadius = 2.0f;
    [SerializeField] private float _animationTime = 0.4f;
    [SerializeField] private int _damage = 50;
    [SerializeField] private float _recoverTime = 10f;
    [SerializeField] private Animator _animator;
    [SerializeField] private Slider _energy;

    public void StartSuper()
    {
        if (_energy.value >= 1)
        {
            StartCoroutine(Super());
            _energy.value = 0;
        }
    }

    private IEnumerator Super()
    {
        GetComponent<PlayerController>().CanMove(false);
        _animator.SetTrigger("Super");
        yield return new WaitForSeconds(_animationTime);

        GameObject effect = Instantiate(_superEffect, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), _superEffect.transform.rotation);
        yield return new WaitForSeconds(1f);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _superRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out CutTree tree))
            {
                tree.Cut();
            }
            if (hitCollider.CompareTag("Enemy"))
            {
                hitCollider.GetComponent<Health>().TakeDamage(_damage, hitCollider.gameObject);
            }
        }

        Destroy(effect);
        GetComponent<PlayerController>().CanMove(true);
        StartCoroutine(RecoverEnergy());
    }

    private IEnumerator RecoverEnergy()
    {
        float elapsed = 0f;
        float startValue = _energy.value;

        while (elapsed < _recoverTime)
        {
            elapsed += Time.deltaTime;
            _energy.value = Mathf.Lerp(startValue, 1, elapsed / _recoverTime);
            yield return null;
        }

        _energy.value = 1;
    }
}
