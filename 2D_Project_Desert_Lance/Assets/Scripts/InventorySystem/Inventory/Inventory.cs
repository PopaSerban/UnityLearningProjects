
using System.Collections.Generic;
using UnityEngine;

public class Inventory : ItemContainer, IItemContainer
{
   [SerializeField] protected List<Item> startingItems;
   [SerializeField] protected Transform itemsParent;
    public GameObject InventoryActivator;
    public GameObject EquipmentAndStats;


    protected override void Start()
    {
        base.Start();
        SetStartingItems();
    }

    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    protected override void OnValidate()
    {
        if(itemsParent != null)
            itemsParent.GetComponentsInChildren<ItemSlot>(includeInactive: true, result: itemSlots);
        
        SetStartingItems();

    }

    //Match the inventory items with the ui display
    private void SetStartingItems()
    {
        Clear();
        for( int i=0; i < startingItems.Count; i++)
        {
            AddItem(startingItems[i].GetCopy());
        }
    }

    public void ToggleInventory(bool value)
    {
        InventoryActivator.gameObject.SetActive(value);

        if(UIManager.Instance.IsCraftingWindowActive()){
            EquipmentAndStats.gameObject.SetActive(false);
        }
        else{
            EquipmentAndStats.gameObject.SetActive(value);
        }
    }

    public void ToggleCrafting(bool value )
    {
        InventoryActivator.gameObject.SetActive(value);
        EquipmentAndStats.gameObject.SetActive(!value);
    }
    public void ToggleInventoryAndStats(bool value)
    {
        InventoryActivator.gameObject.SetActive(value);
        EquipmentAndStats.gameObject.SetActive(value);
    }

    public bool IsEquipmentAndStatsActive()
    {
        return EquipmentAndStats.gameObject.activeSelf;
    }

}
