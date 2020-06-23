using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_up : MonoBehaviour {

    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupId;// 0-tripleshot ,1-speedboost, 2- shield

    [SerializeField]
    private AudioClip _clip;
    private GameManager _gameManager;

   


    
    // Use this for initialization
    void Start () {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }
        if (_gameManager.gameOver == true)
        {
            Destroy(this.gameObject);
        }
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with " + other.name);

        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            //access the player
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                if (powerupId == 0)
                {
                    //turn the triple shot bool true
                    player.TripleShotPowerupON();
                }else if (powerupId==1)
                {
                    //enable speedboost
                    player.SpeedBoostPowerupOn();
                }
                else if (powerupId == 2)
                {
                    //enable shield
                    player.ShieldBoostPowerupOn();
                }

            }

               //destroy powerup
            Destroy(this.gameObject, 0.01f);
   
         }
    

    }
}
