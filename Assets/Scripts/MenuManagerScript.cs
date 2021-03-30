using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManagerScript : MonoBehaviour
{
    //Detta är just nu bara ett script för att skapa ett objekt där man kan förvara settings, och den enda settingen som finns är ljudvolym - Max

    //Objektet man ska skapa - Max
    public GameObject settingsObj;
    //Slidern som bestämmer volymen - Max
    public Slider volumeSlider;

    void Start()
    {
        //Om det redan finns ett objekt (dvs spelaren har gått till main menu efter att ha spelat spelet en gång) så ska det inte skapas ett nytt settingobjekt, utan man använder det gamla - Max
        if (!FindObjectOfType<SettingsDataScript>())
        {
            Instantiate(settingsObj, Vector2.zero, Quaternion.identity);
        }

        SettingsDataScript settingsDataObj = FindObjectOfType<SettingsDataScript>();
        volumeSlider.value = settingsDataObj.volume;

        //Addlistener gör att när man ändrar sliderns värde (dvs drar i slidern), så ändras volymen automatiskt - Max
        //Genom att använda delegate så kan man ha en parameter till funktionen som man callear också - Max
        volumeSlider.onValueChanged.AddListener(delegate { settingsDataObj.ChangeVolume(volumeSlider.value); });
    }

}
