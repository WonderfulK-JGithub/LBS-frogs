﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource backgroundMusic;

    private void Start()
    {
        backgroundMusic = gameObject.GetComponent<AudioSource>();
        backgroundMusic.Play();
    }

    public void PlaySound(AudioClip clip)
    {
        backgroundMusic.PlayOneShot(clip);
    }

    public void PlaySound(AudioClip clip, float volume)
    {
        backgroundMusic.PlayOneShot(clip, volume);
    }

    public void PauseMusic()
    {
        backgroundMusic.Pause();
        backgroundMusic.PlayDelayed(0.5f);
    }
}