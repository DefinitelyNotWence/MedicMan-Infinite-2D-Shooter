using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColour : MonoBehaviour
{
    public Animator animator;
    public EnemyBehaviour enemy;


    // Start is called before the first update frame
    void Start()
    {
        if (enemy.health > 20)
        {
            animator.SetFloat("healthLevel", 3);
        }
        else if (enemy.health > 10)
        {
            animator.SetFloat("healthLevel", 2);
        }
        else if (enemy.health <= 10)
        {
            animator.SetFloat("healthLevel", 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.health > 20)
        {
            animator.SetFloat("healthLevel", 3);
        }
        else if (enemy.health > 10)
        {
            animator.SetFloat("healthLevel", 2);
        }
        else if (enemy.health <= 10)
        {
            animator.SetFloat("healthLevel", 1);
        }

    }
}
