using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Slider _healthImage;   
    [SerializeField] private float _health;
    [SerializeField] private float _healingSpeed = 5f;
     private Transform _cameraTransform;
    private float _maxHealth;
    private void Start()
    {
        UpdateUIHealth();
        _maxHealth = _health;
        _cameraTransform = Camera.main.transform;
    }
    public float TakeDamage(float damage)
    {
        if (!_healthImage.gameObject.activeSelf)
        {
            _healthImage.gameObject.SetActive(true);
        }
        StopAllCoroutines();
        _health -= damage;
        UpdateUIHealth();
        StartCoroutine(Healing());
        return _health;
    }
    private IEnumerator Healing()
    {
        yield return new WaitForSeconds(_healingSpeed);
        _healthImage.gameObject.SetActive(false);
        _health = _maxHealth;
        UpdateUIHealth();
    }
    private void UpdateUIHealth()
    {
        _healthImage.value = _health / 100;
        
    }
    private void Update()
    {
        if (_healthImage.gameObject.activeSelf)
        {
            _healthImage.transform.LookAt(_healthImage.transform.position + _cameraTransform.rotation * Vector3.forward, _cameraTransform.rotation * Vector3.up);
        }
    }
}
