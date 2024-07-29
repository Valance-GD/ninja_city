using System.Collections;
using TMPro;
using UnityEngine;

public class BuildButton : MonoBehaviour
{
    [SerializeField] private GameObject _building;
    [SerializeField] private int _resoursAmountToBuild;
     private int _resoursAmountDef;
    [SerializeField] private string _resoursTypeToBuild;
    [SerializeField] private TextMeshProUGUI _leftAmountText;
    [SerializeField] private GameObject _spendEffect;
    private void Start()
    {
        _resoursAmountDef = _resoursAmountToBuild;
        _leftAmountText.text = _resoursAmountToBuild.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            StartCoroutine(SpendMoneyWithDelay());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            StopAllCoroutines();
        }
    }
    private IEnumerator SpendMoneyWithDelay()
    {
        for (int i = 0; i < _resoursAmountDef; i++)
        {
            if (ResurcesManager.Instance.SpendResource(_resoursTypeToBuild, 1))
            {
                _spendEffect.SetActive(true);
                _resoursAmountToBuild--;
                _leftAmountText.text = _resoursAmountToBuild.ToString();
                if (_resoursAmountToBuild == 0)
                {
                    BuildHouse();
                    GameController.Instance.Save();
                    _spendEffect.SetActive(false);
                    break;
                }
            }
            else
            {
                _spendEffect.SetActive(false);
                break;
            }
            yield return new WaitForFixedUpdate();
        }
    }
    public void BuildHouse()
    {
        Building newBuilding = new Building();
        newBuilding.buildingName = transform.parent.name;
        newBuilding.isBuild = true;
        BuildManager.Instance._buildings.Add(newBuilding);
        _building.SetActive(true);
        Destroy(gameObject);
    }
}
