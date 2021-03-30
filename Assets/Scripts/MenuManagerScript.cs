using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManagerScript : MonoBehaviour
{
    public GameObject settingsObj;
    public Slider volumeSlider;

    void Start()
    {
        if (!FindObjectOfType<SettingsDataScript>())
        {
            Instantiate(settingsObj, Vector2.zero, Quaternion.identity);
        }

        SettingsDataScript settingsDataObj = FindObjectOfType<SettingsDataScript>();
        volumeSlider.value = settingsDataObj.volume;
        volumeSlider.onValueChanged.AddListener(delegate { settingsDataObj.ChangeVolume(volumeSlider.value); });
    }

}
