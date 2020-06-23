using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool _canDamage = true; // boolean for triggering Damage function once per swing


    //Function called when hitBox collider touched another collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit" + collision.name);
        //get the interface of type IDamageable from the object hit
        IDamageable hit = collision.GetComponent<IDamageable>();

        //If hit is not null, then the object hit does implement IDamageable
        if (hit != null)
        {
            if (_canDamage)  //if the cooldown is true
            {
                hit.Damage();                   // call the Damage function of the object hit 
                _canDamage = false;             //set the cooldown 
                StartCoroutine(ResetDamage()); //call the cooldown Timer
            }

        }
    }

    IEnumerator ResetDamage() //Cooldown Timer Coroutine
    {
        yield return new WaitForSeconds(0.5f); //wait 0.5 seconds
        _canDamage = true;  //reset the cooldown to true
    }
}
