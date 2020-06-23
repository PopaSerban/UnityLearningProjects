using UnityEngine;

public enum EquipmentType
{
    Helmet,
    Chest,
    Shield,
    Weapon,
    Ring 
}

[CreateAssetMenu(menuName = "Items/Equippable Item")]
public class EquippableItem : Item
{
   public int FireResistanceBonus;
   public int WaterResistanceBonus;
   public int AirResistanceBonus;
   public int EarthResistanceBonus;
   [Space]
   public float FireResistancePercentBonus;
   public float WaterResistancePercentBonus;
   public float AirResistancePercentBonus;
   public float EarthResistancePercentBonus;
   [Space]
   public EquipmentType equipmentType;


   public override Item GetCopy()
   {
       return Instantiate(this);
   }
    public override void Destroy()
    {
        //Destroy(this);
    }

   public void Equip(CharacterUIManager c)
   {
       //FlatBonus
        if(FireResistanceBonus !=0)
            c.FireResistance.AddModifier(new StatModifier(FireResistanceBonus, StatModifierType.Flat, this));
        if(WaterResistanceBonus !=0)
            c.WaterResistance.AddModifier(new StatModifier(WaterResistanceBonus, StatModifierType.Flat, this));
        if(AirResistanceBonus !=0)
            c.AirResistance.AddModifier(new StatModifier(AirResistanceBonus, StatModifierType.Flat, this));
        if(FireResistanceBonus !=0)
            c.EarthResistance.AddModifier(new StatModifier(EarthResistanceBonus, StatModifierType.Flat, this));
        //PercentBonush
        if(FireResistancePercentBonus !=0)
            c.FireResistance.AddModifier(new StatModifier(FireResistancePercentBonus, StatModifierType.PercentMult, this));
        if(WaterResistancePercentBonus !=0)
            c.WaterResistance.AddModifier(new StatModifier(WaterResistancePercentBonus, StatModifierType.PercentMult, this));
        if(AirResistancePercentBonus !=0)
            c.AirResistance.AddModifier(new StatModifier(AirResistancePercentBonus, StatModifierType.PercentMult, this));
        if(EarthResistancePercentBonus !=0)
            c.EarthResistance.AddModifier(new StatModifier(EarthResistancePercentBonus, StatModifierType.PercentMult, this));
   }

   public void Unequip(CharacterUIManager c)
   {
       c.FireResistance.RemoveAllModifiersFromSource(this);
       c.WaterResistance.RemoveAllModifiersFromSource(this);
       c.AirResistance.RemoveAllModifiersFromSource(this);
       c.EarthResistance.RemoveAllModifiersFromSource(this);
   }

   public override string GetItemType()
   {
       return equipmentType.ToString();
   }
   public override string GetDescription()
   {
        stringBuilder.Length = 0;
        AddStat(FireResistanceBonus, "Fire Resistance.");
        AddStat(WaterResistanceBonus, "Water Resistance.");
        AddStat(AirResistanceBonus, "Air Resistance.");
        AddStat(EarthResistanceBonus, "Earth Resistance.");

        AddStat(FireResistancePercentBonus, "Fire Resistance.", isPercent: true);
        AddStat(WaterResistancePercentBonus, "Water Resistance.", isPercent: true);
        AddStat(AirResistancePercentBonus, "Air Resistance.", isPercent: true);
        AddStat(EarthResistancePercentBonus, "Earth Resistance.", isPercent: true);
        return stringBuilder.ToString();
   }

      public void AddStat(float value, string statName, bool isPercent = false)
    {
        if(value !=0)
        {
            if(stringBuilder.Length >0)
                stringBuilder.AppendLine();
            if(value > 0)
                stringBuilder.Append("+");

            if(isPercent)
            {
                stringBuilder.Append(value * 100);
                stringBuilder.Append("% ");
            }else
            {
                stringBuilder.Append(value);
                stringBuilder.Append(" ");
            }
            stringBuilder.Append(statName);
        }
        
    }

}
