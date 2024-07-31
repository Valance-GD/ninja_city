using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    private SaveSystem saveSystem;
    public GameData gameData;
    [SerializeField] private bool _startFromBegining;
    private static bool isStart = true;
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;  
        }
        else
        {
            Debug.LogError("oneMoreGameController");
            //Destroy(gameObject);
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
        if (isStart)
        {
            SceneManager.LoadScene(gameData.currentMap);
            isStart = false;
        }
        // Завантажте дані у відповідні елементи гри, якщо потрібно
        ApplyGameData(gameData);
        
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
    public void SaveNewData()
    {
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
        BuildManager.Instance._buildings.Clear();
        NinjasManager.Instance.SaveNinjasHouse();
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
            GameObject buildingObject = GameObject.Find(building.buildingName);
            if (buildingObject != null)
            {
                buildingObject.GetComponentInChildren<BuildUpgradeButton>().LoadBuilding(building.level);
            }
            else
            {
                Debug.LogWarning("Building not found: " + building.buildingName);
            }
        }
        BuildManager.Instance._buildings.Clear();
        NinjasManager.Instance.LoadNinjasHouse(gameData.ninjas);
        Settings.Instance.isMusicOn = gameData.isMusicOn;
        LevelManager.currentLevel = gameData.currentLevel;
    }
}
