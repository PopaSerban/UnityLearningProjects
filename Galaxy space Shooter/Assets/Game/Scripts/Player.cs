using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{ 
    //public or private identify
    //option value assigned
    [SerializeField]
    private float _speed = 10.0f;
    //private float _initialSpeed=0;


    //game object variable
    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _animation;

    [SerializeField]
    private GameObject _shieldGameObject;

    [SerializeField]
    private GameObject[] _engines;

    [SerializeField]
    private int _playerHP = 3;


    private UIManager _uIManager;
    private AudioSource _audioSource;
    private SpawnManager _spawnManager;
   
    

    //fireRate is 0.15f
    //canFire-- has the amount of time between firing passed ?
    //Time.time
    [SerializeField]
    private float _firerate = 0.15f;
    private float _canFire = 0.0f;
    

    //check powerups
    public bool canTripleShot;
    public bool speedBoost;
    public bool isShield;
    public bool isAlive = true;


    private int _hitcount = 0;



    // Use this for initialization
    void Start ()
    {
        
        Debug.Log(" x pos : " + transform.position.x);

        transform.position = new Vector3(0, 0, 0);

        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uIManager != null)
        {
            _uIManager.UpdateLives(_playerHP);
        }

        _audioSource = GetComponent<AudioSource>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _spawnManager.StartSpawnRoutines();

        _hitcount = 0;


    }
	


	// Update is called once per frame
	void Update ()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            Fire();
        }

        //if (isShield == true)
        //{
        //    shield.transform.position = transform.position;
        //}

       


    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        
        transform.Translate(new Vector3(0, 1, 0) * _speed * verticalInput * Time.deltaTime);
        transform.Translate(new Vector3(1, 0, 0) * _speed * horizontalInput * Time.deltaTime);


        if (transform.position.y > 4.14f)
        {
            transform.position = new Vector3(transform.position.x, 4.14f, 0);

        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }



        if (transform.position.x > 9.26f)
        {
            transform.position = new Vector3(-9.26f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.26f)
        {
            transform.position = new Vector3(9.26f, transform.position.y, 0);
        }

        
    }

    private void Fire()
    {
        
         if (Time.time > _canFire)
            {
            _audioSource.Play();
               if (canTripleShot == true)
               {
                //tripleShot prefab
                Instantiate(_tripleShotPrefab, transform.position + new Vector3(-0.551f, 0.329f, 0), Quaternion.identity);
                
                }
            else
               {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.95f, 0), Quaternion.identity);
                }
            _canFire = Time.time + _firerate;
        }
        
    }

    public void playerHit()
    {
        if (isShield == true)
        {
            isShield =false;
            _shieldGameObject.SetActive(false);
            return;
        }
        else
        {
            _hitcount++;
            if (_hitcount == 1)
            {
                //turn left engine_failure on
                _engines[0].SetActive(true);
            }else if (_hitcount == 2)
            {
                //turn right engine_failure on
                _engines[1].SetActive(true);
            }
            _playerHP = _playerHP - 1;
            _uIManager.UpdateLives(_playerHP);
            if (_playerHP < 1)
            {
                Instantiate(_animation, transform.position, Quaternion.identity);
                isAlive = false;

                Destroy(this.gameObject);
            }
        
        }
    }


    /// <summary>
    /// Power Ups methods
    /// </summary>
    //ship fires 3 bullets
    public void TripleShotPowerupON()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    //ship speed is increased
    public void SpeedBoostPowerupOn()
    {
        speedBoost = true;
        _speed = 2.0f * _speed;
        StartCoroutine(SpeedBoostPowerDownRoutine());
        Debug.Log("speed = " + _speed);
    }

    //Shield - prevent damage 

    public void ShieldBoostPowerupOn()
    {
        isShield = true;
        _shieldGameObject.SetActive(true);
        StartCoroutine(ShieldBoostPowerDownRoutine());
        Debug.Log("Shield Activated");

    }

    //Coroutine shieldBoost
    IEnumerator ShieldBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(10);

        
        isShield = false;
        _shieldGameObject.SetActive(false);
       
    }

    //Coroutine speedBoost
    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return  new WaitForSeconds(5.0f);
        speedBoost = false;
        _speed = _speed / 2.0f;
    }

    //Coroutine tripleShotBoost
     IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
        Debug.Log("speed = " + _speed);
    }
}
