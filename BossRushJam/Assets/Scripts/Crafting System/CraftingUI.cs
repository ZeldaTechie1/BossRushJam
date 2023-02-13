using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
{
    public GameObject CraftingGroup;
    public List<GameObject> Crafting;

    public GameObject ItemRecipeImage;

    public bool ShowItems;
    public Image CraftableItem;

    private CraftingSystem.CraftingRecipe _currentRecipe;

    // Start is called before the first frame update
    void Start()
    {
        CraftingSystem.Instance.ItemCrafted += SetItemCrafted;
    }

    public void Init(CraftingSystem.CraftingRecipe recipe)
    {
        foreach(ItemObject item in recipe.Items)
        {
            GameObject a = Instantiate(ItemRecipeImage, CraftingGroup.transform);
            a.transform.GetChild(0).GetComponent<Image>().sprite = item.ItemTexture;
            a.GetComponent<CraftingBox>().ToggleDim(true);
            CraftableItem.fillAmount = 0;
            a.SetActive(false);
            Crafting.Add(a);
        }

        ShowItems = false;
        _currentRecipe = recipe;
    }

    public void UpdateProgress(float progress)
    {
        CraftableItem.fillAmount = progress;
        if(progress >= 1)
        {
            //Blink or something idk
            //signifies that it can be crafted
        }
    }

    public void ShowCraftingItems(bool show)
    {
        for (int i = 0; i <  Crafting.Count; i++)
        {
            Crafting[i].SetActive(show);
        }
        if(!show)
        {
            GetComponent<Image>().color = Color.gray;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
        }
    }

    public void SetItemCrafted(CraftingSystem.CraftingRecipe recipe)
    {
        if(recipe == _currentRecipe)
        {
            GetComponent<Outline>().enabled = true;
        }
    }

    public void ItemBroke()
    {
        GetComponent<Outline>().enabled = false;
    }
}
