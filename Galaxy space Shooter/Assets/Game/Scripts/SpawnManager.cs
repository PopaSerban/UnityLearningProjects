using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {




    [SerializeField]
    private GameObject _enemyShipPrefab;

    [SerializeField]
    private GameObject[] _powerupsPrefabs;

    [SerializeField]
    private GameObject _playerPrefab;

    //[SerializeField]
     private GameManager _gameManager;

    //[SerializeField]
    //private float _spawnRate=2.5f;
    
  


	// Use this for initialization
	void Start ()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        StartCoroutine(spawnEnemyCoroutine());
        StartCoroutine(spawnPowerUpsCoroutine());
        //_gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
      
	}

    public void StartSpawnRoutines()
    {
        StartCoroutine(spawnEnemyCoroutine());
        StartCoroutine(spawnPowerUpsCoroutine());
    }
    



    //create a coroutine to spawn the enemy every 5 seconds;
    IEnumerator spawnEnemyCoroutine()
    {
        Player player = _playerPrefab.GetComponent<Player>();
        
        while (_gameManager.gameOver == false)
        {
            Instantiate(_enemyShipPrefab, new Vector3(Random.Range(-8.0f, 8.0f), 6.17f, 0), Quaternion.identity);
            
            yield return new WaitForSeconds(_spawnRate);
            

        }
        

    }



    IEnumerator spawnPowerUpsCoroutine()
    {
        Player player = _playerPrefab.GetComponent<Player>();

        while (_gameManager.gameOver == false)
        {

            int randomPowerup = Random.Range(0, 3);
            Instantiate(_powerupsPrefabs[randomPowerup], new Vector3(Random.Range(-8.0f, 8.0f), 6.17f, 0), Quaternion.identity);

            yield return new WaitForSeconds(18f);
        }


    }



}

