using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerSystem
{
    private float hunger;
    private float hungerMax;

    public HungerSystem(float hungerMax)
    {
        this.hungerMax = hungerMax;
        hunger = hungerMax;
    }

    public float GetHunger()
    {
        return hunger;
    }
    public float GetMaxHunger()
    {
        return hungerMax;
    }

    public void LowerRested( float lowAmount)
    {
        hunger -= lowAmount;
        if(hunger < 0)
            hunger = 0;
    }
    public float GetHungerPercent() 
    {   if(hunger / hungerMax < 0)
            return 0;
        return (float)hunger / hungerMax;
    }
    public void RaiseRested( float raiseAmount)
    {
        hunger += raiseAmount;
        if(hunger > hungerMax)
            hunger = hungerMax;
    }

    
}
