using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // public variables
    public bool ownerPlayer;

    // set lifetime of the procetile
    private void Start()
    {
        StartCoroutine(LifeTime(5));
    }

    IEnumerator LifeTime(int pLifetime)
    {
        yield return new WaitForSeconds(pLifetime);
        Destroy(gameObject);
    }

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
