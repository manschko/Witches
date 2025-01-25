using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private void Start()
    {
        Pot.OnBubbleSpawned.AddListener(bubble => { bubble.OnPopped.AddListener(() => { spawnItem(bubble); }); });
    }

    private void Update()
    {
    }

    private void spawnItem(Bubble bubble)
    {
        GameObject spawendItem = Instantiate(
            bubble.ContainedItem.gameObject,
            bubble.transform.position, 
            Quaternion.identity
            );

        spawendItem.GetComponent<Rigidbody2D>().linearVelocity =
            new Vector2(Random.Range(-15, 15), Random.Range(-15, 15));
    }
}