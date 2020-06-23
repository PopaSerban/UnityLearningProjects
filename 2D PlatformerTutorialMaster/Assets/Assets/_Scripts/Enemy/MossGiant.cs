using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy,IDamageable
{
    public int Health { get; set; }



    public override void Init()
    {
        base.Init();
        Health = base.health;
    }
    public override void WaypointsMovement()
    {
        base.WaypointsMovement();
      
    }

    public void Damage()
    {//<<<<<<Damage function for skeleton>>>>>>

        Debug.Log("Damage()");        //debugger 
        Health -= 1;                  //substract life
        _animator.SetTrigger("Hit"); //call the hit animation
        isHit = true;                //set isHit to true
        _animator.SetBool("InCombat", true); //set the animator combat boolean to true

        if (Health < 1)   //if health is 0 or less
        {
            // Destroy(this.gameObject); // destroy this object 
            _animator.SetTrigger("death"); //play death animation
            isDead = true;
        }
    }
}
