using UnityEngine;

public abstract class UsableItemEffect : ScriptableObject
{
   public abstract void ExecuteEffect(UsableItem parentItem, CharacterUIManager character);
   public abstract string GetDescription();
}
