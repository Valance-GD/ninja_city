using UnityEngine;

public class BuildButton : MonoBehaviour
{
    [SerializeField] private GameObject _building;
    [SerializeField] private int _resoursAmountToBuild;
    [SerializeField] protected string _resoursTypeToBuild;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {

            if (ResurcesManager.Instance.SpendResource(_resoursTypeToBuild, _resoursAmountToBuild))
            {
                BuildHouse();
                GameController.Instance.Save();
            }

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
