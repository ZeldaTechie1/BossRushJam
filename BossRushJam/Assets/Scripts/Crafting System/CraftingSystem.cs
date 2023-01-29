using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CraftingSystem : Core.Singleton<CraftingSystem>
{
    [System.Serializable]
    public struct CraftingRecipe
    {
        public string Name;
        public List<ItemObject> Items;
    }

    [SerializeField]
    public List<CraftingRecipe> CraftingRecipes;
    public List<CraftingUI> CraftingUIs;

    public Dictionary<ItemObject, int> Inventory;

    public bool AutoCrafting;

    public InputActionReference CraftingSelectUp;
    public InputActionReference CraftingSelectDown;

    int _itemSelected = 0;

    // Start is called before the first frame update
    void Start()
    {

        Inventory = new Dictionary<ItemObject, int>();

        CraftingSelectDown.action.performed += SelectItemDown;
        CraftingSelectUp.action.performed += SelectItemUp;

        for (int i = 0; i < CraftingRecipes.Count; i++)
        {
            CraftingUIs[i].Init(CraftingRecipes[i]);
        }

        CraftingUIs[_itemSelected].ShowCraftingItems(true);
    }

    public void EnableControls()
    {
        CraftingSelectUp.action.Enable();
        CraftingSelectDown.action.Enable();
    }

    public void DisableControls()
    {
        CraftingSelectUp.action.Disable();
        CraftingSelectDown.action.Disable();
    }

    public void ItemPickedUp(ItemObject item)
    {
        if (Inventory.ContainsKey(item))
        {
            Inventory[item]++;
        }
        else
        {
            Inventory.Add(item, 1);
        }
        UpdateRecepies();
    }

    public void UpdateRecepies()
    {
        for (int i = 0; i < CraftingRecipes.Count; i++)
        {
            int itemsHave = 0;
            for(int j = 0; j < CraftingRecipes[i].Items.Count; j++)
            {
                if (Inventory.ContainsKey(CraftingRecipes[i].Items[j]) && Inventory[CraftingRecipes[i].Items[j]] > 0)
                {
                    itemsHave++;
                    CraftingUIs[i].Crafting[j].GetComponent<CraftingBox>().ToggleDim(false);
                }
            }
            CraftingUIs[i].UpdateProgress((float)itemsHave / CraftingRecipes[i].Items.Count);

            if(itemsHave == CraftingRecipes[i].Items.Count && AutoCrafting)
            {
                //Craft
            }
        }
    }

    void SelectItemUp(InputAction.CallbackContext callback)
    {
        CraftingUIs[_itemSelected].ShowCraftingItems(false);
        _itemSelected = (int)Mathf.Repeat(_itemSelected - 1, CraftingUIs.Count);
        CraftingUIs[_itemSelected].ShowCraftingItems(true);
    }

    void SelectItemDown(InputAction.CallbackContext callback)
    {
        CraftingUIs[_itemSelected].ShowCraftingItems(false);
        _itemSelected = (int)Mathf.Repeat(_itemSelected + 1, CraftingUIs.Count);
        CraftingUIs[_itemSelected].ShowCraftingItems(true);
    }


}
