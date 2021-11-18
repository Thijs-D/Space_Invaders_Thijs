using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour
{
    // public variables
    public static GameStats gameStatsRef;
    public Text scoreText;
    public Image life1;
    public Image life2;
    public Image life3;
    public Image noLife;
    public Image life;

    // private variables
    private GameObject playerRef;
    private int maximumHP;
    private int currentHP;
    private int currentScore;

    // Awake is called before Start()
    // handle the static self refernce and set the player variable
    private void Awake()
    {
        if (gameStatsRef != null)
        {
            Destroy(gameStatsRef);
        }
        else
        {
            gameStatsRef = this;
        }
        DontDestroyOnLoad(this);
        playerRef = GameObject.Find("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        maximumHP = 3;
        currentHP = maximumHP;
        scoreText.text = currentScore.ToString();
    }

    // return the player ref
    public GameObject getPlayer()
    {
        return playerRef;
    }

    // apply damage
    public void GetDamage()
    {
        if (currentHP - 1 <= 0)
        {
            currentHP = 0;
            Destroy(playerRef);
        }
        else
        {
            currentHP -= 1;
        }
        ChangeLife();
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
        ChangeLife();
    }

    // increase the score of the player
    public void IncreaseScore(int amount)
    {
        if (amount < 0)
        {
            amount *= (-1);
        }
        currentScore += amount;
        scoreText.text = currentScore.ToString();
    }

    // set the lifepoint images
    private void ChangeLife()
    {
        switch (currentHP)
        {
            case 3:
                {
                    life3 = life;
                    life2 = life;
                    life1 = life;
                    break;
                }
            case 2:
                {
                    life3 = noLife;
                    life2 = life;
                    life1 = life;
                    break;
                }
            case 1:
                {
                    life3 = noLife;
                    life2 = noLife;
                    life1 = life;
                    break;
                }
            case 0:
                {
                    life3 = noLife;
                    life2 = noLife;
                    life1 = noLife;
                    break;
                }
        }
    }
}
