using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [HideInInspector]public AudioSource backgroundMusic;

    public AudioClip playerHurt;
    public AudioClip playerJump;
    public AudioClip superMegaEpicBombMusic;
    public AudioClip bombSoundEffect;

    public AudioClip pigDie;
    public AudioClip hönaDie;
    public AudioClip beeDie;

    SettingsDataScript settingsData;

    private void Start()
    {
        backgroundMusic = gameObject.GetComponent<AudioSource>();
        backgroundMusic.Play();

        settingsData = FindObjectOfType<SettingsDataScript>();
        backgroundMusic.volume = settingsData.volume;
    }

    //Funktioner för att spela ljueffekter - Max
    public void PlaySound(AudioClip clip)
    {
        backgroundMusic.PlayOneShot(clip, settingsData.volume);
    }

    public void PlaySound(AudioClip clip, float volume)
    {
        backgroundMusic.PlayOneShot(clip, volume * settingsData.volume);
    }

    //En enkel funktion för att pausa musiken om man vill - Max
    public void PauseMusic()
    {
        backgroundMusic.Pause();
        backgroundMusic.PlayDelayed(0.5f);
    }
}
