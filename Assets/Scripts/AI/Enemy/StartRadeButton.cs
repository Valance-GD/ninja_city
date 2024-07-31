using UnityEngine;


public class StartRadeButton : MonoBehaviour
{
    [SerializeField] private GameObject startRadeUI;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerController player))
        {
            startRadeUI.SetActive(true);
        }
    }
}
