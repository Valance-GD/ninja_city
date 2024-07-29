using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWaveTrigger : MonoBehaviour
{
    [HideInInspector]public LevelSetting _waveSettings;
    public Transform _spawnEnemyPoint;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerController player))
        {
            _waveSettings.LoadWave(this);
        }
    }
}
