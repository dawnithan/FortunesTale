using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public float playerMaxHealth;
    public float playerHealth;
    public float playerDamage;

    private Text HPText;
    public GameObject GameOverText;

    void Start()
    {
        HPText = GameObject.Find("HP").GetComponent<Text>();
    }

    void Update()
    {
        HPText.text = "HP - " + playerHealth.ToString();
        
        if (playerHealth == 0)
        {
            GameOverText.SetActive(true);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToStart()
    {
        SceneManager.LoadScene("Menu");
    }
}
