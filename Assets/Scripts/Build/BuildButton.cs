using UnityEngine;

public class BuildButton : MonoBehaviour
{
    [SerializeField] private GameObject _building;
    [SerializeField] private int _resoursAmountToBuild;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerResurses player))
        {
            if(player.SpendResource("Wood", _resoursAmountToBuild))
            {
                _building.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
