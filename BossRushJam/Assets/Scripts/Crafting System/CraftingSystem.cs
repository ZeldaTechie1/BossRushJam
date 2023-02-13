using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CraftingSystem : Core.Singleton<CraftingSystem>
{
    [System.Serializable]
    public class CraftingRecipe
    {
        public string Name;
        public bool Throwable;
        public bool Wearable;
        public float Durability;
        public List<ItemObject> Items;
        [HideInInspector]
        public bool Craftable { get { return _craftable; }  set { _craftable = value; } }
        bool _craftable;
        [HideInInspector]
        public bool Crafted { get { return _crafted; } set { _crafted = value; } }
        bool _crafted;
    }

    public Action<CraftingRecipe> ItemCrafted;

    [SerializeField]
    public List<CraftingRecipe> CraftingRecipes;
    public List<CraftingUI> CraftingUIs;

    public Dictionary<ItemObject, int> Inventory;

    public bool AutoCrafting;

    public InputActionReference CraftingSelectUp;
    public InputActionReference CraftingSelectDown;
    public InputActionReference CraftingButton;

    public int ItemSelected = 0;

    // Start is called before the first frame update
    void Start()
    {

        Inventory = new Dictionary<ItemObject, int>();

        CraftingSelectDown.action.performed += SelectItemDown;
        CraftingSelectUp.action.performed += SelectItemUp;
        CraftingButton.action.performed += Craft;

        for (int i = 0; i < CraftingRecipes.Count; i++)
        {
            CraftingUIs[i].Init(CraftingRecipes[i]);
            CraftingRecipes[i].Craftable = false;
            CraftingRecipes[i].Crafted = false;
        }

        CraftingUIs[ItemSelected].ShowCraftingItems(true);
    }

    public void EnableControls()
    {
        CraftingSelectUp.action.Enable();
        CraftingSelectDown.action.Enable();
        CraftingButton.action.Enable();
    }

    public void DisableControls()
    {
        CraftingSelectUp.action.Disable();
        CraftingSelectDown.action.Disable();
        CraftingButton.action.Disable();
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
        UpdateRecipes();
    }

    public void UpdateRecipes()
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
                else
                {
                    CraftingUIs[i].Crafting[j].GetComponent<CraftingBox>().ToggleDim(true);
                }
            }
            CraftingUIs[i].UpdateProgress((float)itemsHave / CraftingRecipes[i].Items.Count);

            if(itemsHave == CraftingRecipes[i].Items.Count)
            {
                CraftingRecipes[i].Craftable = true;
                if (AutoCrafting)
                {
                    //Craft
                    CraftItem(CraftingRecipes[i]);
                }
            }
        }
    }

    void SelectItemUp(InputAction.CallbackContext callback)
    {
        CraftingUIs[ItemSelected].ShowCraftingItems(false);
        ItemSelected = (int)Mathf.Repeat(ItemSelected - 1, CraftingUIs.Count);
        CraftingUIs[ItemSelected].ShowCraftingItems(true);
    }

    void SelectItemDown(InputAction.CallbackContext callback)
    {
        CraftingUIs[ItemSelected].ShowCraftingItems(false);
        ItemSelected = (int)Mathf.Repeat(ItemSelected + 1, CraftingUIs.Count);
        CraftingUIs[ItemSelected].ShowCraftingItems(true);
    }

    void Craft(InputAction.CallbackContext callback)
    {
        CraftItem(CraftingRecipes[ItemSelected]);
    }

    void CraftItem(CraftingRecipe recipe)
    {
        if(recipe.Craftable && !recipe.Throwable)
        {
            foreach (ItemObject item in recipe.Items)
            {
                if (Inventory.ContainsKey(item) && Inventory[item] > 0)
                {
                    Inventory[item]--;
                    //undim Items
                }
            }

            recipe.Craftable = false;
            recipe.Crafted = true;

            UpdateRecipes();

            ItemCrafted?.Invoke(recipe);
        }
    }
    public void RemoveDurability(int durabilityLoss)
    {
        CraftingRecipes[ItemSelected].Durability -= durabilityLoss;
        if (CraftingRecipes[ItemSelected].Durability <= 0 && ItemSelected != 0)
        {
            CraftingRecipes[ItemSelected].Crafted = false;
        }
    }
}
