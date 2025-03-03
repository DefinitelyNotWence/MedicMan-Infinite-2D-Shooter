using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    public Animator animator;
    private Vector2 movement = new Vector2();

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); // Sets the horizontal movement to the keys assigned for the x axis
        movement.y = Input.GetAxisRaw("Vertical"); // Sets the vertical movement to the keys assigned for the x axis

        if (movement.x > 0)
        {
            animator.SetFloat("speed", 1); // This sets the animator parameter "speed" to 1 which will trigger the run animation as set up in the animator
        }
        else if (movement.y > 0)
        {
            animator.SetFloat("speed", 1);
        }
        else if (movement.x < 0)
        {
            animator.SetFloat("speed", 1);
        }
        else if (movement.y < 0)
        {
            animator.SetFloat("speed", 1);
        }
        else
        {
            animator.SetFloat("speed", 0); // This sets the animator paremeter "speed" to 0 which will trigger the idle animation as set up in the animator
        }
    }
}
