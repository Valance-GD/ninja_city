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
                _building.SetActive(true);
                Destroy(gameObject);
            }

        }
    }
}
