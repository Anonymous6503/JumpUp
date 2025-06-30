using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioClip[] sounds;
    
    /**
     * Index
     * 0-Start
     * 1-Pickup
     * 2-Jump
     * 3-Death
     * 4-PlatformLand
     */
    
    private void Awake()
    {
        if (instance == null)
            instance = this;

        sfxSource.volume = SaveManager.instance.GetSFXLevel();
        bgmSource.volume = SaveManager.instance.GetBGMLevel();
    }

    public void PlaySound(int index)
    {
        sfxSource.clip = sounds[index];
        sfxSource.Play();
    }

}
