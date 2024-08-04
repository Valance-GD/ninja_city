using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    public static SwordManager Instance { get; private set; }
    [SerializeField] private List<Sword> swords;
    [SerializeField] private Transform swordHandPoint;
    [SerializeField] private Transform shopSword;
    [SerializeField] private GameObject shopSwordPanel;
    [SerializeField] private bool isOnLevel = false;
    private PlayerInteract playerInteract;
    private int _currentSword = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("oneMoreSwordManager");
            //Destroy(gameObject);
        }
    }
    public void LoadSwords(GameData gameData)
    {
        playerInteract = swordHandPoint.transform.root.GetComponent<PlayerInteract>();
        _currentSword = gameData.currentSword;
        foreach (Sword sword in swords)
        {
            GameObject buySword = Instantiate(shopSwordPanel, shopSword);
            BuySword buyPanell = buySword.GetComponent<BuySword>();
            buyPanell.TakeSwordInform(sword.price, sword.index, this);
            if (gameData.broughtSwords.Contains(buyPanell.SwordIndex))
            {
                buyPanell.SwordAlrradyBrought();
            }
        }
        foreach (int sword in gameData.broughtSwords)
        {
            GameObject instSword = Instantiate(swords.Find(sw => sw.index == sword).prefab, swordHandPoint);
        }
        CurrentSword(_currentSword);
    }
    public void CurrentSword(int currentSword)
    {
        _currentSword = currentSword; 
        GameController.Instance.gameData.currentSword = _currentSword; 
        playerInteract.GetCurrentSword(HideAllSwords().Find(sw => sw.GetComponent<Instrument>().Index == _currentSword));
    }


    public void UnlockSword(int index)
    {
        
        
        GameController.Instance.gameData.broughtSwords.Add(index);
        GameObject newSword = Instantiate(swords.Find(sword => sword.index == index).prefab, swordHandPoint);
        playerInteract.GetCurrentSword(newSword);
        CurrentSword(index);
    }
    private List<GameObject> HideAllSwords()
    {
        List<GameObject> buySwords  = swordHandPoint.Cast<Transform>().Where(t => t.parent == swordHandPoint).Select(t => t.gameObject).ToList();
        foreach (GameObject sword in buySwords)
        {
           sword.SetActive(false);
        }
        return buySwords;
    }
}
[Serializable]
public class Sword
{
    public int price;
    public string name;
    public int index;
    public GameObject prefab;
}