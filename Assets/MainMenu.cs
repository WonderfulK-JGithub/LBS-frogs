using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //När du trycker på play knappen så "loadar" du +1 scene. I build settings kommer Title screen först och sedan själva spelet.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame ()
    {
        //Stänger av spelet. I unity kommer det inte stängas av, men i vänstra hörnet kommer man kunna se att det funkar
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
