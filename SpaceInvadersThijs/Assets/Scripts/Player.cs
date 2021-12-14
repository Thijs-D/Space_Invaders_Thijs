using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // public variables
    public GameObject projectile;
    public GameObject projectileL;
    public GameObject projectileR;
    public AudioClip projectileClip;
    public bool player2;

    // private variables
    private enum attackType {NORMAL, DOUBLE, TRIPLE, ULTIMATE, ULTRA};
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
        float moveDirection;
        if (player2)
        {
            moveDirection = Input.GetAxis("Horizontal_Player2");
        }
        else
        {
            moveDirection = Input.GetAxis("Horizontal");
        }
        if (moveDirection != 0)
        {
            direction = new Vector2(moveDirection * moveSpeed, body.velocity.y);
            MovePlayer();
        }
        // start attack
        if (!doAttack)
        {
            if (player2 && Input.GetAxis("Fire1_Player2") != 0)
            {
                StartCoroutine(StartAttack());
            }
            else if (!player2 && Input.GetAxis("Fire1") != 0)
            {
                StartCoroutine(StartAttack());
            }
            
        }
    }

    // moves the player horizontally
    private void MovePlayer()
    {
        transform.Translate(moveSpeed * Time.deltaTime * direction);
    }

    // activates the boost and sets a duration
    public void ActivatePowerup(int pDuration)
    {
        StartCoroutine(StartPowerup(pDuration));
    }

    // starts the powerup and resets it after the time has elapsed
    IEnumerator StartPowerup(int pDuration)
    {
        speedBoost = Random.Range(2,5);
        yield return new WaitForSeconds(pDuration);
        speedBoost = 0;
    }

    // When a weapon item is collected, the current weapon is exchanged for a better one
    public void SetGun()
    {
        switch (currentAttackType)
        {
            case attackType.NORMAL:
                {
                    currentAttackType = attackType.DOUBLE;
                    break;
                }
            case attackType.DOUBLE:
                {
                    currentAttackType = attackType.TRIPLE;
                    break;
                }
            case attackType.TRIPLE:
                {
                    currentAttackType = attackType.ULTIMATE;
                    break;
                }
            case attackType.ULTIMATE:
                {
                    currentAttackType = attackType.ULTRA;
                    break;
                }
            default:
                {
                    currentAttackType = attackType.ULTRA;
                    break;
                }
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
        Vector3 leftProjextile = transform.position;
        leftProjextile.x += 0.2f;
        Vector3 rightProjextile = transform.position;
        rightProjextile.x -= 0.2f;
        // Depending on which attack type is currently selected, a different attack pattern is applied
        switch (currentAttackType)
        {
            case attackType.NORMAL:
                {
                    SpawnProjectile(projectile, transform.position, 0, 1);
                    break;
                }
            case attackType.DOUBLE:
                {
                    SpawnProjectile(projectile, leftProjextile, 0, 1);
                    SpawnProjectile(projectile, rightProjextile, 0, 1);
                    break;
                }
            case attackType.TRIPLE:
                {
                    SpawnProjectile(projectile, transform.position, 0, 1);
                    SpawnProjectile(projectile, leftProjextile, 0, 1);
                    SpawnProjectile(projectile, rightProjextile, 0, 1);
                    break;
                }
            case attackType.ULTIMATE:
                {
                    SpawnProjectile(projectile, transform.position, 0, 2);
                    SpawnProjectile(projectile, leftProjextile, 0, 2);
                    SpawnProjectile(projectile, rightProjextile, 0, 2);
                    break;
                }
            case attackType.ULTRA:
                {
                    SpawnProjectile(projectile, transform.position, 0, 2);
                    SpawnProjectile(projectile, leftProjextile, 0, 2);
                    SpawnProjectile(projectile, rightProjextile, 0, 2);
                    SpawnProjectile(projectileR, leftProjextile, 0.5f, 2);
                    SpawnProjectile(projectileL, rightProjextile, -0.5f, 2);
                    break;
                }
            default:
                {
                    SpawnProjectile(projectile, transform.position, 0, 1);
                    break;
                }
        }
    }

    // spawns the actual projectile
    private void SpawnProjectile(GameObject pProjectile, Vector3 Pposition, float pDirection, int pDamage)
    {
        GameObject currentProjectile = Instantiate(pProjectile, Pposition, Quaternion.identity);
        currentProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(pDirection, 1) * projectileSpeed;
        currentProjectile.GetComponent<Projectile>().damage = pDamage;
        currentProjectile.GetComponent<Projectile>().ownerPlayer = true;
        currentSound.clip = projectileClip;
        currentSound.Play();
    }
}
