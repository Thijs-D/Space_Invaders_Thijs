using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealth : MonoBehaviour
{
    //private variables
    private Rigidbody2D body;
    private int moveSpeed;
    private int health;

    // move the item down
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        moveSpeed = 1;
        body.velocity = new Vector2(0, -1) * moveSpeed;
    }

    // when it collides with the player, it adds life to that player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // random health value
            health = Random.Range(1,4);
            GameStats.gameStatsRef.HealPlayer(health);
            Destroy(gameObject);
        }
    }
}
