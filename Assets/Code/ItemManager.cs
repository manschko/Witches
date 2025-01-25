using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private int popCount = 0;
    public int diviation = 3;

    private int nextDrop = 0;
    public ItemList ItemList;
    private void Start()
    {
        Pot.OnBubbleSpawned.AddListener(bubble => { 
            bubble.OnPopped.AddListener(() => { spawnItem(bubble); });
            tryToAddItemToBubble(bubble);

            });

    }

    private void tryToAddItemToBubble(Bubble bubble)
    {
        if(nextDrop <= 0){
            calcNextDrop();
            int i = Random.Range(1,ItemList.AllItems.Count);
            Item item = ItemList.AllItems[i-1];
            bubble.addItem(item);
        }
    }

    private void calcNextDrop()
    {
        if(popCount < 7){
            nextDrop = popCount = Random.Range(popCount, popCount + diviation);
        }

        nextDrop = popCount = Random.Range(10-diviation, 10 + diviation);
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