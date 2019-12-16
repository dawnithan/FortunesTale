using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float chaseDistance;

    public float health;
    private float maxHealth;

    public Slider healthBar;

    public GameObject heart;
    public float chanceToDropHP;

    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        // Setup the health bar
        maxHealth = health;
        healthBar.value = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Die();
        }

        healthBar.value = health;

        if (target != null && Vector2.Distance(transform.position, target.position) < chaseDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    void Die()
    {
        var roll = Random.Range(0f, 10f);
        // Debug.Log(roll);
        if (roll < chanceToDropHP)
        {
            GameObject healthPickup = Instantiate(heart, transform.position, Quaternion.identity);
        }
        // play sound here
        Destroy(gameObject);
    }
}
