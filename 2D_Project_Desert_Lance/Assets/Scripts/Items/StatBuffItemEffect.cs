using System.Collections;
using UnityEngine;

public enum StatType{ FireResistance, AirResistance, WaterResistance, EarthResistance }

[CreateAssetMenu(menuName = "ItemEffects/StatBuff Effect")]
public class StatBuffItemEffect : UsableItemEffect
{
    public StatType statType;
    public int BuffAmount;
    public float Duration;

    public override void ExecuteEffect(UsableItem parentItem, CharacterUIManager character)
    {
        StatModifier statModifier = new StatModifier(BuffAmount, StatModifierType.Flat, this);
        switch (statType)
        {
            case StatType.FireResistance:
                character.FireResistance.AddModifier(statModifier);
                break;
            case StatType.AirResistance:
                character.AirResistance.AddModifier(statModifier);
                break;
            case StatType.WaterResistance:
                character.WaterResistance.AddModifier(statModifier);
                break;
            case StatType.EarthResistance:
                character.EarthResistance.AddModifier(statModifier);
                break;
            default:
                break;
        }
        character.StartCoroutine(RemoveBuff(character, statType, statModifier, Duration));
        character.UpdateStatPannel();
    }

    public override string GetDescription()
    {
        return "+" + BuffAmount + " " + statType.ToString() + " for " + Duration + " seconds.";
    }

    private static IEnumerator RemoveBuff(CharacterUIManager character,StatType statType, StatModifier statModifier, float duration)
    {
        yield return new WaitForSeconds(duration);
        switch (statType)
        {
            case StatType.FireResistance:
                character.FireResistance.RemoveModifier(statModifier);
                break;
            case StatType.AirResistance:
                character.AirResistance.RemoveModifier(statModifier);
                break;
            case StatType.WaterResistance:
                character.WaterResistance.RemoveModifier(statModifier);
                break;
            case StatType.EarthResistance:
                character.EarthResistance.RemoveModifier(statModifier);
                break;
            default:
                break;
        }
        character.UpdateStatPannel();
        
    }
}
