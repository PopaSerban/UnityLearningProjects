using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
    [SerializeField]
    private float _speed = 17.2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //move 17.2f speed
        //move up
        Move();
	}



    public void Move()
    {

        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > 5.74f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            DestroyObject(this.gameObject);
        }
       
        
    }

    


}
