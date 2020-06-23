using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatsType{ FireResistance, AirResistance, WaterResistance, EarthResistance }


[CreateAssetMenu(menuName ="Abillities/Passive Abillity")]
public class PassiveAbillity : Abillity
{

    public StatsType[] statsType;
    public float PassiveBuffAmount;

    //StringBuilder stringBuilder = new StringBuilder();

    public void ApplyPassiveAbillity( Rune parentRune, CharacterUIManager character)
    {
        StatModifier statModifier = new StatModifier(PassiveBuffAmount,StatModifierType.PercentMult,this);

        //ApplyBuff to all stats;
        foreach (StatsType statType in statsType)
        {
            switch(statType)
            {
                case StatsType.FireResistance:
                character.FireResistance.AddModifier(statModifier);
                break;
            case StatsType.AirResistance:
                character.AirResistance.AddModifier(statModifier);
                break;
            case StatsType.WaterResistance:
                character.WaterResistance.AddModifier(statModifier);
                break;
            case StatsType.EarthResistance:
                character.EarthResistance.AddModifier(statModifier);
                break;
            default:
                break;
            }
        }
        character.UpdateStatPannel();

    }

    public override string GetDescription()
    {
        stringBuilder.Length = 0;

        foreach (StatsType statType in statsType)
        {
            stringBuilder.Append("+" + PassiveBuffAmount + "% " + statsType.ToString());
            stringBuilder.AppendLine();
        }
        return stringBuilder.ToString();
    }
    


    

}
