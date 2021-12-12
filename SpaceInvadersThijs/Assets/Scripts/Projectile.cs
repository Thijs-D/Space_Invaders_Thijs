using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // public variables
    public bool ownerPlayer;
    public int damage;

    // set lifetime of the procetile
    private void Start()
    {
        StartCoroutine(LifeTime(5));
    }

    // Self-destructs after the time has elapsed
    IEnumerator LifeTime(int pLifetime)
    {
        yield return new WaitForSeconds(pLifetime);
        Destroy(gameObject);
    }

    // Inflicts damage on the opponent when hit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ownerPlayer && collision.CompareTag("NPC"))
        {
            collision.GetComponent<NPC>().GetDamage(damage);
            Destroy(gameObject);
        }
        else if (!ownerPlayer && collision.CompareTag("Player"))
        {
            GameStats.gameStatsRef.GetDamage(damage);
            Destroy(gameObject);
        }
    }
}
