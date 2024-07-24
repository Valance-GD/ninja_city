using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaHouse : BaseAiHouse
{
    private NinjaControl _ninjaControl;

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
        GameController.Instance.gameData.alliveNinja = _allaviAIList.Count;
    }
}
