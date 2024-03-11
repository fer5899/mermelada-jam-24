using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public GameManagerSO gameManager;
    public static AudioManager Instance;
    public AudioClip[] musicThemes;
    public AudioClip[] sounds;
    public AudioSource audioSource;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        PlayMusic(0);
    }

    public void OnEnable()
    {
        gameManager.OnCycleStart.AddListener(BattleTheme);
        gameManager.OnUpgradeStart.AddListener(MenuTheme);
    }

    public void OnDisable()
    {
        gameManager.OnCycleStart.RemoveListener(BattleTheme);
        gameManager.OnUpgradeStart.AddListener(MenuTheme);
    }

    public void BattleTheme(int num)
    {
        PlayMusic(1);
    }

    public void MenuTheme()
    {
        PlayMusic(0);
    }

    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            // Clamp the volume value between 0 and 1
            volume = Mathf.Clamp01(volume);
            
            // Set the volume of the audio source
            audioSource.volume = volume;
        }
    }

    public void PlayMusic(int theme)
    {
        audioSource.clip = musicThemes[theme];
        audioSource.Play();
        audioSource.loop = true;
        double volumeValue = 0.16; // Define volumeValue as double
        float floatValue = (float)volumeValue;
        SetVolume(floatValue);
    }
}
