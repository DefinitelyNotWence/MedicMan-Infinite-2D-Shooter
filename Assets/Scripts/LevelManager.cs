using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    public GameObject gameOverScreen;
    public GameObject gameUI;
    public GameObject[] powerUps;
    public Transform player;
    public UiDisplay uiManager;
    public int level;
    public int enemiesNeeded;
    public int enemiesKilled;
    public int enemiesLeft;
    public int levelsToPU = 2;

    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        enemiesNeeded = 5;
        uiManager.bestLevelText.text = "Best : " + PlayerPrefs.GetInt("bestLevel");
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        enemiesLeft = enemiesNeeded - enemiesKilled;

    }

    public void enemyKilled()
    {
        enemiesKilled = enemiesKilled + 1;
        if (enemiesKilled == enemiesNeeded)
        {
            if (level <= 30)
            {
                levelsToPU -= 1;
                if (levelsToPU == 0)
                {
                    enemiesKilled = enemiesNeeded;
                    uiManager.FeedbackText.text = "Choose a PowerUp in";
                    StartCoroutine(PowerUpTimer());
                    
                }
                else
                {
                    uiManager.FeedbackText.text = "Next level in";
                    StartCoroutine(NextLevelTimer());
                }
            }
            else
            {
                uiManager.FeedbackText.text = "Next level in";
                StartCoroutine(NextLevelTimer());
            }
        }
    }

    public void NextLevel()
    {
        uiManager.statBoostText.fontSize = 0;
        uiManager.FeedbackText.text = "Next level in";
        StartCoroutine(NextLevelTimer());
        levelsToPU = 2;
    }

    public IEnumerator NextLevelTimer()
    {
        uiManager.CountdownText.text = "3";
        yield return new WaitForSeconds(0.5f);
        uiManager.CountdownText.text = "2";
        yield return new WaitForSeconds(0.5f);
        uiManager.CountdownText.text = "1";
        yield return new WaitForSeconds(0.5f);
        uiManager.CountdownText.text = "";
        uiManager.FeedbackText.text = "";

        level += 1;
        spawnManager.maxEnemies += 1;
        enemiesKilled = 0;
        enemiesNeeded += 1;
    }

    public IEnumerator PowerUpTimer()
    {
        uiManager.CountdownText.text = "3";
        yield return new WaitForSeconds(0.5f);
        uiManager.CountdownText.text = "2";
        yield return new WaitForSeconds(0.5f);
        uiManager.CountdownText.text = "1";
        yield return new WaitForSeconds(0.5f);
        uiManager.CountdownText.text = "";
        uiManager.FeedbackText.text = "";

        uiManager.statBoostText.fontSize = 15;
        Vector2 spawnPos1 = new Vector2(player.position.x - 5, player.position.y);
        Vector2 spawnPos2 = new Vector2(player.position.x, player.position.y + 5);
        Vector2 spawnPos3 = new Vector2(player.position.x + 5, player.position.y);
        Instantiate(powerUps[0], spawnPos1, powerUps[0].transform.rotation);
        Instantiate(powerUps[1], spawnPos2, powerUps[1].transform.rotation);
        Instantiate(powerUps[2], spawnPos3, powerUps[2].transform.rotation);
    }

    public void GameOver()
    {
        if (level > PlayerPrefs.GetInt("bestLevel"))
        {
            PlayerPrefs.SetInt("bestLevel", level);
        }
        uiManager.GameOverLevelText.text = "You reached level : " + level;
        uiManager.GameOverBestText.text = "Your best is level : " + PlayerPrefs.GetInt("bestLevel");
        gameOverScreen.SetActive(true);
        gameUI.SetActive(false);
    }
}
