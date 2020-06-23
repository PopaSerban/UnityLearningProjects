using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private HungryWarning _warningBox;

    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private Camera _dummyCamera;
    [SerializeField] private Health_HungerPannel _hungerMeter;
    [SerializeField] private Inventory _characterPannel;
    [SerializeField] private CraftingWindow _craftingWindow;
    [HideInInspector] public CraftingWindow CraftingWindow {
         get{ return _craftingWindow;}
         set{ _craftingWindow = value;}
    }
    [SerializeField]private bool _isStillInRange;
    [HideInInspector]public bool IsStillInRange{
        get{ return _isStillInRange;}
        set{ _isStillInRange = value;}
    }


    public Events.EventFadeComplete OnMainMenuFadeComplete;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        _mainMenu.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
    }

    void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        _pauseMenu.gameObject.SetActive(currentState == GameManager.GameState.PAUSED);
        _characterPannel.ToggleInventory(currentState == GameManager.GameState.INVENTORY||currentState == GameManager.GameState.CRAFTING);

        if(currentState == GameManager.GameState.PREGAME){
            _hungerMeter.ToggleHungerMeter(false);
            ToggleCraftingInterface(false);
            ToggleInventoryAndStats(false);
        }
        else if(currentState != GameManager.GameState.PREGAME && previousState == GameManager.GameState.PREGAME)
            _hungerMeter.ToggleHungerMeter(true);
            
        if(previousState == GameManager.GameState.CRAFTING && currentState == GameManager.GameState.PAUSED)
        {
            ToggleInventoryAndStats(false);

            _craftingWindow.gameObject.SetActive(false);
        }
        if(previousState == GameManager.GameState.PAUSED && currentState == GameManager.GameState.RUNNING)
        {
            if( _isStillInRange){
                ToggleCraftingInterface(true);
                GameManager.Instance.ToggleCrafting();
            }
        
        }


    }

    void HandleMainMenuFadeComplete(bool fadeOut)
    {
        OnMainMenuFadeComplete.Invoke(fadeOut);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(GameManager.Instance.CurrentGameState !=GameManager.GameState.PREGAME)
        {
            return;
        }
        if(Input.GetKeyDown(KeyCode.Space) && GameManager.Instance._startGameButton)
        {
            GameManager.Instance.StartGame();
            //_mainMenu.FadeOut();
        }   
    }
    
    public void SetDummyCameraActive(bool active)
    {
        _dummyCamera.gameObject.SetActive(active);
    }
    public void ToggleCraftingInterface(bool value)
    {
        _characterPannel.ToggleCrafting(value);
        _craftingWindow.gameObject.SetActive(value);
    }

    public void ToggleInventoryAndStats(bool value)
    {
        _characterPannel.ToggleInventoryAndStats(value);
    }

    public bool IsCraftingWindowActive()
    {  
        return _craftingWindow.IsWindowActive();
    }

    public void CraftingStillInRange(bool value)
    {
        _isStillInRange = value;
    }
     //============
     public void ToggleDialogueWarning()
     {
         _warningBox.ToggleDialogueBox();
     }
     public void ReplenishHunger(int value)
     {
         _hungerMeter.ReplenishHunger(value);
     }
     public void ToogleCloseDialogueWarning()
     {
         _warningBox.ToggleCloseDialogueBox();
     }
}
