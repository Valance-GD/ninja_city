using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaHouse : BaseAiHouse
{
    private NinjaControl _ninjaControl;
    [SerializeField] private string _ninjaType;
    public string HouseNinjaType => _ninjaType;

    protected override void RespawnProcess()
    {
        if (_ninjaControl == null)
        {
            _ninjaControl = FindObjectOfType<NinjaControl>();
        }
        GameObject ninja = Instantiate(_AIPrefab, _respawnPoint.position, Quaternion.identity);
        ninja.GetComponent<Ninja>()._ninjaHouse = this;
        _allaviAIList.Add(ninja);
        _ninjaControl.button.onClick.AddListener(ninja.GetComponent<Ninja>().StartMoveToTarget);
        var house = NinjasManager.Instance.NinjaType.Find(house => house.ninjaType == _ninjaType);
        if (house != null)
        {
            house.alliveNinja = _alliveAICount;
        }
        else
        {
            Ninjas thisHouse = new Ninjas();
            thisHouse.ninjaType = _ninjaType;
            thisHouse.alliveNinja = _alliveAICount;
            NinjasManager.Instance.NinjaType.Add(thisHouse);
        }
        
    }
}
