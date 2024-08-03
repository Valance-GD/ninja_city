using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class ResourseForBuild
{
    public int _resoursAmountToBuild;
    public string _resoursTypeToBuild;
    public TextMeshProUGUI _leftAmountText;
}
[Serializable]
public class UpgradeData
{
    public GameObject _building;
    public int level;
    public List<ResourseForBuild> buildResourses;
}
public class BuildUpgradeButton : MonoBehaviour
{
    [SerializeField]private List<UpgradeData> _upgradeData;
    private int _currentBuildingLevel = 0;
    private int _resoursAmountDef;
    private List<bool> _payedInfo;
    private List<GameObject> _effects = new List<GameObject>();
    private void Start()
    {
        if (_upgradeData.Count > 0)
        {
            foreach (var res in _upgradeData[_currentBuildingLevel].buildResourses)
            {
                res._leftAmountText.text = res._resoursAmountToBuild.ToString();
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            foreach (var res in _upgradeData[_currentBuildingLevel].buildResourses)
            {
                if (res._resoursAmountToBuild !=0) 
                {
                    StartCoroutine(SpendResWithDelay(res));
                }
                
            }  
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            StopAllCoroutines();
            foreach (GameObject effect in _effects)
            {
                Destroy(effect);
            }
            _effects.Clear();
        }
    }
    private void CheckForBuild()
    {
        _payedInfo = new List<bool>();
        foreach (var res in _upgradeData[_currentBuildingLevel].buildResourses)
        {
            if (res._resoursAmountToBuild == 0)
            {
                _payedInfo.Add(true);
            }
            else
            {
                _payedInfo.Add(false);
            }
        }
        bool allPayed = _payedInfo.TrueForAll(n => n == true);
        if (allPayed)
        {
            BuildOrUpgradeHouse();
        }
    }
    private IEnumerator SpendResWithDelay(ResourseForBuild res)
    {
        _resoursAmountDef = res._resoursAmountToBuild;
        GameObject effectPrefab = ResManager.Instance.UseCurrentEffect(res._resoursTypeToBuild);
        GameObject currentEffect = Instantiate(effectPrefab, transform);
        _effects.Add(currentEffect);
        for (int i = 0; i < _resoursAmountDef; i++)
        {
            if (ResManager.Instance.SpendResource(res._resoursTypeToBuild, 1))
            {
                res._resoursAmountToBuild--;
                
                res._leftAmountText.text = res._resoursAmountToBuild.ToString();
                if (res._resoursAmountToBuild == 0)
                {
                    _effects.Remove(currentEffect);
                    Destroy(currentEffect);
                    CheckForBuild();
                    break;
                }
            }
            else
            {
                _effects.Remove(currentEffect);
                Destroy(currentEffect);
                break;
            }
            yield return new WaitForFixedUpdate();
        }
    }
    public void BuildOrUpgradeHouse()
    {
        Building newBuilding = new Building();
        newBuilding.buildingName = transform.parent.name;
        newBuilding.isBuild = true;
        newBuilding.level = _currentBuildingLevel;
        BuildManager.Instance._buildings.Add(newBuilding);
        foreach (var build in _upgradeData)
        {
            build._building.SetActive(false);
        }
        _upgradeData[_currentBuildingLevel]._building.SetActive(true);
        _currentBuildingLevel++;
        if (_upgradeData.Count == _currentBuildingLevel)
        {
            Destroy(gameObject);
        }
        else
        {
            ChangeButtonForNextUpgrade();
        }
        GameController.Instance.Save();
    }
    public void LoadBuilding(int level)
    {
        foreach (var build in _upgradeData)
        {
            build._building.SetActive(false);
        }
        _currentBuildingLevel = level;
        _upgradeData[_currentBuildingLevel]._building.SetActive(true);
        _currentBuildingLevel++;
        if(_upgradeData.Count == _currentBuildingLevel)
        {
            Destroy(gameObject);
        }
        else
        {
            ChangeButtonForNextUpgrade();
        }
        
    }
    private void ChangeButtonForNextUpgrade()
    {
        foreach (var res in _upgradeData[_currentBuildingLevel].buildResourses)
        {
            res._leftAmountText.text = res._resoursAmountToBuild.ToString();
        }
    }
}
