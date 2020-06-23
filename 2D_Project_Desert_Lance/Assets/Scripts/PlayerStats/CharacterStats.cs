using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


[Serializable]
public class CharacterStats 
{
    public float baseValue;

    public virtual float Value{
        get{
            if(isDirty || baseValue != lastBaseValue){
                lastBaseValue = baseValue;
                _value = CalculateFinalValue();
                isDirty = false;
            }
            return _value;
        }
    }
    protected bool isDirty = true;
    protected float _value;
    protected float lastBaseValue = float.MinValue;

    protected readonly List<StatModifier> statModifiers;
    public readonly ReadOnlyCollection<StatModifier> StatModifiers;


    public CharacterStats()
    {
        statModifiers = new List<StatModifier>();
        StatModifiers = statModifiers.AsReadOnly();  
    }

    //Constructor
    public CharacterStats(float _baseValue): this()
    {
        baseValue = _baseValue;
    }
   
    //Add/remove modifiers methods
    public virtual void AddModifier(StatModifier modifier)
    {
        isDirty = true;
        statModifiers.Add(modifier);
        statModifiers.Sort(CompareModifiersOrder);
    }

    public virtual bool RemoveModifier(StatModifier modifier)
    {
        if( statModifiers.Remove(modifier))
        {
            isDirty = true;
            return true;
        }
        return false;
    }

     //Comparison for modifiers priority( based on enum order)
    protected virtual int CompareModifiersOrder(StatModifier a, StatModifier b)
    {
        if(a.order < b.order)
            return -1;
        else if(a.order > b.order)
            return 1;
        return 0; //if(a.order == b.order)
    }

    public virtual bool RemoveAllModifiersFromSource(object Source)
    {
        bool didRemove = false;

        for (int i = statModifiers.Count -1; i >= 0; i--)
        {
            if(statModifiers[i].source == Source)
            {
                isDirty = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }

    protected virtual float CalculateFinalValue()
    {
        float finalValue = baseValue;
        float sumPercentageAdd = 0;

        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];

            if(mod.statModifierType == StatModifierType.Flat)
            {
                finalValue += mod.value;
            }
            else if(mod.statModifierType == StatModifierType.PercentAdd)
            {
                sumPercentageAdd += mod.value;
                if( i+1 > statModifiers.Count || statModifiers[i+1].statModifierType != StatModifierType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentageAdd;
                    sumPercentageAdd = 0;
                }
            }
            else if(mod.statModifierType == StatModifierType.PercentMult)
            {
                finalValue *= 1 + mod.value;
            }

        }
        return (float)Math.Round(finalValue,4);
    }
}
