using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{


    //variable for speed
    //move down of the screen
    //return back to top into new random location within the bounds.
    [SerializeField]
    private float _speed = 12.0f;
    [SerializeField]
    private int _enemyHP = 1;

    [SerializeField]
    private GameObject _enemyExplosionPrefab;

    [SerializeField]
    private AudioClip _clip;
    private UIManager _uiManager;
    private GameManager _gameManager;
    

   

    // Use this for initialization
    void Start ()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
      
    }
	
	// Update is called once per frame
	void Update () {


        Move();
        if (_gameManager.gameOver == true)
        {
            Destroy(this.gameObject);
        }

	}
    
    private void Move()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y<  -5.9)
        {
            transform.position = new Vector3(Random.Range(-8.0f, 8.0f), 6.17f, 0);
        }
    }

    private void enemyHit()
    {
        _enemyHP = _enemyHP - 1;
        Debug.Log("enemy hp = " + _enemyHP);
        if (_enemyHP < 1)
        {
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position,1f);
            Instantiate(_enemyExplosionPrefab, transform.position,Quaternion.identity);  
            _uiManager.UpdateScore();
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" )
        {
            Player player = other.GetComponent<Player>();

            player.playerHit();
            
            enemyHit();
        }
        else if (other.tag == "Laser")
        {
        
                if (other.transform.parent != null)
                {
                    
                    Destroy(other.transform.parent.gameObject);
                }
                Destroy(other.gameObject);
                enemyHit();
            
        }
    }
}


