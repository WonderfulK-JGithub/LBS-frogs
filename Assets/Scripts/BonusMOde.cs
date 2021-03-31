using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BonusMOde : MonoBehaviour
{


    /* 
     * G
     * A
     * M
     * I
     * N
     * G
     * 2
     */


    AudioSource source;
    [SerializeField] AudioClip chickenBwapBwap = null;
    [SerializeField] AudioClip Woaaaw = null;

    float count = 0f;

    [SerializeField] GameObject game = null;

    [SerializeField] Text text1;
    [SerializeField] Text text2;
    

    [SerializeField] Text text3;

    

    void Start()
    {
        source = GetComponent<AudioSource>();
    }


    public void ChickenPressed()
    {
        source.PlayOneShot(chickenBwapBwap);

        count++;

        if(count == 10)
        {
            game.SetActive(false);
            StartCoroutine(YouWonOmgGoodJobWellPlayedWeShilling());
        }
    }

    IEnumerator YouWonOmgGoodJobWellPlayedWeShilling()
    {
        text1.enabled = true;

        yield return new WaitForSeconds(2f);

        text2.enabled = true;

        yield return new WaitForSeconds(2f);

        text1.enabled = false;
        text2.enabled = false;


        text3.enabled = true;
        source.PlayOneShot(Woaaaw);

        yield return new WaitForSeconds(2f);

        Application.OpenURL("https://www.youtube.com/watch?v=zHD85L9-s6E");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
