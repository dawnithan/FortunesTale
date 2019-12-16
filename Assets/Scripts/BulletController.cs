using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D rb;

    public float secondsToDisappear = 2.0f;
    public float chargeDamage;

    private float damage;

    public AudioClip hit;

    // Update is called once per frame
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        damage = GameObject.Find("PlayerManager").GetComponent<PlayerManager>().playerDamage;

        StartCoroutine(DisappearAfterDelay(secondsToDisappear));
    }

    IEnumerator DisappearAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            // play sound here
            AudioSource.PlayClipAtPoint(hit, gameObject.transform.position);

            float currentEnemyHP = collision.gameObject.GetComponent<EnemyController>().health;
            currentEnemyHP = currentEnemyHP - (damage + chargeDamage);

            collision.gameObject.GetComponent<EnemyController>().health = currentEnemyHP;

            Destroy(gameObject);
        }

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ranged Enemies"))
        {
            // play sound here
            AudioSource.PlayClipAtPoint(hit, gameObject.transform.position);

            float currentEnemyHP = collision.gameObject.GetComponent<EnemyRanged>().health;
            currentEnemyHP = currentEnemyHP - (damage + chargeDamage);

            collision.gameObject.GetComponent<EnemyRanged>().health = currentEnemyHP;

            Destroy(gameObject);
        }

        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        rb.Sleep();
    }
}
