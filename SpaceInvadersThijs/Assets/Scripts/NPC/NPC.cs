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
    public AudioClip projectileClip;

    // private variables
    private Rigidbody2D body;
    private AudioSource currentSound;
    private int maximumHP;
    private int currentHP;
    private int score;
    private float attackSpeed;
    private float projectileSpeed;
    private float moveSpeed;
    private bool doAttack;

    // construct a new NPC and set the variables
    protected NPC(int pHP, float pAttackSpeed, float pProjectileSpeed, float pMoveSpeed, int pScore)
    {
        maximumHP = pHP;
        currentHP = maximumHP;
        attackSpeed = pAttackSpeed;
        projectileSpeed = pProjectileSpeed;
        moveSpeed = pMoveSpeed;
        score = pScore;
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
            GameStats.gameStatsRef.IncreaseScore(score);
            SpawnItem();
            Destroy(gameObject);
        }
        else
        {
            currentHP -= 1;
        }
    }

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
        int randomDuration = Random.Range(-1, 3);
        yield return new WaitForSeconds(attackSpeed + randomDuration);
        doAttack = false;
    }

    // spawn projectiles and attack the enemies
    private void Attack()
    {
        GameObject currentProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        currentProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * projectileSpeed;
        currentSound.clip = projectileClip;
        currentSound.Play();
    }
}
