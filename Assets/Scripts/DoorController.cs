using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public List<GameObject> enemies;

    public AudioClip doorUnlock;

    void Update()
    {
        foreach (GameObject enemy in enemies.ToArray())
        {
            if (enemy == null)
            {
                enemies.Remove(enemy);
            }
        }

        if (enemies.Count == 0)
        {
            // play sound here
            AudioSource.PlayClipAtPoint(doorUnlock, gameObject.transform.position);
            gameObject.SetActive(false);
        }
    }
}