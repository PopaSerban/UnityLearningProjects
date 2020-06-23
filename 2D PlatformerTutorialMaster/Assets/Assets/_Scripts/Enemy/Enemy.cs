using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]protected int health; //enemy HP
    [SerializeField]protected int speed;  //enemy movement SP
    [SerializeField]protected int gems;  // Loot power of enemy

    [SerializeField]protected Transform pointA, pointB;  //points for movement 


    protected Vector3 _currentTarget;
    protected Animator _animator;              //Child sprite animator
    protected SpriteRenderer _spriteRenderer;  //Child sprite - spriteRenderer component
    protected bool isDead;                    //check if enemy is dead

    protected bool isHit;     //check if hit for movement freeze

    protected Player player; //get reffrence of player

    public virtual void Init()
    {
        _animator = GetComponentInChildren<Animator>();                            //get child component
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();                //get child spriteRenderer
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); //find player and get component
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        //check if enemy is in idle animation
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")&&_animator.GetBool("InCombat")==false) // if In idle state and not in combat. then block Movement
            return;

        if(!isDead)   //if enemy is dead, dont move
        WaypointsMovement(); //Call move between two points function ( enemy Base Movement)
    }

    public virtual void WaypointsMovement()//<<<<<Move between two points>>>>
    {
        //First flip the Sprite if needed
        if (_currentTarget == pointB.position)
            _spriteRenderer.flipX = false;
        else if (_currentTarget == pointA.position)
            _spriteRenderer.flipX = true;

        if (transform.position == pointA.position) //if obj on point a
        {
            _currentTarget = pointB.position;   //Switch for point b
            _animator.SetTrigger("Idle");
        }
        else if (transform.position == pointB.position) //else if point b
        {
            _currentTarget = pointA.position;  //Switch for point a
            _animator.SetTrigger("Idle");
        }
        if (!isHit)
        {
            //Move towards the point assigned in the _currentTarget
            transform.position = Vector2.MoveTowards(transform.position, _currentTarget, speed * Time.deltaTime);

        }
        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);   //Calculates the distance from the object to the player
        if(distance>2.0f)            //if the distance is greate than 2
        {       
            _animator.SetBool("InCombat", false); // get object out of combat state - resetting the boolean isCombat to false 
            isHit = false;                        // reset isHit ( now it can move again)
        }

        Vector3 direction = player.transform.localPosition - transform.localPosition; //get distance vector from player to this object
     
        // if distance vector.x is negative the the player is on the left side otherwise it is on the right side
        if (direction.x > 0 && _animator.GetBool("InCombat") == true)
            _spriteRenderer.flipX = false; //faceRight
        else if (direction.x < 0 && _animator.GetBool("InCombat") == true)
            _spriteRenderer.flipX = true; //faceLeft

    }
    
    
}
