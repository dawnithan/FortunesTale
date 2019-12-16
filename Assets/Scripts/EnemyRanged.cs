using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyRanged : MonoBehaviour
{
    public float speed;
    public float targetDistance;
    public float bulletVelocity = 3f;
    private bool cooldown = false;

    public float health;
    private float maxHealth;

    public Slider healthBar;

    public GameObject projectile;
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

        if (target != null && (Vector2.Distance(transform.position, target.position) < targetDistance) && cooldown == false)
        {
            Vector2 direction = target.transform.position - transform.position;
            direction = direction.normalized;

            GameObject bullet = Instantiate(projectile, transform.position + (Vector3)(direction * 0.5f), Quaternion.identity);
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

            bulletRB.velocity = direction * bulletVelocity;

            Invoke("ResetCooldown", 1.8f);
            cooldown = true;
        }
    }

    void ResetCooldown()
    {
        cooldown = false;
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
