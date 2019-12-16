using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickupController : MonoBehaviour
{
    public float healthToRestore = 1;

    public AudioClip restore;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            var currentHealth = GameObject.Find("PlayerManager").GetComponent<PlayerManager>().playerHealth;

            if (currentHealth < GameObject.Find("PlayerManager").GetComponent<PlayerManager>().playerMaxHealth)
            {
                // play sound here
                AudioSource.PlayClipAtPoint(restore, gameObject.transform.position);
                GameObject.Find("PlayerManager").GetComponent<PlayerManager>().playerHealth += healthToRestore;
                Destroy(gameObject);
            }
        }
    }
}
