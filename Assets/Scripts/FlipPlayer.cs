using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipPlayer : MonoBehaviour
{
    private Vector3 mousePos;
    bool facingRight = true;
    public SpriteRenderer spr;
    public Camera mainCamera;

    // Update is called once per frame
    void FixedUpdate()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position; // converts the screen postion of the mouse to the world position of the mouse
        if (mousePos.x > 0)
        {
            if (facingRight == false)
            {
                spr.flipX = false; // This sets the sprite renderer to be oriented the original way around in the x direction
                facingRight = true;
            }
        }
        else if (mousePos.x < 0)
        {
            if (facingRight == true)
            {
                spr.flipX = true; // This sets the sprite renderer to be oriented the opposite way around in the x direction
                facingRight = false;
            }
        }
    }
}
