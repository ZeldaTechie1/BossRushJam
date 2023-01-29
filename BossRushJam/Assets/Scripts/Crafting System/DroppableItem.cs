using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(SpriteRenderer))]
public class DroppableItem : MonoBehaviour
{
    public ItemObject Item;

    public void Init(ItemObject item)
    {
        Item = item;
        GetComponent<SpriteRenderer>().sprite = Item.ItemTexture;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            CraftingSystem.Instance.ItemPickedUp(Item);
            Destroy(transform.parent.gameObject);
        }
    }
}
