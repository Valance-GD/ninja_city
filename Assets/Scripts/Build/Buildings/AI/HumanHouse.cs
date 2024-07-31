using System.Collections.Generic;
using UnityEngine;

public class HumanHouse : BaseAiHouse
{
    protected override void RespawnProcess()
    {

        GameObject human = Instantiate(_AIPrefab, _respawnPoint.position, Quaternion.identity);
        human.GetComponent<Human>()._humanHouse = this;
        _allaviAIList.Add(human);
    }
}
