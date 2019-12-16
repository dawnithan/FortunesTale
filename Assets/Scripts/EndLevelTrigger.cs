using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelTrigger : MonoBehaviour
{
    public string sceneName;

    public AudioSource audioSource;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // play sound here
            audioSource.Play();

            GameObject.Find("Player").SetActive(false);

            Invoke("EndLevel", 1.2f);
        }    
    }

    void EndLevel()
    {
        SceneManager.LoadScene(sceneName);
    }
}
