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
    public AudioSource soundSource;

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
        gameManager.onPlayerAttack.AddListener(BossAttack);
        gameManager.onPlayerGetDamage.AddListener(PlayerGetDamage);
        gameManager.onDiscardCard.AddListener(discardCard);
        gameManager.onPlayerGainBlock.AddListener(gainBlock);
        gameManager.onPlayerGainFury.AddListener(gainFury);
        gameManager.onPlayerGainThorns.AddListener(gainThorns);
        gameManager.onSelectCard.AddListener(selectCard);
        gameManager.onButtonSelect.AddListener(selectButton);
        gameManager.onPlayerGainMana.AddListener(gainMana);
        gameManager.OnBossPoison.AddListener(gainPoison);

    }

    public void OnDisable()
    {
        gameManager.OnCycleStart.RemoveListener(BattleTheme);
        gameManager.OnUpgradeStart.RemoveListener(MenuTheme);
        gameManager.onPlayerAttack.RemoveListener(BossAttack);
        gameManager.onPlayerGetDamage.RemoveListener(PlayerGetDamage);
        gameManager.onDiscardCard.RemoveListener(discardCard);
        gameManager.onPlayerGainBlock.RemoveListener(gainBlock);
        gameManager.onPlayerGainFury.RemoveListener(gainFury);
        gameManager.onPlayerGainThorns.RemoveListener(gainThorns);
        gameManager.onSelectCard.RemoveListener(selectCard);
        gameManager.onButtonSelect.RemoveListener(selectButton);
        gameManager.onPlayerGainMana.RemoveListener(gainMana);
        gameManager.OnBossPoison.RemoveListener(gainPoison);
    }

    public void BattleTheme(int num)
    {
        PlayMusic(1);
    }

    public void MenuTheme()
    {
        PlayMusic(0);
    }

    public void BossAttack()
    {
        PlaySound(0);
    }

    public void PlayerGetDamage()
    {
        PlaySound(1);
    }

    public void gainBlock()
    {
        PlaySound(2);
    }

    
    public void discardCard()
    {
        PlaySound(3);
    }

    public void gainFury()
    {
        PlaySound(4);
    }

    public void selectCard()
    {
        PlaySound(5);
    }
    public void selectButton()
    {
        PlaySound(6);
    }

    public void gainMana()
    {
        PlaySound(7);
    }

    public void gainPoison()
    {
        PlaySound(8);
    }

    public void gainThorns()
    {
        PlaySound(9);
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

    public void PlaySound(int sound)
    {
        soundSource.clip = sounds[sound];
        soundSource.Play();
        soundSource.loop = false;
        //double volumeValue = 0.16; // Define volumeValue as double
        //float floatValue = (float)volumeValue;
        //SetVolume(floatValue);
    }
}
