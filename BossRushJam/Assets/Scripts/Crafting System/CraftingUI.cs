using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
{
    public GameObject CraftingGroup;
    public List<GameObject> Crafting;

    public GameObject ItemRecipieImage;

    public bool ShowItems;
    public Image CrafatableItem;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init(CraftingSystem.CraftingRecipe recipe)
    {
        foreach(ItemObject item in recipe.Items)
        {
            GameObject a = Instantiate(ItemRecipieImage, CraftingGroup.transform);
            a.transform.GetChild(0).GetComponent<Image>().sprite = item.ItemTexture;
            a.GetComponent<CraftingBox>().ToggleDim(true);
            CrafatableItem.fillAmount = 0;
            a.SetActive(false);
            Crafting.Add(a);
        }

        ShowItems = false;
    }

    public void UpdateProgress(float progress)
    {
        CrafatableItem.fillAmount = progress;
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
    }
}
