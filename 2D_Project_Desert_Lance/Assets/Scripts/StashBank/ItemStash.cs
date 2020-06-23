using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStash : ItemContainer
{
    
   private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //UIManager.Instance.CraftingWindow.CraftingRecipes = _craftingRecepies;
            UIManager.Instance.ToggleCraftingInterface(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //UIManager.Instance.CraftingWindow.CraftingRecipes = new List<CraftingRecipe>();
            UIManager.Instance.ToggleCraftingInterface(false);
        }
    }
}
