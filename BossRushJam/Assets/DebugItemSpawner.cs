using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugItemSpawner : MonoBehaviour
{

    public List<ItemObject> ItemObjects;

    public GameObject DroppableItems;

    public float ItemSpawnRate;

    // Start is called before the first frame update
    void Start()
    {
        DOTween.Sequence().AppendInterval(ItemSpawnRate).AppendCallback(() =>
        {
            GameObject a = Instantiate(DroppableItems, transform.position, transform.rotation);
            a.transform.GetChild(0).GetComponent<DroppableItem>().Init(ItemObjects[Random.Range(0, ItemObjects.Count)]);
        }).SetLoops(-1);
    }
}
