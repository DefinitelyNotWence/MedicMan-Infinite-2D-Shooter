using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float speed; // a float is similar to an integer but you can set the value as decimals. Adding public means that it is accessible within the editor itself, and other scripts and game objects can call on this float
    public LevelManager levelManager;
    public Vector2 movement = new Vector2(); // Vector2 is essentially the coordinates system which uses a 2D axis: (x, y)
    public Weapon weapon; // Gets the Weapon script from whatever game object I put into the public field in the editor, in this case I need this in order to call the Fire() method from the weapon script
    public float health = 100f;
    public float maxHealth = 100f;
    public bool canMove = true; // a bool is set to either true or false
    public bool canDodge = true;
    public float bounceForce;
    public float dodgeSpeed = 18f;
    private bool damagable = true; // Private means that this is only accessible within this script and nowhere else
    public Collider2D Collider; // Gets the Collider2D component from whatever game object I put into the public field in the editor
    Rigidbody2D playerRB; // Creates a RigidBody2D constant called playerRB which allows me to use it in the code
    
    

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>(); // Sets the rigidbody to the RigidBody2D component assigned to the player
        canMove = true;
        Collider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        bounceForce = speed * 2;
        if (Input.GetMouseButtonDown(0)) // This is an if statement which checks for the specified condition, if it is met it will run the code within itself, in this case it checks for when the left mouse button is down then it calls the Fire() method from the weapon script
        {
            weapon.Fire(); // This is an example of calling a public method from another script, in this case it's the Fire() method from the Weapon script
        }

        if (canDodge == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                canDodge = false;
                Collider.enabled = false;
                canMove = false;
                damagable = false;
                Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; // this takes the mouse position from camera, and it takes the direction by calcualting the distance from the mouse and the player
                Vector2 dodgeDirection = direction.normalized * dodgeSpeed; // Normalized means that the distance will be set to a magnitude of 1 because the distance of the mouse from the player doesn't matter rather it is where it is relative to the player that matters

                playerRB.linearVelocity = dodgeDirection;
                StartCoroutine(dodgeRecovery(playerRB)); // This calls the IEnumerator method, in this case it is the dodgeRecovery() method

            }
        }
    }
    
    void FixedUpdate() // Different to Update() which runs once per frame, instead FixedUpdate() runs at a constant rate no matter the framerate similar to Time.deltaTime(), this is useful for rigidbodies as it will prevent jittering as a result of inconsitent framerates
    {
        if (canMove == true)
        {
            GetInput();
            MoveCharacter(movement);
        }
    }


    private void GetInput() // This is a method, methods contain code which can be run by calling the method from other parts of the code
    {
        movement.x = Input.GetAxisRaw("Horizontal"); // Sets the horizontal movement to the keys assigned for the x axis
        movement.y = Input.GetAxisRaw("Vertical"); // Sets the vertical movement to the keys assigned for the x axis
    }


    public void MoveCharacter(Vector2 movementVector)
    {
        movementVector.Normalize(); // Sets the movement to have a magnitude of 1
        // Set the velocity of the RigidBody2D instead of moving the Transform using translate to prevent jittering
        playerRB.linearVelocity = movementVector * speed;
    }

    public void OnTriggerEnter2D(Collider2D other) // OnTriggerEnter2D detects when another object as entered a Collider which is a trigger, when it detects this it will run the code inside it
    {
        if (damagable == true)
        {

            switch (other.gameObject.tag) // Switch statements and cases work similarly to if else statements. The main didfference is, instead of running through lots of if and else statements it will just go straight to the right case and run the code within that case.
            {
                case "Enemy":
                    Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
                    canMove = false;
                    health = health - 25;
                    if (health <= 0)
                    {
                        Destroy(gameObject);
                        levelManager.GameOver();
                    }
                    Vector2 differenceDirection = transform.position - enemy.transform.position;
                    Vector2 difference = differenceDirection.normalized * bounceForce;

                    playerRB.linearVelocity = difference;
                    StartCoroutine(recovery(playerRB));

                    break;
            }
        }
    }   

    private IEnumerator recovery(Rigidbody2D playerRB) // an IEnumerator is a type of method I used in order to add time delays when using invoke wouldn't be applicable
    {
        
        yield return new WaitForSeconds(0.2f); // This essentially adds a time delay where the float in the brackets specifies the seconds it should wait for before running the next line
        playerRB.linearVelocity = new Vector2();
        canMove = true;

    }

    private IEnumerator dodgeRecovery(Rigidbody2D playerRB)
    {
        yield return new WaitForSeconds(0.5f);
        playerRB.linearVelocity = new Vector2();
        canMove = true;
        damagable = true;
        Collider.enabled = true;

        yield return new WaitForSeconds(3f);
        canDodge = true;
    }
}
