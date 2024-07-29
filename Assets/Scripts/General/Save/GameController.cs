using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    private SaveSystem saveSystem;
    public GameData gameData;
    private float saveInterval = 90f; // sec
    private float nextSaveTime;
    [SerializeField] private bool _startFromBegining;
    [SerializeField] private BaseAiHouse _houseAI;

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
            _startFromBegining = false;
        }

        // Завантажте дані у відповідні елементи гри, якщо потрібно
        ApplyGameData(gameData);
    }

    private void Update()
    {

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
        foreach (Building building in BuildManager.Instance._buildings)
        {
            gameData.buildings.Add(building);
        }
        gameData.currentLevel = LevelManager.currentLevel;
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
        foreach (Building building in gameData.buildings)
        {
            //BuildManager.Instance._buildings.Add(building);
            GameObject buildingObject = GameObject.Find(building.buildingName);
            if (buildingObject != null)
            {
                buildingObject.GetComponentInChildren<BuildButton>().BuildHouse();
            }
            else
            {
                Debug.LogWarning("Building not found: " + building.buildingName);
            }
        }
        _houseAI._alliveAICount = gameData.alliveNinja;
        Settings.Instance.isMusicOn = gameData.isMusicOn;
        LevelManager.currentLevel = gameData.currentLevel;
    }
}
