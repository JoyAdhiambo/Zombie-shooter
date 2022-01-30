using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zombies : MonoBehaviour
{
    GameObject player;

    public float minSpeed;
    public float maxSpeed;
    public float speed;
    public float minDistance;
    Animator anime;
    SpriteRenderer sprite;
    [HideInInspector]
    public float health;
    [HideInInspector]
    public float damages;
    bool spawnCollectables = false;




    void Start()
    {
        if (Random.value < 0.1f)
            spawnCollectables = true;
        health = Random.Range(2.1f, 5.4f);
        damages = Random.Range(5F, 10F);

        player = GameObject.FindGameObjectsWithTag("Player")[0];
        anime = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            Destroy(gameObject);
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.transform.position) > minDistance)
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Random.Range(minSpeed, maxSpeed) * Time.deltaTime);
            if (Vector2.Distance(transform.position, player.transform.position) < minDistance)
            {
                if (!anime.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    anime.Play("Attack");
            }
            if (transform.position.x - player.transform.position.x > 0)
            {
                sprite.flipX = true;
            }
            if (transform.position.x - player.transform.position.x < 0)
            {
                sprite.flipX = false;
            }
        }
        else
            Time.timeScale = 0;

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            health -= 1F;
        }
    }
    public void damagePlayer()
    {
        player.GetComponent<player>().health -= damages;
    }

}
