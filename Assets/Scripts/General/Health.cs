using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private Slider _healthImage;
    [SerializeField] private float _health;
    [SerializeField] private float _healingSpeed = 5f;
    private Transform _cameraTransform;
    private float _maxHealth;
    public float MaxHealth => _maxHealth;

    
    private void Start()
    {
        _maxHealth = _health;
        _cameraTransform = Camera.main.transform;
    }
    public float TakeDamage(float damage, GameObject target, int resoursAmount = 0)
    {
        if (!_healthImage.gameObject.activeSelf)
        {
            _healthImage.gameObject.SetActive(true);
        }
        StopAllCoroutines();
        _health -= damage;
        if (_health <= 0)
        {
            if (target.TryGetComponent(out PlayerController player))
            {
                SceneManager.LoadScene(0);
            }
            else if (target.TryGetComponent(out Ninja ninja))
            {
                ninja._ninjaHouse?.AIDie(target);
                GameController.Instance.gameData.ninjas.Find(n => n.ninjaType == ninja.Type).alliveNinja--;
            }
            else if (target.TryGetComponent(out Human human))
            {
                human._humanHouse.AIDie(target);
            }
            else if (target.TryGetComponent(out Enemy enemy))
            {
                BattleManager.Instance?.EnemyDie(target);
            }

            Destroy(target);
        }
        UpdateUIHealth();
        StartCoroutine(Healing());
        return _health;
    }
    public void AddHealth(float health)
    {
        _maxHealth += health;
        _health = _maxHealth;
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
        _healthImage.value = _health / _maxHealth;
    }
    private void Update()
    {
        if (_healthImage.gameObject.activeSelf)
        {
            _healthImage.transform.LookAt(_healthImage.transform.position + _cameraTransform.rotation * Vector3.forward, _cameraTransform.rotation * Vector3.up);
        }
    }
}