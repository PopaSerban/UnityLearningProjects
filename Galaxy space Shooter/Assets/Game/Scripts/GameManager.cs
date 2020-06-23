using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private UIManager _uiManager;

    [SerializeField]
    private SpawnManager _spwnManager;

    [SerializeField]
    private Player _player;

    public bool gameOver = true;
    private bool gameStarted = false;
	// Use this for initialization
	void Start ()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        

    }
	
	// Update is called once per frame
	void Update ()
    {
        Begin();

        if (GameObject.Find("Player(Clone)") == null)
        {
            
            EndGame();
        }
    }
        

    private void Begin()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gameOver==true)
        {
          
            
            Instantiate(_player);
            gameOver = false;
            gameStarted = true;
            _uiManager.ShowTitleScreen();
          //  Instantiate(_spwnManager);
           // _spwnManager = GameObject.Find("Spawn_Manager(Clone)").GetComponent<SpawnManager>();
            
        }
        else
        {
            return;
        }
    }

    private void EndGame()
    {
        
        if(gameStarted==true)
        {
            if (_spwnManager != null)
            {
               // _spwnManager.stopManager();
                _uiManager.HideTitleScreen();
                gameOver = true;
            }
            
        }
        else
        {
            return;
        }
        
        

    }

    


}
