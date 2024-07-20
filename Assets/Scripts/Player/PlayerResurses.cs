using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerResurses : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _woodText;

    private Dictionary<string, int> _resources = new Dictionary<string, int>();
    private Dictionary<string, TextMeshProUGUI> _resourceTexts = new Dictionary<string, TextMeshProUGUI>();

    private void Awake()
    {
        _resources["Money"] = 0;
        _resources["Wood"] = 0;

        _resourceTexts["Money"] = _moneyText;
        _resourceTexts["Wood"] = _woodText;
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
            Debug.LogWarning($"Resource {resourceName} does not exist.");
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
            Debug.LogWarning($"Resource {resourceName} does not exist.");
            return false;
        }
    }
    private void UpdateUI(string resourceName)
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
}