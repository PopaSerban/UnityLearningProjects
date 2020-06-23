using UnityEngine;

public class WorldItem : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] SpriteRenderer sprite;

    private void Start() 
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = item.Icon;
    }


    void OnValidate()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = item.Icon;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if(other.gameObject.CompareTag("Player"))
      {
          Inventory.Instance.AddItem(item.GetCopy());
          Destroy(this.gameObject);
      }   
    }


}
