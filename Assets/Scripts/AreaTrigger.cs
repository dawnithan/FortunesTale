using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    public bool triggerOnce;
    public bool spawnEnemies;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponent<DialogueTrigger>().TriggerDialogue();

            if (triggerOnce)
            {
                gameObject.SetActive(false);
            }

            if (spawnEnemies)
            {
                // && !collision.GetComponent<PlayerController>().isInDialogue
                GameObject.Find("EnemiesGroupParent").transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
