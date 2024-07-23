using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResurcesManager : MonoBehaviour
{
    public static ResurcesManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _woodText;
    [SerializeField] private TextMeshProUGUI _foodText;

    public Dictionary<string, int> _resources = new Dictionary<string, int>();
    private Dictionary<string, TextMeshProUGUI> _resourceTexts = new Dictionary<string, TextMeshProUGUI>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("more than one ResourseManager");
            Destroy(gameObject);
        }
       
    }
    private void Start()
    {
        InitializeResource();
        UpdateAllResourcesUI();
    }

    private void InitializeResource()
    {
        _resourceTexts["Money"] = _moneyText;
        _resourceTexts["Wood"] = _woodText;
        _resourceTexts["Food"] = _foodText;
    }

    public void AddResource(string resourceName, int amount)
    {
        
        if (_resources.ContainsKey(resourceName))
        {
            _resources[resourceName] += amount;
            UpdateUI(resourceName);
        }
        else
        {
            Debug.LogError($"Resource {resourceName} does not exist.");
        }
    }

    public bool SpendResource(string resourceName, int amount)
    {

        if (_resources.ContainsKey(resourceName))
        {
            if (_resources[resourceName] >= amount)
            {
                _resources[resourceName] -= amount;
                UpdateUI(resourceName);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            Debug.LogError($"Resource {resourceName} does not exist.");
            return false;
        }
    }

    public void UpdateUI(string resourceName)
    {
        if (_resourceTexts.ContainsKey(resourceName))
        {
            _resourceTexts[resourceName].text = _resources[resourceName].ToString();
        }
        else
        {
            Debug.LogWarning($"UI Text for resource {resourceName} does not exist.");
        }
    }

    private void UpdateAllResourcesUI()
    {
        foreach (var resourceName in _resources.Keys)
        {
            UpdateUI(resourceName);
        }
    }
}
