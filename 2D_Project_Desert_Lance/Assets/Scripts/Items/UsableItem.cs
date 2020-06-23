using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Usable Item")]
public class UsableItem : Item
{
    public bool isConsumable;
    public List<UsableItemEffect> itemEffects;


    public virtual void Use(CharacterUIManager characterUIManager)
    {
        foreach (UsableItemEffect effect in itemEffects)
        {
            effect.ExecuteEffect(this, characterUIManager);
        }
    }

    public override string GetItemType()
    {
        return isConsumable ? "Consumable" : "Usable";
    }

    public override string GetDescription()
    {
        stringBuilder.Length = 0;

        foreach (UsableItemEffect effect in itemEffects)
        {
            stringBuilder.AppendLine(effect.GetDescription());
        }
        return stringBuilder.ToString();
    }
}
