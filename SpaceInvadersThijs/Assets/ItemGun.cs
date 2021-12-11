using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGun : MonoBehaviour
{
    //private variables
    private Rigidbody2D body;
    private int moveSpeed;

    // move the item down
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        moveSpeed = 1;
        body.velocity = new Vector2(0, -1) * moveSpeed;
    }

    // when it collides with the player, his weapon will be upgraded
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().SetGun();
            Destroy(gameObject);
        }
    }
}
