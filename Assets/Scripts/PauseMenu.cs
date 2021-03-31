using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    //Är spelet pausat?
    // Ja, det är det
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    private void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //När man trycker på esc i spelet, får man pause meny. När man trycker på esc i pause meny, går man tillbaka till spelet. - Melker J
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        //Spelet startas vid samma hastighet som förut - Melker J
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        //Fryser spelet så att det inte pågor samtidigt som du spelar - Melker J
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        print("fefa");
        SceneManager.LoadScene(0);
        //Spelet startar i samma hastighet som förut - Melker J
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        //Stänger programmet - Melker J
        Debug.Log("Quitting game...");
        Application.Quit();
    }

}
