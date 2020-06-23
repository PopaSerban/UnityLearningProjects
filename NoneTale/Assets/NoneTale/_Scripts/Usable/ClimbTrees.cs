using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbTrees : MonoBehaviour,IUseable {
  

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Use()
    {
        //Debug.Log("Now you can climb you piece of shit");
        if (Player.Instance._onTree)
        {
            UseTree(false,1,0);
        }
        else
        {
            UseTree(true,0,1);

        }
    }

    private void UseTree(bool onLadder,int gravity, int layerWeight)
    {
        Player.Instance._onTree = onLadder;
        Player.Instance._rigidbody2D.gravityScale = gravity;
        Player.Instance._animator.SetLayerWeight(2, layerWeight);
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.tag == "Player")
        {
            UseTree(false, 1,0);
        }
    }
}
