using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // public variables
    public bool ownerPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ownerPlayer && collision.CompareTag("NPC"))
        {
            collision.GetComponent<NPC>().GetDamage();
            Destroy(gameObject);
        }
        else if (!ownerPlayer && collision.CompareTag("Player"))
        {
            GameStats.gameStatsRef.GetDamage();
            Destroy(gameObject);
        }
    }
}
