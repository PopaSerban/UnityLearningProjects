using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingShopper : MonoBehaviour
{
     [SerializeField] private List<CraftingRecipe> _craftingRecepies;
    private bool _stillInRange;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.CraftingWindow.CraftingRecipes = _craftingRecepies;
            UIManager.Instance.ToggleCraftingInterface(true);
            GameManager.Instance.ToggleCrafting();

            UIManager.Instance.CraftingStillInRange(true);

        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.CraftingWindow.CraftingRecipes = new List<CraftingRecipe>();
            UIManager.Instance.ToggleCraftingInterface(false);
            GameManager.Instance.ToggleRunning();

            UIManager.Instance.CraftingStillInRange(false);
        }     
    }
}
