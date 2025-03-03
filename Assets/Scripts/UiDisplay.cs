using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiDisplay : MonoBehaviour
{
    public LevelManager levelManager;
    public PlayerController player;
    public SpawnManager spawnManager;
    public PlayerController playerController;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI bestLevelText;
    public TextMeshProUGUI enemyCounter;
    public TextMeshProUGUI dodgeCheck;
    public TextMeshProUGUI statBoostText;
    public TextMeshProUGUI CountdownText;
    public TextMeshProUGUI FeedbackText;
    public TextMeshProUGUI GameOverLevelText;
    public TextMeshProUGUI GameOverBestText;

    // Start is called before the first frame update
    void Start()
    {
        statBoostText.fontSize = 0;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "HP : " + player.health;
        levelText.text = "Level : " + levelManager.level;
        enemyCounter.text = "Enemies left : " + levelManager.enemiesLeft;
        if (playerController.canDodge == true)
        {
            dodgeCheck.text = "Dodge : RightClick";
        }
        else if (playerController.canDodge == false)
        {
            dodgeCheck.text = "Dodge : Recharging";
        }
    }
}
