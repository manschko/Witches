using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private int popCount = 0;
    public int diviation = 3;

    private int nextDrop = 0;
    public ItemList ItemList;
    private List<Item> itemList; 
    private void Start()
    {
        itemList = new List<Item>(ItemList.AllItems);
        Pot.OnBubbleSpawned.AddListener(bubble => { 
            bubble.OnPopped.AddListener(() => { spawnItem(bubble); });
            tryToAddItemToBubble(bubble);
            bubble.OnDestroy.AddListener(()=>{returnItemToList(bubble);});
            });

    }

    private void returnItemToList(Bubble bubble)
    {
        if(!bubble.ContainedItem){
            return;
        }
        itemList.Add(bubble.ContainedItem);

    }

    private void tryToAddItemToBubble(Bubble bubble)
    {
        int itemCount = itemList.Count;
        if(nextDrop <= 0 && itemCount > 0){
            calcNextDrop();
            int i = Random.Range(1,itemCount);
            Item item = itemList[i-1];
            bubble.addItem(item);
            itemList.Remove(item);
        }
    }

    private void calcNextDrop()
    {
        if(popCount < 7){
            nextDrop = Random.Range(popCount, popCount + diviation);
        }

        nextDrop = Random.Range(10-diviation, 10 + diviation);
    }

    private void spawnItem(Bubble bubble)
    {
        nextDrop--;
    
        Debug.Log("next Drop in: " + nextDrop);

        if(!bubble.ContainedItem){
            return;
        }
        GameObject spawendItem = Instantiate(
            bubble.ContainedItem.gameObject,
            bubble.transform.position, 
            Quaternion.identity
            );

        spawendItem.GetComponent<Rigidbody2D>().linearVelocity =
            new Vector2(Random.Range(-15, 15), Random.Range(5, 15));
        
    }
}