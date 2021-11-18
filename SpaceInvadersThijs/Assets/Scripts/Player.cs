using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // public variables
    public GameObject projectile;

    // private variables
    private int moveSpeed;
    private int attackSpeed;
    private int speedBoost;
    private int projectileSpeed;
    private bool doAttack;
    private Rigidbody2D body;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        attackSpeed = 3;
        moveSpeed = 2;
        projectileSpeed = 3;
        speedBoost = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float moveDirection = Input.GetAxis("Horizontal");
        if (moveDirection != 0)
        {
            direction = new Vector2(moveDirection * moveSpeed, body.velocity.y);
            MovePlayer();
        }
        if (!doAttack && Input.GetAxis("Fire1") != 0)
        {
            StartCoroutine(StartAttack());
        }
    }

    // moves the player horizontally
    private void MovePlayer()
    {
        transform.Translate(moveSpeed * Time.deltaTime * direction);
    }

    // activate the boost
    public void ActivateBoost(int duration)
    {
        StartBoost(duration);
    }

    IEnumerator StartBoost(int duration)
    {
        speedBoost = 2;
        yield return new WaitForSeconds(duration);
        speedBoost = 0;
    }

    // set the attack variable and start the attack
    IEnumerator StartAttack()
    {
        doAttack = true;
        Attack();
        yield return new WaitForSeconds(attackSpeed - speedBoost);
        doAttack = false;
    }

    // spawn projectiles and attack the enemies
    private void Attack()
    {
        GameObject currentProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        currentProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0,1) * projectileSpeed;
        currentProjectile.GetComponent<Projectile>().ownerPlayer = true;
    }
}
