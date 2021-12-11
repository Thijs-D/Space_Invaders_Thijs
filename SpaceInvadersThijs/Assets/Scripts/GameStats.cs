using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStats : MonoBehaviour
{
    // public variables
    public static GameStats gameStatsRef;
    public AudioClip winClip;
    public AudioClip loseClip;
    public Text scoreText;
    public Text healthText;
    public Slider healthSlider;
    public int countEnemies;

    // private variables
    private GameObject playerRef;
    private AudioSource currentSound;
    private bool isDead;
    private int maximumHP;
    private int currentHP;
    private int currentScore;

    // Awake is called before Start()
    // handle the static self refernce and set the player variable
    private void Awake()
    {
        gameStatsRef = this;
        playerRef = GameObject.Find("Player");
        currentSound = gameObject.AddComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //TODO set amount of enemies
        countEnemies = 2;

        currentScore = 0;
        maximumHP = 5;
        currentHP = maximumHP;
        scoreText.text = "Score: " + currentScore.ToString();
        healthText.text = currentHP + "/" + maximumHP;
        healthSlider.value = getProcentualHealth();
    }

    // return the player ref
    public GameObject GetPlayer()
    {
        return playerRef;
    }

    // apply damage
    public void GetDamage()
    {
        if (!isDead)
        {
            if (currentHP - 1 <= 0)
            {
                currentHP = 0;
                isDead = true;
                StartCoroutine(EndGame(true));
            }
            else
            {
                currentHP -= 1;
            }
            healthSlider.value = getProcentualHealth();
            healthText.text = currentHP + "/" + maximumHP;
        }        
    }

    // heal character
    public void HealPlayer(int heal)
    {
        if (heal < 0)
        {
            heal *= (-1);
        }
        if (currentHP + heal > maximumHP)
        {
            currentHP = maximumHP;
        }
        else
        {
            currentHP += heal;
        }
        healthSlider.value = getProcentualHealth();
        healthText.text = currentHP + "/" + maximumHP;
    }

    // increase the score of the player
    public void IncreaseScore(int amount)
    {
        countEnemies--;
        if (amount < 0)
        {
            amount *= (-1);
        }
        currentScore += amount;
        scoreText.text = "Score: " + currentScore.ToString();
        if (countEnemies <= 0)
        {
            StartCoroutine(EndGame(false));
        }
    }

    // ends the game and plays a different sound depending on whether you have won or lost
    private IEnumerator EndGame(bool death)
    {
        if (death)
        {
            currentSound.clip = loseClip;
            Destroy(playerRef);
        }
        else
        {
            currentSound.clip = winClip;
        }
        currentSound.Play();
        float fLength = currentSound.clip.length;
        yield return new WaitForSeconds(fLength);
        SceneManager.LoadScene(0);
    }

    // return the procentual health with 1 -> 100% and 0 -> 0% and 0.5 -> 50%
    private float getProcentualHealth()
    {
        return ((float)currentHP / (float)maximumHP);
    }
}
