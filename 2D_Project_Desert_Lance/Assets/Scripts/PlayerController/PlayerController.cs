using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Reffrences")]
    public Rigidbody2D playerRigidBody2D;
    public Animator playerAnimator;

    [Space]
    [Header("Character attributes")]
    public float baseMovementSpeed = 1.0f;

    [Space]
    [Header("Character Statistics")]
    public Vector2 movementDirection;
    public float movementSpeed;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     ProcessInputs();
    }
    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        Movement();
        Animate();
    }

    void ProcessInputs(){
        movementDirection = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        movementSpeed = Mathf.Clamp(movementDirection.magnitude,0.0f, 1.0f);
        movementDirection.Normalize();
    }

    void Movement(){
        playerRigidBody2D.velocity = movementDirection * movementSpeed * baseMovementSpeed;
    }

    void Animate(){
        if(movementDirection != Vector2.zero){
        playerAnimator.SetFloat("Horizontal", movementDirection.x);
        playerAnimator.SetFloat("Vertical", movementDirection.y);
        }
        playerAnimator.SetFloat("Speed",movementSpeed);

    }

   /* private void OnTriggerEnter2D(Collider2D other)
    {
        //check if triggeder by item, and add it to the inventory
        var item = other.GetComponent<GroundItem>();
        if(item)
        {
            playerInventory.AddItem(new Item(item.item),1);
            Destroy(other.gameObject);
        } 
    }
    */

}
