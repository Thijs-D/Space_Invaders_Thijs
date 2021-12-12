using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // public variables
    public GameObject itemHealth;
    public GameObject itemPowerUp;
    public GameObject itemGun;
    public GameObject projectile;
    public GameObject projectileR;
    public GameObject projectileL;
    public AudioClip projectileClip;
    public AudioClip deathClip;

    // private variables
    protected enum AlienTypes {NORMAL, MEDIUM, HARD, BOSS};
    private  AlienTypes alientype;
    private Rigidbody2D body;
    private AudioSource currentSound;
    private int maximumHP;
    private int currentHP;
    private int score;
    private int damage;
    private float attackSpeed;
    private float projectileSpeed;
    private float moveSpeed;
    private bool doAttack;
    private bool isDead;

    // construct a new NPC and set the variables
    protected NPC(int pHP, int pDamage, float pAttackSpeed, float pProjectileSpeed, float pMoveSpeed, int pScore, AlienTypes pType)
    {
        maximumHP = pHP;
        damage = pDamage;
        currentHP = maximumHP;
        attackSpeed = pAttackSpeed;
        projectileSpeed = pProjectileSpeed;
        moveSpeed = pMoveSpeed;
        score = pScore;
        alientype = pType;
    }

    // Start is called before the first frame update
    protected void Start()
    {
        body = GetComponent<Rigidbody2D>();
        currentSound = gameObject.AddComponent<AudioSource>();
        body.velocity = new Vector2(0, -1) * moveSpeed;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (!doAttack && !isDead)
        {
            StartCoroutine(StartAttack());
        }
    }

    // apply damage
    public void GetDamage(int pAmount)
    {
        if (!isDead)
        {
            if (currentHP - pAmount <= 0)
            {
                currentHP = 0;
                isDead = true;
                StartCoroutine(Death());
            }
            else
            {
                currentHP -= pAmount;
            }
        }        
    }

    // If the life points drop to zero, it destroys itself
    IEnumerator Death()
    {
        currentSound.clip = deathClip;
        currentSound.Play();        
        yield return new WaitForSeconds(currentSound.clip.length);
        GameStats.gameStatsRef.IncreaseScore(score);
        SpawnItem();
        Destroy(gameObject);
    }

    // if it is destroyed, an item spawns with a certain probability
    private void SpawnItem()
    {
        Vector3 spawnPoint = transform.position;
        int spawnChance = Random.Range(0,11);
        if(spawnChance == 10)
        {
            Instantiate(itemGun, spawnPoint, Quaternion.identity);
        }
        else if (spawnChance > 7)
        {
            Instantiate(itemPowerUp, spawnPoint, Quaternion.identity);
        }
        else if (spawnChance > 4)
        {
            Instantiate(itemHealth, spawnPoint, Quaternion.identity);
        }
    }

    // set the attack variable and start the attack
    IEnumerator StartAttack()
    {
        doAttack = true;
        Attack();
        float randomDuration = Random.Range(-attackSpeed/2, attackSpeed*2);
        yield return new WaitForSeconds(attackSpeed + randomDuration);
        doAttack = false;
    }

    // spawn projectiles and attack the enemies
    private void Attack()
    {
        switch (alientype)
        {
            case AlienTypes.NORMAL:
                {
                    SpawnProjectile(projectile, transform.position, 0);
                    break;
                }
            case AlienTypes.MEDIUM:
                {
                    SpawnProjectile(projectile, transform.position, 0);
                    break;
                }
            case AlienTypes.HARD:
                {
                    Vector3 leftProjextile = transform.position;
                    leftProjextile.x += 0.4f;
                    Vector3 rightProjextile = transform.position;
                    rightProjextile.x -= 0.4f;
                    SpawnProjectile(projectileR, leftProjextile, 0.5f);
                    SpawnProjectile(projectileL, rightProjextile, -0.5f);
                    break;
                }
            case AlienTypes.BOSS:
                {
                    Vector3 leftProjextile = transform.position;
                    leftProjextile.x += 0.8f;
                    Vector3 rightProjextile = transform.position;
                    rightProjextile.x -= 0.8f;
                    SpawnProjectile(projectile, transform.position, 0);
                    SpawnProjectile(projectile, leftProjextile, 0);
                    SpawnProjectile(projectile, rightProjextile, 0);
                    SpawnProjectile(projectileR, leftProjextile, 0.5f);
                    SpawnProjectile(projectileL, rightProjextile, -0.5f);
                    break;
                }
            default:
                {
                    SpawnProjectile(projectile, transform.position, 0);
                    break;
                }
        }
    }

    // spawns the actual projectile
    private void SpawnProjectile(GameObject pProjectile, Vector3 Pposition, float pDirection)
    {
        GameObject currentProjectile = Instantiate(pProjectile, Pposition, Quaternion.identity);
        currentProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(pDirection, -1) * projectileSpeed;
        currentProjectile.GetComponent<Projectile>().damage = damage;
        currentSound.clip = projectileClip;
        currentSound.Play();
    }
}
