using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Slider bowCharge;

    Vector2 movement;

    public GameObject projectile;

    public float moveSpeed = 5f;
    public float bulletVelocity = 3f;
    public float bulletVelocityLimit = 10f;
    public float chargeSpeed = 10f;

    public bool isInDialogue;

    private bool cooldown = false;
    private float defaultVelocity;
    private float health;

    public AudioSource shoot;
    public AudioSource hurt;
    public AudioClip gameover;

    void Start()
    {
        defaultVelocity = bulletVelocity;

        // Current HP = Max HP
        health = GameObject.Find("PlayerManager").GetComponent<PlayerManager>().playerMaxHealth;

        // Set charge slider
        bowCharge.value = 0;
    }

    void Update()
    {
        health = GameObject.Find("PlayerManager").GetComponent<PlayerManager>().playerHealth;
        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(gameover, gameObject.transform.position);
            Destroy(gameObject);
        }

        if (!isInDialogue)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movement = movement.normalized;

            // Charging shot
            if (Input.GetButton("Fire1") && bulletVelocity < bulletVelocityLimit && cooldown == false)
            {
                bulletVelocity += Time.deltaTime * chargeSpeed;
                bowCharge.value = bulletVelocity;
                bowCharge.minValue = defaultVelocity;
                bowCharge.maxValue = bulletVelocityLimit;
            }

            // Releasing shot
            if (Input.GetButtonUp("Fire1") && cooldown == false)
            {
                Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = worldMousePos - transform.position;
                direction = direction.normalized;

                GameObject bullet = Instantiate(projectile, rb.transform.position + (Vector3)(direction * 0.5f), Quaternion.identity);

                // play sound here
                shoot.Play();

                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
                bulletRB.velocity = direction * bulletVelocity;
                bulletRB.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(worldMousePos.y - bullet.transform.position.y, worldMousePos.x - bullet.transform.position.x) * Mathf.Rad2Deg - 90f);

                bullet.GetComponent<BulletController>().chargeDamage = bowCharge.value;

                // Reset the next projectile's speed
                bulletVelocity = defaultVelocity;

                Invoke("ResetCooldown", 0.8f); // Bow upgrades could reduce this
                cooldown = true;

                bowCharge.value = 0;
            }
        }
        else
        {
            // In dialogue, so don't move.
            movement = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            InvokeRepeating("TakeDamage", 0f, 1f);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            CancelInvoke("TakeDamage");
        }
    }

    void ResetCooldown()
    {
        cooldown = false;
    }

    public void TakeDamage()
    {
        // play sound here
        hurt.Play();
        health--;
        GameObject.Find("PlayerManager").GetComponent<PlayerManager>().playerHealth = health;
    }
}
