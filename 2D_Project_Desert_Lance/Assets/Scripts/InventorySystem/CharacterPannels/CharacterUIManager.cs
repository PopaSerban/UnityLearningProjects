using UnityEngine;
using UnityEngine.UI;

public class CharacterUIManager : MonoBehaviour
{
    public int Health = 50;

    public CharacterStats FireResistance;
    public CharacterStats WaterResistance;
    public CharacterStats AirResistance;
    public CharacterStats EarthResistance;

    [SerializeField]  Inventory inventory;
    [SerializeField]  EquipmentPannel equipmentPannel;
    [SerializeField]  CraftingWindow craftingWindow;
    [HideInInspector] public CraftingWindow _craftingWindow{ get{ return craftingWindow; } }
    [SerializeField]  StatPannel statPannel;
    [SerializeField]  ItemTooltip itemTooltip;
    [SerializeField]  Image draggableItem;
    [SerializeField]  DropItemArea dropItemArea; 
    [SerializeField]  QuestionDialog questionDialog;
    private BaseItemSlot dragItemSlot; 

    private void OnValidate()
    {
        if(itemTooltip == null)
            itemTooltip = FindObjectOfType<ItemTooltip>();
    }

    private void Start()
    {
        if( itemTooltip == null)
        itemTooltip = FindObjectOfType<ItemTooltip>();
        statPannel.SetStats(FireResistance,WaterResistance, AirResistance, EarthResistance);
        statPannel.UpdateStatValues();

        //Setup Events:
        //RightClick Event
        inventory.OnRightClickEvent += InventoryRightClick;
        equipmentPannel.OnRightClickEvent += EquipmentPanelRightClick;
        //PointerEnter Event
        inventory.OnPointerEnterEvent += ShowTooltip;
        equipmentPannel.OnPointerEnterEvent += ShowTooltip;
        craftingWindow.OnPointerEnterEvent += ShowTooltip;
        //PointerExit Event
        inventory.OnPointerExitEvent += HideTooltip;
        equipmentPannel.OnPointerExitEvent += HideTooltip;
        craftingWindow.OnPointerExitEvent += HideTooltip;
        //BeginDrag Event
        inventory.OnBeginDragEvent += BeginDrag;
        equipmentPannel.OnBeginDragEvent += BeginDrag;
        //EndDrag Event
        inventory.OnEndDragEvent += EndDrag;
        equipmentPannel.OnEndDragEvent += EndDrag;
        //Drag Event
        inventory.OnDragEvent += Drag;
        equipmentPannel.OnDragEvent += Drag;
        //Drop Event
        inventory.OnDropEvent += Drop;
        equipmentPannel.OnDropEvent += Drop;
        dropItemArea.OnDropEvent += DropItemOutsideUI;

    }

    private void InventoryRightClick( BaseItemSlot itemSlot)
    {
        if(itemSlot.Item is EquippableItem)
        {
            Equip((EquippableItem)itemSlot.Item);
        }
        else if(itemSlot.Item is UsableItem)
        {
            UsableItem usableItem = (UsableItem)itemSlot.Item;
            usableItem.Use(this);

            if(usableItem.isConsumable)
            {
                inventory.RemoveItem(usableItem);
                usableItem.Destroy();
            }
        }
    }
    private void EquipmentPanelRightClick( BaseItemSlot itemSlot)
    {
        if(itemSlot.Item is EquippableItem)
        {
            Unequip((EquippableItem)itemSlot.Item);
        }
    }
    private void ShowTooltip(BaseItemSlot itemSlot)
    {
        if(itemSlot.Item != null)
        {
            itemTooltip.ShowTooltip(itemSlot.Item);
        }
    }
    private void HideTooltip(BaseItemSlot itemSlot)
    {
        itemTooltip.HideTooltip();
    }
    private void BeginDrag(BaseItemSlot itemSlot)
    {
        if(itemSlot.Item != null)
        {
            dragItemSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.Icon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }
     private void EndDrag(BaseItemSlot itemSlot)
    {
        dragItemSlot = null;
        draggableItem.enabled = false;
    }
     private void Drag(BaseItemSlot itemSlot)
    {
       // if(draggableItem.enabled)
        draggableItem.transform.position = Input.mousePosition;
        
    }
     private void Drop(BaseItemSlot dropItemSlot)
    {
        if(dragItemSlot == null) return;
        if(dropItemSlot.CanRecieveItem(dragItemSlot.Item) && dragItemSlot.CanRecieveItem(dropItemSlot.Item))
        {
            EquippableItem dragItem = dragItemSlot.Item as EquippableItem;
            EquippableItem dropItem = dropItemSlot.Item as EquippableItem;

            if(dragItemSlot is EquipmentSlots)
            {
                 if(dragItem != null) dragItem.Unequip(this);
                 if(dropItem != null) dropItem.Equip(this);
            }
            if(dropItemSlot is EquipmentSlots)
            {
                if(dragItem != null) dragItem.Equip(this);
                 if(dropItem != null) dropItem.Unequip(this);
            }
            statPannel.UpdateStatValues();

            Item draggedItem = dragItemSlot.Item;
            int draggedItemAmount = dragItemSlot.Amount;

            dragItemSlot.Item = dropItemSlot.Item;
            dragItemSlot.Amount = dropItemSlot.Amount;

            dropItemSlot.Item = draggedItem;
            dropItemSlot.Amount = draggedItemAmount;
        }
        
    }

   private void DropItemOutsideUI()
    {
        if(dragItemSlot == null) return;
        
        questionDialog.Show();
        BaseItemSlot baseItemSlot = dragItemSlot;
        questionDialog.OnYesEvent += () => DestroyItemInSlot(baseItemSlot);
    }

    private void DestroyItemInSlot(BaseItemSlot baseItemSlot)
    {
        baseItemSlot.Item.Destroy();
        baseItemSlot.Item = null;
    }


    public void Equip( EquippableItem item)
    {
        if(inventory.RemoveItem(item))
        {
             EquippableItem previousItem= null;
            if(equipmentPannel.AddItem((EquippableItem)item, out previousItem))
            {
                if(previousItem != null)
                {
                    inventory.AddItem(previousItem);
                    previousItem.Unequip(this);
                    statPannel.UpdateStatValues();
                }
                item.Equip(this);
                statPannel.UpdateStatValues();
            }
            else{
                inventory.AddItem(item);
            }
        }
    }

    public void Unequip( EquippableItem item)
    {
        if(inventory.CanAddItem(item) && equipmentPannel.RemoveItem(item))
        {
            item.Unequip(this);
            statPannel.UpdateStatValues();
            inventory.AddItem(item.GetCopy());
        }
    }

    public void UpdateStatPannel()
    {
        statPannel.UpdateStatValues();
    }

}
