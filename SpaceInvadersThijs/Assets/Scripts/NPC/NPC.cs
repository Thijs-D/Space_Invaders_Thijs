using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // public variables
    public GameObject projectile;

    // private variables
    private int maximumHP;
    private int currentHP;
    private int attackSpeed;
    private int projectileSpeed;
    private bool doAttack;

    // construct a new NPC and set the variables
    protected NPC(int pHP, int pAttackSpeed, int pProjectileSpeed)
    {
        maximumHP = pHP;
        currentHP = maximumHP;
        attackSpeed = pAttackSpeed;
        projectileSpeed = pProjectileSpeed;
    }

    // Start is called before the first frame update
    protected void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        if (!doAttack)
        {
            StartCoroutine(StartAttack());
        }
    }

    // apply damage
    public void GetDamage()
    {
        if (currentHP - 1 <= 0)
        {
            currentHP = 0;
            Destroy(gameObject);
        }
        else
        {
            currentHP -= 1;
        }
    }

    // set the attack variable and start the attack
    IEnumerator StartAttack()
    {
        doAttack = true;
        Attack();
        yield return new WaitForSeconds(attackSpeed);
        doAttack = false;
    }

    // spawn projectiles and attack the enemies
    private void Attack()
    {
        GameObject currentProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        currentProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * projectileSpeed;
    }
}
