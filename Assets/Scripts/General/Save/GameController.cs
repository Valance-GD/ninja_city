using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    private SaveSystem saveSystem;
    private GameData gameData;
    private float saveInterval = 90f; // sec
    private float nextSaveTime;
    [SerializeField] private bool _startFromBegining;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Переконуємося, що об'єкт не знищується при завантаженні нової сцени
        }
        else
        {
            Debug.LogError("oneMoreGameController");
            Destroy(gameObject);
        }
    }
    private void Start()
    {
       
        saveSystem = GetComponent<SaveSystem>();
        gameData = saveSystem.LoadGame();

        if (gameData == null || _startFromBegining)
        {
            gameData = new GameData();
            InitializeGameData();
        }

        // Завантажте дані у відповідні елементи гри, якщо потрібно
        ApplyGameData(gameData);
    }

    private void Update()
    {
        // Регулярне збереження
        if (Time.time >= nextSaveTime)
        {
            SaveGameData();
            saveSystem.SaveGame(gameData);
            nextSaveTime = Time.time + saveInterval;
        }

        // Для тестування збереження та завантаження
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    private void OnApplicationQuit()
    {
        SaveGameData();
        saveSystem.SaveGame(gameData);
    }
    public void Save()
    {
        SaveGameData();
        saveSystem.SaveGame(gameData);
    }
    public void Load()
    {
        gameData = saveSystem.LoadGame();
        ApplyGameData(gameData);
    }

    private void InitializeGameData()
    {
        // Додайте початкові будівлі та ресурси, якщо потрібно
        gameData.resources.Add(new Resource { resourceName = "Money", quantity = 0 });
        gameData.resources.Add(new Resource { resourceName = "Wood", quantity = 0 });
        gameData.resources.Add(new Resource { resourceName = "Food", quantity = 0 });
    }

    private void SaveGameData()
    {
        foreach (var resource in ResurcesManager.Instance._resources)
        {
            var existingResource = gameData.resources.FirstOrDefault(r => r.resourceName == resource.Key);

            if (existingResource != null)
            {
                existingResource.quantity = resource.Value;
            }
        }
    }
    public void ApplyGameData(GameData gameData)
    {
        // Очищаємо існуючий словник
        ResurcesManager.Instance._resources.Clear();

        // Ітеруємося по ресурсах у gameData і оновлюємо словник
        foreach (var resource in gameData.resources)
        {
            ResurcesManager.Instance._resources[resource.resourceName] = resource.quantity;
            ResurcesManager.Instance.UpdateUI(resource.resourceName);
        }
        // Тепер _resources містить оновлені значення з gameData
    }
}
