using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HungerReplenishType
{
    SMALL,
    MEDIUM,
    LARGE
}
[CreateAssetMenu(menuName = "ItemEffects/HungerBuff Effect")]
public class FoodItemEffect : UsableItemEffect
{
    [SerializeField] private HungerReplenishType _replenishType;
    [SerializeField] private int[] replenishVariationValues = new int[3];
    private int replenishAmount;


    public override void ExecuteEffect(UsableItem parentItem, CharacterUIManager character)
    {
        replenishAmount = GetReplenishAmount(_replenishType);

        UIManager.Instance.ReplenishHunger(replenishAmount);
        
    }
   public override  string GetDescription()
   {
       return "Restores a "+ _replenishType.ToString().ToLower() + " of your hunger meter.";
   }

   private int GetReplenishAmount( HungerReplenishType _replenishType)
   {
       int valueReplenish = 0;

       switch(_replenishType)
       {
            case HungerReplenishType.SMALL :
                valueReplenish = replenishVariationValues[0];
                break;

            case HungerReplenishType.MEDIUM :
                valueReplenish = replenishVariationValues[1];
                break;

            case HungerReplenishType.LARGE :
                valueReplenish = replenishVariationValues[2];
                break;

            default: 
                break;

       }
       return valueReplenish;
        
   }
}
