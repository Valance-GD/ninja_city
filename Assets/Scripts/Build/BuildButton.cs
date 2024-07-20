using UnityEngine;

public class BuildButton : MonoBehaviour
{
    [SerializeField] private GameObject _building;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerResurses player))
        {
            if(player.SpendResource("Wood", 20))
            {
                _building.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
