using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySword : MonoBehaviour
{
    private int swordIndex;
    private int _price;
    private SwordManager _manager;
    [SerializeField] private GameObject _buyButton;
    [SerializeField] private GameObject _useButton;

    public int SwordIndex => swordIndex;

    public void TakeSwordInform(int price, int index, SwordManager manager)
    {
        _price = price;
        _manager = manager;
        swordIndex = index;
    }


    public void Buy()
    {
        if (ResManager.Instance.SpendResource("Money", _price))
        {
            _manager.UnlockSword(swordIndex);
            SwordAlrradyBrought();
        }

    }
    public void Use()
    {
        _manager.CurrentSword(swordIndex);

    }

    public void SwordAlrradyBrought()
    {
        _buyButton.SetActive(false);    
        _useButton.SetActive(true);
    }
}
