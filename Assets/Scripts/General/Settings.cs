using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public static Settings Instance { get; private set; }
    [SerializeField] private GameObject _settingsUI;
    [SerializeField] private Button _musicButton;
    private List<AudioSource> _audioSourses;
    private bool isOpen;
    public bool isMusicOn = true;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("More Than One settings");
        }
    }
    private void Start()
    {
        _audioSourses = FindObjectsOfType<AudioSource>().ToList();
        if (isMusicOn)
        {
            foreach (AudioSource sours in _audioSourses)
            {
                sours.enabled = true;
            }
            _musicButton.onClick.AddListener(TurnOffMusic);
        }
        else
        {
            foreach (AudioSource sours in _audioSourses)
            {
                sours.enabled = false;
            }
            _musicButton.onClick.AddListener(TurnOnMusic);
        }
    }
    private void Update() // поміняти на юі
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isOpen)
            {
                isOpen = true;
                Time.timeScale = 0f;
                _settingsUI.SetActive(true);

            }
            else
            {
                isOpen = false;
                Time.timeScale = 1.0f;
                _settingsUI.SetActive(false);
            }
        }
    }
    public void TurnOffMusic()
    {
        foreach (AudioSource sours in _audioSourses)
        {
            sours.enabled = false;
        }
        _musicButton.onClick.RemoveAllListeners();
        _musicButton.onClick.AddListener(TurnOnMusic);
        isMusicOn = false;
        GameController.Instance.gameData.isMusicOn = isMusicOn;
    }
    public void TurnOnMusic()
    {
        foreach (AudioSource sours in _audioSourses)
        {
            sours.enabled = true;
        }
        isMusicOn = true;
        _musicButton.onClick.RemoveAllListeners();
        _musicButton.onClick.AddListener(TurnOffMusic);
        GameController.Instance.gameData.isMusicOn = isMusicOn;
    }
}
