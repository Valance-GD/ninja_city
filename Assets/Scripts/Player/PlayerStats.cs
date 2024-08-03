using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }
    private int _playerDamage;
    private int _playerHealth;
    private GameController _gameController;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("oneMorePlayerStats");
            //Destroy(gameObject);
        }
    }
    public void Damage(int damage)
    {
        _playerDamage = damage;
    }
    public void Health(int health)
    {
        _playerHealth = health;
    }

    public void SavePlayerStats()
    {
        _gameController.gameData.playerDamage = _playerDamage;
        _gameController.gameData.playerHP = _playerHealth;
    }
    public void LoadPlayerStats()
    {
        _gameController = GameController.Instance;
        _playerDamage = _gameController.gameData.playerDamage;
        _playerHealth = _gameController.gameData.playerHP;

        GetComponent<PlayerInteract>().AddDamage(_gameController.gameData.playerDamage);
        GetComponent<Health>().AddHealth(_gameController.gameData.playerHP);
    }
}
