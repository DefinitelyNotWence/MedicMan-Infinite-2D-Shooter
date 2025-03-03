using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
    public Projectile projectile;
    public LevelManager levelManager;
    public int powerUpType;
    public GameObject player; // Allows me to add the player as a game object which allows me to get its position
                              

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>(); // As it is a prefab I can't directly set these gameobjects into the prefab, so I set them when they spawn in by using tags
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        player = GameObject.Find("Player");
        
    }

    private void FixedUpdate()
    {
        switch (powerUpType)
        {
            case 0:
                transform.position = player.transform.position + new Vector3(5, 0, 0);
                break;
            case 1:
                transform.position = player.transform.position + new Vector3(0, 5, 0);
                break;
            case 2:
                transform.position = player.transform.position + new Vector3(-5, 0, 0);
                break;

        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Projectile":
                DestroyAll("PowerUP");
                levelManager.NextLevel();
                if (powerUpType == 0)
                {
                    playerController.speed = playerController.speed + 1f;
                }
                else if (powerUpType == 1)
                {
                    playerController.maxHealth = playerController.maxHealth + 25f;
                    playerController.health = playerController.maxHealth;
                }
                else if (powerUpType == 2)
                {
                    projectile.damage = projectile.damage + 2.5f;
                }
                break;
            case "Player":
                DestroyAll("PowerUP");
                levelManager.NextLevel();
                if (powerUpType == 0)
                {
                    if (playerController.Collider.enabled == true)
                    {
                        playerController.speed = playerController.speed + 0.5f;
                    }
                    else if (playerController.Collider.enabled == false)
                    {
                        playerController.speed = playerController.speed + 1f;
                    }

                }
                else if (powerUpType == 1)
                {
                    if (playerController.Collider.enabled == true)
                    {
                        playerController.maxHealth = playerController.maxHealth + 12.5f;
                        playerController.health = playerController.maxHealth;
                    }
                    else if (playerController.Collider.enabled == false)
                    {
                        playerController.maxHealth = playerController.maxHealth + 25f;
                        playerController.health = playerController.maxHealth;
                    }
                }
                else if (powerUpType == 2)
                {
                    if (playerController.Collider.enabled == true)
                    {
                        projectile.damage = projectile.damage + 1.25f;
                    }
                    else if (playerController.Collider.enabled == false)
                    {
                        projectile.damage = projectile.damage + 2.5f;
                    }
                }
                break;
        }
        
        void DestroyAll(string tag)
        {
            GameObject[] powerUpsOnScreen = GameObject.FindGameObjectsWithTag(tag); // This creates a local array which contains all game objects with the tag specified
            foreach (GameObject powerUp in powerUpsOnScreen) // this repeats the code inside for every single game object in the array
            {
                GameObject.Destroy(powerUp); // This removes the game object from the game world
            }
        }
    }
}
