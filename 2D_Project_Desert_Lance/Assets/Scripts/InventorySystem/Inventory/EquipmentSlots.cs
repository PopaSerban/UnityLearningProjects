
public class EquipmentSlots : ItemSlot
{
    public EquipmentType equipmentType;

    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = equipmentType.ToString() + " Slot";

    }

    public override bool CanRecieveItem(Item item)
    {
        if(item == null)
            return true;
        
        EquippableItem equippableItem = item as EquippableItem;
        return equippableItem != null && equippableItem.equipmentType == equipmentType;
    }
}
