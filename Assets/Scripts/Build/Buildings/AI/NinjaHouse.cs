using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class NinjaHouse : BaseAiHouse
{
    private NinjaControl _ninjaControl;
    [SerializeField] private string _ninjaType;
    public string HouseNinjaType => _ninjaType;
    [SerializeField] protected List<HomePoint> _ninjasHomePoints;

    protected override void Start()
    {
        base.Start();
    }
    protected override void RespawnProcess()
    {
        foreach (HomePoint point in _ninjasHomePoints)
        {
            if(point._ninja != null) 
            {
                point._isfree = false;
            }
            else
            {
                point._isfree = true;
            }
        }
        if (_ninjaControl == null)
        {
            _ninjaControl = FindObjectOfType<NinjaControl>();
        }
        GameObject ninjaObj = Instantiate(_AIPrefab, _respawnPoint.position, Quaternion.identity);
        Ninja ninja = ninjaObj.GetComponent<Ninja>();
        ninja._ninjaHouse = this;
        ninja._ninjaControl = _ninjaControl;
        ninja._homePoint = GetFreePoint(ninjaObj);
        _allaviAIList.Add(ninjaObj);
        _ninjaControl.Ninjas.Add(ninjaObj);
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
    private Transform GetFreePoint(GameObject ninja)
    {
        foreach (HomePoint point in _ninjasHomePoints)
        {
            if (point._isfree)
            {
                point._ninja = ninja;
                return point.transform;
            } 
        }
        return null;
    }
}
