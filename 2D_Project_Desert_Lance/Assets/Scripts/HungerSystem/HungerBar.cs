using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerBar : MonoBehaviour
{
    [SerializeField] private float decreaseAmount;
    public HungerSystem hungerSystem;
    public GameObject hungerBar;
    private float _starvedToDeath;


    public void Setup(HungerSystem hungerSystem)
    {
        this.hungerSystem = hungerSystem;
    }

    public void ReplenishHunger( float value)
    {
        if(value > hungerSystem.GetHunger())
            value = hungerSystem.GetMaxHunger();
        
        hungerSystem.RaiseRested(value);
    }

    public void HealthBarStatus()
    {
        hungerBar.transform.localScale = new Vector3(hungerSystem.GetHungerPercent(),1);
    }

    public void StartDecreaseSystem()
    {
        StartCoroutine(HungerDecreaser(decreaseAmount));

    }
    public void StopDecreaseSystem()
    {
        StopCoroutine(HungerDecreaser(decreaseAmount));
    }


    private bool IsStarvedToDeath()
    {
        float isStarved = hungerSystem.GetHungerPercent();
        if(isStarved > 0 && isStarved <0.15){
            UIManager.Instance.ToggleDialogueWarning();
            return false;
        }else if(isStarved > 0.15){
            return false;
        }else if( (isStarved>0 && isStarved <0.15) || isStarved > 0.15){
            
            UIManager.Instance.ToogleCloseDialogueWarning();
            return false;
        }
        else{
            return true;
        }
    }
    public void ToggleHungerMeter(bool value)
     {
         if(value){
             StopDecreaseSystem();
             Setup( new HungerSystem(100));
             this.gameObject.SetActive(!value);
         }
     }

     private void OnDisable()
     {
         StopDecreaseSystem();
         
     }
     private void OnEnable()
     {

         Setup( new HungerSystem(100));
         HealthBarStatus();
         StartDecreaseSystem();
         
     }

    private IEnumerator HungerDecreaser(float decreaseAmount)
    {
        yield return new WaitForSeconds(0.5f);
        hungerSystem.LowerRested(decreaseAmount);
        HealthBarStatus();
        if(IsStarvedToDeath())
        {
            StopDecreaseSystem();
            GameManager.Instance.RestartGame();
            Debug.Log("Starved to death");
        }else
        {
            StartCoroutine(HungerDecreaser(decreaseAmount));
        }
            
    }
}
