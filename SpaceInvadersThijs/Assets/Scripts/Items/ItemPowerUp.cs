using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPowerUp : MonoBehaviour
{
    //private variables
    private Rigidbody2D body;
    private int moveSpeed;
    private int duration;

    // move the item down
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        moveSpeed = 1;
        body.velocity = new Vector2(0, -1) * moveSpeed;
    }

    // if it collides with the player, the boost will be activated
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.CompareTag("Player"))
        {
            duration = Random.Range(1, 4);
            collision.GetComponent<Player>().ActivateBoost(duration);
            Destroy(gameObject);
        }
    }
}
