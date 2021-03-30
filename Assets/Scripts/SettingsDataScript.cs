using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsDataScript : MonoBehaviour
{
    [Range(0f, 1f)] public float volume;

    void Start()
    {
        //Gör så att objektet finns kvar när man startar spelet - Max
        DontDestroyOnLoad(this.gameObject);
    }

    //Jag använder en void istället för en struct på grund av att detta objekt inte kan dras in direkt i volymslidern utan man måste använda AddListener och det är svårt att göra så med dynamic floats - Max
    public void ChangeVolume(float value)
    {
        volume = value;
    }
}
