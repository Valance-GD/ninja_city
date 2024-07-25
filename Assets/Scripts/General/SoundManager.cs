using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
 private AudioSource _audioSrc => GetComponent<AudioSource>();

    private void Start()
    {
        _audioSrc.Play();
    }
}
