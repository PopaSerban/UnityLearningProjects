using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_HungerPannel : MonoBehaviour
{
    [SerializeField] private HungerBar _hungerBar;


    public void ToggleHungerMeter(bool value)
    {
        _hungerBar.gameObject.SetActive(value);
    }
    public void StopTimerHunger()
    {
        _hungerBar.StopDecreaseSystem();
    }
    public void StartTimerHunger()
    {
        _hungerBar.StartDecreaseSystem();
    }

    public void ReplenishHunger(int value)
    {
        _hungerBar.ReplenishHunger(value);
    }
}
