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
            DontDestroyOnLoad(gameObject); // ������������, �� ��'��� �� ��������� ��� ����������� ���� �����
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

        // ���������� ��� � ������� �������� ���, ���� �������
        ApplyGameData(gameData);
    }

    private void Update()
    {
        // ��������� ����������
        if (Time.time >= nextSaveTime)
        {
            SaveGameData();
            saveSystem.SaveGame(gameData);
            nextSaveTime = Time.time + saveInterval;
        }

        // ��� ���������� ���������� �� ������������
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
        // ������� �������� ����� �� �������, ���� �������
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
        // ������� �������� �������
        ResurcesManager.Instance._resources.Clear();

        // ��������� �� �������� � gameData � ��������� �������
        foreach (var resource in gameData.resources)
        {
            ResurcesManager.Instance._resources[resource.resourceName] = resource.quantity;
            ResurcesManager.Instance.UpdateUI(resource.resourceName);
        }
        // ����� _resources ������ ������� �������� � gameData
    }
}
