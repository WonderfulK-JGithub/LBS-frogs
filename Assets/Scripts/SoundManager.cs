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

    private void Start()
    {
        backgroundMusic = gameObject.GetComponent<AudioSource>();
        backgroundMusic.Play();
    }

    //Funktioner för att spela ljueffekter - Max
    public void PlaySound(AudioClip clip)
    {
        backgroundMusic.PlayOneShot(clip);
    }

    public void PlaySound(AudioClip clip, float volume)
    {
        backgroundMusic.PlayOneShot(clip, volume);
    }

    //En enkel funktion för att pausa musiken om man vill - Max
    public void PauseMusic()
    {
        backgroundMusic.Pause();
        backgroundMusic.PlayDelayed(0.5f);
    }
}
