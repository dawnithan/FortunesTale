using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject Instructions;
    public AudioSource select;

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void HowToPlay()
    {
        Instructions.SetActive(true);
        select.Play();
    }

    public void CloseHowToPlay()
    {
        Instructions.SetActive(false);
        select.Play();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
