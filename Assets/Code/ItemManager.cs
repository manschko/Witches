
using UnityEngine;
public class ItemManager: MonoBehaviour
{

    private void Start()
    {
        Pot.OnBubbleSpawned.AddListener(bubble =>
        {
            bubble.OnPopped.AddListener(() =>
            {
                spawnItem(bubble.ContainedItem);
            });
        });
    }
    private void Update()
    {
        
    }

    private void spawnItem(Item item)
    {
        GameObject spawendItem = Instantiate(item.gameObject);
        
        item.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(Random.Range(1,15), Random.Range(1,15));
    }

}