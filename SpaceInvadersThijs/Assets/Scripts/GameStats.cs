using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStats : MonoBehaviour
{
    // public variables
    public static GameStats gameStatsRef;
    public AudioClip music;
    public AudioClip winClip;
    public AudioClip loseClip;
    public AudioClip deathClip;
    public Text scoreText;
    public Text AliveText;
    public Text WaveText;
    public Text healthText;
    public Slider healthSlider;
    public Spawner spawnerRef;
    public int countEnemies;
    public int countWaves;
    public int lastWave;

    // private variables
    private GameObject playerRef;
    private AudioSource backgroundMusic;
    private AudioSource currentSound;
    private AudioSource deathSound;
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
        backgroundMusic = gameObject.AddComponent<AudioSource>();
        currentSound = gameObject.AddComponent<AudioSource>();
        deathSound = gameObject.AddComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        maximumHP = 10;
        currentHP = maximumHP;
        scoreText.text = "Score: " + currentScore.ToString();
        healthText.text = currentHP + "/" + maximumHP;
        healthSlider.value = getProcentualHealth();
        backgroundMusic.clip = music;        
        backgroundMusic.loop = true;
        backgroundMusic.Play();
    }

    // return the player ref
    public GameObject GetPlayer()
    {
        return playerRef;
    }

    // apply damage
    public void GetDamage(int pAmount)
    {
        // if the player is dead, the code should not be executed any further
        if (!isDead)
        {
            if (currentHP - pAmount <= 0)
            {
                currentHP = 0;
                isDead = true;
                Destroy(playerRef);
                StartCoroutine(EndGame(true));
            }
            else
            {
                currentHP -= pAmount;
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
        AliveText.text = "Alive: " + countEnemies;
        if (amount < 0)
        {
            amount *= (-1);
        }
        currentScore += amount;
        scoreText.text = "Score: " + currentScore.ToString();
        // When all opponents have been defeated, either the next wave is started
        // or the game is ended if this was the last wave
        if (countEnemies <= 0)
        {
            if (countWaves == lastWave)
            {
                StartCoroutine(EndGame(false));
            }
            else
            {
                spawnerRef.currentWave++;
                spawnerRef.StartWave();
            }            
        }
    }

    // ends the game and plays a different sound depending on whether you have won or lost
    private IEnumerator EndGame(bool death)
    {
        if (death)
        {
            deathSound.clip = deathClip;
            deathSound.Play();
            currentSound.clip = loseClip;
            currentSound.Play();
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

    // sets the new values for the next wave
    public void SetWaveStats(int pAmount, int pWave)
    {
        countEnemies = pAmount;
        countWaves = pWave;
        AliveText.text = "Alive: " + countEnemies;
        WaveText.text = "Wave: " + countWaves;
    }
}
