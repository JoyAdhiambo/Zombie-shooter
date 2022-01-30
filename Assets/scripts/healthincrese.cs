using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthincrese : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.GetComponent<player>().health += 10;
            Destroy(gameObject);
        }

    }
}
