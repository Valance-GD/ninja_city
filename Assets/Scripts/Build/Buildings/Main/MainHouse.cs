using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;

public class MainHouse : MonoBehaviour
{
    [SerializeField] private GameObject _shopUi;
    [SerializeField] private TextMeshProUGUI _currentHP, _currentDamage, _addHealthPrice, _addDamagePrice, _money;
    private GameObject _player;
    private Health _playerHealth;
    private PlayerInteract _playerDamage;

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>().gameObject;
        _playerDamage = _player.GetComponent<PlayerInteract>();
        _playerHealth = _player.GetComponent<Health>();
    }
    public void OpenShop()
    {
        Initialize();
        _shopUi.SetActive(true);
    }
    public void Initialize()
    {
        _currentHP.text = _playerHealth.MaxHealth.ToString();
        _currentDamage.text = _playerDamage.Damage.ToString();
        _addDamagePrice.text = _playerDamage.Damage.ToString();
        _addHealthPrice.text = ((int)_playerHealth.MaxHealth / 10).ToString();
        _money.text = ResManager.Instance._resources["Money"].ToString();
    }
    public void CloseShop()
    {
        _shopUi.SetActive(false);
    }
    public void AddDamage()
    {
        if (ResManager.Instance.SpendResource("Money",_playerDamage.Damage))
        {
            _playerDamage.AddDamage(1);
            _currentDamage.text = _playerDamage.Damage.ToString();
            _money.text = ResManager.Instance._resources["Money"].ToString();
            _addDamagePrice.text = _playerDamage.Damage.ToString();
            PlayerStats.Instance.Damage(_playerDamage.Damage);
        }
    }
    public void AddHealth()
    {
        if (ResManager.Instance.SpendResource("Money", (int)_playerHealth.MaxHealth / 10))
        {
            _playerHealth.AddHealth(5);
            _currentHP.text = _playerHealth.MaxHealth.ToString();
            _addHealthPrice.text = ((int)_playerHealth.MaxHealth / 10).ToString();
            _money.text = ResManager.Instance._resources["Money"].ToString();
            PlayerStats.Instance.Health((int)_playerHealth.MaxHealth);
        }
       
    }
}
