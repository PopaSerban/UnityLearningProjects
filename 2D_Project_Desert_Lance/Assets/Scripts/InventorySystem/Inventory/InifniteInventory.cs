using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InifniteInventory : Inventory
{
    [SerializeField] GameObject itemSlotPrefab; 

    [SerializeField] int maxSlots;
    public int MaxSlots{
        get{ return maxSlots;}
        set{ SetMaxSlots(value);}
    }

    protected override void Start()
    {
        SetMaxSlots(maxSlots);
        base.Start();
    }

    private void SetMaxSlots(int value)
    {
        if( value <= 0){
            maxSlots = 1;
        }else{
            maxSlots =value;
        }

        if( maxSlots < itemSlots.Count)
        {
            for (int i = maxSlots; i < itemSlots.Count; i++)
            {
                Destroy(itemSlots[i].transform.parent.gameObject);
            }
            int diff = itemSlots.Count - maxSlots;
            itemSlots.RemoveRange(maxSlots, diff);
            
        }else if(maxSlots > itemSlots.Count)
        {
            int diff = maxSlots -itemSlots.Count;

            for(int i = 0; i < diff; i++)
            {
                GameObject itemSlotGameObj = Instantiate(itemSlotPrefab);
                itemSlotGameObj.transform.SetParent(itemsParent, worldPositionStays: false);
                itemSlots.Add(itemSlotGameObj.GetComponentInChildren<ItemSlot>());
            }
        }

    }
}
