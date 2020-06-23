using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IDamageable
{
    public int Health { get; set; }  //Interface IDamageable health 
    //Cache Variables
    private Rigidbody2D rigidBody2D;           //player Rigidbody
    private PlayerAnimation playerAnimation;   // animator controller class
    private SpriteRenderer spriteRenderer;     // player sprite controller
    private SpriteRenderer swordSpriteRendere; // sword trail sprite controller

    [SerializeField]
    private float jumpForce;        //Jump Power modifier
    [SerializeField]
    private float moveSpeed;       //Movement Speed modifier
    private float horizontalInput; //Controller inputs
    private float verticalInput;  // Controller inputs

    private bool attack;          //variable for attack input checker
    private bool _jumpReset;      //jump Cooldown variable

    public LayerMask layerMask; //Layermask used for setting colliders for Raycast ( 8 for ground)

   
    // Start is called before the first frame update
    void Start()
    {
        //Player ControllerComponents
        rigidBody2D = GetComponent<Rigidbody2D>();         //Player Rb2D
        playerAnimation = GetComponent<PlayerAnimation>(); //PLayer AnimClass Controller
        //Child Components
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();                 //Player Sprite Renderer
        swordSpriteRendere = transform.GetChild(1).GetComponent<SpriteRenderer>(); //Sword Trail SpriteRenderer
    }

    // Update is called once per frame
    void Update()
    {
        //Moving Input
        horizontalInput = Input.GetAxisRaw("Horizontal"); //Horizontal input
        verticalInput = Input.GetAxisRaw("Vertical");     //Vertical Input

        //Attack input
        attack = Input.GetMouseButtonDown(0);            //Left mouse button input 

    }
    void FixedUpdate()
    {
        isGrounded();  //Check if player is on ground
        Movement();    //Handle Movement
 
        if (verticalInput != 0 && isGrounded() == true) //Jump Conditions
            Jump();    //Jump Method                                  

        if (attack == true && isGrounded()==true)       //Attack Conditions
            playerAnimation.Attack(); //playerAnimation class call method

        
        
    }

    public void Movement()
    {
        //SpriteFlipper Method
        Flip();
        
        //Movement Logic
        rigidBody2D.velocity = new Vector2(horizontalInput*moveSpeed, rigidBody2D.velocity.y); //movement of player through Physics
        playerAnimation.Move(horizontalInput);            //Call Move Animation
    }
    public void Jump()
    {
        // Jumping
        rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpForce); //player jump through Physics
        //Jump Cooldown invoke
        StartCoroutine(JumpResetCoroutine()); // Call the jump cooldown counter ( Coroutine )
        //Set animator to play proper animation
        playerAnimation.Jump(true);          //Call Jump Animation
        
    }

    public bool isGrounded()
    {
        //Function :
        //Return if character is on ground or not
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down,0.8f, layerMask.value);  //Raycast   
        Debug.DrawRay(transform.position, Vector2.down*0.8f,Color.red, 0.56f);                         // RayDraw for run              
        if (hit.collider != null) {    //if hit collided with the floor
           if(!_jumpReset)             //and jump cooldown is false
            {
                playerAnimation.Jump(false); //exit jump animation
                return true;                 // tell the Game that the object isGrounded
            }
        }
        return false;                       // Else tell the Game that the object is NOT Grounded
    }
    public void Flip()
    {
        //Flipping the sprites based on the direction
        if (horizontalInput < 0)
        {
            //spriteRendere is for Plaayer sprite
            spriteRenderer.flipX = true;
            swordSpriteRendere.flipY = spriteRenderer.flipX;
            //swordSpriteRendere.flipX = spriteRenderer.flipX;

            Vector3 newPosition = swordSpriteRendere.transform.localPosition; // Get position of child object that has this spriteRenderer
            newPosition.x = -1.01f;                  //Change x Position of sword trail
            swordSpriteRendere.transform.localPosition = newPosition;         //Assign the new position to sword trail

        }
        else if (horizontalInput > 0)
        {
            //swordSpriteRendere is for sword trail sprite
            spriteRenderer.flipX = false;
            swordSpriteRendere.flipY = spriteRenderer.flipX;
            //swordSpriteRendere.flipX = spriteRenderer.flipX;

            Vector3 newPosition = swordSpriteRendere.transform.localPosition; // Get position of child object that has this spriteRenderer
            newPosition.x = 1.01f;                  //Change x Position of sword trail
            swordSpriteRendere.transform.localPosition = newPosition;         //Assign the new position to sword trail

        }
    }

    public void Damage() // Interface contract - Class needs to implements its own Damage() function
    {


    }
    IEnumerator JumpResetCoroutine()
    {
        
        _jumpReset = true; //start jump cooldown
        yield return new WaitForSeconds(0.5f); // wait for 0.5 seconds
        _jumpReset = false; //reset the jump cooldown
    }
}
