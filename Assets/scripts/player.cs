using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    Rigidbody2D rb;
    public float timeBetweeenSprawn;

    public float speed;
    public GameObject bullet;
    public Transform bulletPosition;
    Vector2 movePosition;
    Animator anime;
    bool called = false;
    public float health;
    public Slider sl;
    public GameObject diedPannel;

    void Start()
    {
        sl.value = health;
        sl.maxValue = health;
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            diedPannel.SetActive(true);
            Destroy(gameObject);
        }
        sl.value = health;
        movePosition = new Vector2(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime);
        movePosition = (rb.position + movePosition);
        movePosition.x = Mathf.Clamp(movePosition.x, -10.1f, 10.11f);
        movePosition.y = Mathf.Clamp(movePosition.y, -4.2f, 4.48f);
        if (Input.GetAxis("Horizontal") > 0)
        {
            anime.Play("walk");
            transform.eulerAngles = new Vector3(0, 0, 0);
            bulletPosition.eulerAngles = new Vector3(0, 0, 0);
        }


        if (Input.GetAxis("Horizontal") < 0 && !called)
        {
            anime.Play("walk");
            transform.eulerAngles = new Vector3(0, 180, 0);
            bulletPosition.eulerAngles = new Vector3(0, 180, 0);
        }
        if ((Input.GetAxis("Vertical") < 0 || Input.GetAxis("Vertical") > 0) && !called)
        {
            anime.Play("walk");
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (!called)
            {
                called = true;
                StartCoroutine(SpawnBullet());
            }
            if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0 && called)
                anime.Play("fire");

        }
        if ((Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) && called)
        {
            anime.Play("walk and fire");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            called = false;
        }

    }

    void FixedUpdate()
    {
        rb.MovePosition(movePosition);
    }
    IEnumerator SpawnBullet()
    {
        yield return new WaitForSeconds(timeBetweeenSprawn);
        if (called)
            Instantiate(bullet, bulletPosition.position, bulletPosition.rotation);
        if (called)
            StartCoroutine(SpawnBullet());
    }


}
