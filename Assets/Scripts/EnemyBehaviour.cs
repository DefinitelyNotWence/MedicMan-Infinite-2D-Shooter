using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    public float speed;
    public Transform target;
    public float maxHealth;
    public float health;
    public SpawnManager spawnManager;
    public LevelManager levelManager;
    public int enemyType;

    public Rigidbody2D rb;
    public Projectile projectile;
    Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    { 
        target = GameObject.Find("Player").transform;
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        
        if (enemyType == 0)
        {
            if (levelManager.level < 5)
            {
                maxHealth = 10;
                speed = 4;
            }
            else if (levelManager.level < 10)
            {
                maxHealth = Random.Range(10, 20);
                speed = 5;
            }
            else if (levelManager.level < 15)
            {
                maxHealth = Random.Range(10, 30);
                speed = 6;
            }
            else if (levelManager.level < 20)
            {
                maxHealth = Random.Range(15, 40);
                speed = 7;
            }
            else if (levelManager.level < 25)
            {
                maxHealth = Random.Range(20, 45);
                speed = 8;
            }
            else if (levelManager.level < 30)
            {
                maxHealth = Random.Range(25, 50);
                speed = 9;
            }
            else if (levelManager.level >= 30)
            {
                maxHealth = Random.Range(30, 55);
                speed = 10;
            }
        }
        else if (enemyType == 1)
        {
            if (levelManager.level < 10)
            {
                maxHealth = 5;
                speed = 7;
            }
            else if (levelManager.level < 15)
            {
                maxHealth = Random.Range(5, 15);
                speed = 8.5f;
            }
            else if (levelManager.level < 20)
            {
                maxHealth = Random.Range(10, 25);
                speed = 10;
            }
            else if (levelManager.level < 25)
            {
                maxHealth = Random.Range(15, 35);
                speed = 11.5f;
            }
            else if (levelManager.level < 30)
            {
                maxHealth = Random.Range(20, 40);
                speed = 13;
            }
            else if (levelManager.level >= 30)
            {
                maxHealth = Random.Range(25, 50);
                speed = 14.5f;
            }
        }
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Mathf is a collection of mathematical functions in unity, Atan2 essentially gets the angle and Rad2Deg converts the angle from radians to degrees
        rb.rotation = angle;
        moveDirection = direction;
        if (health <= 0)
        {
            spawnManager.enemyCount -= 1;
            levelManager.enemyKilled();
            Destroy(gameObject);
        }

    }

    public void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * speed; 
    }

    
    
}
