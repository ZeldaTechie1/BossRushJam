using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingBox : MonoBehaviour
{
    public ItemObject ItemFramed;

    public Color DimmedColor;

    public void ToggleDim(bool isDimmed)
    {
        if (isDimmed)
        {
            gameObject.transform.GetChild(0).GetComponent<Image>().color = DimmedColor;
        }
        else
        {
            gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.white;
        }
    }
}
