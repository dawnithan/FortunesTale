using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Rigidbody2D rb;

    public float secondsToDisappear = 2.0f;

    private float damage;

    public AudioClip hit;
    public AudioClip fire;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(DisappearAfterDelay(secondsToDisappear));

        AudioSource.PlayClipAtPoint(fire, gameObject.transform.position);
    }

    IEnumerator DisappearAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(hit, gameObject.transform.position);

            GameObject.Find("Player").GetComponent<PlayerController>().TakeDamage();

            Destroy(gameObject);
        }

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            Destroy(gameObject);
        }
    }
}
