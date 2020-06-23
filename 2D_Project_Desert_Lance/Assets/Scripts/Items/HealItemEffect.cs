using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffects/Heal Effect")]
public class HealItemEffect : UsableItemEffect
{
    public int HealthAmount;

    public override void ExecuteEffect(UsableItem parentItem, CharacterUIManager characterUIManager)
    {
        characterUIManager.Health += HealthAmount;
    }

    public override string GetDescription()
    {
        return "Heals for " + HealthAmount + " health.";
    }
}
