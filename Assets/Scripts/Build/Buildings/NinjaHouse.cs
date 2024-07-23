using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaHouse : BaseAiHouse
{
    private NinjaControl _ninjaControl;
    public override void AIDie(GameObject target = null)
    {  
        if (_ninjaControl == null)
        {
            _ninjaControl = FindObjectOfType<NinjaControl>();
        }
        base.AIDie(target);
    }
    protected override void RespawnProcess()
    {
        GameObject ninja = Instantiate(_AIPrefab, _respawnPoint.position, Quaternion.identity);
        ninja.GetComponent<Ninja>()._ninjaHouse = this;
        _allaviAIList.Add(ninja);
        _ninjaControl.button.onClick.AddListener(ninja.GetComponent<Ninja>().StartMoveToTarget);
    }
}
