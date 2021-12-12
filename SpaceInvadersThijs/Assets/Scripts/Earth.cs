using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    // Destroys any ship that flies past the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            collision.GetComponent<NPC>().GetDamage(1000);
        }        
    }
}
