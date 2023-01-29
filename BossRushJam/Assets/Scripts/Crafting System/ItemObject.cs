using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "CraftingSystem/Item", order = 1)]
public class ItemObject : ScriptableObject
{
    public Sprite ItemTexture;
    public string Name;
    //...
}
