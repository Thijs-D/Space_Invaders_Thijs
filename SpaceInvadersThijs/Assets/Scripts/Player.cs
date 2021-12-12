using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // public variables
    public GameObject projectileNormal;
    public GameObject projectileDouble;
    public GameObject projectileTriple;
    public AudioClip projectileClip;    

    // private variables
    private enum attackType {NORMAL, DOUBLE, TRIPLE};
    private attackType currentAttackType = attackType.NORMAL;
    private AudioSource currentSound;
    private int moveSpeed;
    private float attackSpeed;
    private int speedBoost;
    private int projectileSpeed;
    private bool doAttack;
    private Rigidbody2D body;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        currentSound = gameObject.AddComponent<AudioSource>();
        attackSpeed = 0.5f;
        moveSpeed = 3;
        projectileSpeed = 10;
        speedBoost = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // move the player
        float moveDirection = Input.GetAxis("Horizontal");
        if (moveDirection != 0)
        {
            direction = new Vector2(moveDirection * moveSpeed, body.velocity.y);
            MovePlayer();
        }
        // start attack
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

    // activates the boost and sets a duration
    public void ActivateBoost(int duration)
    {
        StartCoroutine(StartBoost(duration));
    }

    // starts the boost and resets it after the time has elapsed
    IEnumerator StartBoost(int duration)
    {
        speedBoost = 2;
        yield return new WaitForSeconds(duration);
        speedBoost = 0;
    }

    public void SetGun()
    {
        if (currentAttackType == attackType.NORMAL)
        {
            currentAttackType = attackType.DOUBLE;
        }
        else if (currentAttackType == attackType.DOUBLE)
        {
            currentAttackType = attackType.TRIPLE;
        }
    }

    // set the attack variable and start the attack
    IEnumerator StartAttack()
    {
        doAttack = true;
        Attack();
        if (speedBoost < 1)
        {
            speedBoost = 1;
        }
        yield return new WaitForSeconds(attackSpeed / speedBoost);
        doAttack = false;
    }

    // spawn projectiles and attack the enemies
    private void Attack()
    {
        GameObject currentProjectile;
        switch (currentAttackType)
        {
            case attackType.NORMAL:
                {
                    currentProjectile = Instantiate(projectileNormal, transform.position, Quaternion.identity);
                    currentProjectile.GetComponent<Projectile>().damage = 1;
                    break;
                }
            case attackType.DOUBLE:
                {
                    currentProjectile = Instantiate(projectileDouble, transform.position, Quaternion.identity);
                    currentProjectile.GetComponent<Projectile>().damage = 2;
                    break;
                }
            case attackType.TRIPLE:
                {
                    currentProjectile = Instantiate(projectileTriple, transform.position, Quaternion.identity);
                    currentProjectile.GetComponent<Projectile>().damage = 3;
                    break;
                }
            default:
                {
                    currentProjectile = Instantiate(projectileNormal, transform.position, Quaternion.identity);
                    currentProjectile.GetComponent<Projectile>().damage = 1;
                    break;
                }
        }
        currentProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1) * projectileSpeed;
        currentProjectile.GetComponent<Projectile>().ownerPlayer = true;
        currentSound.clip = projectileClip;
        currentSound.Play();
    }
}
