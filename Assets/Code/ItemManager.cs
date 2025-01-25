using System.Collections.Generic;
using UnityEditor.Search;
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
        if(popCount >= nextDrop && itemCount > 0){
            //next dropt calc
            int i = Random.Range(1,itemCount);
            Item item = itemList[i-1];
            bubble.addItem(item);
            itemList.Remove(item);
        }
    }

    private void Update()
    {
    }

    private void spawnItem(Bubble bubble)
    {
        popCount++;
    
        Debug.Log("popCount:" + popCount);

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