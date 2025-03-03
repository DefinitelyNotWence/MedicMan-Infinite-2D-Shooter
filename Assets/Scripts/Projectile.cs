using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    public float damage = 5;
    public float XMax;
    public float YMax;
    

    private void Update()
    {
        if (transform.position.x > XMax || transform.position.x < -XMax || transform.position.y > YMax || transform.position.y < -YMax) // || means or in C# so this code will run if any of the conditions are met, this is done to be more efficient as it prevents me from writing a lot of if statements.
        {
            Destroy(gameObject);
        }

        

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Walls":
                Destroy(gameObject);
                break;
            case "Enemy":
                other.gameObject.GetComponent<EnemyBehaviour>().health -= damage;
                Destroy(gameObject);
                break;
            
        }
    }

}
