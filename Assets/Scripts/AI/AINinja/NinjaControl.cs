using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NinjaControl : MonoBehaviour
{
    [SerializeField] private Button attackButton;
    [SerializeField] private Button followButton;
    [SerializeField] private Button stayHomeButton;

    public enum NinjaState { Attacking, Following, StayingHome }
    private NinjaState currentState;
    public NinjaState State => currentState;

    private List<GameObject> ninjas = new List<GameObject>();
    private List<GameObject> _dieNinjas = new List<GameObject>();
    public List<GameObject> Ninjas => ninjas;
    private void Start()
    {
        ninjas = FindObjectsOfType<Ninja>().Select(n => n.gameObject).ToList();

        attackButton.onClick.AddListener(() => NinjaAttack());
        followButton.onClick.AddListener(() => NinjaFollow());
        stayHomeButton?.onClick.AddListener(() => NinjaStayHome());
        currentState = NinjaState.StayingHome;
    }



    private void NinjaAttack()
    {
        currentState = NinjaState.Attacking;
        foreach (GameObject n in ninjas)
        {
            if (n != null)
            {
                n.GetComponent<Ninja>().StartAttack();
            }  
            else
            {
                _dieNinjas.Add(n);
            }
        }
        ClearList();
    }

    private void NinjaFollow()
    {
        currentState = NinjaState.Following;
        foreach (GameObject n in ninjas)
        {
            if (n != null)
            {
                n.GetComponent<Ninja>().StartFollow();
            }
            else
            {
                _dieNinjas.Add(n);
            }
        }
        ClearList();
    }
   
    private void NinjaStayHome()
    {
        currentState = NinjaState.StayingHome;
        foreach (GameObject n in ninjas)
        {
            if (n != null)
            {
                n.GetComponent<Ninja>().StartGoToHome();
            }
            else
            {
                _dieNinjas.Add(n);
            }
        }
    }
    private void ClearList()
    {
        foreach (GameObject n in _dieNinjas)
        {
            ninjas.Remove(n);
        }
        _dieNinjas.Clear();
    }
}
