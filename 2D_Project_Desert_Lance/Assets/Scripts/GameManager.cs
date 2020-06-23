using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;



public class GameManager : Singleton<GameManager>
{
   //Keep track of the game state

   // gameStates : PREGAME, RUNNING, PAUSED
   public enum GameState
   {
       PREGAME,
       RUNNING,
       INVENTORY,
       CRAFTING,
       PAUSED
   }

    //List of systems prefabs
   public GameObject[] SystemPrefabs;
   public Events.EventGameState OnGameStateChanged;
    // List of systems after initialization
   List<GameObject> _instancedSystemPrefabs;
   List<AsyncOperation> _loadOperations;

   GameState _currentGameState = GameState.PREGAME;

   private string _currentLevelName = string.Empty;
   public bool _startGameButton = true;

   public GameState CurrentGameState
   {
       get { return _currentGameState; }
       private set { _currentGameState = value; }
   }

   private void Start() 
   {
       DontDestroyOnLoad(gameObject);
       _instancedSystemPrefabs = new List<GameObject>();
       _loadOperations = new List<AsyncOperation>();

       InstantiateSystemPrefabs();

       UIManager.Instance.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
   }


   /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(_currentGameState == GameState.PREGAME)
        {
            return;
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
           TogglePause();
        }else if(Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }   
    }


    ///<summary>
    /// After async operation complete, remove the operation from pending complete list of async @_loadOperations
    //  Returns a log confirming the completions of operation
    ///</summary>
   void OnLoadOperationComplete(AsyncOperation asyncOp)
   {
       if(_loadOperations.Contains(asyncOp))
       {
           _loadOperations.Remove(asyncOp);
           if(_loadOperations.Count == 0)
           {
               UpdateGameState(GameState.RUNNING);
           }        
           //dispatch meesages or tranzitions between scenes
       }
       Debug.Log("Load Complete");

   }
   ///<summary>
   /// Returns a logs confirming the success unload of an async operation
   ///</summary>
   void OnUnloadOperationComplete(AsyncOperation asyncOp)
   {
       _startGameButton = true;
       Debug.Log("Unload Complete");
   }

   void HandleMainMenuFadeComplete( bool fadeOut)
   {
       if(!fadeOut)
        UnloadLevel(_currentLevelName);
   }

    ///<summary>
    ///Update the current game state of the game
    ///</summary>
   void UpdateGameState(GameState state)
   {
       GameState previousGameState = _currentGameState;
       _currentGameState = state;
       Debug.Log(_currentGameState);

       switch(_currentGameState)
       {
            case GameState.PREGAME:
                Time.timeScale = 1.0f;
                break;
            case GameState.RUNNING:
                Time.timeScale = 1.0f;
                break;
            case GameState.INVENTORY:
                Time.timeScale = 1.0f;
                break;
            case GameState.PAUSED:
                Time.timeScale = 0.0f;
                break;
            default:
                break;

       }
       OnGameStateChanged.Invoke(_currentGameState, previousGameState);
   }

    ///<summary>
    ///Instantiate all the systems from @SystemPrefabs list 
    ///Add the instances reffrences to @_instancedSystemPrefab List for controll 
    ///</summary>
   void InstantiateSystemPrefabs()
   {
       GameObject prefabInstance;
       for( int i=0; i< SystemPrefabs.Length; i++)
       {
           prefabInstance = Instantiate(SystemPrefabs[i]);
           _instancedSystemPrefabs.Add(prefabInstance);

       }
   }

   ///<summary>
   /// Loads specific level based on its name ( loads the level async and in additive mode)
   /// On completion calls the OnLoadOperationComplete
   /// set the current level name to the passed argument name
   ///</summary>
   public void LoadLevel(string levelName)
   {
       AsyncOperation asyncOp = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
       if( asyncOp == null)
        {
            Debug.LogError("[ Game Manager ] Unable to load level " + levelName);
            return;
        }
       asyncOp.completed +=OnLoadOperationComplete;
       _loadOperations.Add(asyncOp);
       _currentLevelName = levelName;


   }

    ///<summary>
    /// UnLoads specific level from scene based on its name (unloads async)
    /// On completion calls the OnUnloadOperationComplete
    ///</summary>
   public void UnloadLevel(string levelName)
   {
       AsyncOperation asyncOp =SceneManager.UnloadSceneAsync(levelName);
       if( asyncOp == null)
        {
            Debug.LogError("[ Game Manager ] Unable to unload level " + levelName);
            return;
        }
       asyncOp.completed +=OnUnloadOperationComplete;

   }
    ///<summary>
    /// Ovveride the base method of Singleton<T> Class
    /// Destroys all the instanced system available in the scene
    ///</summary>
    protected override void OnDestroy()
    {   
        base.OnDestroy();

        for (int i = 0; i < _instancedSystemPrefabs.Count; ++i)
        {
            Destroy(_instancedSystemPrefabs[i]);
        }
        _instancedSystemPrefabs.Clear();   
    }

    public void StartGame()
    {
        LoadLevel("Test_Functionalities_Scene");
    }

    public void TogglePause()
    {
        //conidtion ? true : false
        if(_currentGameState == GameState.INVENTORY || _currentGameState == GameState.CRAFTING)
        { 
            UpdateGameState(GameState.PAUSED);
        }else
        {
            UpdateGameState(_currentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING);
        }  
    }
    public void ToggleCrafting()
    {
        if(CurrentGameState ==GameState.INVENTORY || _currentGameState == GameState.RUNNING)
            UpdateGameState(GameState.CRAFTING);
    }
    public void ToggleRunning()
    {
        UpdateGameState(GameState.RUNNING);
    }

    public void ToggleInventory()
    {
        if(_currentGameState == GameState.PAUSED)
            return;
        if(_currentGameState == GameState.CRAFTING)
            UpdateGameState(GameState.CRAFTING);
        else
            UpdateGameState(_currentGameState == GameState.RUNNING ? GameState.INVENTORY :GameState.RUNNING);
    }

    public void RestartGame()
    {
        _startGameButton = false;
        UpdateGameState(GameState.PREGAME);
    }

    public void QuitGame()
    {
        //implement features for quitting
        Debug.LogError("Application closed");
        Application.Quit();
    }

}
