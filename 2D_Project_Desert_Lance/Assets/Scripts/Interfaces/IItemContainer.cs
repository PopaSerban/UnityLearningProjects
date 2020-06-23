
public interface IItemContainer 
{
    int ItemCount(string itemID);
    bool CanAddItem(Item item, int amount = 1);
    Item RemoveItem(string itemID);
    bool RemoveItem(Item item);
    bool AddItem(Item item);
    void Clear();
}
